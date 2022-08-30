using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MPLATFORMLib;

namespace MPlatformWebRTC_Streamer
{
    internal class Program
    {
        static ZInputFileStream file0 = new ZInputFileStream("stream0.mp4", "https://rtc.medialooks.com:8889/Room410/Streamer1");
        static ZInputFileStream file1 = new ZInputFileStream("stream1.mp4", "https://rtc.medialooks.com:8889/Room410/Streamer2");
        static ZOutputStream masterOutput = new ZOutputStream(file0, "https://rtc.medialooks.com:8889/Room410/Streamer0");
        static ZCommunicationStream communicationStream = new ZCommunicationStream("https://rtc.medialooks.com:8889/Room410/Streamer999");

        static void Main(string[] args) {

            communicationStream.stream.OnEventSafe += masterStream_OnEventSafe;

            Console.WriteLine("Started playback - Press any key to stop");
            Console.ReadKey();
            Console.WriteLine("Stopped");
        }


        private static void masterStream_OnEventSafe(string bsChannelID, string bsEventName, string bsEventParam, object pEventObject)
        {
            if (bsEventName == "message")
            {
                if (bsEventParam == "0")
                    masterOutput.switchStream(file0);
                else
                    masterOutput.switchStream(file1);
            }
        }
    }
}
