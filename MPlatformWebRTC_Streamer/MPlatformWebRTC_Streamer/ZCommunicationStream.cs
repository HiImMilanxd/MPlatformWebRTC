using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MPLATFORMLib;

namespace MPlatformWebRTC_Streamer
{
    class ZCommunicationStream
    {
        public MWebRTCClass stream;


        public ZCommunicationStream(string url)
        {
            stream = new MWebRTCClass();
            stream.Login(url, "", out _);
        }


        // Aby sme sa bezpečne odhlasili 
        ~ZCommunicationStream()
        {
            stream.Logout();
            Marshal.ReleaseComObject(stream);
        }

    }
}
