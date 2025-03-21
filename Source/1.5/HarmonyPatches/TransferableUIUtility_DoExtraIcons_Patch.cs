using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AnimalsGenderOnCaravan
{
    /// <summary>
    /// Adds gender and life stage icons to animals and colonists in caravan and trade panels.
    /// </summary>
    [HarmonyPatch(typeof(TransferableUIUtility), nameof(TransferableUIUtility.DoExtraIcons))]
    public static class TransferableUIUtility_DoExtraIcons_Patch
    {
        [HarmonyPrefix]
        public static void Prefix(Transferable trad, Rect rect, ref float curX)
        {
            Pawn pawn = trad.AnyThing as Pawn;
            if (pawn != null && (pawn.RaceProps.Animal || (pawn.RaceProps.Humanlike && pawn.RaceProps.hasGenders)))
            {
                bool isAnimal = pawn.RaceProps.Animal;
                Settings settings = Settings.Get();
                
                // Life stage icon
                if (settings.GetShowLifeStage(isAnimal))
                {
                    Texture2D lifeStageIcon = pawn.ageTracker.CurLifeStageRace.GetIcon(pawn);
                    if (lifeStageIcon != null)
                    {
                        Rect rect2 = new Rect(curX - Resources.LifeStageIconWidth, (rect.height - Resources.LifeStageIconWidth) / 2f, Resources.LifeStageIconWidth, Resources.LifeStageIconWidth);
                        curX -= Resources.LifeStageIconWidth;
                        pawn.ageTracker.AgeBiologicalTicks.TicksToPeriod(out int years, out int quadrums, out int days, out float hoursFloat);
                        TooltipHandler.TipRegion(rect2, pawn.ageTracker.CurLifeStage.LabelCap.CapitalizeFirst() + "\n\n" + "AgeBiological".Translate(years, quadrums, days));
                        GUI.DrawTexture(rect2, lifeStageIcon);
                    }
                }

                // Gender icon
                if (settings.GetShowGender(isAnimal))
                {
                    Texture2D genderIcon = pawn.gender.GetIcon();
                    Rect rect1 = new Rect(curX - Resources.GenderIconWidth, (rect.height - Resources.GenderIconWidth) / 2f, Resources.GenderIconWidth, Resources.GenderIconWidth);
                    curX -= Resources.GenderIconWidth;
                    TooltipHandler.TipRegion(rect1, pawn.GetGenderLabel().CapitalizeFirst());
                    GUI.DrawTexture(rect1, genderIcon);
                }

                // Follow Drafted Master icon
                if (settings.showFollowDraftedMaster)
                {
                    if (pawn.playerSettings?.followDrafted ?? false)
                    {
                        Texture2D followMasterIcon = Resources.FollowMasterIcon;
                        Rect rectIcon = new Rect(curX - Resources.FollowMasterIconWidth, (rect.height - Resources.FollowMasterIconWidth) / 2f, Resources.FollowMasterIconWidth, Resources.FollowMasterIconWidth);
                        curX -= Resources.FollowMasterIconWidth;
                        TooltipHandler.TipRegion(rectIcon, "CreatureFollowDrafted".Translate());
                        GUI.DrawTexture(rectIcon, followMasterIcon);
                    }
                }
            }
        }
    }
}