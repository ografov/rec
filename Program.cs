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
            string recPath = GetRecordingsPath(@"c:\decisions\Recordings\");
            string fileName = args.Length > 0 && !String.IsNullOrWhiteSpace(args[0]) ? args[0] + ".wma" : $"{DateTime.Now:yyyy-MMM-dd-hhmm}.wma";
            string filePath = Path.Combine(recPath, fileName);

            Console.WriteLine("Starting recording to " + filePath);
            AudioRecorder.RecordToWma(filePath);
        }

        private static string GetRecordingsPath(string suggestedPath)
        {
            if (String.IsNullOrEmpty(suggestedPath))
            {
                const string defaultRecPath = @".\Recordings\";
                if (!Directory.Exists(defaultRecPath))
                    Directory.CreateDirectory(defaultRecPath);
                return defaultRecPath;
            }

            if (!Directory.Exists(suggestedPath))
                Directory.CreateDirectory(suggestedPath);
            return suggestedPath;
        }
    }
}
