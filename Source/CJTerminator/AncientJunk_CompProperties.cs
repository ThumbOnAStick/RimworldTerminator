using Verse;

namespace Terminator
{
    public class Bastion_AncientJunk_CompProperties : CompProperties
    {
        public ThingDef mechDef;
        public Rot4? spawnRotation;
        public CJ_AncientJunk_CompProperties()
        {
            compClass = typeof(CJ_AncientJunkComp);
        }
    }
}
