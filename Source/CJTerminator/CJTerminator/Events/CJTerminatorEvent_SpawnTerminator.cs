using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CJTerminator.Events   
{
    public class CJTerminatorEvent_SpawnTerminator : CJTerminatorMapEvent
    {
        private bool fired = false;
        private bool effectorFired = false;
        private readonly int delay = 300;
        private readonly int effectorDelay = 200;

        private IntVec3 targetCell;
        private Pawn newOverseer;

        public CJTerminatorEvent_SpawnTerminator()
        {

        }
        public CJTerminatorEvent_SpawnTerminator(Map eventMap, IntVec3 targetCell, Pawn p) : base(eventMap)
        {
            this.targetCell = targetCell;
            this.newOverseer = p;
        }
        public override void EventTick(int ticksGame)
        {
            if (ticksGame > effectorDelay + this.lastFireTick && !effectorFired)
            {
                effectorFired = true;
                CJTerminatorUtil.SpawnSphere(targetCell, eventMap);
            }

            if (ticksGame > effectorDelay + delay + this.lastFireTick && !fired)
            {
                lastFireTick = ticksGame;
                this.FireEvent();
                fired = true;
            }

        }

        public override void PostAppend()
        {
            CJTerminatorUtil.SpawnLightning(targetCell, eventMap);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref fired, "fired");
            Scribe_Values.Look(ref effectorFired, "effectorFired");
            Scribe_Values.Look(ref targetCell, "targetCell");
            Scribe_References.Look(ref newOverseer, "newOverseer");

        }

        public override void FireEvent()
        {
            if (eventMap == null || newOverseer == null)
                return;
            PawnKindDef localKindDef = CJTerminatorDefOf.Mech_CJTerminator;
            if (newOverseer.mechanitor != null && newOverseer.mechanitor.TotalBandwidth - newOverseer.mechanitor.UsedBandwidth >= .5f)
            {
                Find.MusicManagerPlay.ForceSilenceFor(5f);
                Faction faction = FactionUtility.DefaultFactionFrom(FactionDefOf.PlayerColony);
                Pawn terminator = PawnGenerator.GeneratePawn(localKindDef, faction);
                Pawn overseer = terminator.GetOverseer();
                overseer?.relations.RemoveDirectRelation(PawnRelationDefOf.Overseer, terminator);
                newOverseer.relations.AddDirectRelation(PawnRelationDefOf.Overseer, terminator);
                GenSpawn.Spawn(terminator, targetCell, Find.CurrentMap, WipeMode.Vanish);
                CJTerminatorDefOf.T800Apears.PlayOneShotOnCamera();
            }
        }

        public override bool ShouldEventBeRemoved(int ticksGame)
        {
            return this.fired;
        }


    }
}
