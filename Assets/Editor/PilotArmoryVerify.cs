using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dc2Pilot.EditorTools
{
    public static class PilotArmoryVerify
    {
        static string Label(Dc2Scene s,int id) {foreach(var l in s.Labels)if(l.id==id)return l.Text;return null;}
        static void Click(SelectScene s,int id) {var p=s.GetControl(id).FullRect.center;s.Touch(0,p);s.Touch(2,p);}
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/armory-r14");
            var args=Environment.GetCommandLineArgs();for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];
            Directory.CreateDirectory(output);
            string dir=Path.Combine(Path.GetTempPath(),"dc2_armory_"+Guid.NewGuid().ToString("N"));
            var log=new StringBuilder();int passed=0,failed=0;
            Action<string,bool> check=(n,v)=>{log.AppendLine((v?"PASS ":"FAIL ")+n);if(v)passed++;else failed++;};
            Dc2Game game=null;Camera cam=null;RenderTexture rt=null;Texture2D img=null;
            try
            {
                cam=Bootstrap.MakeCamera();game=Dc2Game.Create(dir);game.manualStep=true;game.SkipMissionIntro=true;game.cam=cam;
                rt=new RenderTexture(1024,768,24);img=new Texture2D(1024,768,TextureFormat.RGB24,false);
                Action<string> shot=name=>{cam.targetTexture=rt;Bootstrap.RenderAll(cam);RenderTexture.active=rt;
                    img.ReadPixels(new Rect(0,0,1024,768),0,0);img.Apply();File.WriteAllBytes(Path.Combine(output,name),img.EncodeToPNG());
                    cam.targetTexture=null;RenderTexture.active=null;};
                game.SwitchScene("selectScene");game.StepFor(1f);var scene=(SelectScene)game.Current;
                Click(scene,55);check("map Armory button queues mode1",game.MapType==1);game.StepFor(1f);scene=(SelectScene)game.Current;
                check("six native icon slots including melee",scene.GetControl(11).FrameName=="icon_A1Frame"&&scene.GetControl(16).FrameName=="icon_G1Frame");
                check("ownership/ammo/upgrade vectors are distinct",game.Profile.weaponOwned[0]==0&&game.Profile.weaponOwned[1]==-1&&game.Profile.weaponOwned[5]==-1&&game.Profile.weaponLevels[0]==1&&game.Profile.weaponLevels[5]==1&&game.Profile.weaponAmmo[0]==200&&game.Profile.weaponAmmo[1]==0);
                check("native left panel iPad x nudge",scene.GetControl(76).FullRect==new Rect(54,170,442,416));
                check("native icon and selection rect",scene.GetControl(11).FullRect==new Rect(62,196,206,114)&&scene.GetControl(27).FullRect==new Rect(72,186,202,138));
                check("r119i native header gold/IAP: showUpgrade y-10 / y-20 before RectScale (0x103a0e, 0x103c98)",scene.GetControl(50).FullRect==new Rect(760,40,46,44)&&scene.GetControl(49).FullRect==new Rect(616,92,340,82));
                check("native root A1 current2 next4 upgrade600",scene.weaponAttackShown==2&&scene.weaponNextAttackShown==4&&scene.weaponUpgradePrice==600&&Label(scene,151)=="2"&&Label(scene,155)=="$600");
                check("native ammo200 next240 price150",Label(scene,153)=="200"&&Label(scene,154)=="240"&&Label(scene,156)=="$150");
                check("binary32 helper boundaries preserve exact native integer bonus",N.HpWithJigsaw(100,40)==140&&SelectScene.ArmoryAttack(Dc2GameData.Weapons()[0][0],2,25)==5);
                shot("armory_default.png");
                UiLabel ammoPrice=null;foreach(var label in scene.Labels)if(label.id==156)ammoPrice=label;
                var visiblePrice=img.GetPixels32();ammoPrice.gameObject.SetActive(false);shot("armory_price_hidden.png");
                var hiddenPrice=img.GetPixels32();int pricePixels=0;for(int p=0;p<visiblePrice.Length;p++)if(!visiblePrice[p].Equals(hiddenPrice[p]))pricePixels++;
                ammoPrice.gameObject.SetActive(true);
                check("ammo price contributes rendered pixels above purchase button",pricePixels>0);log.AppendLine("PRICE_PIXELS "+pricePixels);
                Click(scene,12);game.StepFor(0.6f);
                check("locked slot1 uses native card and hides purchase controls",scene.weaponLocked&&scene.GetControl(91).FrameName=="p1weapon1Frame"&&scene.GetControl(91).FullRect==new Rect(544,196,416,296)&&scene.GetControl(19)==null&&scene.GetControl(22)==null&&Label(scene,151)==null);
                shot("armory_locked.png");
                Click(scene,16);game.StepFor(0.6f);
                check("melee selectable despite negative ownership marker; ammo hidden",!scene.weaponLocked&&scene.weaponCursor==5&&scene.GetControl(19)!=null&&scene.GetControl(22)==null&&Label(scene,153)==null);
                shot("armory_melee.png");
                Click(scene,11);game.StepFor(0.6f);
                Click(scene,19);game.StepFor(0.1f);
                check("insufficient cash opens dialog and retains level",scene.confirmOpen&&game.Profile.weaponLevels[0]==1&&game.Profile.weaponLastCost[0]==600);
                check("short cash dialog uses native message rather than reset warning",Label(scene,900)=="You're short on cash. Want to buy some more?");
                shot("armory_short_cash.png");
                Click(scene,901);game.StepFor(0.1f);
                game.Profile.cash=600;Click(scene,19);game.StepFor(0.1f);
                check("equal upgrade cash accepted",game.Profile.cash==0&&game.Profile.weaponLevels[0]==2&&game.Profile.weaponUpgradeCount[0]==1&&game.Profile.weaponUpgradeSpent[0]==600);
                check("upgrade refresh without entrance tween",scene.weaponAttackShown==4&&scene.weaponUpgradePrice==1000&&Label(scene,51)=="0"&&scene.GetControl(19).CurrentRect==scene.GetControl(19).FullRect);
                game.Profile.cash=150;game.Profile.jigsaw3Count=25;Click(scene,22);game.StepFor(0.1f);
                log.AppendLine("AMMO_DIAGNOSTIC cash="+game.Profile.cash+" ammo="+game.Profile.weaponAmmo[0]+" count="+game.Profile.ammoPurchaseCount[0]+" spent="+game.Profile.ammoPurchaseSpent[0]+" bonus="+game.Profile.jigsaw3Count);
                check("equal ammo cash accepted with native25percent addition",game.Profile.cash==0&&game.Profile.weaponAmmo[0]==250&&game.Profile.ammoPurchaseCount[0]==1&&game.Profile.ammoPurchaseSpent[0]==150);
                game.Profile.weaponAmmo[0]=490;game.Profile.cash=150;Click(scene,22);
                check("ammo purchase caps500 while charging full price",game.Profile.weaponAmmo[0]==500&&game.Profile.cash==0&&game.Profile.ammoPurchaseCount[0]==2);
                game.Profile.cash=150;Click(scene,22);
                check("already full ammo does not charge",game.Profile.cash==150&&game.Profile.ammoPurchaseCount[0]==2);
                var saved=Profile.Load(dir);check("native save keeps weapon level and ammo; the port-only ammoPurchaseCount is not in dinocap2hd.sav (r118z, engine_audio_save_r118z.md)",saved.weaponLevels[0]==2&&saved.weaponAmmo[0]==500&&saved.ammoPurchaseCount[0]==0);
                for(int hero=1;hero<=4;hero++)
                {
                    game.Profile.playerType=hero;game.Profile.level[hero]=0;game.SwitchScene("selectScene");game.StepFor(1f);scene=(SelectScene)game.Current;
                    check("hero"+hero+" native six slots",scene.GetControl(16)!=null&&scene.GetControl(11)!=null);
                    Click(scene,12);game.StepFor(0.6f);
                    check("hero"+hero+" native locked card",scene.GetControl(91).FrameName=="p"+(hero+1)+"weapon1Frame");
                    shot("armory_hero"+hero+"_locked.png");
                    if(hero==4)check("girl HP native half and larger icon rect",scene.HpShown==50&&scene.GetControl(60).FullRect==new Rect(16,20,146,174));
                }
                Click(scene,16);game.StepFor(0.6f);
                game.Profile.weaponLevels[29]=Dc2GameData.PlayerWeapons(4)[5].maxLevel-1;
                game.Profile.cash=10000000;Click(scene,11);game.StepFor(0.6f);Click(scene,16);game.StepFor(0.6f);
                Click(scene,19);game.StepFor(0.1f);
                check("girl maximum hammer refresh uses native G7_1 icon",scene.GetControl(16).FrameName=="icon_G7_1Frame"&&scene.GetControl(19)==null&&Label(scene,155)=="MAX");
                shot("armory_girl_max_hammer.png");
                Click(scene,49);game.StepFor(1f);scene=(SelectScene)game.Current;
                Click(scene,17);game.StepFor(1f);scene=(SelectScene)game.Current;
                check("Bank back restores Armory",game.MapType==1&&scene.GetControl(16)!=null);
                Click(scene,17);game.StepFor(1f);scene=(SelectScene)game.Current;
                check("back returns to map",game.MapType==0&&scene.GetControl(55)!=null);
            }
            catch(Exception e){failed++;log.AppendLine("EXCEPTION "+e);}
            finally
            {
                if(rt!=null)UnityEngine.Object.DestroyImmediate(rt);if(img!=null)UnityEngine.Object.DestroyImmediate(img);
                if(game!=null)UnityEngine.Object.DestroyImmediate(game.gameObject);if(cam!=null)UnityEngine.Object.DestroyImmediate(cam.gameObject);
                if(Directory.Exists(dir))Directory.Delete(dir,true);
                log.AppendLine("SUMMARY passes="+passed+" fails="+failed);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
            }
        }
    }
}
