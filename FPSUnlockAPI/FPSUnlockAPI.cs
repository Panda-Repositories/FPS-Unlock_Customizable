using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPSUnlockAPI
{
    public class FPSUnlockAPI
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WaitNamedPipe(string name, int timeout);
        //function to check if the pipe exist

        private static bool FPSUnlockPipeExist(string pipeName)
        {
            try
            {
                if (!WaitNamedPipe($"\\\\.\\pipe\\{pipeName}", 0))
                {
                    int lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error == 0)
                    {
                        return false;
                    }
                    if (lastWin32Error == 2)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string fpspipe = "PandaFPSUnlock_Value";

        public void SendFPSValue(double value)
        {
            new Thread(() =>//lets run this in another thread so if roblox crash the ui/gui don't freeze or something
            {
                try
                {
                    using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", fpspipe, PipeDirection.Out))
                    {
                        namedPipeClientStream.Connect();
                        using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream, System.Text.Encoding.Default, 999999))//changed buffer to max 1mb since default buffer is 1kb
                        {
                            streamWriter.Write(value.ToString()); 
                            streamWriter.Dispose();
                        }
                        namedPipeClientStream.Dispose();
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Error occured connecting to the pipe.", "Connection Failed!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }).Start();
        }

        public void RunFPSUnlock(bool IsCustomURL = false, string CustomURL = "https://cdn.discordapp.com/attachments/903620122269319189/913227922934996992/rbxfpsunlocker.exe")
        {
            string execution_path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            if (!FPS_Process())
            {
                if (!Directory.Exists("bin"))
                {
                    Directory.CreateDirectory("bin");
                }
                if (File.Exists(execution_path + "\\bin\\rbxfpsunlocker.exe"))
                {
                    File.Delete(execution_path + "\\bin\\rbxfpsunlocker.exe");
                }
                if (IsCustomURL) //if set to true, the Custom Download Link must be putted on the CustomURL or else it will download the fps unlock from ours ( possible later older ).
                {
                    new WebClient().DownloadFile(CustomURL, execution_path + "\\bin\\rbxfpsunlocker.exe");
                }
                else
                {
                    //You're using from Panda-Technology Modded FPS Unlock, you can change it if you want to
                    string URL = new WebClient().DownloadString("https://raw.githubusercontent.com/Panda-Respiratory/-Panda---Database/main/Serverside-Config/FPS-Unlock");
                    new WebClient().DownloadFile(URL, execution_path + "\\bin\\rbxfpsunlocker.exe");
                }
                Process.Start(execution_path + "\\bin\\rbxfpsunlocker.exe");
            }
            else
            {
                if (FPSUnlockPipeExist(fpspipe) == false)
                {
                    //Kill the Existing FPS Unlock
                    Process[] RobloxProcesses = Process.GetProcessesByName("RobloxPlayerBeta");
                    foreach (var Processes in RobloxProcesses)
                    {
                        Processes.Kill();
                    }
                    RunFPSUnlock();
                    return;
                }
            }
        }

        private bool FPS_Process()
        {
            Process[] processes = Process.GetProcessesByName("rbxfpsunlocker");
            if (processes.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
