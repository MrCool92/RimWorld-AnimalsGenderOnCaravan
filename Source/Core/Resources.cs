using Verse;
using UnityEngine;

namespace AnimalsGenderOnCaravan
{
    public static class Resources
    {
        [TweakValue("Interface", 0f, 50f)]
        public static float GenderIconWidth = 24f;

        [TweakValue("Interface", 0f, 50f)]
        public static float LifeStageIconWidth = 24f;

        public static readonly Texture2D VeryYoungIcon = ContentFinder<Texture2D>.Get("UI/Icons/LifeStage/VeryYoung", true);
        public static readonly Texture2D YoungIcon = ContentFinder<Texture2D>.Get("UI/Icons/LifeStage/Young", true);
        public static readonly Texture2D AdultIcon = ContentFinder<Texture2D>.Get("UI/Icons/LifeStage/Adult", true);
    }
}