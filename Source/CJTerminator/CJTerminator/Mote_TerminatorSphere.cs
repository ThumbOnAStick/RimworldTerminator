using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CJTerminator
{
    public class Mote_TerminatorSphere : Mote
    {
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
        }

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            TryToDrawBase(drawLoc, flip);
        }

        void TryToDrawBase(Vector3 drawLoc, bool flip = false)
        {
            base.DrawAt(drawLoc, flip);

        }


    }
}
