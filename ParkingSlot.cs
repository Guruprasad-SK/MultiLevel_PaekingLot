using ParkingLotSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotSimulation
{
    public class ParkingSlot
    {
        public int Id { get; set; }
        public VehicleType SlotType { get; set; }

        public bool IsAvailable = true;

    }
    public enum VehicleType
    {
        TwoWheelerVehicles = 0,
        FourWheelerVehicles,
        HeavyVehicles
    }
   
}

