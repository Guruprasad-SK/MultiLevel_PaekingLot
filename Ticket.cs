using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotEx
{
    public class Ticket

    {
        public int TicketNo { get; set; }
        public string VehicalNo { get; set; }
        public int SlotNo { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public int Level { get; set; }

        public override string ToString()
        {
            var str =  "=> Ticket no=" + TicketNo + "\n=>Slot Number = " + SlotNo + "\n=>Vehical Number = " + VehicalNo + "\n=>Intime = " + InTime;
            if(OutTime != default)
            {
               str += "=> Out Time=" + OutTime;
            }

            return str;
        }
    }
}
