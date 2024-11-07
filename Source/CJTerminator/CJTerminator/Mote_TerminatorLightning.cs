using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CJTerminator
{
    public class Mote_TerminatorLightning : Mote
    {
        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            DrawLightning(this.def.altitudeLayer.AltitudeFor(), drawLoc);

        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            startTick = Find.TickManager.TicksGame;

        }

        void DrawLightning(float altitude, Vector3 drawLoc)
        {
            if (!this.paused && startTick + maintainTick > Find.TickManager.TicksGame)
            {
                this.exactPosition.y = altitude + this.yOffset;
                Graphics.DrawMesh(this.GetboltMesh(), drawLoc, Quaternion.identity, FadedMaterialPool.FadedVersionOf(LightningMat, 1f), 0);
            }
        }

        private Mesh GetboltMesh()
        {
            return LightningBoltMeshPool.RandomBoltMesh;
        }

        private readonly Material LightningMat = MatLoader.LoadMat("Weather/LightningBolt");
        private int startTick;
        private readonly int maintainTick = 200;

    }
}
