using Harmony;
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
                        Rect rect2 = new Rect(curX - (float)lifeStageIcon.width, (rect.height - (float)lifeStageIcon.width) / 2f, (float)lifeStageIcon.width, (float)lifeStageIcon.width);
                        curX -= (float)lifeStageIcon.width;
                        TooltipHandler.TipRegion(rect2, pawn.ageTracker.CurLifeStage.LabelCap);
                        GUI.DrawTexture(rect2, lifeStageIcon);
                    }
                }

                Texture2D genderIcon = pawn.gender.GetIcon();
                Rect rect1 = new Rect(curX - (float)genderIcon.width, (rect.height - (float)genderIcon.width) / 2f, (float)genderIcon.width, (float)genderIcon.width);
                curX -= (float)genderIcon.width;
                TooltipHandler.TipRegion(rect1, pawn.GetGenderLabel().CapitalizeFirst());
                GUI.DrawTexture(rect1, genderIcon);
            }
        }
    }
}