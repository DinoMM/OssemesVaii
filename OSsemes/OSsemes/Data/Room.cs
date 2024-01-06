
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSsemes.Data
{
    public class Room
    {
        [Key]
        public string RoomName { get; set; }
        [NotMapped]
        public string[] Describe { get; set; }
        [NotMapped]
        public string[] Furnishing { get; set; }
        [NotMapped]
        public string[] Bathroom { get; set; }
        [NotMapped]
        public string[] Services { get; set; }
        [NotMapped]
        public string[] Photos { get; set; }

        public int MaxNumberOfGuest { get; set; }
        public double Cost { get; set; }       //cena za den

        public void setFromOtherRoom(Room room) {
            RoomName = room.RoomName;
            Describe = room.Describe;
            Furnishing = room.Furnishing;
            Bathroom = room.Bathroom;
            Services = room.Services;
            Photos = room.Photos;
            MaxNumberOfGuest = room.MaxNumberOfGuest;
            Cost = room.Cost;
        }

    }
}
