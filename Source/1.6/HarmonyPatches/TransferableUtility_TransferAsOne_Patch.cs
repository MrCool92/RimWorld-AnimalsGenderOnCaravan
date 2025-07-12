using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalsGenderOnCaravan
{
    /// <summary>
    /// Avoid stacking of animals with different stats 
    /// </summary>
    [HarmonyPatch(typeof(TransferableUtility), nameof(TransferableUtility.TransferAsOne))]
    public class TransferableUtility_TransferAsOne_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool __result, Thing a, Thing b, TransferAsOneMode mode)
        {
            if (a.def.category != ThingCategory.Pawn || b.def != a.def) 
                return true; // let original RimWorld decide here
            
            Pawn pawn1 = (Pawn) a;
            if (pawn1.RaceProps.Animal)
            {
                Pawn pawn2 = (Pawn) b;
                TrainableDef attackTraining = TrainableDefOf.Release;
                if (
                    (pawn1.training?.HasLearned(attackTraining) ?? false) ^
                    (pawn2.training?.HasLearned(attackTraining) ?? false)
                )
                {
                    // if one has learned to be released for attacking and the other one not then avoid stacking
                    __result = false;
                    return false;
                }

                if (
                    (pawn1.playerSettings?.followDrafted ?? false) ^
                    (pawn2.playerSettings?.followDrafted ?? false)
                )
                {
                    // if one is set to follow drafted master and the other one not then avoid stacking
                    __result = false;
                    return false;
                }
            }

            return true;
        }
    }
}