
namespace OSsemes.Data
{
    public class Room
    {
        public string RoomName { get; set; }
        public string[] Describe { get; set; }
        public string[] Furnishing { get; set; }
        public string[] Bathroom { get; set; }
        public string[] Services { get; set; }
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
