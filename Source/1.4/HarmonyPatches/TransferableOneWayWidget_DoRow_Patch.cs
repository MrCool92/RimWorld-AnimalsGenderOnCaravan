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
        public static void Prefix(TransferableOneWayWidget __instance, Rect rect, TransferableOneWay trad, int index, float availableMass)
        {
            Pawn pawn = trad.AnyThing as Pawn;
            bool shouldDrawWeapon = pawn != null && pawn.equipment != null && pawn.equipment.Primary != null;
            drawEquippedWeapon(__instance) = drawEquippedWeapon(__instance) && shouldDrawWeapon;
        }
    }
}