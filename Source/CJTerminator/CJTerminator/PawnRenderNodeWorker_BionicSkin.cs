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
    public class PawnRenderNodeWorker_BionicSkin : PawnRenderNodeWorker
    {
        public PawnRenderNodeWorker_BionicSkin()
        {
            if (this.corpseRotation == 0f)
            {
                this.corpseRotation = Rand.Range(-45f, 45f);
            }
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

        private float corpseRotation;


    }

}