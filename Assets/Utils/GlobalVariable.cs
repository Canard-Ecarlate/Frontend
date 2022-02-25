using Models;
using Controlers;

namespace Utils
{
    static class GlobalVariable
    {
        public static readonly User CurrentUser = new User();
        public static readonly RoomDto CurrentRoom = new RoomDto();
        public static readonly string URL = "http://canardecarlate.fr:3100/api/";
        public static readonly WebCommunicatorControler WebCommunicatorControler = new WebCommunicatorControler();
    }
}
