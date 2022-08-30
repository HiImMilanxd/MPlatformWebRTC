using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MPLATFORMLib;

namespace MPlatformWebRTC_Streamer
{
    class ZOutputStream
    {
        public MWebRTCClass stream;
        ZInputFileStream currentlyPlaying;

        public ZOutputStream(ZInputFileStream startStream, string url)
        {
            stream = new MWebRTCClass();
            stream.PropsSet("mode", "sender");
            stream.Login(url, "", out _);

            currentlyPlaying = startStream; // Budeme potrebovať pri switchovaní
            currentlyPlaying.mFile.PluginsAdd(stream, 10);
        }

        public void switchStream(ZInputFileStream switchTo)
        {
            currentlyPlaying.mFile.PluginsRemove(stream);
            currentlyPlaying = switchTo;
            currentlyPlaying.mFile.PluginsAdd(stream, 10);
        }

        // Aby sme sa bezpečne odhlasili 
        ~ZOutputStream()
        {
            stream.Logout();
            Marshal.ReleaseComObject(stream);
        }
    }
}
