using Harmony;
using System.Reflection;
using Verse;
using RimWorld;
using UnityEngine;

namespace AnimalsGenderOnCaravan
{
    [StaticConstructorOnStartup]
    public class AnimalsGenderOnCaravan
    {
        static AnimalsGenderOnCaravan()
        {
            var harmony = HarmonyInstance.Create("com.animals.gender.on.caravan");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(TransferableUIUtility), "DoExtraAnimalIcons")]
        static class TransferableUIUtility_DoExtraAnimalIcons_Patch
        {
            static void Prefix(Transferable trad, Rect rect, ref float curX)
            {
                Pawn pawn = trad.AnyThing as Pawn;
                if (pawn != null && pawn.RaceProps.Animal)
                {
                    Texture2D genderIcon = pawn.gender.GetIcon();
                    Rect position = new Rect(curX - (float)genderIcon.width, (rect.height - (float)genderIcon.width) / 2f, (float)genderIcon.width, (float)genderIcon.width);
                    curX -= (float)genderIcon.width;
                    TooltipHandler.TipRegion(position, pawn.GetGenderLabel().CapitalizeFirst());
                    GUI.DrawTexture(position, genderIcon);
                }
            }
        }
    }
}