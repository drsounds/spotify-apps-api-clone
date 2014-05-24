using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace Qi
{
    public enum AudioSessionState
    {
        AudioSessionStateInactive = 0,
        AudioSessionStateActive = 1,
        AudioSessionStateExpired = 2
    }
    public class Mashcast
    {

        public float? GetApplicationVolume(string name)
        {
            List<ISimpleAudioVolume> volumes = GetVolumeObjects(name);

            float level = 0;
            if (volumes.Count > 0)
            volumes[0].GetMasterVolume(out level);
            return level * 100;
        }

        public bool? GetApplicationMute(string name)
        {
            List<ISimpleAudioVolume> volumes = GetVolumeObjects(name);
            
            bool mute = false;
            if (volumes.Count > 0)
             volumes[0].GetMute(out mute);
            return mute;
        }
        public String[] GetApps()
        {
            return EnumerateApplications().ToArray();
        }
        public void PlayMedia(string url)
        {
            Qi.Program.MainForm.PlayMedia(url);
        }
        public void ShowPopup(int type, string title, String msg)
        {
            Program.MainForm.ShowPopup(type, title, msg);
        }
        public void ShowMessage(int type, String msg)
        {
            Program.MainForm.ShowMessage(type, msg);
        }
        public bool SetApplicationVolume(string name, float level)
        {
            
            List<ISimpleAudioVolume> volumes = GetVolumeObjects(name);
         

            Guid guid = Guid.Empty;
            foreach (ISimpleAudioVolume volume in volumes)
            {
              
                    volume.SetMasterVolume(level / 100, ref guid);
              
            }
            return true;
        }
        [DllImport("user32.dll")]
        public static extern int SendMessage(
              int hWnd,      // handle to destination window
              uint Msg,       // message
              long wParam,  // first message parameter
              long lParam   // second message parameter
              );

        const uint WM_KEYDOWN = 0x100;
        const uint WM_KEYUP = 0x101;  
        public void SetApplicationMute(string name, bool mute)
        {
            List<ISimpleAudioVolume> volumes = GetVolumeObjects(name);
            

            Guid guid = Guid.Empty;
            foreach (ISimpleAudioVolume volume in volumes)
            {
                int i = volume.SetMute(mute, ref guid);
            }
            
        }
        List<uint> pids = new List<uint>();
        public void TogglePlaybackState()
        {
            foreach (uint pid in pids)
            {
                IntPtr handle = Process.GetProcessById((int)pid).MainWindowHandle;
                try
                {
              //      SendMessage((int)handle, WM_KEYDOWN, (int)Keys.Space, 0);
                }
                catch (Exception e)
                {

                }
                break;
            }
        }
        public Mashcast()
        {
            ActiveChannels = new List<string>();
        }
        public void AddActiveChannel(string channel)
        {
            ActiveChannels.Add(channel);
        }
        public bool IsChannelActive(string channel)
        {
            return ActiveChannels.Contains(channel);
        }
        public void RemoveActiveChannel(string channel)
        {
            try
            {
                ActiveChannels.Remove(channel);
            }
            catch (Exception e)
            {

            }
        }
        public List<String> ActiveChannels { get; set; }
        public void ToggleStartStop(string windowName)
        {

        }

        public IEnumerable<string> EnumerateApplications()
        {
            // get the speakers (1st render + multimedia) device
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
            IMMDevice speakers;
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out speakers);

            // activate the session manager. we need the enumerator
            Guid IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
            object o;
            speakers.Activate(ref IID_IAudioSessionManager2, 0, IntPtr.Zero, out o);
            IAudioSessionManager2 mgr = (IAudioSessionManager2)o;

            // enumerate sessions for on this device
            IAudioSessionEnumerator sessionEnumerator;
            mgr.GetSessionEnumerator(out sessionEnumerator);
            int count;
            sessionEnumerator.GetCount(out count);

            for (int i = 0; i < count; i++)
            {
                Process p = null;
                IAudioSessionControl2 ctl;
                    
                try
                {
                    sessionEnumerator.GetSession(i, out ctl);
                    uint processId;
                    ctl.GetProcessId(out processId);

                   p =  Process.GetProcessById((int)processId);
                }
                catch (Exception e)
                {
                    continue;

                } 
                yield return p.ProcessName;
                Marshal.ReleaseComObject(ctl);
                
            }
            Marshal.ReleaseComObject(sessionEnumerator);
            Marshal.ReleaseComObject(mgr);
            Marshal.ReleaseComObject(speakers);
            Marshal.ReleaseComObject(deviceEnumerator);
        }

        private List<ISimpleAudioVolume> GetVolumeObjects(string name)
        {
            List<ISimpleAudioVolume> objects = new List<ISimpleAudioVolume>();
            // get the speakers (1st render + multimedia) device
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
            IMMDevice speakers;
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eConsole, out speakers);
            this.pids.Clear();
            // activate the session manager. we need the enumerator
            Guid IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
            object o;
            speakers.Activate(ref IID_IAudioSessionManager2, 0, IntPtr.Zero, out o);
            IAudioSessionManager2 mgr = (IAudioSessionManager2)o;

            // enumerate sessions for on this device
            IAudioSessionEnumerator sessionEnumerator;
            mgr.GetSessionEnumerator(out sessionEnumerator);
            int count;
            sessionEnumerator.GetCount(out count);

            // search for an audio session with the required name
            // NOTE: we could also use the process id instead of the app name (with IAudioSessionControl2)
            ISimpleAudioVolume volumeControl = null;
            for (int i = 0; i < count; i++)
            {
                IAudioSessionControl2 ctl;
                sessionEnumerator.GetSession(i, out ctl);
                uint processId;
                ctl.GetProcessId(out processId);
                try
                {
                    Process p = Process.GetProcessById((int)processId);
                    if (string.Compare(name, p.ProcessName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        volumeControl = ctl as ISimpleAudioVolume;
                        objects.Add(ctl as ISimpleAudioVolume);
                        this.pids.Add(processId);
                    }
                }
                catch (Exception e)
                {
                }
            }
            Marshal.ReleaseComObject(sessionEnumerator);
            Marshal.ReleaseComObject(mgr);
            Marshal.ReleaseComObject(speakers);
            Marshal.ReleaseComObject(deviceEnumerator);
            return objects;
        }

        [ComImport]
        [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
        internal class MMDeviceEnumerator
        {
        }

        internal enum EDataFlow
        {
            eRender,
            eCapture,
            eAll,
            EDataFlow_enum_count
        }

        internal enum ERole
        {
            eConsole,
            eMultimedia,
            eCommunications,
            ERole_enum_count
        }
        public enum AudioSessionState
    {
         AudioSessionStateInactive = 0,
         AudioSessionStateActive = 1,
         AudioSessionStateExpired = 2
    }
        [Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IAudioSessionEvents
        {
            [PreserveSig]
            int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string NewDisplayName, Guid EventContext);
            [PreserveSig]
            int OnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string NewIconPath, Guid EventContext);
            [PreserveSig]
            int OnSimpleVolumeChanged(float NewVolume, bool newMute, Guid EventContext);
            [PreserveSig]
            int OnChannelVolumeChanged(UInt32 ChannelCount, IntPtr NewChannelVolumeArray, UInt32 ChangedChannel, Guid EventContext);
            [PreserveSig]
            int OnGroupingParamChanged(Guid NewGroupingParam, Guid EventContext);
            [PreserveSig]
            int OnStateChanged(AudioSessionState NewState);
            [PreserveSig]
            int OnSessionDisconnected(AudioSessionDisconnectReason DisconnectReason);
        }
        public enum AudioSessionDisconnectReason
    {
        DisconnectReasonDeviceRemoval = 0,
        DisconnectReasonServerShutdown = (DisconnectReasonDeviceRemoval + 1),
        DisconnectReasonFormatChanged = (DisconnectReasonServerShutdown + 1),
        DisconnectReasonSessionLogoff = (DisconnectReasonFormatChanged + 1),
        DisconnectReasonSessionDisconnected = (DisconnectReasonSessionLogoff + 1),
        DisconnectReasonExclusiveModeOverride = (DisconnectReasonSessionDisconnected + 1) 
    }
        [Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d"),
      InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IAudioSessionControl2
        {
            //IAudioSession functions
            [PreserveSig]
            int GetState(out AudioSessionState state);
            [PreserveSig]
            int GetDisplayName(IntPtr name);
            [PreserveSig]
            int SetDisplayName(string value, Guid EventContext);
            [PreserveSig]
            int GetIconPath(out IntPtr Path);
            [PreserveSig]
            int SetIconPath(string Value, Guid EventContext);
            [PreserveSig]
            int GetGroupingParam(out Guid GroupingParam);
            [PreserveSig]
            int SetGroupingParam(Guid Override, Guid Eventcontext);
            [PreserveSig]
            int RegisterAudioSessionNotification(IAudioSessionEvents NewNotifications);
            [PreserveSig]
            int UnregisterAudioSessionNotification(IAudioSessionEvents NewNotifications);
            //IAudioSession2 functions
            [PreserveSig]
            int GetSessionIdentifier(out IntPtr retVal);
            [PreserveSig]
            int GetSessionInstanceIdentifier(out IntPtr retVal);
            [PreserveSig]
            int GetProcessId(out UInt32 retvVal);
            [PreserveSig]
            int IsSystemSoundsSession();
            [PreserveSig]
            int SetDuckingPreference(bool optOut);


        }
        [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMMDeviceEnumerator
        {
            int NotImpl1();

            [PreserveSig]
            int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppDevice);

            // the rest is not implemented
        }

        [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMMDevice
        {
            [PreserveSig]
            int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);

            // the rest is not implemented
        }

        [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IAudioSessionManager2
        {
            int NotImpl1();
            int NotImpl2();

            [PreserveSig]
            int GetSessionEnumerator(out IAudioSessionEnumerator SessionEnum);

            // the rest is not implemented
        }

        [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IAudioSessionEnumerator
        {
            [PreserveSig]
            int GetCount(out int SessionCount);

            [PreserveSig]
            int GetSession(int SessionCount, out IAudioSessionControl2 Session);
        }

        [Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IAudioSessionControl
        {
            int NotImpl1();

            [PreserveSig]
            int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);

            // the rest is not implemented
        }

        [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface ISimpleAudioVolume
        {
            [PreserveSig]
            int SetMasterVolume(float fLevel, ref Guid EventContext);

            [PreserveSig]
            int GetMasterVolume(out float pfLevel);

            [PreserveSig]
            int SetMute(bool bMute, ref Guid EventContext);

            [PreserveSig]
            int GetMute(out bool pbMute);
        }
    }
    public class LocalStorage
    {
        public Dictionary<String, String> Settings = new Dictionary<string, string>();
        public LocalStorage()
        {
            if (File.Exists(Program.AppPath + "settings.xml"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Program.AppPath + "settings.xml");
                foreach (XmlNode n in xmlDoc.GetElementsByTagName("attribute"))
                {
                    if (n.GetType() == typeof(XmlElement))
                    {
                        XmlElement elm = (XmlElement)n;
                        Settings.Add(elm.GetAttribute("name"), elm.InnerText);
                    }
                }
            }
        }
        public void Save()
        {
            XmlDocument xmlDoc = new XmlDocument();
            var body = xmlDoc.CreateElement("settings");
            xmlDoc.AppendChild(body);
            foreach (KeyValuePair<String, string> prop in Settings)
            {
                var attribute = xmlDoc.CreateElement("attribute");
                attribute.SetAttribute("name", prop.Key);
                attribute.InnerText = prop.Value;
                body.AppendChild(attribute);
            }
            xmlDoc.Save(Program.AppPath + "settings.xml");

        }
        public String GetItem(string name, string def = null)
        {

            if (this.Settings.ContainsKey(name))
                return this.Settings[name];
            else
                return def;
        }
        public void SetItem(string name, string value)
        {
            if(!this.Settings.ContainsKey(name))
                this.Settings.Add(name, value);
            else
                this.Settings[name] = value;
            this.Save();

        }
    }
}