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

        private readonly int lightningLastFrames = 10;
        private readonly int maxCells = 5;
        private readonly Material LightningMat = MatLoader.LoadMat("Weather/LightningBolt");
        private int startTick;
        private int frameCounter = 0;
        private float angle;
        private float x;
        private float y;
        private float rotateDir = 1;
        private Vector3 offset;

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            TryDrawLightning(this.def.altitudeLayer.AltitudeFor(), drawLoc);

        }

        public override void Tick()
        {
            base.Tick();
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            startTick = Find.TickManager.TicksGame;
            ResetRotationAndOffset();
        }

        void TryDrawLightning(float altitude, Vector3 drawLoc)
        {
            DoDrawLightning(altitude, drawLoc);
        }

        void DoDrawLightning(float altitude, Vector3 drawLoc)
        {
            if (CanDrawNextFrame())
            {
                ResetRotationAndOffset();
            }
            RotateRotationAndOffset();
            this.exactPosition.y = altitude + this.yOffset;
            Vector3 newDrawLoc = drawLoc + offset;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
            Matrix4x4 location = new Matrix4x4(new Vector4(1, 0, 0, 0),
                             new Vector4(0, 1, 0, 0),
                             new Vector4(0, 0, 1, 0),
                             new Vector4(newDrawLoc.x, newDrawLoc.y, newDrawLoc.z, 1));
            Matrix4x4 rotation = Matrix4x4.Rotate(q);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(0.05f, 1, GetMaxCell() * 0.0045f));
            Matrix4x4 matrix = location * rotation * scale * Matrix4x4.identity;
            Graphics.DrawMesh(this.GetboltMesh(), matrix, LightningMat, 1);
            AddUpFrameCounter();
        }

        void ResetRotationAndOffset()
        {
            rotateDir = new FloatRange(-1.1f, 1.1f).RandomInRange;
            angle = new FloatRange(0, 360).RandomInRange;
            ResetOffset();
        }

        void RotateRotationAndOffset()
        {
            angle += rotateDir;
            ResetOffset();
        }

        void ResetOffset()
        {
            x = (float)Math.Sin(Mathf.Deg2Rad * angle);
            y = (float)Math.Cos(Mathf.Deg2Rad * angle);
            offset = new Vector3(x, 0, y);
        }

        bool CanDrawNextFrame()
        {
            return frameCounter >= lightningLastFrames;
        }

        void ResetFrameCounter()
        {
            frameCounter = 0;
        }

        void AddUpFrameCounter()
        {
            frameCounter++;
            if (frameCounter > lightningLastFrames)
            {
                ResetFrameCounter();
            }
        }

        int GetMaxCell()
        {
            int result = 0;
            for (int i = 0; i < maxCells; i++)
            {
                IntVec3 cell = IntVec3.FromVector3(this.offset.normalized * i + this.exactPosition);
                if (!cell.Walkable(Find.CurrentMap))
                {
                    Building wall = cell.GetFirstThing<Building>(Find.CurrentMap);
                    if (wall != null && wall.def.useHitPoints)
                    {
                        wall.TakeDamage(new DamageInfo(DamageDefOf.Blunt, 1));
                    }
                    break;
                }
                result++;
            }
            return result;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref startTick, "startTick", GenTicks.TicksGame);
            Scribe_Values.Look(ref frameCounter, "startTick", 0);
            Scribe_Values.Look(ref angle, "angle", 0);
            Scribe_Values.Look(ref x, "x", 0);
            Scribe_Values.Look(ref y, "y", 0);

        }

        private Mesh GetboltMesh()
        {
            return LightningBoltMeshPool.RandomBoltMesh;
        }


    }
}
