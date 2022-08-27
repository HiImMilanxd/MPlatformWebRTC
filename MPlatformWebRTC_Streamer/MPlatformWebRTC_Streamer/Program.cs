using System;
using System.Runtime.InteropServices;
using MPLATFORMLib;

namespace MPlatformWebRTC_Streamer
{
    internal class Program
    {

        const bool launchChrome = true; // niekedy to je otravne

        static void Main(string[] args) {
            MFileClass myFile = new MFileClass();
            bool stopRequest = false;
            MWebRTC_PluginClass webrtcObj = new MWebRTC_PluginClass();
            Random rnd = new Random();
            string pathToFile;
            string url = "https://rtc.medialooks.com:8889/Room410/Streamer210";
            pathToFile = "stream0.mp4";

            Console.WriteLine("Select stream (0 or 1)");           
            if (Console.ReadLine() == "1") 
                pathToFile = "stream1.mp4";

            myFile.FileNameSet(pathToFile, "loop=true");
                        
            webrtcObj.PropsSet("mode", "sender"); // set mode as a sender
            webrtcObj.Login(url, "", out _);

            Console.WriteLine(url);
            if (launchChrome)
                 System.Diagnostics.Process.Start("chrome", "\"" + url + "\"");

            myFile.FilePlayStart();
            myFile.PluginsAdd(webrtcObj, 10);
            Console.WriteLine("Playback started");

            while (!stopRequest)
            {
                Console.WriteLine("Select stream to switch (0 or 1; 2 to stop)");
                switch (Console.ReadLine())
                {
                    case "0":
                        switchSource(myFile, "stream0.mp4");
                        break;
                    case "1":
                        switchSource(myFile, "stream1.mp4");
                        break;
                    case "2":
                        stopRequest = true;
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }

            Console.WriteLine("Finished Playback");
            webrtcObj.Logout();
            Marshal.ReleaseComObject(webrtcObj);
            webrtcObj = null;
        }

        static void switchSource(MFileClass currentlyPlaying, string pathToFile)
        {
            currentlyPlaying.FilePlayStop(0);
            currentlyPlaying.FileNameSet(pathToFile, "loop=true");
            currentlyPlaying.FilePlayStart();
            Console.WriteLine("Switched Playback");

        }
    }
}
