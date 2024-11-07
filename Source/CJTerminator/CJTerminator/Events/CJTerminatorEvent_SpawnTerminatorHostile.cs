using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;

namespace CJTerminator.Events
{
    public class CJTerminatorEvent_SpawnTerminatorHostile : CJTerminatorMapEvent
    {
        public CJTerminatorEvent_SpawnTerminatorHostile(Map m) : base(m)
        {

        }
        public CJTerminatorEvent_SpawnTerminatorHostile()
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

        public override void FireEvent()
        {
            if (selfCell == null)
                return;
            if (eventMap == null)
                return;
            Find.MusicManagerPlay.ForceSilenceFor(5f);
            CJTerminatorDefOf.T800Apears.PlayOneShotOnCamera();
            for (int i = 0; i < eventMap.wealthWatcher.WealthTotal / 100000; i++)
            {
                SpawnTerminator();
            }
        }

        void SpawnTerminator()
        {
            PawnKindDef localKindDef = CJTerminatorDefOf.Mech_CJTerminator;
            Faction faction = FactionUtility.DefaultFactionFrom(FactionDefOf.Mechanoid);
            Pawn terminator = PawnGenerator.GeneratePawn(localKindDef, faction);
            GenSpawn.Spawn(terminator, selfCell, Find.CurrentMap, WipeMode.Vanish);
            Lord lord = LordMaker.MakeNewLord(terminator.Faction, new LordJob_AssaultColony(), Find.CurrentMap, null);
            lord.AddPawn(terminator);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref fired, "fired");
            Scribe_Values.Look(ref selfCell, "selfCell");
        }

        public override void PostAppend()
        {
            selfCell = eventMap.AllCells.RandomElement();
            Effecter e = CJTerminatorDefOf.TerminatorApears.Spawn(selfCell, eventMap);
            TargetInfo currentTargetInfo = new TargetInfo(selfCell, eventMap);
            IntVec3 rndOffset = new IntVec3(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            TargetInfo nextTargetInfo = new TargetInfo(selfCell + rndOffset, eventMap);
            e.Trigger(currentTargetInfo, nextTargetInfo);
        }

        public override bool ShouldEventBeRemoved(int ticksGame)
        {
            return this.fired;
        }

        private readonly int delay = 250;
        private IntVec3 selfCell;
        private bool fired;

    }

}

