using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Database;
using Klei.AI;
using UnityEngine;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_RefinaryShop
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.RefinaryShop.IncludeRoom)
            {
                __instance.Add(RoomTypes_AllModded.RefinaryShop);


            }
        }

        private static void MakeGameObjectRefinable(GameObject go)
        {
            if (Settings.Instance.RefinaryShop.IncludeRoom)
            {
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.ChemicalBuildingTag);
                go.AddOrGet<Refinable>();
            }
        }

        [HarmonyPatch(typeof(FertilizerMakerConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class FertilizerMakerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(OxyliteRefineryConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class OxyliteRefineryConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(PolymerizerConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class PolymerizerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(AlgaeDistilleryConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class AlgaeDistilleryConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(ChlorinatorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class ChlorinatorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(MilkFatSeparatorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class MilkFatSeparatorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(EthanolDistilleryConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class EthanolDistilleryConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(WaterPurifierConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class WaterPurifierConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }

        [HarmonyPatch(typeof(OilRefineryConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class OilRefineryConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                MakeGameObjectRefinable(go);
            }
        }
    }
}
