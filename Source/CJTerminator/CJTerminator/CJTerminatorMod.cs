using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CJTerminator
{
    public class CJTerminatorMod : Mod
    {
        public CJTerminatorMod(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony(content.PackageId);
            harmony.PatchAll();
        }
    }
}
