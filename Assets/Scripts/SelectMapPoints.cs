using System.Collections.Generic;
using UnityEngine;

namespace Dc2Pilot
{
    public partial class SelectScene
    {
        public readonly Dictionary<int, NativeMapState.Choice> MapChoices = new Dictionary<int, NativeMapState.Choice>();
        public int SelectedMapSlot = -1;
        // Native showMapPoint starts from map background56 tween completion, not scene entry.
        bool HandleMapPointEvent(UiControl c, int ev)
        {
            if (MissionOpen)
            {
                if (ev==0 && c.id==72) RemoveMissionInfo();
                else if (ev==0 && c.id==73) { Game.LaunchMission=SelectedMission; Game.LaunchMissionType=SelectedMission.type; Game.MapType=6; lastAction="mission:start"; Game.SwitchScene("selectScene"); }
                return true;
            }
            if (ev==1 && c.id==73 && MissionLeaving) { FinishMissionLeave(); return true; }
            if (ev==1 && c.id==56) { ShowMapPoints(); return true; }
            if (ev==1 && c.id>=310 && c.id<=313) { PulseMapPoint(c,c.FullRect); return true; }
            if (ev==0 && c.id>=300 && c.id<=303 && MapChoices.ContainsKey(c.id-300))
            { SelectedMapSlot=c.id-300; ShowMissionInfo(); return true; }
            return false;
        }
        void ShowMapPoints()
        {
            if (Game.MapState==null) Game.MapState=new NativeMapState();
            Game.MapState.Generate(Game.SaveDir,Profile.mission,Profile.level[Profile.playerType]);
            MapChoices.Clear(); int normalSlot=0;
            foreach (var choice in Game.MapState.choices)
            {
                int slot=choice.boss?3:normalSlot++;
                MapChoices[slot]=choice;
                CreateMapPoint(slot,choice);
            }
        }
        void CreateMapPoint(int slot,NativeMapState.Choice choice)
        {
                DeleteControl(300+slot); DeleteControl(310+slot);
                var mission=Game.MapState.missions[choice.mapIndex];
                float w=N.F(1106247680),h=N.F(1108082688);
                if (mission.type==8) {w=N.F(1108082688);h=N.F(1109393408);}
                else if (mission.type==7) {w=N.F(1108344832);h=N.F(1106247680);}
                var icon=CreateControl(300+slot,3);
                icon.SetTexture("ui_map_point_type"+mission.type+"Frame");
                Rect r=Px(choice.point.x,choice.point.y,w,h);
                icon.SetRect(r); ScaleIn(icon,r);
                var ring=CreateControl(310+slot,2); ring.touchable=false;
                ring.SetTexture("ui_map_point_bgFrame");
                Rect rr=new Rect(r.x+N.F(-1056964608)*N.S,r.y+N.F(1104150528)*N.S,N.F(1111228416)*N.S,N.F(1101004800)*N.S);
                ring.SetRect(rr); PulseMapPoint(ring,rr);
        }
        // startScaleUI_Point ARM0x10a234: half-size opaque -> full-size transparent over2s.
        // Completion events 310..313 restart it, including the boss ring. r119p: the user confirmed the boss ring on the device (the earlier "except for Boss" note was a misobservation); history:
        // native showBossPoint 0x10a2e8 creates ring 313 (layer 2) and calls startScaleUI_Point like the normal points (NATIVE, PROVEN). LIKELY cause: the boss icon
        // (35x40, layer 3) covers most of the ring rect (x-8S, y+19S, 47S x 20S; normal icons are 30x35). Statically unresolvable: device A/B item (r118x).
        void PulseMapPoint(UiControl c,Rect r)
        {
            float x=r.center.x,y=r.center.y;
            var finish=new MoveData(x,y,r.width,r.height,new Color32(255,255,255,0));
            c.StartMove(new MoveData(x,y,r.width*0.5f,r.height*0.5f,White),finish,finish,2f,0f);
        }
    }
}
