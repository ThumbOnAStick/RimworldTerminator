using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CJTerminator
{
    public class SubEffecterTerminatorShpere : SubEffecter
    {
        public SubEffecterTerminatorShpere(SubEffecterDef subDef, Effecter parent) : base(subDef, parent)
        {

        }


        public override void SubTrigger(TargetInfo A, TargetInfo B, int overrideSpawnTick = -1, bool force = false)
        {
            Vector3 vector = (A.HasThing && A.Thing.DrawPosHeld != null) ? A.Thing.DrawPosHeld.Value : A.Cell.ToVector3Shifted();
            Mote_TerminatorSphere terminatorSphere = (Mote_TerminatorSphere)ThingMaker.MakeThing(this.def.moteDef, null);
            terminatorSphere.exactPosition = vector;
            terminatorSphere.rotationRate = this.def.rotationRate.RandomInRange;
            terminatorSphere.exactRotation = this.def.rotation.RandomInRange;
            GenSpawn.Spawn(terminatorSphere, vector.ToIntVec3(), A.Map, WipeMode.Vanish);

        }

       


    }
}
