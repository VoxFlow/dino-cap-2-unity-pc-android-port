using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Native LoadXml/Expand f4b0/f14e, CAFrame f014, CASprite b73c.
    /// Bounded to recovered XML poses with at most16 common parts (native insertion-sort branch).
    /// Raw XML is preserved; no authored Unity animation or fitted geometry.</summary>
    public static class NativeXmlCaAnimation
    {
        sealed class Key { public int ordinal; public readonly SortedDictionary<string,NativeCaAnimation.Part> parts = new SortedDictionary<string,NativeCaAnimation.Part>(StringComparer.Ordinal); }
        static float Number(XmlNode node,string tag) { return (float)double.Parse(node[tag].InnerText,CultureInfo.InvariantCulture); }
        static float Lerp(float a,float b,float t) { return N.Add32(a,N.Mul32(t,N.Add32(b,-a))); }
        public static NativeCaAnimation Load(string resource,float expansion)
        {
            var data=Resources.Load<TextAsset>(resource);if(data==null)throw new Exception("Missing native XML pose "+resource);
            string text=data.text;int first=text.IndexOf("<AnimFrame",StringComparison.Ordinal);
            var document=new XmlDocument();document.LoadXml("<root>"+text.Substring(first)+"</root>");
            var keys=new List<Key>();var result=new NativeCaAnimation();
            foreach(XmlNode node in document.DocumentElement.ChildNodes)
            {
                if(node.Name!="AnimFrame")continue;
                var key=new Key {ordinal=int.Parse(node.Attributes["frame"].Value,CultureInfo.InvariantCulture)};
                foreach(XmlNode sprite in node.ChildNodes)
                {
                    if(sprite.Name!="Sprite")continue;
                    var part=new NativeCaAnimation.Part {name=sprite.Attributes["name"].Value,
                        x=Number(sprite,"x"),y=Number(sprite,"y"),w=Number(sprite,"width"),h=Number(sprite,"height"),
                        pivotX=Number(sprite,"offsetX"),pivotY=Number(sprite,"offsetY"),radians=Number(sprite,"rotation"),
                        layerBits=unchecked((uint)int.Parse(sprite["layer"].InnerText,CultureInfo.InvariantCulture)),
                        rgba=uint.Parse(sprite["color"].InnerText,NumberStyles.HexNumber,CultureInfo.InvariantCulture),reference=sprite["image"]?.InnerText??""};
                    // XML hexadecimal ARGB -> native little-endian r,g,b,a bytes (beF8..bf0e).
                    uint rgba=part.rgba;
                    part.rgba=(rgba&0xff000000)|((rgba>>16)&255)|(rgba&0xff00)|((rgba&255)<<16);
                    // CASprite LoadXml iPad g_scale1; atlas dimensions resolved before expansion.
                    if(Atlas.Has(NativeReferenceSuffix.FrameName(part.reference))&&part.w<10f&&part.h<10f)
                    {
                        var size=Atlas.FrameSize(part.reference+"Frame");
                        if(part.reference=="yingzi") {part.w=size.x;part.h=size.y;}
                        else {part.w=N.Mul32(part.w,size.x);part.h=N.Mul32(part.h,size.y);}
                    }
                    float half=N.Mul32(.5f,part.w),left=N.Add32(part.x,-half),right=N.Add32(half,part.x);
                    if(left<result.minX)result.minX=left;if(right>result.maxX)result.maxX=right;
                    key.parts[part.name]=part;
                }
                keys.Add(key);
            }
            if(keys.Count<2)throw new Exception("Unrecovered XML key-count case "+resource);
            var frames=new List<NativeCaAnimation.Frame>();int segment=0;
            float last=N.Mul32(keys[keys.Count-1].ordinal,expansion);
            for(float time=0f;time<last;time=N.Add32(time,1f))
            {
                while(time>N.Mul32(keys[segment+1].ordinal,expansion))segment++;
                var a=keys[segment];var b=keys[segment+1];
                float left=N.Mul32(a.ordinal,expansion),right=N.Mul32(b.ordinal,expansion);
                float ratio=N.F32((double)N.Add32(time,-left)/(double)N.Add32(right,-left));
                var parts=new List<NativeCaAnimation.Part>();
                foreach(var item in a.parts)
                {
                    NativeCaAnimation.Part end;if(!b.parts.TryGetValue(item.Key,out end))continue;
                    var start=item.Value;float delta=N.Add32(end.radians,-start.radians);
                    if(delta>180f)delta=N.Add32(delta,-360f);else if(delta< -180f)delta=N.Add32(delta,360f);
                    uint color=0;for(int shift=0;shift<32;shift+=8)
                        color|=(unchecked((uint)Lerp((start.rgba>>shift)&255,(end.rgba>>shift)&255,ratio))&255)<<shift;
                    var part=new NativeCaAnimation.Part {name=start.name,reference=start.reference,layerBits=start.layerBits,rgba=color,
                        x=Lerp(start.x,end.x,ratio),y=Lerp(start.y,end.y,ratio),w=Lerp(start.w,end.w,ratio),h=Lerp(start.h,end.h,ratio),
                        pivotX=Lerp(start.pivotX,end.pivotX,ratio),pivotY=Lerp(start.pivotY,end.pivotY,ratio),
                        radians=N.Mul32(N.Add32(start.radians,N.Mul32(delta,ratio)),N.F(1016003125))};
                    // Native frame expansion starts in map-name order; strict signed layer insertion preserves ties.
                    int at=parts.Count;while(at>0&&unchecked((int)parts[at-1].layerBits)>unchecked((int)part.layerBits))at--;
                    parts.Insert(at,part);
                }
                if(parts.Count>16)throw new Exception("Unrecovered XML sort branch "+resource);
                frames.Add(new NativeCaAnimation.Frame {ordinal=0,parts=parts.ToArray()});
            }
            result.frames=frames.ToArray();return result;
        }
    }
}
