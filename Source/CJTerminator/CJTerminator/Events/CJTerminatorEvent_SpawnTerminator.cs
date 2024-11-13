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
        public CJTerminatorEvent_SpawnTerminator(Map eventMap, IntVec3 targetCell, Pawn p) : base(eventMap)
        {
            this.targetCell = targetCell;
            this.newOverseer = p;
        }
        public CJTerminatorEvent_SpawnTerminator()
        {

        }
        public override void EventTick(int ticksGame)
        {
            if (ticksGame > delay + this.lastFireTick)
            {
                lastFireTick = ticksGame;
                this.FireEvent();
                fired = true;
            }

        }

        public override void PostAppend()
        {
            Effecter e = CJTerminatorDefOf.TerminatorApears.Spawn(targetCell, eventMap);
            TargetInfo currentTargetInfo = new TargetInfo(targetCell, eventMap);
            IntVec3 rndOffset = new IntVec3(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            TargetInfo nextTargetInfo = new TargetInfo(targetCell + rndOffset, eventMap);
            e.Trigger(currentTargetInfo, nextTargetInfo);

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref fired, "fired");
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



        private bool fired;
        private readonly int delay = 250;
        private IntVec3 targetCell;
        private Pawn newOverseer;
    }
}
