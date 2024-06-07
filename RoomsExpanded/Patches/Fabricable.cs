using HarmonyLib;
using Database;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class Fabricable : KMonoBehaviour
    {
        static StatusItem ProductionShopWorkEfficiencyBonus;

        static Fabricable()
        {
            ProductionShopWorkEfficiencyBonus = new StatusItem("ProductionShopWorkEfficiencyBonus", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID);
            ProductionShopWorkEfficiencyBonus.resolveStringCallback = (Func<string, object, string>)((str, data) =>
            {
                string str3 = string.Format((string)"{0} TEMP2 SHIT2 SHIT2 SHIT2 SHIT2", (object)GameUtil.AddPositiveSign(GameUtil.GetFormattedPercent(10f), true));
                return string.Format(str, (object)str3);
            });
        }

        bool currentlyInProductionShop;
        protected Guid productionShopEfficiencyBonusStatusItemHandle;
        private static readonly EventSystem.IntraObjectHandler<Fabricable> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<Fabricable>((Action<Fabricable, object>)((component, data) => component.OnUpdateRoom(data)));

        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.Subscribe<Fabricable>(144050788, Fabricable.OnUpdateRoomDelegate);
        }

        private void OnUpdateRoom(object data)
        {
            var worker = this.gameObject?.GetComponent<ComplexFabricator>()?.GetComponent<ComplexFabricatorWorkable>()?.worker;

            Debug.Log($"RoomsExpanded OnUpdateRoom gameObject: {gameObject} Fabricable {gameObject?.GetComponent<Workable>()} worker {worker}");

            Room room = (Room)data;
            if (room != null && room.roomType.Id == RoomTypeProductionShopData.RoomId)
            {
                this.currentlyInProductionShop = true;

                if (!(this.productionShopEfficiencyBonusStatusItemHandle == Guid.Empty))
                    return;
                this.productionShopEfficiencyBonusStatusItemHandle = gameObject.GetComponent<KSelectable>().AddStatusItem(ProductionShopWorkEfficiencyBonus, (object)this);
            }
            else
            {
                this.currentlyInProductionShop = false;
                if (!(this.productionShopEfficiencyBonusStatusItemHandle != Guid.Empty))
                    return;
                this.productionShopEfficiencyBonusStatusItemHandle = gameObject.GetComponent<KSelectable>().RemoveStatusItem(this.productionShopEfficiencyBonusStatusItemHandle);
            }
        }


        [HarmonyPatch(typeof(ComplexFabricatorWorkable))]
        [HarmonyPatch("OnWorkTick")]
        public static class ComplexFabricatorWorkable_OnWorkTick_Patch
        {
            public static void Postfix(ref ComplexFabricatorWorkable __instance, float dt)
            {
                if (!Settings.Instance.ProductionShop.IncludeRoom) return;

                if (RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeProductionShopData.RoomId))
                {
                    __instance.WorkTimeRemaining -= dt * Settings.Instance.ProductionShop.Bonus;

                    Debug.Log($"RoomsExpanded OnWorkTick dt {dt} __instance: {__instance} ");
                }
            }
        }

        //[HarmonyPatch(typeof(ComplexFabricatorWorkable))]
        //[HarmonyPatch("StopWork")]
        //public static class ComplexFabricatorWorkable_StopWork_Patch
        //{
        //    public static void Postfix(ref ComplexFabricatorWorkable __instance, Worker workerToStop)
        //    {
        //        var temp = __instance.gameObject.GetComponent<Fabricable>();
        //        if (temp != null && temp.productionShopEfficiencyBonusStatusItemHandle != Guid.Empty)
        //            temp.productionShopEfficiencyBonusStatusItemHandle = workerToStop.GetComponent<KSelectable>().RemoveStatusItem(ProductionShopWorkEfficiencyBonus);

        //        Debug.Log($"RoomsExpanded StopWork worker {workerToStop} w.gameObject: {__instance.gameObject} Fabricable {temp} guid {temp?.productionShopEfficiencyBonusStatusItemHandle}");
        //    }
        //}

        [HarmonyPatch(typeof(Workable))]
        [HarmonyPatch("GetEfficiencyMultiplier")]
        public static class ComplexFabricatorWorkable_GetEfficiencyMultiplier_Patch
        {
            public static void Postfix(ref float __result, ref Workable __instance)
            {
                var temp = __instance?.gameObject?.GetComponent<Fabricable>();
                if (temp != null && temp.currentlyInProductionShop)
                    __result += Settings.Instance.ProductionShop.Bonus;

                Debug.Log($"RoomsExpanded GetEfficiencyMultiplier __result {__result} ComplexFabricatorWorkable: {__instance} w.gameobject {__instance?.gameObject} Fabricable {temp}");
            }
        }
    }
}
