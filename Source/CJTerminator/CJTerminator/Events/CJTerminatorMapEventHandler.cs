using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CJTerminator
{
    public class CJTerminatorMapEventHandler : MapComponent
    {
        public CJTerminatorMapEventHandler(Map map) : base(map)
        {
            events = new List<CJTerminatorMapEvent>();
            this.AppendEvent(CJTerminatorUtil.SpawnTerminatorEventPossitive(map));
            this.AppendEvent(CJTerminatorUtil.SpawnTerminatorEventNegative(map));

        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            int ticksGame = Find.TickManager.TicksGame;
            for(int i = 0; i< events.Count; i++)
            {
                CJTerminatorMapEvent e = events[i];
                if(e == null)
                {
                    events.RemoveAt(i);
                    return;
                }
                e.EventTick(ticksGame);
                if(e.ShouldEventBeRemoved(ticksGame))
                {
                    events.RemoveAt(i);
                    return;
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref events, "TerminatorMapEvents",LookMode.Deep);
            for (int i = 0; i < events.Count; i++)
            {
                CJTerminatorMapEvent e = events[i];
            }

        }

        public override void MapGenerated()
        {
            base.MapGenerated();

        }

        public void AppendEvent(CJTerminatorMapEvent newEvent)
        {
            if(this.events.Any(x => x.GetType() == newEvent.GetType()))
            {
                return;
            }
            events.Add(newEvent);
            newEvent.SetLastFireTick(Find.TickManager.TicksGame);
            newEvent.PostAppend();
            
        }

        private List<CJTerminatorMapEvent> events ;
    }
}
