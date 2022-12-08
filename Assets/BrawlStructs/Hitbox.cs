using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.BrawlStructs
{
    public struct Hitbox
    {
        public uint damage;
        public Vector3 offset;
        public float size;
        public uint trajectory;
        public uint KBG;
        public uint WDSK;
        public uint BKB;
        public uint triprate;
        public float hitlagMult;
        public float sdiMult;
        public uint flags;
    }
}
