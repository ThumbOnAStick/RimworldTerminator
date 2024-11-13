using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace CJTerminator
{
    public class PawnRenderNodeWorker_BionicSkin : PawnRenderNodeWorker
    {
        public PawnRenderNodeWorker_BionicSkin()
        {
            if (this.corpseRotation == 0f)
            {
                this.corpseRotation = Rand.Range(-45f, 45f);
            }
        }


        public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
        {
            if (!base.CanDrawNow(node, parms))
            {
                return false;
            }
            if (!parms.Portrait)
            {
                if (parms.posture == PawnPosture.Standing)
                {
                    return true;
                }
                Pawn_MindState mindState = parms.pawn.mindState;
                bool flag;
                if (mindState == null)
                {
                    flag = false;
                }
                else
                {
                    PawnDuty duty = mindState.duty;
                    bool? flag2;
                    if (duty == null)
                    {
                        flag2 = null;
                    }
                    else
                    {
                        DutyDef def = duty.def;
                        flag2 = ((def != null) ? new bool?(def.drawBodyOverride != null) : null);
                    }
                    bool? flag3 = flag2;
                    bool flag4 = true;
                    flag = (flag3.GetValueOrDefault() == flag4 & flag3 != null);
                }
                if (flag)
                {
                    return parms.pawn.mindState.duty.def.drawBodyOverride.Value;
                }
                if (parms.bed != null && parms.pawn.RaceProps.Humanlike)
                {
                    return parms.bed.def.building.bed_showSleeperBody;
                }
            }
            return true;
        }
        public override void PostDraw(PawnRenderNode node, PawnDrawParms parms, Mesh mesh, Matrix4x4 matrix)
        {
            base.PostDraw(node, parms, mesh, matrix);

            CJTerminatorUtil.DrawBionicSkin(parms.pawn, node, parms);
            if (parms.pawn.Drafted)
                CJTerminatorUtil.DrawEyeGlow(parms.pawn, parms);
        }

        public override void AppendDrawRequests(PawnRenderNode node, PawnDrawParms parms, List<PawnGraphicDrawRequest> requests)
        {
            requests.Add(new PawnGraphicDrawRequest(node, null, null));
        }

        public override Vector3 ScaleFor(PawnRenderNode n, PawnDrawParms parms)
        {
            return parms.pawn.DrawSize;
        }

        protected override Graphic GetGraphic(PawnRenderNode node, PawnDrawParms parms)
        {
            return CJTerminatorUtil.GraphicForBionicSkin(parms.pawn);
        }


        private float corpseRotation;


    }

}