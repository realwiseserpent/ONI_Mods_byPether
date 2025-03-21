﻿using HarmonyLib;
using UnityEngine;

namespace MultiplayerStorage
{
    class ModInfo : KMod.UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Debug.Log($"{GetType().Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{GetType().Namespace}: Mod version: {this.mod.packagedModInfo.version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");
        }
    }
}
