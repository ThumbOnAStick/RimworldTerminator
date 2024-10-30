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
    public static class CJTerminatorUtil
    {

        public static float BodyPartHarmRatio(Pawn pawn, BodyPartRecord r)
        {
            float hitPoints = pawn.health.hediffSet.GetPartHealth(r);
            float maxHitPoint = r.def.GetMaxHealth(pawn); ;
            if (maxHitPoint == 0)
                return 0;
            return hitPoints / maxHitPoint;
        }

        public static BodyPartRecord GetRecordByName(Pawn pawn, string name)
        {
            BodyPartDef targetDef = DefDatabase<BodyPartDef>.GetNamed(name);
            return pawn.health.hediffSet.GetBodyPartRecord(targetDef);
        }

        public static void DrawBionicSkin(Pawn p,PawnRenderNode node, PawnDrawParms parms)
        {
            float angle = p.Drawer.renderer.BodyAngle(PawnRenderFlags.DrawNow);
            Graphic g = GraphicForBionicSkin(p);
            if (p.Dead)
            {
                g.Draw(p.DrawPos, p.Rotation, p, angle);
            }
            else
            {
                g.Draw(p.DrawPos, p.Rotation, p);

            }
        }

        public static Graphic GraphicForBionicSkin(Pawn pawn)
        {
            BodyPartRecord r = GetRecordByName(pawn, "MechanicalThorax");
            if (r == null)
                Log.Error("CJTerminator: No MechanicalThorax part found!");
            float ratio = BodyPartHarmRatio(pawn, r);

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


        static readonly string SkinGoodMultiplePath = "Terminator/Mech/CJTerminator2";
        static readonly string SkinBadMultiplePath = "Terminator/Mech/CJTerminator3";
        static readonly string NoSkinMultiplePath = "Terminator/Mech/CJTerminator";
    }
}
