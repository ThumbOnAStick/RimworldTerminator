using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Noise;
using Verse.Sound;
using static UnityEngine.Scripting.GarbageCollector;

namespace CJTerminator
{
    public class SubEffecterTerminatorLightning : SubEffecter
    {
        public SubEffecterTerminatorLightning(SubEffecterDef subDef, Effecter parent) : base(subDef, parent)
        {

        }


        public override void SubTrigger(TargetInfo A, TargetInfo B, int overrideSpawnTick = -1, bool force = false)
        {
            Vector3 vector = (A.HasThing && A.Thing.DrawPosHeld != null) ? A.Thing.DrawPosHeld.Value : A.Cell.ToVector3Shifted();
            CJTerminatorDefOf.T800ApearsLightning.PlayOneShotOnCamera();
            Mote_TerminatorLightning terminatorLightning = (Mote_TerminatorLightning)ThingMaker.MakeThing(this.def.moteDef, null);
            terminatorLightning.exactPosition = vector + base.EffectiveOffset + Gen.RandomHorizontalVector(this.def.positionRadius);
            terminatorLightning.rotationRate = 0;
            terminatorLightning.exactRotation = 0;
            GenSpawn.Spawn(terminatorLightning, vector.ToIntVec3(), A.Map, WipeMode.Vanish);

        }


    }
}
