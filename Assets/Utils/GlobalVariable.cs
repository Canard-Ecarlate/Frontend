using Models;
using Controlers;

namespace Utils
{
    static class GlobalVariable
    {
        public readonly static User CurrentUser = new User();
        public readonly static string URL = "http://canardecarlate.fr:3100/api/";
        public readonly static WebCommunicatorControler WebCommunicatorControler = new WebCommunicatorControler();
    }
}
