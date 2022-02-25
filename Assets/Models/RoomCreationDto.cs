namespace Models
{
    public class RoomCreationDto
    {
        public string RoomName { get; set; } = "";
        public string ContainerId { get; set; } = "";
        public bool IsPrivate { get; set; }
        public int NbPlayers { get; set; }
    }
}
