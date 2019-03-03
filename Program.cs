using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.MediaFoundation;
using CSCore.SoundIn;
using CSCore.Streams;

namespace Rec
{
    class Program
    {
        static void Main(string[] args)
        {
            string location = @"D:\Decisions\Recordings\";
            string fileName = Path.Combine(location, $"{DateTime.Now.ToShortDateString()}-{DateTime.Now.ToString("hhmm")}.wma");
            Console.WriteLine("Starting recording to " + fileName);
            AudioRecorder.RecordToWma(fileName);
        }
    }
}
