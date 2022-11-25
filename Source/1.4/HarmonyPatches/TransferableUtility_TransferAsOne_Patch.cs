using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalsGenderOnCaravan
{
    /// <summary>
    /// Avoid Stacking of different training (only Attack training is checked)
    /// </summary>
    [HarmonyPatch(typeof(TransferableUtility), nameof(TransferableUtility.TransferAsOne))]
    public class TransferableUtility_TransferAsOne_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool __result, Thing a, Thing b, TransferAsOneMode mode)
        {
            if (a.def.category == ThingCategory.Pawn && b.def == a.def)
            {
                Pawn pawn1 = (Pawn) a;
                Pawn pawn2 = (Pawn) b;
                if (pawn1.RaceProps.Animal)
                {
                    var attackTraining = TrainableDefOf.Release;
                    if (
                        pawn1.training.HasLearned(attackTraining) ^
                        pawn2.training.HasLearned(attackTraining)
                        )
                    {
                        // if one has learned to be released for attacking and the other one not then avoid stacking
                        __result = false;
                        return false; // skip original
                    }

                    if (
                        (pawn1.playerSettings?.followDrafted ?? false) ^
                        (pawn2.playerSettings?.followDrafted ?? false)
                        )
                    {
                        // if one has a caller and the other one not then avoid stacking
                        __result = false;
                        return false;
                    }
                    
                        
                }
            }

            return true;
        }
    }
}