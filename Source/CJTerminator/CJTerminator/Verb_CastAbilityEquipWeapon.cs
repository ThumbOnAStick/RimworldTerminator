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
using Verse.Sound;
using static RimWorld.PsychicRitualRoleDef;
using static UnityEngine.GraphicsBuffer;

namespace CJTerminator
{
    public class Verb_CastAbilityEquipWeapon: Verb
    {
        protected override bool TryCastShot()
        {
            return true;
        }

        public override void OrderForceTarget(LocalTargetInfo target)
        {
            //CasterPawn.drafter.Drafted = false;
            Job job = JobMaker.MakeJob(JobDefOf.Equip, currentWeapon);
            CJTerminator.CJTerminatorDefOf.PickUpWeapon1.PlayOneShot(new TargetInfo(caster.Position, caster.MapHeld, false));

            CasterPawn.jobs.TryTakeOrderedJob(job, new JobTag?(JobTag.Misc));
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {

            if (!target.Cell.InBounds(caster.Map))
            {
                return false;
            }
            List<Thing> l = target.Cell.GetThingList(caster.Map);
            if (l.Count() < 1)
            {
                return false;
            }
            if (!l.Any(x => x.def.IsWeapon))
            {
                return false;
            }
            Thing weapon = l.First(x => x.def.IsWeapon);
            currentWeapon = weapon;
            return true;

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

        private Thing currentWeapon;

    }

   
}
