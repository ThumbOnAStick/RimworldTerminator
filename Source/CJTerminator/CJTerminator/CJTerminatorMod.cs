using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CJTerminator
{
    public class CJTerminatorMod : Mod
    {
        public CJTerminatorMod(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony(content.PackageId);
            harmony.PatchAll();
            settings = GetSettings<TerminatorModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            GetSettings<TerminatorModSettings>().DoWindowsContent(inRect);

        }

        public override string SettingsCategory()
        {
            return "Terminators".Translate();
        }

        public static TerminatorModSettings settings;



    }
}
