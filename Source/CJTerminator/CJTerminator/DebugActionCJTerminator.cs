using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using LudeonTK;
using UnityEngine;

namespace CJTerminator
{
    public static class DebugActionCJTerminator
    {
        [DebugAction("CJTerminator", "", false, false, false, false, 0, false, allowedGameStates = AllowedGameStates.PlayingOnMap, displayPriority = 1000)]
        public static DebugActionNode SpawnTerminator()
        {

            PawnKindDef localKindDef = CJTerminatorDefOf.Mech_CJTerminator;
            var action = new DebugActionNode(localKindDef.defName, DebugActionType.ToolMap, null, null)
            {
                category = DebugToolsSpawning.GetCategoryForPawnKind(localKindDef),
                action = delegate ()
                {
                    Faction faction = FactionUtility.DefaultFactionFrom(localKindDef.defaultFactionType);
                    Pawn pawn = PawnGenerator.GeneratePawn(localKindDef, faction);
                    GenSpawn.Spawn(pawn, UI.MouseCell(), Find.CurrentMap, WipeMode.Vanish);
                }
            };

            return action;
        }

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





    }
}
