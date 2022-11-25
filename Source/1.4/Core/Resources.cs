using UnityEngine;
using Verse;

namespace AnimalsGenderOnCaravan
{
    [StaticConstructorOnStartup]
    public static class Resources
    {
        [TweakValue("Interface", 0f, 50f)]
        public static float GenderIconWidth = 24f;

        [TweakValue("Interface", 0f, 50f)]
        public static float LifeStageIconWidth = 24f;
        
        [TweakValue("Interface", 0f, 50f)]
        public static float FollowMasterIconWidth = 24f;

        public static readonly Texture2D VeryYoungIcon;
        public static readonly Texture2D YoungIcon;
        public static readonly Texture2D AdultIcon;
        public static readonly Texture2D FollowMasterIcon;

        static Resources()
        {
            VeryYoungIcon = ContentFinder<Texture2D>.Get("UI/Icons/LifeStage/VeryYoung", true);
            YoungIcon = ContentFinder<Texture2D>.Get("UI/Icons/LifeStage/Young", true);
            AdultIcon = ContentFinder<Texture2D>.Get("UI/Icons/LifeStage/Adult", true);
            FollowMasterIcon = ContentFinder<Texture2D>.Get("UI/Icons/Animal/FollowDrafted", true);
        }
    }
}