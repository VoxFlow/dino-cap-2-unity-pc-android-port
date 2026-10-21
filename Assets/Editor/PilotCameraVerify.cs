using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotCameraVerify
    {
        [Serializable] public class Input { public float hero,left,dt,right; public bool facingLeft,turn; }
        [Serializable] public class Output { public float left; public bool turn; }
        [Serializable] public class Case { public Input input; public Output output; }
        [Serializable] public class Oracle { public Case[] cases; }
        [Serializable] public class DinoCase { public string scene; public int skin; public float y; }
        [Serializable] public class DinoOracle { public DinoCase[] cases; }
        public static void Run()
        {
            string output = Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/camera-r23");
            var args = Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++) if(args[i]=="-reportDir") output=args[i+1];
            Directory.CreateDirectory(output); int passed=0,failed=0; var log=new StringBuilder();
            try
            {
                // Expectations come from independently executed verified ARM, not this vessel.
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/camera_oracle.r23.json")));
                foreach(var c in oracle.cases)
                {
                    var camera=new NativeGameCamera { left=c.input.left,turning=c.input.turn };
                    camera.Step(c.input.hero,c.input.facingLeft,c.input.dt,-240,c.input.right);
                    bool ok=camera.left==c.output.left && camera.turning==c.output.turn;
                    if(ok) passed++; else failed++;
                    log.AppendLine((ok?"PASS ":"FAIL ")+"ARM camera left="+c.output.left+" turn="+c.output.turn+" got="+camera.left+","+camera.turning);
                }
                var dinos=JsonUtility.FromJson<DinoOracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/dino_origin_oracle.r25.json")));
                foreach(var c in dinos.cases)
                {
                    bool ok=GameScene.NativeDinoSpawnY(c.skin)==c.y;
                    if(ok) passed++; else failed++;
                    log.AppendLine((ok?"PASS ":"FAIL ")+"ARM "+c.scene+" skin"+c.skin+" spawn Y="+c.y);
                }
            }
            catch(Exception e) { failed++;log.AppendLine(e.ToString()); }
            log.AppendLine("SUMMARY passes="+passed+" fails="+failed);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());
            Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
        }
    }
}
