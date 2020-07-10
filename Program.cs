using System.Threading;
using WiiRemote.WiiMoteLib;

namespace WiiRemote
{
    class Program
    {
        private static readonly WiiMote Wm = new WiiMote();
        private static readonly WiiMoteMouse WiiMoteMouse = new WiiMoteMouse(new State());

        static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        static void Main()
        {
            Start();
            _quitEvent.WaitOne();
            Stop();
        }
        public static void Start()
        {
            Wm.Connect();
            Wm.SetReportType(InputReport.IRAccel, true);
            Wm.WiimoteChanged += Wm_WiimoteChanged;
        }

        public static void Stop()
        {
            Wm.Disconnect();
        }
        private static void Wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            if (e.WiimoteState.ButtonState.Plus && e.WiimoteState.ButtonState.Minus)
            {
                _quitEvent.Set();
                return;
            }

            if (e.WiimoteState.Extension && e.WiimoteState.ExtensionType == ExtensionType.Nunchuk)
            {
                WiiMoteMouse.MoveMouse(e.WiimoteState.NunchukState);
                WiiMoteMouse.MouseClick(e.WiimoteState.NunchukState);
            }

            WiiMoteMouse.MoveMouse(e.WiimoteState.ButtonState);
            WiiMoteMouse.MouseClick(e.WiimoteState.ButtonState);

            WiiMoteMouse.SetState(e.WiimoteState.ButtonState, e.WiimoteState.NunchukState);
        }


    }
}
