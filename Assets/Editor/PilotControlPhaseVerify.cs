using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Dc2Pilot.EditorTools
{
    public static class PilotControlPhaseVerify
    {
        [Serializable] public class Case {public int state,next_state;public bool left,leftDown,rightDown,active,control_called,has_velocity,next_left,turning;public float velocity;}
        [Serializable] public class Oracle {public Case[] cases;}
        public static void Run()
        {
            string output=Path.Combine(Application.dataPath,"../Reports/DC2-PILOT-001/control-phase-r40");var args=Environment.GetCommandLineArgs();
            for(int i=0;i<args.Length-1;i++)if(args[i]=="-reportDir")output=args[i+1];Directory.CreateDirectory(output);
            int passed=0,failed=0;var log=new StringBuilder();
            try
            {
                var oracle=JsonUtility.FromJson<Oracle>(File.ReadAllText(Path.Combine(Application.dataPath,"../../Dev/Reports/DC2-PILOT-001/player_control_oracle.r39b.json")));
                foreach(var c in oracle.cases)
                {
                    var r=NativeHeroControl.Decide(c.state,c.left,c.leftDown,c.rightDown,c.active,300);
                    bool good=r.state==c.next_state&&r.facingLeft==c.next_left&&r.turning==c.turning&&r.called==c.control_called&&r.hasVelocity==c.has_velocity&&(!r.hasVelocity||r.velocity==c.velocity);
                    if(good)passed++;else failed++;log.AppendLine((good?"PASS ":"FAIL ")+"ARM state"+c.state+" face"+c.left+" buttons"+c.leftDown+"/"+c.rightDown+" active"+c.active);
                }
            }
            catch(Exception e){failed++;log.AppendLine(e.ToString());}
            log.AppendLine("SUMMARY passes="+passed+" fails="+failed);File.WriteAllText(Path.Combine(output,"verify.txt"),log.ToString());Debug.Log(log.ToString());PilotExit.Exit(failed==0?0:1);
        }
    }
}
