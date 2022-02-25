namespace Models
{
    public class RoomDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public string HostId { get; set; } = "";
        public string HostName { get; set; } = "";
        public RoomConfiguration RoomConfiguration { get; set; }
        public bool IsPlaying { get; set; }

        public void SetRoom(RoomDto roomDto)
        {
            Id = roomDto.Id;
            Name = roomDto.Name;
            Code = roomDto.Code;
            HostId = roomDto.HostId;
            HostName = roomDto.HostName;
            RoomConfiguration = roomDto.RoomConfiguration;
            IsPlaying = roomDto.IsPlaying;
        }
    }
}