using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    internal class RoomTypeProductionShopData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "ProductionShopRoom";
        public static readonly string BathroomTagName = "ProductionShop";

        public RoomTypeProductionShopData()
        {
            Id = RoomId;
            Name = "ProductionShopRoom";
            Tooltip = "ProductionShopRoom TOOLTIP";
            Effect = string.Format("ProductionShopRoom EFFECT {0}", MiscUtils.Percent(Settings.Instance.Bathroom.Bonus));
            Catergory = Db.Get().RoomTypeCategories.Industrial;
            ConstraintPrimary = RoomModdedConstraints.At_LEAST_TWO_FACTORY;
            ConstrantsAdditional = new RoomConstraints.Constraint[]
                                    {
                                    RoomModdedConstraints.NO_GENERATORS,
                                    RoomConstraints.LIGHT,
                                    RoomConstraints.MINIMUM_SIZE_12,
                                    RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Industrial.MaxSize)
                                    };

            RoomDetails = new RoomDetails.Detail[2]
                                {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.BUILDING_COUNT.NAME, (object) room.buildings.Count)))
                                };

            Priority = 0;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(11);//SortingCounter.PowerPlantSortKey);
        }

    }
}
