using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CJTerminator
{
    public class PawnRenderNode_BionicSkin : PawnRenderNode_AnimalPart
    {



        public PawnRenderNode_BionicSkin(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree) : base(pawn, props, tree)
        {

        }

        Vector2 BionicSkinDrawSize
        {
            get
            {
                return this.tree.pawn.kindDef.lifeStages[0].bodyGraphicData.drawSize;
            }
        }
        
        public override Graphic GraphicFor(Pawn pawn)
        {
            BodyPartRecord r = CJTerminatorUtil.GetRecordByName(pawn, "MechanicalThorax");
            if (r == null)
                Log.Error("CJTerminator: No MechanicalThorax part found!");
            float ratio = CJTerminatorUtil.BodyPartHarmRatio(pawn, r);

            string path = SkinGoodMultiplePath;
            if (ratio < .5f)
            {
                path = NoSkinMultiplePath;
            }
            else if (ratio < .8f)
            {
                path = SkinBadMultiplePath;
            }

            return GraphicDatabase.Get<Graphic_Multi>(path, ShaderDatabase.CutoutSkinColorOverride, BionicSkinDrawSize, Color.white, Color.white, null);
        }

        static readonly string SkinGoodMultiplePath = "Terminator/Mech/CJTerminator2";
        static readonly string SkinBadMultiplePath = "Terminator/Mech/CJTerminator3";
        static readonly string NoSkinMultiplePath = "Terminator/Mech/CJTerminator";

    }
}
