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

        public IdentityUserOwn Guest { get; set; }

    }
}
