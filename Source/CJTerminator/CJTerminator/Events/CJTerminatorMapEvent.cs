using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CJTerminator
{
    public abstract class CJTerminatorMapEvent : IExposable
    {
        public CJTerminatorMapEvent(Map m) { this.eventMap = m; }
        public abstract void FireEvent();
        public abstract void EventTick(int ticksGame);
        public abstract bool ShouldEventBeRemoved(int ticksGame);
        public abstract void PostAppend();
        public int GetLastFireTick()
        {
            return this.lastFireTick;
        }
        public void SetLastFireTick(int val)
        {
            lastFireTick = val;
        }

        public virtual void ExposeData()
        {
            Scribe_Values.Look(ref lastFireTick, "lastFireTick");
            Scribe_References.Look(ref eventMap, "eventMap");

        }

        protected int lastFireTick = 0;
        protected Map eventMap;


    }
}
