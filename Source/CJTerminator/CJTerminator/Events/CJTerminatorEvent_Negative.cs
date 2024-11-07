using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CJTerminator.Events
{
    public class CJTerminatorEvent_Negative : CJTerminatorMapEvent
    {
        public CJTerminatorEvent_Negative(Map m) : base(m)
        {

        }

        public override void EventTick(int ticksGame)
        {

            if (ticksGame > TerminatorModSettings.eventFrequency * 900000 + this.lastFireTick && ticksGame > TerminatorModSettings.minFireDayNegative * 60000)
            {
                lastFireTick = ticksGame;
                this.FireEvent();
            }
        }

        public override void FireEvent()
        {
            if (!TerminatorModSettings.isNegativeEnabled)
            {
                return;
            }
            if (this.eventMap == null)
            {
                return;
            }
            
            eventMap.GetComponent<CJTerminatorMapEventHandler>().AppendEvent(CJTerminatorUtil.SpawnTerminatorEventHostile(eventMap));
            Letter letter = LetterMaker.MakeLetter("TerminatorNegative.Label".Translate(), "TerminatorNegative.Desc".Translate(), LetterDefOf.ThreatBig);
            Find.LetterStack.ReceiveLetter(letter);
        }

        public override void PostAppend()
        {

        }

        public override bool ShouldEventBeRemoved(int ticksGame)
        {
            return false;
        }

        public override void ExposeData()
        {
            base.ExposeData();

        }
    }
}
