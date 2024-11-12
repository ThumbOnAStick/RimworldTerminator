using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace CJTerminator
{
    public class TerminatorModSettings : Verse.ModSettings
    {
        public TerminatorModSettings()
        {
            eventFrequency = 2;
            minFireDayPossitive = 30;
            minFireDayNegative = 100;
            isNegativeEnabled = true;
            isPossitiveEnabled = true;




        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref eventFrequency, "eventFrequency");
            Scribe_Values.Look<int>(ref minFireDayPossitive, "minFireDayPossitive");
            Scribe_Values.Look<int>(ref minFireDayNegative, "minFireDayNegative");
            Scribe_Values.Look<bool>(ref isPossitiveEnabled, "isPossitiveEnabled",true);
            Scribe_Values.Look<bool>(ref isNegativeEnabled, "isNegativeEnabled",true);

        }

        public void DoWindowsContent(Rect rect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(rect);
            listing_Standard.Label("eventFrequency".Translate(eventFrequency));
            eventFrequency = (int)listing_Standard.Slider(eventFrequency, 1, 10);
            listing_Standard.GapLine();
            listing_Standard.CheckboxLabeled("isNegativeEnabled".Translate(), ref isNegativeEnabled);
            if (isNegativeEnabled)
            {
                listing_Standard.Label("minFireDayNegative".Translate(minFireDayNegative));
                minFireDayNegative = (int)listing_Standard.Slider(minFireDayNegative, 1, 1000);
            }
            listing_Standard.GapLine();
            listing_Standard.CheckboxLabeled("isPossitiveEnabled".Translate(), ref isPossitiveEnabled);
            if (isPossitiveEnabled)
            {
                listing_Standard.Label("minFireDayPossitive".Translate(minFireDayPossitive));
                minFireDayPossitive = (int)listing_Standard.Slider(minFireDayPossitive, 1, 1000);
            }
            listing_Standard.End();
            this.Write();
        }

        public static int eventFrequency;
        public static bool isPossitiveEnabled;
        public static int minFireDayPossitive;
        public static bool isNegativeEnabled;
        public static int minFireDayNegative;



    }
}
