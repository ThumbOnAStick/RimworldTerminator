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
            startTick = Find.TickManager.TicksGame;
        }

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            base.DrawAt(drawLoc, flip);
            DrawTerminatorSphere(def.altitudeLayer.AltitudeFor(), drawLoc);

        }

        void DrawTerminatorSphere(float altitude, Vector3 drawLoc)
        {
            if (!this.paused)
            {
                this.exactPosition.y = altitude;
            }
            if (drawStarted)
            {
                GraphicForSphere().Draw(drawLoc, Rot4.South, this);
            }
        }

        public static Graphic GraphicForSphere()
        {

            return GraphicDatabase.Get<Graphic_Single>(DotPath, ShaderDatabase.Mote, Vector2.one * 2f, Color.black, Color.black);
        }

        public override void Tick()
        {
            base.Tick();
            if (startTick + delay < Find.TickManager.TicksGame)
            {
                drawStarted = true;
            }
        }

        static readonly string DotPath = "Terminator/Mech/Dot";

        private bool drawStarted = false;
        private int startTick;
        private readonly int delay = 200;

    }
}
