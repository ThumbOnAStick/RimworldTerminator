using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using LudeonTK;
using UnityEngine;
using Verse.AI;

namespace CJTerminator
{
    public static class DebugActionCJTerminator
    {

        [DebugAction("CJTerminator", null, false, false, false, false, 0, false, actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void DamageTerminatorBodyByFifty(Pawn p)
        {
            BodyPartRecord r = CJTerminatorUtil.GetRecordByName(p, "MechanicalThorax");
            if (r == null)
            {
                Log.Error("CJTerminator: MechanicalThorax is non-exist for the pawn");
                return;
            }
            DamageDef damageDef = DamageDefOf.Blunt;
            DamageInfo dinfo = new DamageInfo(damageDef, 50, 999f, -1f, null, r);
            p.TakeDamage(dinfo);
        }

        [DebugAction("CJTerminator", null, false, false, false, false, 0, false, actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void GetTerminatorBodyHealthRatio(Pawn p)
        {
            BodyPartRecord r = CJTerminatorUtil.GetRecordByName(p, "MechanicalThorax");
            if (r == null)
            {
                Log.Error("CJTerminator: MechanicalThorax is non-exist for the pawn");
                return;
            }
            Log.Message(CJTerminatorUtil.BodyPartHarmRatio(p,r));
        }

        [DebugAction("CJTerminator", null, false, false, false, false, 0, false, actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void EquipWeaponForTerminator(Pawn p)
        {
            Thing weapon = Find.CurrentMap.spawnedThings.First(x => x.def.IsWeapon);
            Job job = JobMaker.MakeJob(JobDefOf.Equip, weapon);

            p.jobs.TryTakeOrderedJob(job, new JobTag?(JobTag.Misc));
        }


        [DebugAction("CJTerminator", null, false, false, false, false, 0, false, actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SpawnTerminatorProtectingPawn(Pawn p)
        {
            Find.CurrentMap.GetComponent<CJTerminatorMapEventHandler>().AppendEvent(CJTerminatorUtil.SpawnTerminatorEvent(p.Map, p.Position, p));
        }

        [DebugAction("CJTerminator", null, false, false, false, false, 0, false, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SpawnTerminatorKillingColonist()
        {
            Find.CurrentMap.GetComponent<CJTerminatorMapEventHandler>().AppendEvent(CJTerminatorUtil.SpawnTerminatorEventHostile(Find.CurrentMap));
        }


        [DebugAction("CJTerminator", null, false, false, false, false, 0, false, actionType = DebugActionType.ToolMap, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        public static void SpawnTerminatorEffect()
        {
            IntVec3 cell = UI.MouseCell();
            Map m = Find.CurrentMap;
            Effecter e = CJTerminatorDefOf.TerminatorApears.Spawn(cell, m);
            TargetInfo currentTargetInfo = new TargetInfo(cell, m);
            IntVec3 rndOffset = new IntVec3(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            TargetInfo nextTargetInfo = new TargetInfo(cell + rndOffset, m);
            e.Trigger(currentTargetInfo, nextTargetInfo);

        }

    }
}
