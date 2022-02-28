namespace Models
{
    public class UserAndRoomDto
    {
        public string RoomCode { get; set; }

        public UserAndRoomDto(string roomCode)
        {
            RoomCode = roomCode;
        }
    }
}