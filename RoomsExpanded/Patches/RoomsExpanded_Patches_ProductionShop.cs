using Database;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_ProductionShop
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.ProductionShop.IncludeRoom)
            {
                __instance.Add(RoomTypes_AllModded.ProductionShop);
            }
        }

        //[HarmonyPatch(typeof(Shower))]
        //[HarmonyPatch("OnWorkTick")]
        public static class Shower_OnWorkTick_Patch1
        {
            private static string PlumbedBathroomId = string.Empty;

            public static void Postfix(ref Shower __instance, float dt)
            {
                if (!Settings.Instance.Bathroom.IncludeRoom) return;

                if (string.IsNullOrEmpty(PlumbedBathroomId))
                    PlumbedBathroomId = Db.Get().RoomTypes.PlumbedBathroom.Id;

                if (RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeBathroomData.RoomId)
                    || RoomTypes_AllModded.IsInTheRoom(__instance, PlumbedBathroomId)
                    || RoomTypes_AllModded.IsInTheRoom(__instance, Db.Get().RoomTypes.PrivateBedroom.Id))
                {
                    __instance.WorkTimeRemaining -= dt * Settings.Instance.Bathroom.Bonus;
                }
            }
        }



        [HarmonyPatch(typeof(RockCrusherConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class RockCrusherConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(GlassForgeConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class GlassForgeConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                {
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
                    go.AddOrGet<Fabricable>();
                }
            }
        }

        [HarmonyPatch(typeof(SupermaterialRefineryConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class SupermaterialRefineryConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                {
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
                    go.AddOrGet<Fabricable>();
                }
            }
        }

        [HarmonyPatch(typeof(MetalRefineryConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class MetalRefineryConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(DiamondPressConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class DiamondPressConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(MilkPressConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class MilkPressConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(SludgePressConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class SludgePressConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(CraftingTableConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class CraftingTableConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(ClothingAlterationStationConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class ClothingAlterationStationConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(MissileFabricatorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class MissileFabricatorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }

        [HarmonyPatch(typeof(ClothingFabricatorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class ClothingFabricatorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }
        [HarmonyPatch(typeof(SuitFabricatorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class SuitFabricatorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.FactoryBuildingTag);
            }
        }


        //-------------------------------------------------------------------
        [HarmonyPatch(typeof(WoodGasGeneratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class WoodGasGeneratorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GeneratorBuildingTag);
            }
        }

        [HarmonyPatch(typeof(HydrogenGeneratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class HydrogenGeneratorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GeneratorBuildingTag);
            }
        }

        [HarmonyPatch(typeof(ManualGeneratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class ManualGeneratorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GeneratorBuildingTag);
            }
        }

        [HarmonyPatch(typeof(MethaneGeneratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class MethaneGeneratorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GeneratorBuildingTag);
            }
        }

        [HarmonyPatch(typeof(PetroleumGeneratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class PetroleumGeneratorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GeneratorBuildingTag);
            }
        }

        [HarmonyPatch(typeof(GeneratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class GeneratorConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GeneratorBuildingTag);
            }
        }

        [HarmonyPatch(typeof(SteamTurbineConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class SteamTurbineConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.ProductionShop.IncludeRoom || Settings.Instance.RefinaryShop.IncludeRoom)
                    go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GeneratorBuildingTag);
            }
        }
    }
}
