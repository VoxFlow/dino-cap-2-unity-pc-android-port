using System.Globalization;
using UnityEngine;

namespace Dc2Pilot
{
    public partial class SelectScene
    {
        public bool MissionOpen {get;private set;}
        public bool MissionLeaving {get;private set;}
        public float MissionTarget {get;private set;}
        NativeMapState.Mission SelectedMission {get{return Game.MapState.missions[MapChoices[SelectedMapSlot].mapIndex];}}
        static Rect Raw(uint x,uint y,uint w,uint h) {return new Rect(N.F(unchecked((int)x)),N.F(unchecked((int)y)),N.F(unchecked((int)w)),N.F(unchecked((int)h)));}
        static Rect LayoutRaw(Rect r) {return N.Layout(r.x,r.y,r.width,r.height);}
        static Rect BgRaw() {return Raw(0,0,0x43f00000,0x43a00000);}
        static Rect InfoRaw() {return Raw(0x427c0000,0x428a0000,0x43af8000,0x42a60000);}
        static Rect BackRaw() {return Raw(0x422c0000,0x43770000,0x42d00000,0x41f80000);}
        static Rect StartRaw() {return Raw(0x43a50000,0x436c0000,0x42da0000,0x42440000);}
        static Rect ClawRaw() {return Raw(0x43380000,0x431e0000,0x42240000,0x423c0000);}
        UiControl PopupImage(int id,int layer,string frame,Rect r,bool touch=false)
        {DeleteControl(id);var c=CreateControl(id,layer);c.SetTexture(frame);c.SetRect(r);c.touchable=touch;MoveRight(c,r,White);return c;}
        void LabelRight(UiLabel l,Rect r)
        {
            float x=r.center.x,y=r.center.y;
            l.StartMove(new MoveData(x+480*N.S,y,r.width,r.height,White),new MoveData(x+N.F(-1046478848)*N.S,y,r.width,r.height,White),MoveData.FromRect(r,White),0.2f,0.05f);
        }
        Rect BackgroundRect(bool leaving)
        {Rect r=BgRaw();if(leaving)r.x+=480; r=N.RectScale(r.x,r.y,r.width,r.height);r.y+=32;r.width+=N.WOffset;r.height+=N.HOffset;return r;}
        void ShowMissionInfo()
        {
            if(MissionOpen)return;
            var m=SelectedMission;int type=m.type,level=Profile.level[Profile.playerType];
            int difficulty=type==8?Game.MapState.boss.level:m.level;
            PopupImage(67,4,"ui_map_popbgFrame",BackgroundRect(false));
            PopupImage(68,5,"ui_maptype"+type+"Frame",LayoutRaw(InfoRaw()));
            Rect number=default,unit=default;bool hasNumber=type<=4||type==7;
            if(type<=4)
            {
                int[] nx={0,1130299392,1126301696,1128071168,1129447424};
                int[] ux={0,1133543424,1128660992,1130758144,1131937792};
                int[] uw={0,1116209152,1116602368,1113325568,1124728832};
                number=Px(N.F(nx[type]),122,N.F(1128792064),N.F(1106247680));
                unit=Px(N.F(ux[type]),type==3?128:129,N.F(uw[type]),N.F(type==3?1101004800:1099956224));
                if(type==1)MissionTarget=Mathf.Min(m.count+(float)unchecked(400*level),N.F(1184645120));
                else if(type==2)MissionTarget=Mathf.Min(m.count+(float)unchecked(10*level),N.F(1128792064));
                else if(type==3)MissionTarget=Mathf.Min(m.count+(float)unchecked(5*level),N.F(1128792064));
            }
            else if(type==7)
            {number=Px(N.F(1133510656),101,N.F(1128792064),N.F(1106247680));MissionTarget=Mathf.Min(m.count,N.F(1128792064));}
            if(hasNumber)
            {
                DeleteLabel(69);var label=CreateLabel(69,5,30);label.consumesTouch = true;/* r118z4: native label left enabled - consumes touches in its rect (showMissionInfo 0xfd38c / 0xfd996) */label.SetNative("Adventure_16", 0f, 0f, 0, 0);label.SetColor(White);
                label.SetRect(number);label.SetText(((int)MissionTarget).ToString("N0",CultureInfo.InvariantCulture));LabelRight(label,number);
                if(type<=4)PopupImage(70,5,"ui_maptype"+type+"_endFrame",unit);
            }
            for(int i=0;i<difficulty;i++)
            {Rect r=ClawRaw();r.x+=49*i;r=LayoutRaw(r);r.y+=N.F(1113063424);PopupImage(471+i,5,"ui_img_levelFrame",r);}
            Rect back=LayoutRaw(BackRaw());back.x-=N.F(1107296256);back.y+=N.F(1113063424);
            Rect start=LayoutRaw(StartRaw());start.x+=N.F(1107296256);start.y+=N.F(1113063424);
            PopupImage(72,5,"ui_btn_backFrame",back,true);PopupImage(73,5,"ui_btn_startFrame",start,true);
            MissionOpen=true;MissionLeaving=false;
            foreach(int id in new[]{54,93,55,95,66,65,64})DeleteControl(id);
        }
        void ReturnRight(UiControl c,Rect r)
        {
            float x=r.center.x,y=r.center.y;
            c.StartMove(new MoveData(x+N.F(-1007026176)*N.S,y,r.width,r.height,White),new MoveData(x+N.F(-1007681536)*N.S,y,r.width,r.height,White),MoveData.FromRect(r,White),N.F(1028443341),N.F(1045220557));
        }
        void ReturnLabel(UiLabel c,Rect r)
        {
            float x=r.center.x,y=r.center.y;
            c.StartMove(new MoveData(x+N.F(-1007026176)*N.S,y,r.width,r.height,White),new MoveData(x+N.F(-1007681536)*N.S,y,r.width,r.height,White),MoveData.FromRect(r,White),N.F(1028443341),N.F(1045220557));
        }
        UiControl ReturnImage(int id,int layer,string frame,Rect r)
        {DeleteControl(id);var c=CreateControl(id,layer);c.SetTexture(frame);c.SetRect(r);c.touchable=false;ReturnRight(c,r);return c;}
        static Rect RightOf(Rect raw) {raw.x+=480;return LayoutRaw(raw);}
        void RemoveMissionInfo()
        {
            if(!MissionOpen)return;
            var m=SelectedMission;int type=m.type;
            // Native leave path remaps types6->1,5->2,7/8->3, including its different label text.
            if(type==6)type=1;else if(type==5)type=2;else if(type==7||type==8)type=3;
            ReturnImage(67,3,"ui_map_popbgFrame",BackgroundRect(true));
            ReturnImage(68,4,"ui_maptype"+type+"Frame",RightOf(InfoRaw()));
            int[] nx={0,1143980032,1142980608,1143422976,1143767040};
            int[] ux={0,1145061376,1143570432,1144094720,1144389632};
            float[] uw={0,68,71,55,138};
            Rect nr=Px(N.F(nx[type]),N.F(1123942400),N.F(1128792064),N.F(1106247680));
            Rect ur=Px(N.F(ux[type]),N.F(type==3?1124401152:1124466688),uw[type],N.F(type==3?1101004800:1099956224));
            DeleteLabel(69);var label=CreateLabel(69,4,30);label.consumesTouch = true;/* r118z4: native label left enabled - consumes touches in its rect (removeMissionInfo 0xfa934) */label.SetNative("Adventure_16", 0f, 0f, 0, 0);label.SetColor(White);label.SetText(((int)m.count).ToString(CultureInfo.InvariantCulture));label.SetRect(nr);ReturnLabel(label,nr);
            ReturnImage(70,4,"ui_maptype"+type+"_endFrame",ur);
            for(int i=0;i<m.level;i++)
            {
                // ARM0xfaebc offsets a different local rect. Claw itself stays scaled, without RectOffset.
                Rect r=ClawRaw();r.x+=480+49*i;r=N.RectScale(r.x,r.y,r.width,r.height);
                ReturnImage(471+i,4,"ui_img_levelFrame",r);
            }
            ReturnImage(72,4,"ui_btn_backFrame",RightOf(BackRaw()));
            var start=ReturnImage(73,4,"ui_btn_startFrame",RightOf(StartRaw()));start.SetRect(StartRaw()); // shipped unscaled SetRect quirk0xfb0ce; tween still uses target.
            MissionOpen=false;MissionLeaving=true;
        }
        /// <summary>LIKELY selectScene::resetMapUI 0x10b036 (OnUIEvent 0x10c6f0 runs it after DeleteControl 474..477); line-by-line comparison pending (ledger audit r118x).</summary>
        void FinishMissionLeave()
        {
            foreach(int id in new[]{67,68,72,73,471,472,473,474,475,476,477})DeleteControl(id);
            // Native completion cleanup leaves69/70 offscreen; preserve that lifetime.
            MissionLeaving=false;
            string[] frames={"ui_btn_chengjiuFrame","ui_btn_paimingFrame","ui_btn_optionFrame"};
            for(int k=0;k<3;k++){var c=CreateControl(66-k,0);c.SetTexture(frames[k]);Rect r=Px(331+49*k,35,37,32);c.SetRect(r);MoveRight(c,r,White);}
            MakeScaleButton(54,"ui_btn_heroFrame",Px(10,271,110,48));MakeScaleButton(55,"ui_btn_armoryFrame",Px(361,271,110,48));
            MakeScaleButton(93,"bankFrame",Px(125,271,110,48));MakeScaleButton(95,"jigsaw_buttonFrame",Px(245,271,110,48));
            foreach(var kv in MapChoices)if(!kv.Value.boss)
            {Game.MapState.missions[kv.Value.mapIndex].background=Game.MapState.Range(0,10000)%6;CreateMapPoint(kv.Key,kv.Value);}
        }
    }
}
