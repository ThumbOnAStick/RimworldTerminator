﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;
using VFECore;
using RimWorld;

namespace Terminator
{
    public class CJ_WorkGiver_RestoreJunk : WorkGiver_Scanner
    {
        public override PathEndMode PathEndMode => PathEndMode.Touch;

        public override Danger MaxPathDanger(Pawn pawn)
        {
            return Danger.Deadly;
        }

        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
        {
            IEnumerable<Thing> workThings = Find.CurrentMap.listerThings.AllThings.Where((x) => { return x.def.defName == "CJ_AncientTerminatorJunk"; });
            return workThings;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!pawn.CanReserveAndReach(t, PathEndMode, MaxPathDanger(pawn), 1, 1, null, forced))
            {
                return false;
            }
            if (pawn.mechanitor == null)
            {
                return false;
            }
            if (t.def == Definitions.CJ_AncientTerminatorJunk)
            {
                List<Thing> things = pawn.MapHeld.listerThings.ThingsOfDef(ThingDefOf.Plasteel);
                if (!things.Where((c) => { return c.stackCount > 100; }).Any() && things.Count() == 75)
                {
                    return false;
                }
            }


            if (t.Map.designationManager.DesignationOn(t, Definitions.CJ_RestoreJunk) == null)
            {
                return false;
            }
            return true;
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            Job job;
            
            //Find.CurrentMap.listerThings.ThingsOfDef(ThingDefOf.Plasteel).First();
            if (t.def == Definitions.CJ_AncientTerminatorJunk)
            {
                Thing thing = GenClosest.ClosestThing_Global_Reachable(t.PositionHeld, t.MapHeld, pawn.MapHeld.listerThings.ThingsOfDef(ThingDefOf.Plasteel), PathEndMode.ClosestTouch, TraverseParms.For(pawn), 9999f);
                job = JobMaker.MakeJob(Definitions.CJ_RestoreMechJunk, t, thing);
                job.count = 2;
            }

            return job;

        }
    }
}
