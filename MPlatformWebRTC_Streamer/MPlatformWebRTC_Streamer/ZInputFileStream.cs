using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MPLATFORMLib;

namespace MPlatformWebRTC_Streamer
{
    class ZInputFileStream
    {
        public MFileClass mFile;
        public MWebRTCClass stream;

        public ZInputFileStream(string path, string url)
        {
            mFile = new MFileClass();
            mFile.FileNameSet(path, "loop=true");
            mFile.FilePlayStart();

            stream = new MWebRTCClass();
            stream.PropsSet("mode", "sender");
            stream.Login(url, "", out _);

            mFile.PluginsAdd(stream, 10);
        }


        // Aby sme sa bezpečne odhlasili 
        ~ZInputFileStream()
        {
            stream.Logout();
            Marshal.ReleaseComObject(stream);
        }
    }
}
