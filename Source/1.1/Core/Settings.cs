using Verse;
using UnityEngine;

namespace AnimalsGenderOnCaravan
{
    internal class Settings : ModSettings
    {
        public static Settings Get()
        {
            return LoadedModManager.GetMod<Mod>().GetSettings<Settings>();
        }

        public void DoWindowContents(Rect wrect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(wrect);
            listing_Standard.CheckboxLabeled("AGOC.ShowLifeStage".Translate(), ref showLifeStage, null);
            Texture2D icon = GetLifeStagIcon();
            Rect rect = new Rect(Text.CalcSize("AGOC.ShowLifeStage".Translate()).x + 5f, -1f, Resources.LifeStageIconWidth, Resources.LifeStageIconWidth);
            Color previousColor = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, showLifeStage ? 1f : .2f);
            GUI.DrawTexture(rect, icon);
            GUI.color = previousColor;          
            listing_Standard.Gap(12f);
            listing_Standard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref showLifeStage, "showLifeStage", true, false);
        }

        private Texture2D GetLifeStagIcon()
        {
            float p = Time.time % 3f;
            if (p > 2f)
            {
                return Resources.VeryYoungIcon;
            } else if (p > 1f)
            {
                return Resources.YoungIcon;
            }
            return Resources.AdultIcon;          
        }

        public bool showLifeStage;
    }
}