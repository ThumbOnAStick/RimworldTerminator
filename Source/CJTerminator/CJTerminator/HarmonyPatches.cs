using HarmonyLib;
using RimWorld.Planet;
using RimWorld.QuestGen;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CJTerminator.HarmonyPatches
{



    //[HarmonyPatch(typeof(Pawn), nameof(Pawn.Tick))]
    //internal static class QuestNode_GetNearbySettlement_RandomNearbyTradeableSettlement
    //{
    //    static bool IsTerminator(Pawn p)
    //    {
    //        if (p.kindDef == CJTerminatorDefOf.Mech_CJTerminator)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    static void Postfix(ref Pawn __instance)
    //    {
    //        if (!IsTerminator(__instance))
    //        {
    //            return;
    //        }
    //        Log.Message("it worked");
    //        __instance.Drawer.renderer.EnsureGraphicsInitialized();
    //    }
    //}

}
