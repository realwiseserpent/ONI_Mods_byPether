﻿using TUNING;
using System.Collections.Generic;
using UnityEngine;
using TodoList;

namespace InterplanarInfrastructure
{
    class RadiationSateliteModuleConfig : IBuildingConfig
    {
        public const string ID = "RadiationSateliteModule";

        public override string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public override BuildingDef CreateBuildingDef()
        {
            Todo.Note("Adjust building materials and cost");
            Todo.Note("Provide final kanim");
            float[] hollowTieR1 = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER1;
            string[] refinedMetals = MATERIALS.REFINED_METALS;
            EffectorValues tieR2 = NOISE_POLLUTION.NOISY.TIER2;
            EffectorValues none = BUILDINGS.DECOR.NONE;
            EffectorValues noise = tieR2;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 3, 3, "rocket_pioneer_cargo_module_kanim", 1000, 30f, hollowTieR1, refinedMetals, 9999f, BuildLocationRule.Anywhere, none, noise);
            BuildingTemplates.CreateRocketBuildingDef(buildingDef);
            buildingDef.DefaultAnimState = "deployed";
            buildingDef.AttachmentSlotTag = GameTags.Rocket;
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
            buildingDef.OverheatTemperature = 2273.15f;
            buildingDef.Floodable = false;
            buildingDef.ObjectLayer = ObjectLayer.Building;
            buildingDef.RequiresPowerInput = false;
            buildingDef.CanMove = true;
            buildingDef.Cancellable = false;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
            go.AddOrGet<LoopingSounds>();
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery);
            Storage storage = go.AddComponent<Storage>();
            storage.showInUI = true;
            storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
            BuildingInternalConstructor.Def def1 = go.AddOrGetDef<BuildingInternalConstructor.Def>();
            def1.constructionMass = 400f;
            def1.outputIDs = new List<string>() { RadiationLenseSateliteConfig.ID };
            def1.spawnIntoStorage = true;
            def1.storage = (DefComponent<Storage>)storage;
            def1.constructionSymbol = "under_construction";
            go.AddOrGet<BuildingInternalConstructorWorkable>().SetWorkTime(30f);
            JettisonableCargoModule.Def def2 = go.AddOrGetDef<JettisonableCargoModule.Def>();
            def2.landerPrefabID = RadiationLenseSateliteConfig.ID.ToTag();
            def2.landerContainer = (DefComponent<Storage>)storage;
            def2.clusterMapFXPrefabID = "";// "DeployingPioneerLanderFX";
            go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[1]
            {
                new BuildingAttachPoint.HardPoint(new CellOffset(0, 3), GameTags.Rocket, (AttachableBuilding) null)
            };
            go.AddOrGet<NavTeleporter>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            Prioritizable.AddRef(go);
            BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, (string)null, ROCKETRY.BURDEN.MODERATE);
            FakeFloorAdder fakeFloorAdder = go.AddOrGet<FakeFloorAdder>();
            fakeFloorAdder.floorOffsets = new CellOffset[3]
            {
              new CellOffset(-1, -1),
              new CellOffset(0, -1),
              new CellOffset(1, -1)
            };
            fakeFloorAdder.initiallyActive = false;
        }
    }
}
