using System;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.MediaFoundation;
using CSCore.SoundIn;
using CSCore.Streams;

namespace Rec
{
    internal class AudioRecorder
    {
        internal static void RecordToWav(string fileName)
        {
            using (WasapiCapture capture = new WasapiLoopbackCapture())
            {
                //if nessesary, you can choose a device here
                //to do so, simply set the device property of the capture to any MMDevice
                //to choose a device, take a look at the sample here: http://cscore.codeplex.com/

                //initialize the selected device for recording
                capture.Initialize();

                //create a wavewriter to write the data to
                using (WaveWriter w = new WaveWriter(fileName, capture.WaveFormat))
                {
                    //setup an eventhandler to receive the recorded data
                    capture.DataAvailable += (s, e) =>
                    {
                        //save the recorded audio
                        w.Write(e.Data, e.Offset, e.ByteCount);
                        Console.Write(".");
                    };

                    //start recording
                    capture.Start();

                    Console.ReadKey();

                    //stop recording
                    capture.Stop();
                }
            }
        }

        internal static void RecordToWma(string fileName)
        {
            using (var wasapiCapture = new WasapiLoopbackCapture())
            {
                wasapiCapture.Initialize();
                var wasapiCaptureSource = new SoundInSource(wasapiCapture);
                using (var stereoSource = wasapiCaptureSource.ToStereo())
                {
                    using (var writer = MediaFoundationEncoder.CreateWMAEncoder(stereoSource.WaveFormat, fileName))
                    {
                        byte[] buffer = new byte[stereoSource.WaveFormat.BytesPerSecond];
                        wasapiCaptureSource.DataAvailable += (s, e) =>
                        {
                            int read = stereoSource.Read(buffer, 0, buffer.Length);
                            writer.Write(buffer, 0, read);
                            Console.Write(".");
                        };

                        wasapiCapture.Start();

                        Console.ReadKey();

                        wasapiCapture.Stop();
                    }
                }
            }
        }
    }
}
