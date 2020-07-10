using System;

namespace WiiRemote.WiiMoteLib
{
    /// <summary>
    /// Argument sent through the WiimoteChangedEvent
    /// </summary>
    public class WiimoteChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The current state of the Wiimote and extension controllers
        /// </summary>
        public WiimoteState WiimoteState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ws">Wiimote state</param>
        public WiimoteChangedEventArgs(WiimoteState ws)
        {
            WiimoteState = ws;
        }
    }
}