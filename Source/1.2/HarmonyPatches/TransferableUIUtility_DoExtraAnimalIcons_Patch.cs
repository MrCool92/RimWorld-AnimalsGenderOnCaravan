using HarmonyLib;
using Verse;
using RimWorld;
using UnityEngine;

namespace AnimalsGenderOnCaravan
{
    [HarmonyPatch(typeof(TransferableUIUtility), "DoExtraAnimalIcons")]
    static class TransferableUIUtility_DoExtraAnimalIcons_Patch
    {
        static void Prefix(Transferable trad, Rect rect, ref float curX)
        {
            Pawn pawn = trad.AnyThing as Pawn;
            if (pawn != null && pawn.RaceProps.Animal)
            {
                if (Settings.Get().showLifeStage)
                {
                    Texture2D lifeStageIcon = pawn.ageTracker.CurLifeStageRace.GetIcon(pawn);
                    if (lifeStageIcon != null)
                    {
                        Rect rect2 = new Rect(curX - Resources.LifeStageIconWidth, (rect.height - Resources.LifeStageIconWidth) / 2f, Resources.LifeStageIconWidth, Resources.LifeStageIconWidth);
                        curX -= Resources.LifeStageIconWidth;
                        TooltipHandler.TipRegion(rect2, pawn.ageTracker.CurLifeStage.LabelCap);
                        GUI.DrawTexture(rect2, lifeStageIcon);
                    }
                }

                Texture2D genderIcon = pawn.gender.GetIcon();
                Rect rect1 = new Rect(curX - Resources.GenderIconWidth, (rect.height - Resources.GenderIconWidth) / 2f, Resources.GenderIconWidth, Resources.GenderIconWidth);
                curX -= Resources.GenderIconWidth;
                TooltipHandler.TipRegion(rect1, pawn.GetGenderLabel().CapitalizeFirst());
                GUI.DrawTexture(rect1, genderIcon);
            }
        }
    }
}