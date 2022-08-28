using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MPLATFORMLib;

namespace MPlatformWebRTC_Streamer
{
    internal class Program
    {

        static MWebRTCClass masterStream = new MWebRTCClass();
        static List<MWebRTCClass> sourceStreams = new List<MWebRTCClass>();
        static List<MFileClass> files = new List<MFileClass>();

        const string BasePreviewURL = "https://rtc.medialooks.com:8889/Room410/Streamer"; // Stream 0 je master; stream 1 ... n sú preview
        static int loopIterator = 1;
        static MFileClass currentStream;

        static void Main(string[] args) {

            // foreach file in current directory; use for loop

            foreach (string file in System.IO.Directory.GetFiles(System.Environment.CurrentDirectory)) {
                if (file.EndsWith(".mp4")) { // pretože mame aj .exe súbory
                    Console.WriteLine("Found file: " + file);
                    // create stream for each file

                    MFileClass mFile = new MFileClass();
                    mFile.FileNameSet(file, "loop=true");
                    mFile.FilePlayStart();
                    
                    MWebRTCClass stream = new MWebRTCClass();
                    stream.PropsSet("mode", "sender");
                    stream.Login(BasePreviewURL + loopIterator.ToString(), "", out _);

                    mFile.PluginsAdd(stream, 10);

                    sourceStreams.Add(stream);
                    files.Add(mFile);

                    loopIterator++;
                }
            }
            
            // create master stream
            masterStream.PropsSet("mode", "sender");
            masterStream.OnEventSafe += masterStream_OnEventSafe; // Master bude zároven aj komunikacný kanal
            masterStream.Login(BasePreviewURL + "0", "", out _);

            currentStream = files[0];
            currentStream.PluginsAdd(masterStream, 10);

            Console.WriteLine("Started playback - Press any key to stop");
            Console.ReadKey();  
            Console.WriteLine("Stopped");

            // make sure that every stream is properly disposed
            foreach (MWebRTCClass stream in sourceStreams)
            {
                stream.Logout(); 
                Marshal.ReleaseComObject(stream);
            }            
        }


        private static void masterStream_OnEventSafe(string bsChannelID, string bsEventName, string bsEventParam, object pEventObject)
        {
            if (bsEventName == "message")
            {
                currentStream.PluginsRemove(masterStream);
                currentStream = files[int.Parse(bsEventParam)];
                Console.WriteLine("Switched to " + bsEventParam);
                currentStream.PluginsAdd(masterStream, 10);

            }
        }
    }
}
