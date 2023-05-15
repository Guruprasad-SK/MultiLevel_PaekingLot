using ParkingLotSimulation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotEx
{
    internal class ParkingArea
    {
        List<ParkingLot> Building = new();
        List<Ticket> Tickets = new();
        public int _type { get; set; }

        int _ticketNo = 1;
        int UserTicketNo { get; set; }
        public ParkingArea()
        {
            Intialize();
            ParkingOperation();
        }
        private void Intialize()
        {
            Console.WriteLine("Enter number of Levels");
            int _numberOfLots = ValidateInput(Console.ReadLine());
            for (int i = 1; i <= _numberOfLots; i++)
            {
                Building.Add(new ParkingLot() { LevelId = i });
                Console.WriteLine("Parking Lot number " + i);
            }

            foreach (var floor in Building)
            {
                List<ParkingSlot> listOfParkingSlots = new List<ParkingSlot>();
                Console.WriteLine("Enter no of slots in level {0} ", floor.LevelId);

                Console.WriteLine("Enter number of slots for 2 wheeles vehicals");
                int _numberOfTwoWheelerSlots = ValidateInput(Console.ReadLine());

                Console.WriteLine("Enter number of slots for 4 wheeler vehical");
                int _numberOfFourWheelerSlots = ValidateInput(Console.ReadLine());

                Console.WriteLine("Enter number of  slots for Heavy  Vehicle");
                int _numberOfHeavyVehicalSlots = ValidateInput(Console.ReadLine());

                var totalAvailableSlots = (_numberOfTwoWheelerSlots + _numberOfFourWheelerSlots + _numberOfHeavyVehicalSlots);

                for (int i = 1; i <= totalAvailableSlots; i++)
                {
                    if (i <= _numberOfTwoWheelerSlots)
                    {
                        listOfParkingSlots.Add(new ParkingSlot { Id = (floor.LevelId * 100) + i, SlotType = VehicleType.TwoWheelerVehicles, IsAvailable = true });
                    }
                    else if (i <= _numberOfTwoWheelerSlots + _numberOfFourWheelerSlots)
                    {
                        listOfParkingSlots.Add(new ParkingSlot { Id = (floor.LevelId * 100) + i, SlotType = VehicleType.FourWheelerVehicles, IsAvailable = true });
                    }
                    else if (i <= totalAvailableSlots)
                    {
                        listOfParkingSlots.Add(new ParkingSlot { Id = (floor.LevelId * 100) + i, SlotType = VehicleType.HeavyVehicles, IsAvailable = true });
                    }
                }

                foreach (var level in Building)
                {
                    if (level.LevelId == floor.LevelId)
                    {
                        level.listOfParkingSlotsinlevels = listOfParkingSlots;
                    }
                }
            }
            Display();
        }

        void ParkingOperation()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1.Park your Vehicle\n2.Unpark your Vehicle\n3 Exit");
                Console.WriteLine("Enter the choice");

                int _choice = ValidateInput(Console.ReadLine());
                switch (_choice)
                {
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine("1.Two wheeler vehicle\n2Four wheeler Vehicle \n3Heavy vehicle");
                        ParkVehicalAndGenerateTicket();
                        break;

                    case 2:
                        Console.WriteLine("Enter Ticket no");
                        UnparkAndGenerateTicket();
                        break;
                    case 3:
                        Console.WriteLine("Thank you...have a nice day");
                        Console.ReadLine();
                        break;
                }
            }

        }
        void ParkVehicalAndGenerateTicket()
        {
            _type = ValidateInput(Console.ReadLine());

            switch (_type)
            {
                case 1:
                    Parking(VehicleType.TwoWheelerVehicles);
                    break;
                case 2:
                    Parking(VehicleType.FourWheelerVehicles);
                    break;
                case 3:
                    Parking(VehicleType.HeavyVehicles);
                    break;
                default:
                    Console.WriteLine("enter valid choice");
                    break;
            }
        }

        private void Parking(VehicleType type)
        {

            var emptySlot = ParkVehicleAtAvailableSlot(type);
            if (emptySlot != null)
            {
                GenerateInTicket(emptySlot.Id);
            }
            else
            {
                Console.WriteLine("No parking slots are available, Please come back later");
            }
            Display();
        }
        private ParkingSlot ParkVehicleAtAvailableSlot(VehicleType vehicleType)
        {
            foreach (var floor in Building)
            {
                var emptySlot = floor.listOfParkingSlotsinlevels.FirstOrDefault(slot => slot.IsAvailable && slot.SlotType == vehicleType);
                if (emptySlot != null)
                {
                    emptySlot.IsAvailable = false;
                    return emptySlot;
                }
            }
           return null;
        }
        private void GenerateInTicket(int slotId)
        {
            Console.WriteLine("Enter Vehicle number");
            var vehicleNo = Console.ReadLine();

            var ticket = new Ticket()
            {
                InTime = DateTime.Now,
                SlotNo = slotId,
                TicketNo = _ticketNo,
                VehicalNo = vehicleNo,
            };

            Tickets.Add(ticket);

            Console.WriteLine("Vehicle Parked Successfully!");

            Console.WriteLine(ticket);
            Console.WriteLine();

            _ticketNo++;
        }
        void UnparkAndGenerateTicket()
        {
            UserTicketNo = ValidateInput(Console.ReadLine());
            var ticket = this.Tickets.Find(x => x.TicketNo == UserTicketNo);
            if (ticket == null)
            {
                Console.WriteLine("enter valid ticket number");
                UnparkAndGenerateTicket();
            }
            else
            {
                GenerateOutTicket(ticket.TicketNo);
                Display();
            }
        }
        private void GenerateOutTicket(int ticketId)
        {
            var ticket = Tickets.FirstOrDefault(ticket => ticket.TicketNo == ticketId);
            if (ticket != null)
            {
                foreach (var floor in Building)
                {
                    var slot = floor.listOfParkingSlotsinlevels.FirstOrDefault(slot => !slot.IsAvailable && slot.Id == ticket.SlotNo);
                    if (slot != null)
                    {
                        slot.IsAvailable = true;
                    }
                }
                ticket.OutTime = DateTime.Now;
                Console.WriteLine("Vehicle Unparked Succesfully!");
                Console.WriteLine(ticket);
                Console.WriteLine();
            }
        }
       int ValidateInput(string input)
        {
            int output;
            var isValid = int.TryParse(input, out output);
            if (isValid)
            {
                return output;
            }
            Console.WriteLine(" Enter Valid INPUT");
            var input2 = Console.ReadLine();
            return ValidateInput(input2);
        }
        void Display()
        {
            foreach (var n in Building)
            {
                Console.WriteLine();
                Console.WriteLine("Parking Level={0}", n.LevelId);
                foreach (var m in n.listOfParkingSlotsinlevels)
                {
                    Console.WriteLine("Slot No. - {0}, Vehicle Type - {1} is {2}", m.Id, m.SlotType, m.IsAvailable ? "Available" : "Occupied");
                }

            }

        }

    }
}
