using System;
using UnityEngine;
namespace Dc2Pilot
{
    /// <summary>Bounded native GameSprite clock and recovered D4/two energy registrations.
    /// Evidence r88 Update480 ticks/ordered registrations. Other animations OPEN.</summary>
    public sealed class NativeSpriteAnimation
    {
        [Serializable] class Registration{public string name;public string[] frames;}
        [Serializable] class Table{public Registration[] animations;}
        static Table table;
        public string Name {get;private set;}
        public float Time {get;private set;}
        public uint FrameIndex {get;private set;}
        public bool Ended {get;private set;}
        public bool Paused;
        public float Rate {get;private set;}
        public bool Looping {get;private set;}
        string[] frames;
        public string FrameName {get{return frames[FrameIndex];}}
        public void Play(string name,bool loop,float rate)
        {
            if(table==null)table=JsonUtility.FromJson<Table>(Resources.Load<TextAsset>("DC2/sprite_animations.json").text);
            frames=null;foreach(var r in table.animations)if(r.name==name){frames=r.frames;break;}
            if(frames==null)throw new InvalidOperationException("Unrecovered native sprite animation "+name);
            Name=name;Looping=loop;Rate=rate;Time=0;FrameIndex=0;Ended=false; // native PlayAnimation leaves pause flag intact.
        }
        public void Step(float dt)
        {
            if(!Paused)
            {
                Time=N.Add32(Time,dt);FrameIndex=unchecked((uint)N.Mul32(Time,Rate));
                if(Looping)FrameIndex%=(uint)frames.Length;
                else if(FrameIndex>=(uint)frames.Length){FrameIndex=(uint)frames.Length-1;Ended=true;}
            }
        }
    }
}
