using HarmonyLib;
using Verse;
using RimWorld;
using UnityEngine;

namespace AnimalsGenderOnCaravan
{
    [HarmonyPatch(typeof(TransferableUIUtility), "DoExtraIcons")]
    static class TransferableUIUtility_DoExtraIcons_Patch
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
                        pawn.ageTracker.AgeBiologicalTicks.TicksToPeriod(out int years, out int quadrums, out int days, out float hoursFloat);
                        TooltipHandler.TipRegion(rect2, pawn.ageTracker.CurLifeStage.LabelCap.CapitalizeFirst() + "/n/n" + "AgeBiological".Translate(years, quadrums, days));
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