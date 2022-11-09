using UnityEngine;
using Verse;

namespace AnimalsGenderOnCaravan
{
    internal class Settings : ModSettings
    {
        public bool showLifeStage = true;
        public bool humanShowGender = false;
        public bool humanShowLifeStage = false;
        public bool removeReservedWeaponIconSpace = false;

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
            DrawLifeStage(listing_Standard, ref showLifeStage);
            
            listing_Standard.Gap();
            
            // Colonists
            listing_Standard.Label("Colonists");
            
            // Show gender
            listing_Standard.CheckboxLabeled("AGOC.ShowGender".Translate(), ref humanShowGender, null);
            
            // Show life stage
            DrawLifeStage(listing_Standard, ref humanShowLifeStage);
            
            // Remove reserved empty weapon icon space
            bool changed = removeReservedWeaponIconSpace;
            listing_Standard.CheckboxLabeled("AGOC.PawnWeaponIconFix".Translate(), ref removeReservedWeaponIconSpace, null);
            if (changed != removeReservedWeaponIconSpace)
                AnimalsGenderOnCaravan.Mod.Patch_TransferableOneWayWidget(removeReservedWeaponIconSpace);
            
            listing_Standard.End();
        }
        
        public override void ExposeData()
        {
            Scribe_Values.Look(ref showLifeStage, "showLifeStage", true, false);
            Scribe_Values.Look(ref humanShowGender, "humanShowGender", false, false);
            Scribe_Values.Look(ref humanShowLifeStage, "humanShowLifeStage", false, false);
            Scribe_Values.Look(ref removeReservedWeaponIconSpace, "removeReservedWeaponIconSpace", false, false);
        }
        
        private void DrawLifeStage(Listing_Standard listing, ref bool enabled)
        {
            listing.CheckboxLabeled("AGOC.ShowLifeStage".Translate(), ref enabled, null);

            float iconYPosition = listing.CurHeight - Resources.LifeStageIconWidth;
            Rect rect = new Rect(Text.CalcSize("AGOC.ShowLifeStage".Translate()).x + 5f, iconYPosition - 1f, Resources.LifeStageIconWidth, Resources.LifeStageIconWidth);
            Color previousColor = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, enabled ? 1f : .2f);
            GUI.DrawTexture(rect, GetLifeStageIcon());
            GUI.color = previousColor;
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

        public bool GetShowGender(bool isAnimal)
        {
            return isAnimal || humanShowGender;
        }
        
        public bool GetShowLifeStage(bool isAnimal)
        {
            return isAnimal ? showLifeStage : humanShowLifeStage;
        }

    }
}