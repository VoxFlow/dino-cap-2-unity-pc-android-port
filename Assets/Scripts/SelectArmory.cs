using UnityEngine;

namespace Dc2Pilot
{
    /// <summary>Armory vessel: OnLoad107a22, showUpgrade10363e, updateWeaponIconfef68,
    /// lockedWeapon10071e, UpgradeWeaponf52c2, AddBulletf55b0. Raw ARM/spec in r12..r14 reports.</summary>
    public partial class SelectScene
    {
        public int weaponCursor, weaponAttackShown, weaponNextAttackShown, weaponUpgradePrice, ammoNextShown;
        public bool weaponLocked;
        static readonly Rect[] WeaponRects = {
            new Rect(30,82,103,57), new Rect(130,82,103,57), new Rect(30,148,103,57),
            new Rect(130,148,103,57), new Rect(30,208,103,57), new Rect(130,208,103,57)
        };
        static readonly Color32 GoldText = new Color32(255,205,0,255);
        static readonly Color32 PriceText = new Color32(151,74,1,255);
        int WeaponIndex { get { return Profile.playerType*6+weaponCursor; } }
        static Rect ArmoryBackRect(float x,float y,float w,float h)
        { var r=N.Layout(x,y,w,h); r.y+=22f; return r; }

        UiControl ArmoryControl(int id, int layer, string frame, Rect rect, bool right, bool move=true, bool touch=false, Color32? color=null)
        {
            DeleteControl(id);
            var c=CreateControl(id,layer); c.touchable=touch; c.SetTexture(frame); c.SetRect(rect);
            var tint=color ?? White; c.SetColor(tint);
            if(move) { if(right) MoveRight(c,rect,tint); else MoveLeft(c,rect,tint); }
            return c;
        }

        UiLabel ArmoryLabel(int id, string text, Rect rect, bool right, bool move=true, int font=32, bool price=false)   // font 32 -> Adventure_16, 24 -> Adventure_12 (showUpgrade 0x10363e emulation)
        {
            DeleteLabel(id); var label=CreateLabel(id,id>=151 && id<=156 ? 2 : id==51 ? 0 : 1,font==24 ? 24 : 30);   // r118z4: showUpgrade 0x103aae creates 51 in layer 0
            label.consumesTouch = id == 51 || id == 61 || id == 62 || id == 63;   // r118z4: showUpgrade 0x103aae..0x10436e leaves 51/61/62/63 enabled, 151..156 disabled
            // Native (emulation of showUpgrade): 61/62/51 Adventure_16 (0,0); 63 Adventure_12 (1,2); 151..154 Adventure_16 (1,1); 155/156 Adventure_12 (0,0).
            int nh = id>=151 && id<=154 ? 1 : id==63 ? 1 : 0, nv = id>=151 && id<=154 ? 1 : id==63 ? 2 : 0;
            label.SetNative(font==24 ? "Adventure_12" : "Adventure_16", 0f, 0f, nh, nv);
            label.SetText(text); label.SetColor(price ? PriceText : GoldText); label.SetRect(rect);
            if(move) { if(right) MoveRightLabel(label,rect); else LabelMoveLeft(label,rect); }
            return label;
        }

        void ShowArmoryScene()
        {
            Atlas.LoadByConf("DC2/Image/ip/uiarmory_h","DC2/Image/ip/uiarmory_h.conf");
            Atlas.LoadByConf("DC2/Image/weapon_icon1","DC2/Image/weapon_icon1.conf");
            Atlas.LoadArmoryTable();
            Atlas.LoadByConf("DC2/Image/ip/weaponinfo5_h","DC2/Image/ip/weaponinfo5_h.conf");
            ArmoryControl(9100,-3,"ui_bg_allFrame",new Rect(0,32,1024,704),false,false);
            ArmoryHeader();
            Rect bg=N.Layout(16,69,221,208); bg.x-=10f;
            ArmoryControl(76,0,"ui_weapon_bgFrame",bg,false);
            int type=Profile.playerType;
            var slots=Dc2GameData.PlayerWeapons(type); var weapons=Dc2GameData.Weapons();
            for(int i=0;i<6;i++)
            {
                string identity=weapons[slots[i].type][slots[i].subtype].name;
                // showUpgrade104804..104848: shipped icon aliases, not inferred weapon equivalence.
                if(identity=="B5") identity="B3";
                if(identity=="D5") identity="D4";
                if(identity=="E3") identity="E1";
                if(i==5 && type==4 && Profile.weaponLevels[type*6+5]>=slots[5].maxLevel) identity+="_1";
                Rect r=N.Layout(WeaponRects[i].x,WeaponRects[i].y,103,57); r.x-=30f;
                bool owned=i==5 || Profile.weaponOwned[type*6+i]>=0;
                ArmoryControl(11+i,1,"icon_"+identity+"Frame",r,false,true,true,
                    owned ? White : new Color32(100,100,100,255));
            }
            ArmoryControl(17,1,"ui_btn_backFrame",ArmoryBackRect(16,284,104,30),true,true,true);
            ArmoryControl(18,1,"ui_btn_back_imgFrame",ArmoryBackRect(51,285,69,27),true);
            weaponCursor=0;
            UpdateArmoryDetails(true);
        }

        void ArmoryHeader()
        {
            int type=Profile.playerType, level=Profile.level[type]; var h=Dc2Data.Heroes()[type];
            ArmoryControl(59,0,"ui_map_player_infoFrame",N.Layout(10,6,218,63),false);
            Rect head=type==4 ? N.Layout(-8,-6,73,87) : N.Layout(11,13,56,53);
            ArmoryControl(60,1,"ui_p"+(type+1)+"_iconFrame",head,false);
            ArmoryLabel(61,(level+1).ToString(),N.Layout(171,2,200,30),false);
            int hp=h.hp-15+15*(level+1);
            if(type==4) hp=Dc2Data.GirlHp(hp,Profile.level[Profile.playerType]);
            HpShown=N.HpWithJigsaw(hp,Profile.jigsaw1Count);
            ArmoryLabel(62,HpShown.ToString(),N.Layout(147,21,200,30),false);
            ArmoryControl(57,0,"ui_exp_bgFrame",N.Layout(120,43,106,28),false);
            int needed=(level+1)*30*level+h.baseExp;
            ExpPercent=100*Profile.exp[type]/needed;
            int fill=(int)((float)ExpPercent*N.F(1064514355));
            ExpFillRect=N.Layout(127+fill,47,95-fill,19);
            ArmoryControl(58,1,"whiteFrame",ExpFillRect,false,true,false,new Color32(0,0,0,224));
            ArmoryLabel(63,ExpPercent.ToString(),N.Layout(147,33,50,30),false,true,24);
            foreach(var label in Labels) if(label.id==63) label.SetColor(PriceText);
            ArmoryControl(90,1,"%Frame",N.Layout(181,51,11,11),true,true,false,PriceText);
            // r119i (user: "Get Extra Cash + cash amount sit higher natively"): showUpgrade subtracts from the table y before RectScale, unconditionally:
            // taGoldRect1 (364,14) y - 10 (0x103a0e), taGoldTxtRect1 (387,10) y - 10 (0x103b38), taBuyRect1 (292,50) y - 20 (0x103c98; 0x41200000 = 10.0,
            // 0x41A00000 = 20.0). The port used the raw tables. showIAP (bank) uses the same gold tables without an offset (0x101b44..0x101b4c).
            ArmoryControl(50,0,"ui_goldFrame",N.Layout(364,14-10,23,22),true);
            ArmoryLabel(51,UpdateExpCash(),N.Layout(387,10-10,200,30),true);
            ArmoryControl(49,0,"ui_btn_iapFrame",N.Layout(292,50-20,170,41),true,true,true);
        }

        public static int ArmoryUpgradeCost(WeaponDef weapon, int level)
        {
            unchecked
            {
                switch(weapon.type)
                {
                    case 0:return weapon.exp+level*(50*(level+1)+200);
                    case 1:return weapon.exp+level*(100*(level+1)+300);
                    case 2:return weapon.exp+level*(75*(level+1)+250);
                    case 3:return weapon.exp+level*(100*(level+1)+500);
                    case 4:return weapon.exp+level*(150*(level+1)+700);
                    case 5:return weapon.exp+level*(200*(level+1)+600);
                    case 6:return weapon.exp-500+500*level;
                    default:return 0;
                }
            }
        }

        public static int ArmoryAttack(WeaponDef weapon,int level,int bonus)
        {
            int growth;
            switch(weapon.type)
            {
                case 0:growth=2;break; case 1:growth=5;break; case 2:growth=3;break;
                case 3:growth=4;break; case 4:growth=8;break;
                case 5:growth=weapon.index==5 ? 7 : 5;break;
                case 6:growth=weapon.index==6 ? 4 : 2;break;
                default:growth=0;break;
            }
            float value=N.Add32(weapon.atkMin,N.F32(growth*level-growth));
            float addition=N.Mul32(N.F32(bonus),value); addition=N.Mul32(addition,N.F(1008981770));
            return (int)N.Add32(addition,value);
        }

        static int ArmoryAmmoAdd(int amount,int bonus)
        { return (int)N.Add32(N.F32(amount),N.Mul32(N.F32(unchecked(bonus*amount)),N.F(1008981770))); }

        void UpdateArmoryDetails(bool move)
        {
            foreach(int id in new[]{19,20,21,22,23,77,91,92}) DeleteControl(id);
            foreach(int id in new[]{151,152,153,154,155,156}) DeleteLabel(id);
            Rect selected=WeaponRects[weaponCursor];
            Rect selection=N.Layout(selected.x+5,selected.y-5,101,69); selection.x-=30f;
            ArmoryControl(27,3,"ui_weapon_selectFrame",selection,false,false);
            ArmoryControl(77,0,"ui_weapon_attriFrame",N.Layout(248,70,216,206),true,move);
            weaponLocked=weaponCursor!=5 && Profile.weaponOwned[WeaponIndex]<0;
            if(weaponLocked)
            {
                ArmoryControl(91,1,"p"+(Profile.playerType+1)+"weapon"+weaponCursor+"Frame",N.Layout(256,82,208,148),true,move);
                return;
            }
            var slot=Dc2GameData.PlayerWeapons(Profile.playerType)[weaponCursor];
            var weapon=Dc2GameData.Weapons()[slot.type][slot.subtype];
            int level=Profile.weaponLevels[WeaponIndex];
            // updateWeaponIconNoMove0xf85d8..0xf86da refreshes the max-level girl hammer icon.
            if(Profile.playerType==4 && weaponCursor==5)
                GetControl(16).SetTexture(level>=slot.maxLevel ? "icon_G7_1Frame" : "icon_G7Frame");
            weaponAttackShown=ArmoryAttack(weapon,level,Profile.jigsaw2Count);
            weaponNextAttackShown=ArmoryAttack(weapon,level+1,Profile.jigsaw2Count);
            weaponUpgradePrice=ArmoryUpgradeCost(weapon,level);
            ArmoryControl(92,1,"ui_img_attackFrame",N.Layout(259,83,99,22),true,move);
            ArmoryLabel(151,weaponAttackShown.ToString(),N.Layout(250,124,50,30),true,move);
            if(level<slot.maxLevel)
            {
                ArmoryControl(19,1,"ui_btn_upgradeFrame",N.Layout(346,113,110,48),true,move,true);
                ArmoryControl(20,1,"ui_img_jiantouFrame",N.Layout(295,133,17,16),true,move);
                ArmoryLabel(152,weaponNextAttackShown.ToString(),N.Layout(304,124,50,30),true,move);
                ArmoryLabel(155,"$"+weaponUpgradePrice,N.Layout(381,132,200,55),true,move,24,true);
            }
            else
            {
                Rect max=N.Layout(selected.x+35,selected.y+35,selected.width,selected.height); max.x-=0.5f;
                ArmoryLabel(155,"MAX",max,true,move);
            }
            if(weaponCursor==5) return;
            int ammo=Profile.weaponAmmo[WeaponIndex]; if(ammo>Profile.maxBullet)ammo=Profile.maxBullet;
            int add=ArmoryAmmoAdd(slot.ammoAdd,Profile.jigsaw3Count);
            ammoNextShown=ammo+add; if(ammoNextShown>Profile.maxBullet)ammoNextShown=Profile.maxBullet;
            ArmoryLabel(153,ammo.ToString(),N.Layout(250,226,50,30),true,move);
            ArmoryLabel(154,ammoNextShown.ToString(),N.Layout(304,226,50,30),true,move);
            ArmoryLabel(156,"$"+slot.ammoPrice,N.Layout(381,230,200,30),true,move,24,true);
            ArmoryControl(23,1,"ui_img_jiantouFrame",N.Layout(295,237,17,16),true,move);
            ArmoryControl(21,1,"ui_img_ammoFrame",N.Layout(259,184,91,21),true,move);
            ArmoryControl(22,1,"ui_btn_buyammoFrame",N.Layout(346,211,110,48),true,move,true);
        }

        void ArmoryNoMoney()
        {
            Audio.PlaySound("Audio_button2Audio");
            // PLATFORM_ADAPTER: existing in-scene vessel for native OSMessageBox/StoreKit.
            OpenDialog("You're short on cash. Want to buy some more?","nomoney",()=>{
                Game.LastMapType=1; Game.MapType=4; Game.SwitchScene("selectScene");
            });
        }

        bool HandleArmoryEvent(UiControl c,int ev)
        {
            if(Game.MapType!=1 || ev!=0)return false;
            if(c.id>=11 && c.id<=16) {weaponCursor=c.id-11;UpdateArmoryDetails(true);return true;}
            if(c.id==17) {Game.MapType=0;Game.SwitchScene("selectScene");return true;}
            if(c.id==49) {Game.LastMapType=1;Game.MapType=4;Game.SwitchScene("selectScene");return true;}
            if(c.id!=19 && c.id!=22)return false;
            var slot=Dc2GameData.PlayerWeapons(Profile.playerType)[weaponCursor];
            var weapon=Dc2GameData.Weapons()[slot.type][slot.subtype];
            if(c.id==19)
            {
                int level=Profile.weaponLevels[WeaponIndex];
                if(level>=slot.maxLevel) {Audio.PlaySound("Audio_button2Audio");return true;}
                int price=ArmoryUpgradeCost(weapon,level); Profile.weaponLastCost[WeaponIndex]=price;
                if(price>Profile.cash)ArmoryNoMoney();
                else
                {
                    Profile.cash-=price; Profile.weaponLevels[WeaponIndex]++;
                    Profile.weaponUpgradeCount[Profile.playerType]++; Profile.weaponUpgradeSpent[Profile.playerType]+=price;
                    UpdateGoldLabel(); Game.SaveFile();
                }
                // UpgradeWeapon plays Button1 after both the success and short-money branches.
                Audio.PlaySound("Audio_Button1Audio");
            }
            else if(Profile.weaponAmmo[WeaponIndex]<Profile.maxBullet)
            {
                if(Profile.cash<slot.ammoPrice)ArmoryNoMoney();
                else
                {
                    int add=ArmoryAmmoAdd(slot.ammoAdd,Profile.jigsaw3Count);
                    Profile.weaponAmmo[WeaponIndex]+=add;
                    if(Profile.weaponAmmo[WeaponIndex]>Profile.maxBullet)Profile.weaponAmmo[WeaponIndex]=Profile.maxBullet;
                    Profile.ammoPurchaseCount[Profile.playerType]++; Profile.ammoPurchaseSpent[Profile.playerType]+=slot.ammoPrice;
                    Profile.cash-=slot.ammoPrice; Audio.PlaySound("Audio_Button1Audio"); UpdateGoldLabel();Game.SaveFile();
                }
            }
            UpdateArmoryDetails(false); return true;
        }
    }
}
