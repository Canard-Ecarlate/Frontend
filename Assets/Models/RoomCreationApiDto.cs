namespace Models
{
    public class RoomCreationApiDto
    {
        public string RoomName { get; set; }

        public RoomCreationApiDto(string roomName)
        {
            RoomName = roomName;
        }
    }
}