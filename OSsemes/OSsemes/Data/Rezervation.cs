using OSsemes.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSsemes.Data
{
    public class Rezervation
    {
        [Key]
        public long Id { get; set; }
        public int RoomNumber { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int NumberGuest { get; set; }

        [Column(TypeName = "nvarchar(450)")]
        public string GuestId { get; set; }             //id Usera

        [ForeignKey("Room")]
        public string IDroom { get; set; }                  //id miestnosti

        public IdentityUserOwn Guest { get; set; }

        public Room Room { get; set; }


        //public void setToDef()
        //{
        //    Id = 0;
        //    RoomNumber = 0;
        //    ArrivalDate = DateTime.Now;
        //    DepartureDate = DateTime.Now;
        //    NumberGuest = 0;
        //    GuestId = "";
        //    Guest = new IdentityUserOwn();
        //}

        public void setFromReservation(Rezervation res)
        {
            Id = res.Id;
            RoomNumber = res.RoomNumber;
            ArrivalDate = res.ArrivalDate;
            DepartureDate = res.DepartureDate;
            NumberGuest = res.NumberGuest;
            GuestId = res.GuestId;
            Guest = res.Guest;
        }
    }


}
