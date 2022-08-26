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
            MWebRTC_PluginClass webrtcObj = new MWebRTC_PluginClass();
            Random rnd = new Random();
            string pathToFile; 
            Console.WriteLine("Select stream (0 or 1)");

            pathToFile = "stream0.mp4";
            if (Console.ReadLine() == "1") 
                pathToFile = "stream1.mp4";

            myFile.FileNameSet(pathToFile, "");

            String url = "https://rtc.medialooks.com:8889" + "/Room" + +rnd.Next(1000) + "/Streamer" + rnd.Next(1000);
            Console.WriteLine(url);
            
            webrtcObj.PropsSet("mode", "sender"); // set mode as a sender
            webrtcObj.Login(url, "", out _);
            
            if (launchChrome)
                 System.Diagnostics.Process.Start("chrome", "\"" + url + "\"");

            myFile.FilePlayStart();
            myFile.PluginsAdd(webrtcObj, 10);

            Console.WriteLine("Finished Playback");


            webrtcObj.Logout();
            Marshal.ReleaseComObject(webrtcObj);
            webrtcObj = null;
        }
    }
}
