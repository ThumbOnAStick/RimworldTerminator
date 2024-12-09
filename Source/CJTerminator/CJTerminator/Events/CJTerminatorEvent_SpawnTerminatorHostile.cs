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
        private bool fired = false;
        private bool effectorFired = false;
        private readonly int delay = 300;
        private readonly int effectorDelay = 200;

        private IntVec3 selfCell;


        public CJTerminatorEvent_SpawnTerminatorHostile()
        {

        }
        public CJTerminatorEvent_SpawnTerminatorHostile(Map m) : base(m)
        {

        }
        public override void EventTick(int ticksGame)
        {

            if (ticksGame > effectorDelay + this.lastFireTick && !effectorFired)
            {
                effectorFired = true;
                CJTerminatorUtil.SpawnSphere(this.selfCell, eventMap);
            }

            if (ticksGame > effectorDelay + delay + this.lastFireTick)
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
            terminator.TryGetComp<CompStunnable>()?.StunHandler?.StunFor(500, terminator);
            Lord lord = LordMaker.MakeNewLord(terminator.Faction, new LordJob_AssaultColony(), Find.CurrentMap, null);
            lord.AddPawn(terminator);

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref fired, "fired");
            Scribe_Values.Look(ref effectorFired, "effectorFired");
            Scribe_Values.Look(ref selfCell, "selfCell");
        }

        public override void PostAppend()
        {
            CellFinder.TryFindRandomCell(eventMap, new Predicate<IntVec3>(x => x.Walkable(eventMap) && !x.CloseToEdge(eventMap, 30)), out selfCell);
            Effecter e = CJTerminatorDefOf.TerminatorApears.Spawn(selfCell, eventMap);
            TargetInfo currentTargetInfo = new TargetInfo(selfCell, eventMap);
            IntVec3 rndOffset = new IntVec3(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            TargetInfo nextTargetInfo = new TargetInfo(selfCell + rndOffset, eventMap);
            e.Trigger(currentTargetInfo, nextTargetInfo);
            var lookTargets = new LookTargets(selfCell, eventMap);
            Letter letter = LetterMaker.MakeLetter("TerminatorNegative.Label".Translate(), "TerminatorNegative.Desc".Translate(), LetterDefOf.ThreatBig, lookTargets: lookTargets);
            Find.LetterStack.ReceiveLetter(letter);

        }

        public override bool ShouldEventBeRemoved(int ticksGame)
        {
            return this.fired;
        }



    }

}

