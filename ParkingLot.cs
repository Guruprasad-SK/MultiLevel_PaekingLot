using ParkingLotEx;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ParkingLotSimulation
{
    public class ParkingLot
    {
        public int LevelId { get; set; }

        public List<ParkingSlot> listOfParkingSlotsinlevels = new();
     
    }
}

