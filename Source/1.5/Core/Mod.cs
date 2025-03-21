using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AnimalsGenderOnCaravan
{
    public class Mod : Verse.Mod
    {
        private static Harmony harmony;
        private static string id = "mrcool92.agoc";

        public Mod(ModContentPack content) : base(content)
        {
            Settings settings = GetSettings<Settings>();
            harmony = new Harmony(id);
            harmony.PatchAll();
        }
        
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            GetSettings<Settings>().DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "AGOC.AnimalsGenderOnCaravan".Translate();
        }
    }
}