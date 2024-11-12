using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CJTerminator
{
    public static class TerminatorDraw
    {
        public static void DrawBionicSkin(Pawn p, PawnRenderNode node, PawnDrawParms parms)
        {
            float angle = p.Drawer.renderer.BodyAngle(PawnRenderFlags.DrawNow);
            Graphic g = GraphicForBionicSkin(p);
            if (p.Dead || p.Downed)
            {
                return;
            }
            else
            {
                g.Draw(p.DrawPos, p.Rotation, p, p.Drawer.renderer.BodyAngle(PawnRenderFlags.None));

            }
        }

        public static void DrawEyeGlow(Pawn p, PawnDrawParms parms)
        {
            if (p.Dead)
            {
                return;
            }

            Graphic g = GraphicForEyeGlow(p);
            if (p.Rotation == Rot4.South)
                g.Draw(p.DrawPos + eyeOffset1, p.Rotation, p);
            else if (p.Rotation == Rot4.West)
                g.Draw(p.DrawPos + eyeOffset2, p.Rotation, p);

        }

        public static Graphic GraphicForEyeGlow(Pawn pawn)
        {
            return GraphicDatabase.Get<Graphic_Single>(EyeGlowPath, ShaderDatabase.MoteGlow, BionicSkinDrawSize(pawn) * 0.1f, Color.red, Color.red);
        }


        public static Graphic GraphicForBionicSkin(Pawn pawn)
        {
            BodyPartRecord r = TerminatorUtil.GetRecordByName(pawn, "MechanicalThorax");
            if (r == null)
                Log.Error("CJTerminator: No MechanicalThorax part found!");
            float ratio = TerminatorUtil.BodyPartHarmRatio(pawn, r);

            string path = SkinGoodMultiplePath;
            if (ratio < .5f)
            {
                path = NoSkinMultiplePath;
            }
            else if (ratio < .8f)
            {
                path = SkinBadMultiplePath;
            }

            return GraphicDatabase.Get<Graphic_Multi>(path, ShaderDatabase.CutoutSkinColorOverride, BionicSkinDrawSize(pawn), Color.white, Color.white, null);
        }

        public static Vector2 BionicSkinDrawSize(Pawn p)
        {
            return p.kindDef.lifeStages[0].bodyGraphicData.drawSize;
        }

        static readonly Vector3 eyeOffset1 = new Vector3(0.13f, 0, 0.38f);
        static readonly Vector3 eyeOffset2 = new Vector3(-0.3f, 0, 0.45f);

        static readonly string EyeGlowPath = "Terminator/Mech/Dot";
        static readonly string SkinGoodMultiplePath = "Terminator/Mech/CJTerminator2";
        static readonly string SkinBadMultiplePath = "Terminator/Mech/CJTerminator3";
        static readonly string NoSkinMultiplePath = "Terminator/Mech/CJTerminator";
    }
}
