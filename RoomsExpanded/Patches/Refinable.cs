using HarmonyLib;
using STRINGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsExpanded
{
    public delegate void OnUpdateRoom(object data);

    class Refinable : KMonoBehaviour
    {
        static StatusItem RefinaryShopWorkEfficiencyBonus;

        static Refinable()
        {
            RefinaryShopWorkEfficiencyBonus = new StatusItem("RefinableWorkEfficiencyBonus", "DUPLICANTS", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID);
            RefinaryShopWorkEfficiencyBonus.resolveStringCallback = (Func<string, object, string>)((str, data) =>
            {
                string str3 = string.Format((string)"{0} TEMP SHIT SHIT SHIT SHIT", (object)GameUtil.AddPositiveSign(GameUtil.GetFormattedPercent(10f), true));
                return string.Format(str, (object)str3);
            });
        }

        protected Guid RefinaryShopEfficiencyBonusStatusItemHandle;
        public static readonly EventSystem.IntraObjectHandler<Refinable> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<Refinable>((Action<Refinable, object>)((component, data) => component.OnUpdateRoom(data)));

        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.Subscribe<Refinable>(144050788, Refinable.OnUpdateRoomDelegate);
        }

        void OnUpdateRoom(object data)
        {
            if ((UnityEngine.Object)this.gameObject == (UnityEngine.Object)null)
                return;

            ElementConverter elementConverter = this.gameObject.GetComponent<ElementConverter>();
            Room room = (Room)data;
            if (room != null && elementConverter != null && room.roomType.Id == RoomTypeRefinaryShopData.RoomId)
            {
                for (int i = 0; i < elementConverter.consumedElements.Length; i++)
                    elementConverter.consumedElements[i].MassConsumptionRate *= (1 + Settings.Instance.RefinaryShop.Bonus);

                for (int i = 0; i < elementConverter.outputElements.Length; i++)
                    elementConverter.outputElements[i].massGenerationRate *= (1 + Settings.Instance.RefinaryShop.Bonus);

                if (!(this.RefinaryShopEfficiencyBonusStatusItemHandle == Guid.Empty))
                    return;
                this.RefinaryShopEfficiencyBonusStatusItemHandle = this.GetComponent<KSelectable>().AddStatusItem(RefinaryShopWorkEfficiencyBonus, (object)this);
            }
            else
            {
                for (int i = 0; i < elementConverter.consumedElements.Length; i++)
                    elementConverter.consumedElements[i].MassConsumptionRate /= (1 + Settings.Instance.RefinaryShop.Bonus);

                for (int i = 0; i < elementConverter.outputElements.Length; i++)
                    elementConverter.outputElements[i].massGenerationRate /= (1 + Settings.Instance.RefinaryShop.Bonus);

                if (!(this.RefinaryShopEfficiencyBonusStatusItemHandle != Guid.Empty))
                    return;
                this.RefinaryShopEfficiencyBonusStatusItemHandle = this.GetComponent<KSelectable>().RemoveStatusItem(this.RefinaryShopEfficiencyBonusStatusItemHandle);
            }
        }

    }
}
