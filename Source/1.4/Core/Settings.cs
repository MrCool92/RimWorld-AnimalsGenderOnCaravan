using UnityEngine;
using Verse;

namespace AnimalsGenderOnCaravan
{
    internal class Settings : ModSettings
    {
        public bool showLifeStage = true;
        public bool humanShowGender = false;
        public bool humanShowLifeStage = false;

        public static Settings Get()
        {
            return LoadedModManager.GetMod<Mod>().GetSettings<Settings>();
        }

        public void DoWindowContents(Rect wrect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(wrect);
            
            // Animals
            listing_Standard.Label("Animals");
            
            // Show life stage
            LifeStage(listing_Standard, ref showLifeStage);
            
            listing_Standard.Gap();
            
            // Humanlike
            listing_Standard.Label("Humanlike");
            
            // Show gender
            listing_Standard.CheckboxLabeled("AGOC.ShowGender".Translate(), ref humanShowGender, null);
            
            // Show life stage
            LifeStage(listing_Standard, ref humanShowLifeStage);
            
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref showLifeStage, "showLifeStage", true, false);
        }

        private Texture2D GetLifeStageIcon()
        {
            float p = Time.time % 3f;
            if (p > 2f)
                return Resources.VeryYoungIcon;
            if (p > 1f)
                return Resources.YoungIcon;
            return Resources.AdultIcon;
        }

        public bool ShowGender(bool isAnimal)
        {
            return isAnimal || humanShowGender;
        }
        
        public bool ShowLifeStage(bool isAnimal)
        {
            return isAnimal ? showLifeStage : humanShowLifeStage;
        }

        private void LifeStage(Listing_Standard listing, ref bool enabled)
        {
            listing.CheckboxLabeled("AGOC.ShowLifeStage".Translate(), ref enabled, null);

            float iconYPosition = listing.CurHeight - Resources.LifeStageIconWidth;
            Rect rect = new Rect(Text.CalcSize("AGOC.ShowLifeStage".Translate()).x + 5f, iconYPosition - 1f, Resources.LifeStageIconWidth, Resources.LifeStageIconWidth);
            Color previousColor = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, enabled ? 1f : .2f);
            GUI.DrawTexture(rect, GetLifeStageIcon());
            GUI.color = previousColor;
        }
    }
}