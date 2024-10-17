using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using VFECore;

namespace Terminator
{
    public class CJ_AncientTerminatorComp : ThingComp
    {
        public CJ_AncientJunk_CompProperties Props => props as CJ_AncientJunk_CompProperties;

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            Designation restoreMechDesignation = parent.Map.designationManager.DesignationOn(parent, Definitions.CJ_RestoreJunk);
            if (restoreMechDesignation == null)
            {
                yield return new Command_Action
                {
                    defaultLabel = "RestoreMechLabel".Translate(),
                    defaultDesc = "RestoreMechDesc".Translate(),
                    icon = ContentFinder<Texture2D>.Get("Terminator/Other/repairjunk_icon"),
                    action = delegate
                    {
                        parent.Map.designationManager.AddDesignation(new Designation(parent, Definitions.CJ_RestoreJunk));
                    }
                };
            }
           /* else
            {
                yield return new Command_Action
                {
                    defaultLabel = "Cancel".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Designators/Cancel"),
                    action = delegate
                    {
                        parent.Map.designationManager.RemoveDesignation(restoreMechDesignation);
                    }
                };
            }*/
        }
        public void RestoreMech(Pawn worker)
        {
            ThingDef mechDef = Props.mechDef;
            IntVec3 position = parent.Position;
            Map map = parent.Map;
            Faction faction = worker.Faction;
            CellRect existingRect = parent.OccupiedRect();
            Rot4 rot = (Props.spawnRotation.HasValue ? Props.spawnRotation.Value : parent.Rotation);
            PawnKindDef kind = DefDatabase<PawnKindDef>.AllDefs.Where((PawnKindDef pk) => pk.race == Props.mechDef).First();
            Pawn pawn2 = PawnGenerator.GeneratePawn(new PawnGenerationRequest(kind, faction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: true, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, null, null, null, 0f, DevelopmentalStage.Newborn));
            worker.relations.AddDirectRelation(PawnRelationDefOf.Overseer, pawn2);
            parent.Destroy();
            GenSpawn.Spawn(pawn2, existingRect.CenterCell, map, rot, WipeMode.VanishOrMoveAside);
        }

    }
}
