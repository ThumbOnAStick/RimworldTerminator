using CJTerminator.Events;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CJTerminator
{
    public static class TerminatorUtil
    {

        public static float BodyPartHarmRatio(Pawn pawn, BodyPartRecord r)
        {
            float hitPoints = pawn.health.hediffSet.GetPartHealth(r);
            float maxHitPoint = r.def.GetMaxHealth(pawn); ;
            if (maxHitPoint == 0)
                return 0;
            return hitPoints / maxHitPoint;
        }

        public static BodyPartRecord GetRecordByName(Pawn pawn, string name)
        {
            BodyPartDef targetDef = DefDatabase<BodyPartDef>.GetNamed(name);
            return pawn.health.hediffSet.GetBodyPartRecord(targetDef);
        }

      

        #region Events
        public static CJTerminatorEvent_SpawnTerminator SpawnTerminatorEvent(Map map, IntVec3 Loc, Pawn p)
        {
            return new CJTerminatorEvent_SpawnTerminator(map, Loc, p);
        }
        public static CJTerminatorEvent_SpawnTerminatorHostile SpawnTerminatorEventHostile(Map map)
        {
            return new CJTerminatorEvent_SpawnTerminatorHostile(map);
        }
        public static CJTerminatorEvent_Possitive SpawnTerminatorEventPossitive(Map map)
        {
            return new CJTerminatorEvent_Possitive(map);
        }
        public static CJTerminatorEvent_Negative SpawnTerminatorEventNegative(Map map)
        {
            return new CJTerminatorEvent_Negative(map);
        }
        #endregion

     
    }
}
