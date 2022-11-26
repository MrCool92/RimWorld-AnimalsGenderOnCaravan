using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AnimalsGenderOnCaravan
{
    /// <summary>
    /// Removes reserved icon space for pawns with no weapon equipped in caravan and trade panels.
    /// </summary>
    [HarmonyPatch(typeof(TransferableOneWayWidget), "DoRow")]
    public static class TransferableOneWayWidget_DoRow_Patch
    {
        static AccessTools.FieldRef<TransferableOneWayWidget, bool> drawEquippedWeapon =
            AccessTools.FieldRefAccess<TransferableOneWayWidget, bool>("drawEquippedWeapon");

        [HarmonyPrefix]
        public static void Prefix(out bool __state, TransferableOneWayWidget __instance, Rect rect, TransferableOneWay trad, int index, float availableMass)
        {
            // save original value so we can make a new decision for each row
            __state = drawEquippedWeapon(__instance);

            if (Settings.Get().removeReservedWeaponIconSpace)
            {
                // change for current method according to pawn equipment
                Pawn pawn = trad.AnyThing as Pawn;
                bool shouldDrawWeapon = pawn != null && pawn.equipment != null && pawn.equipment.Primary != null;
                drawEquippedWeapon(__instance) = shouldDrawWeapon;
            }
            
        }
        
        [HarmonyPostfix]
        public static void Postfix(bool __state, TransferableOneWayWidget __instance, Rect rect, TransferableOneWay trad, int index,
            float availableMass)
        {
            // revert to original value
            drawEquippedWeapon(__instance) = __state;
        }
    }
}