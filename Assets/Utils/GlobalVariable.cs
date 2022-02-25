using System.Collections.Generic;
using Models;
using Controlers;

namespace Utils
{
    static class GlobalVariable
    {
        public static readonly User User = new User();
        public static readonly RoomDto Room = new RoomDto();
        public static readonly List<PlayerInWaitingRoomDto> Players = new List<PlayerInWaitingRoomDto>();
        public static readonly GameDto Game = new GameDto();
        public static readonly WebCommunicatorControler WebCommunicatorControler = new WebCommunicatorControler();
    }
}
