using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CJTerminator
{
    public class CJTerminatorEvent_Possitive : CJTerminatorMapEvent
    {

        public CJTerminatorEvent_Possitive(Map m) : base(m) { }
        public CJTerminatorEvent_Possitive()
        {

        }

        public override void EventTick(int ticksGame)
        {

            if (ticksGame > TerminatorModSettings.eventFrequency * 900000 + this.lastFireTick && ticksGame > TerminatorModSettings.minFireDayPossitive * 60000)
            {
                lastFireTick = ticksGame;
                this.FireEvent();
            }
        }

        public override void FireEvent()
        {
            if (!TerminatorModSettings.isPossitiveEnabled)
            {
                return;
            }
            if (this.eventMap == null)
            {
                return;
            }
            List<Pawn> mechanitors = eventMap.mapPawns.FreeColonists.Where(p => p.mechanitor != null).ToList();
            if (mechanitors.Count < 1)
            {
                return;
            }
            Pawn mechanitor = mechanitors.RandomElement();
            eventMap.GetComponent<CJTerminatorMapEventHandler>().AppendEvent(CJTerminatorUtil.SpawnTerminatorEvent(eventMap, mechanitor.Position, mechanitor));
            Letter letter = LetterMaker.MakeLetter("TerminatorPossitive.Label".Translate(), "TerminatorPossitive.Desc".Translate(), LetterDefOf.PositiveEvent);
            letter.lookTargets = new LookTargets(mechanitor);
            Find.LetterStack.ReceiveLetter(letter);
        }

        public override void PostAppend()
        {

        }
        public override void ExposeData()
        {
            Scribe_References.Look(ref eventMap, "eventMapPossitive");
        }

        public override bool ShouldEventBeRemoved(int ticksGame)
        {
            return false;
        }


    }
}
