using System;
namespace Dc2Pilot
{
    /// <summary>Native static-frame name lookup ab82..ac3a; failed suffixed lookup
    /// retains the original cached frame identity. Sprite-animation suffix lookup OPEN.</summary>
    public static class NativeReferenceSuffix
    {
        public static string Frame(string reference,string suffix,Func<string,bool> exists)
        {
            if(suffix.Length!=0){if(!alternatives.TryGetValue((reference,suffix),out string alternative)){alternative=reference+suffix+"Frame";alternatives[(reference,suffix)]=alternative;}if(exists(alternative))return alternative;}
            return FrameName(reference);
        }
        // r119x perf: the per-part name strings were concatenated on every animation step (String.Concat garbage); the same strings are now built once.
        // Only the names are cached; the atlas existence test still runs on every call.
        static readonly System.Collections.Generic.Dictionary<string,string> frameNames=new System.Collections.Generic.Dictionary<string,string>(),animNames=new System.Collections.Generic.Dictionary<string,string>();
        static readonly System.Collections.Generic.Dictionary<(string,string),string> alternatives=new System.Collections.Generic.Dictionary<(string,string),string>();
        public static string FrameName(string reference){if(reference==null)return "Frame";if(!frameNames.TryGetValue(reference,out string v)){v=reference+"Frame";frameNames[reference]=v;}return v;}
        public static string AnimName(string reference){if(reference==null)return "Anim";if(!animNames.TryGetValue(reference,out string v)){v=reference+"Anim";animNames[reference]=v;}return v;}
    }
}
