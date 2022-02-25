namespace Models
{
    public class PlayerInWaitingRoomDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public bool Ready { get; set; }
    }
}