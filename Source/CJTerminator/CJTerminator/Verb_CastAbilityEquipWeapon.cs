using RimWorld;
using RimWorld.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Verse;
using Verse.AI;

namespace CJTerminator
{
    public class Verb_CastAbilityEquipWeapon: Verb
    {
        protected override bool TryCastShot()
        {
            return true;
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            //bool newResult = false;
            //if (target != null && target.HasThing && target.Thing.def.IsWeapon &&
            //    ability.pawn.CanReach(target, PathEndMode.Touch, ability.pawn.NormalMaxDanger()))
            //{
            //    newResult = true;
            //}


            return target != null && target.Cell.TryGetFirstThing(map;
           //     && target.HasThing && target.Thing.def.IsWeapon &&
           //this.CasterPawn.CanReach(target, PathEndMode.Touch, this.CasterPawn.NormalMaxDanger());
        }


        public override bool MultiSelect
        {
            get
            {
                return true;
            }
        }


        public virtual ThingDef JumpFlyerDef
        {
            get
            {
                return ThingDefOf.PawnFlyer;
            }
        }



        public override void OnGUI(LocalTargetInfo target)
        {
            if (this.ValidateTarget(target))
            {
                return;
            }
            GenUI.DrawMouseAttachment(TexCommand.CannotShoot);
        }



        private float cachedEffectiveRange = -1f;

    }

   
}
