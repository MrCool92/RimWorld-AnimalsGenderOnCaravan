using HarmonyLib;
using RimWorld;
using System.Linq;
using System.Reflection;
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

            Patch_TransferableUIUtility();
            Patch_TransferableOneWayWidget(settings.removeReservedWeaponIconSpace);
        }

        private static void Patch_TransferableUIUtility()
        {
            MethodInfo original = typeof(TransferableUIUtility).GetMethod("DoExtraIcons");
            var prefix = new HarmonyMethod(typeof(TransferableUIUtility_DoExtraIcons_Patch).GetMethod("Prefix"));
            harmony.Patch(original, prefix: prefix);
        }

        public static void Patch_TransferableOneWayWidget(bool toggle)
        {
            MethodInfo original = typeof(TransferableOneWayWidget).GetMethod("DoRow", BindingFlags.Instance | BindingFlags.NonPublic);
            if (toggle)
            {
                var prefix = new HarmonyMethod(typeof(TransferableOneWayWidget_DoRow_Patch).GetMethod("Prefix"));
                harmony.Patch(original, prefix: prefix);
            }
            else
            {
                if (harmony.GetPatchedMethods().Contains(original))
                    harmony.Unpatch(original, HarmonyPatchType.Prefix);
            }
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