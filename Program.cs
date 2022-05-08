using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WavHelper
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        const int ChannelLoc = 22;
        static int default_interlace_sample_count = 57344;

        public static string deinterlaceFromPS2Stereo(string filename, string saveToFileName)
        {
            return deinterlaceFromPS2Stereo(filename,  saveToFileName, default_interlace_sample_count);
        }

        public static string deinterlaceFromPS2Stereo(string filename, string saveToFileName, int interlace_sample_count)
        {
            DateTime start = DateTime.Now;
            byte[] data = File.ReadAllBytes(filename);
            WavInfo info = new WavInfo(data);
            Console.WriteLine("working on '{0}'; DataSize:{1:N0}; chunk_sz:{2}; chunks:{3}",
                filename, info.DataSize, interlace_sample_count, info.DataSize / interlace_sample_count);
            byte[] stereoData = new byte[info.DataSize];
            int chunk1_start = (int)info.DataStart;
            int chunk2_start = chunk1_start + interlace_sample_count;
            int p1 = 0;
            int p2 = 0;
            bool done = false;
            Console.WriteLine("p1:{0:N0}; c1_s:{1:N0}-{2:N0}; c2_s:{3:N0}-{4:N0}",
                     p1, chunk1_start,chunk1_start+interlace_sample_count, chunk2_start, chunk2_start+interlace_sample_count);
            int double_chunk = 2 * interlace_sample_count;
            try
            {
                while (!done)
                {
                    stereoData[p1] = data[chunk1_start + p2];
                    stereoData[p1 + 1] = data[chunk1_start + p2 + 1];
                    stereoData[p1 + 2] = data[chunk2_start + p2];
                    stereoData[p1 + 3] = data[chunk2_start + p2 + 1];
                    p2 += 2;
                    if (p1 > 0 && p1 % double_chunk == 0)
                    {
                        chunk1_start += (2 * interlace_sample_count);
                        chunk2_start = chunk1_start + interlace_sample_count;
                        //Console.WriteLine("p1:{0:N0}; c1_s:{1:N0}-{2:N0}; c2_s:{3:N0}-{4:N0}",
                        // p1, chunk1_start, chunk1_start + interlace_sample_count, chunk2_start, chunk2_start + interlace_sample_count);
                        p2 = 0;
                    }
                    p1 += 4;
                    if (p1 >= stereoData.Length)
                    {
                        break;
                    }
                }
            }
            catch (Exception) { Console.WriteLine("Oops, screwed up the termination case."); }
            /*List<byte> retVal = GetHeader(data);
            retVal[ChannelLoc] = 2;

            // calc block align 
            //32, 2, BlockAlign= NumChannels * BitsPerSample/8
            UInt16 blockAlign = (UInt16)(2 * info.BitsPerSample / 8);
            byte[] blkAlign = BitConverter.GetBytes(blockAlign);
            retVal[32] = blkAlign[0];
            retVal[33] = blkAlign[1];

            retVal.AddRange(stereoData);*/

            WavHeader hdr = new WavHeader((ushort)2, (UInt32)stereoData.Length, (UInt32)info.SampleRate);
            List<byte> retVal = new List<byte>(hdr.ToByteArray());
            retVal.AddRange(stereoData);

            TimeSpan ts = DateTime.Now - start;
            Console.WriteLine("Time: {0}ms", ts.TotalMilliseconds);
            string fn = saveToFileName;
            int i = 0;
            while (File.Exists(fn))
                fn = saveToFileName.Replace(".wav", "_" + ++i + ".wav");
            byte[] outData = retVal.ToArray();
            WavInfo info2 = new WavInfo(outData);
            File.WriteAllBytes(fn, outData);
            Console.WriteLine("Saved to {0}", fn);
            return fn;
        }

        /// <summary>
        /// do not use, not quite right yet
        /// </summary>
        public static void deinterlaceXboxPcAudio(string filename, string frontFileName, string backFileName, int interleaveSampleCount)
        {
            DateTime start = DateTime.Now;
            byte[] data = File.ReadAllBytes(filename);
            WavInfo info = new WavInfo(data);
            int chunks = (int)(info.DataSize / interleaveSampleCount);
            Console.WriteLine("working on '{0}'; DataSize:{1:N0}; chunk_sz:{2}; chunks:{3}",
                filename, info.DataSize, interleaveSampleCount, chunks);
            List<byte> front = new List<byte>((int)(info.DataSize / 2));
            List<byte> back = new List<byte>((int)(info.DataSize / 2));
            byte[] chunk_holder = new byte[interleaveSampleCount];
            try
            {
                long startLoc = info.DataStart;
                for (int i = 0; i < chunks; i++)
                {
                    startLoc = info.DataStart + i * interleaveSampleCount;                    
                    Array.Copy(data, startLoc, chunk_holder, 0, chunk_holder.Length);
                    if (i % 2 == 0)
                        front.AddRange(chunk_holder);
                    else
                        back.AddRange(chunk_holder);
                    Array.Clear(chunk_holder, 0, chunk_holder.Length);
                }
            }
            catch (Exception ex) { 
                Console.WriteLine("Exception while processing file {0}\n{1}", filename, ex.Message);
            }
            
            //List<byte> frontData = new List<byte>(WavInfo.MakeWavHeader_ffmpeg(2, (uint)info.SampleRate, (uint)front.Count));
            WavHeader frntHeader = new WavHeader((ushort)2, (uint)front.Count, (uint)info.SampleRate);
            List<byte> frontData = new List<byte>(frntHeader.ToByteArray());
            frontData.AddRange(front.ToArray());

            //List<byte> backData = new List<byte>(WavInfo.MakeWavHeader_ffmpeg(2, (uint)info.SampleRate, (uint)back.Count));
            WavHeader bckHeader = new WavHeader((ushort)2, (uint)back.Count, (uint)info.SampleRate);
            List<byte> backData = new List<byte>(bckHeader.ToByteArray());
            backData.AddRange(back.ToArray());

            TimeSpan ts = DateTime.Now - start;
            Console.WriteLine("Time: {0}ms", ts.TotalMilliseconds);
            string fn = frontFileName;
            int j = 0;
            while (File.Exists(fn))
                fn = frontFileName.Replace(".wav", "_" + ++j + ".wav");
            byte[] outData = frontData.ToArray();
            File.WriteAllBytes(fn, outData);
            Console.WriteLine("Saved front to {0}", fn);
            WavInfo fnt = new WavInfo(outData);


            fn = backFileName;
            j = 0;
            while (File.Exists(fn))
                fn = backFileName.Replace(".wav", "_" + ++j + ".wav");
            outData = frontData.ToArray();
            File.WriteAllBytes(fn, outData);
            Console.WriteLine("Saved back to {0}", fn);
        }

        public static List<byte> GetHeader(byte[] data)
        {
            WavInfo info = new WavInfo(data);
            List<byte> retVal = new List<byte>((int)info.DataStart);
            for (int i = 0; i < info.DataStart; i++)
                retVal.Add(data[i]);
            return retVal;
        }

        public static string RunCommandAndGetOutput(string programName, string args, bool includeStdErr)
        {
            Console.WriteLine("Running command: " + programName + " " + args);
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            var process = Process.Start(processStartInfo);
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (includeStdErr)
                output = output + "\r\n" + err;
            return output;
        }

        public static string RunCommandAndGetOutput(string programName, string args, bool includeStdErr, string killString)
        {
            sKillString = killString;
            Console.WriteLine("Running command: " + programName + " " + args);
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            Process process = new Process(); //Process.Start(processStartInfo);
            process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.Start();
            process.BeginErrorReadLine();
            process.WaitForExit();

            string output = sProcessOutout;
            sProcessOutout = "";
            sKillString = null;
            return output;
        }
        static string sProcessOutout = "";
        static string sKillString = null;

        static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            sProcessOutout += e.Data;
            if (String.IsNullOrEmpty(sKillString) && e.Data.Contains(sKillString))
            {
                p.Kill();
                sProcessOutout += "\nProcess killed";
            }
        }

        public static Process RunCommand(string programName, string args)
        {
            Console.WriteLine("Running command: " + programName + " " + args);
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            Process process = Process.Start(processStartInfo);
            return process;
        }

    }

    public class SoundReference : IComparable<SoundReference>
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string FileName { get; set; }
        public int    LineNumber { get; set; }
        public string LineBefore { get; set; }
        public string Line { get; set; }
        public string LineAfter { get; set; }

        public int CompareTo(SoundReference other)
        {
            return Name.CompareTo(other.Name);
        }

        public string GetInfo()
        {
            string retVal = 
            "Name:" + Name + "\r\n" +
            "Alias:" + Alias + "\r\n" +
            "FileName:" + FileName + "\r\n" +
            //"LineNumber:" + LineNumber + "\r\n" +
            "LineBefore:" + LineBefore + "\r\n" +
            "Line:" + Line + "\r\n" +
            "LineAfter:" + LineAfter + "\r\n";
            return retVal;
        }
    }

    public class WavInfo
    {
        // References:
        //https://onestepcode.com/read-wav-header/
        //http://soundfile.sapp.org/doc/WaveFormat/  (this one seems specific to output of the 'sox' program)
        //http://www-mmsp.ece.mcgill.ca/Documents/AudioFormats/WAVE/WAVE.html

        //  name; // offset, size, desc 
        public string ChunkId;   //0,4  Should be "RIFF"


        public UInt32 ChunkSize;       //4,4
        //This is the size of the rest of the chunk 
        //following this number.  This is the size of the 
        //entire file in bytes minus 8 bytes for the
        //two fields not included in this count:
        //ChunkID and ChunkSize.

        public string Format;       //8,4, should be "WAVE"
        public string Subchunk1ID;  // 12, 4 , should be "fmt "
        public UInt32 Subchunk1Size;   //16, 4, 16 for PCM
        public UInt16 AudioFormat;     //20, 2, PCM=1 
        public UInt16 NumChannels;     //22, 2, Mono = 1, Stereo = 2
        public UInt32 SampleRate;      //24, 4 , 8000, 44100, etc.
        public UInt32 ByteRate;        //28, 4, == SampleRate * NumChannels * BitsPerSample/8
        public UInt16 BlockAlign;      //32, 2, == NumChannels * BitsPerSample/8
        //The number of bytes for one sample including
        //all channels.

        public UInt16 BitsPerSample;    //34, 2, 8 bits = 8, 16 bits = 16, etc.
        public string Subchunk2ID;   //36, 4, Contains the letters "data"
        public UInt32 Subchunk2Size; //40, 4,  == NumSamples * NumChannels * BitsPerSample/8
        //This is the number of bytes in the data.
        //You can also think of this as the size
        //of the read of the subchunk following this 
        //number.

        public UInt32 DataStart;   //often 44, where The actual sound data begins.
        public UInt32 NumSamples;
        public UInt32 DataSize;

        public WavInfo() { }

        /// <summary>
        /// Constructs a 'WavInfo' from binary data.
        /// </summary>
        /// <param name="data">A byte array of the wav file </param>
        public WavInfo(byte[] data)
        {
            int pos = 12; // move past initial  'RIFFF ... WAVE' portion 
            // Keep iterating until we find the 'data' chunk (i.e. 64 61 74 61 == 'data' )
            int limit = data.Length < 200? data.Length : 200;
            while (!(data[pos] == 0x64 && data[pos + 1] == 0x61 && data[pos + 2] == 0x74 && data[pos + 3] == 0x61) && pos < limit)
            {
                pos++;
            }
            if (pos == limit)
                throw new InvalidEnumArgumentException("'data' is not wav data");

            pos += 4; // advance to 'dataSize'
            UInt32 dataSize = BitConverter.ToUInt32(data, pos);
            pos += 4; // advance to 'data'
            // Pos is now positioned to start of actual sound data.
            NumChannels = data[22];

            UInt32 samples = (UInt32) dataSize / 2; // 2 bytes per sample (16 bit sound mono)
            if (NumChannels == 2) samples /= 2;

            ChunkId = StringFormByteArray(data, 0, 4);
            ChunkSize = BitConverter.ToUInt32(data, 4);
            Format = StringFormByteArray(data, 8, 4);
            Subchunk1ID = StringFormByteArray(data, 12, 4);
            Subchunk1Size = BitConverter.ToUInt32(data, 16);
            AudioFormat = data[20];
            //NumChannels = data[22]; // moved above 
            SampleRate = BitConverter.ToUInt32(data, 24);
            ByteRate = BitConverter.ToUInt32(data, 28);
            BlockAlign = BitConverter.ToUInt16(data, 32);
            BitsPerSample = BitConverter.ToUInt16(data, 34);
            Subchunk2ID = StringFormByteArray(data, 36, 4);
            Subchunk2Size = BitConverter.ToUInt32(data, 40);
            DataStart = (UInt32)pos;
            DataSize = (UInt32)dataSize;
            NumSamples = (UInt32)samples;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ChunkId: " + ChunkId + "\n");
            sb.Append("ChunkSize: " + ChunkSize + "\n");
            sb.Append("Format: " + Format + "\n");
            sb.Append("Subchunk1ID: " + Subchunk1ID + "\n");
            sb.Append("Subchunk1Size: " + Subchunk1Size + "\n");
            sb.Append("AudioFormat: " + AudioFormat + "\n");
            sb.Append("NumChannels: " + NumChannels + "\n");
            sb.Append("SampleRate: " + SampleRate + "\n");
            sb.Append("ByteRate: " + ByteRate + "\n");
            sb.Append("BlockAlign: " + BlockAlign + "\n");
            sb.Append("BitsPerSample: " + BitsPerSample + "\n");
            sb.Append("Subchunk2ID: " + Subchunk2ID + "\n");
            sb.Append("Subchunk2Size: " + Subchunk2Size + "\n");
            sb.Append("DataStart: " + DataStart + "\n");
            sb.Append("NumSamples (calculated): " + NumSamples + "\n");
            sb.Append("DataSize (calculated): " + DataSize + "\n");
            string retVal = sb.ToString();
            return retVal;
        }
        static string StringFormByteArray(byte[] source, int start, int length)
        {
            StringBuilder sb = new StringBuilder(length);
            char c;
            int limit = start + length;
            for (int i = start; i < limit; i++)
            {
                c = (char)source[i];
                sb.Append(c);
            }
            return sb.ToString();
        }
    }

    public struct WavHeader {
        public byte[] riff;           /* "RIFF"    4                             */
        public UInt32 file_length;    /* Size of the overall file - 8 bytes      */
        public byte[] wave;           /* "WAVE"    4                             */
        public byte[] fmt;            /* "fmt "    4                             */
        public UInt32 chunk_size;     /* size of FMT chunk in bytes (usually 16) */
        public ushort format_tag;     /* 1=PCM, 257=Mu-Law, 258=A-Law, 259=ADPCM */
        public ushort num_chans;      /* 1=mono, 2=stereo                        */
        public UInt32 sample_rate;    /* Sampling rate in samples per second     */
        public UInt32 bytes_per_sec;  /* bytes per second = sample_rate*bytes_per_samp */
        public ushort bytes_per_samp; /* 2=16-bit mono, 4=16-bit stereo          */
        public ushort bits_per_samp;  /* Number of bits per sample               */
        public byte[] data;           /* "data"     4                            */
        public UInt32 data_length;    /* data length in bytes (filelength - 44)  */

        public WavHeader(ushort channels, UInt32 dataLength, UInt32 sampleRate)
        {
            // total size =11* 4
            riff = new byte[]{0x52,0x49,0x46,0x46};
            file_length = dataLength +9*4 ;//? check this
            wave = new byte[] {0x57,0x41,0x56,0x45,};
            fmt = new byte[] {0x66,0x6d,0x74,0x20,};
            chunk_size = 16;
            format_tag = 1;
            num_chans = channels;
            sample_rate = sampleRate;
            bytes_per_samp = channels == 2?(ushort)4:(ushort)2;
            bytes_per_sec = sampleRate * bytes_per_samp;
            bits_per_samp = 16;
            data = new byte[]{0x64,0x61,0x74,0x61,};
            data_length = dataLength;
        }
        
      public byte[] ToByteArray()
      {
          List<byte> retVal = new List<byte>(40);
          retVal.AddRange(riff);
          retVal.AddRange(BitConverter.GetBytes(file_length));
          retVal.AddRange(wave);
          retVal.AddRange(fmt);
          retVal.AddRange(BitConverter.GetBytes(chunk_size));
          retVal.AddRange(BitConverter.GetBytes(format_tag));
          retVal.AddRange(BitConverter.GetBytes(num_chans));
          retVal.AddRange(BitConverter.GetBytes(sample_rate));
          retVal.AddRange(BitConverter.GetBytes(bytes_per_sec));
          retVal.AddRange(BitConverter.GetBytes(bytes_per_samp));
          retVal.AddRange(BitConverter.GetBytes(bits_per_samp));
          retVal.AddRange(data);
          retVal.AddRange(BitConverter.GetBytes(data_length));
          return retVal.ToArray();
      }
    }
}
