using HarmonyLib;
using System.Reflection;
using Verse;
using UnityEngine;

namespace AnimalsGenderOnCaravan
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            GetSettings<Settings>();
            Harmony harmony = new HarmonyLib.Harmony("mrcool92.agoc");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
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