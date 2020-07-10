using System;

namespace WiiRemote.WiiMoteLib
{
    public class WiimoteExtensionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The extenstion type inserted or removed
        /// </summary>
        public ExtensionType ExtensionType;
        /// <summary>
        /// Whether the extension was inserted or removed
        /// </summary>
        public bool Inserted;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The extension type inserted or removed</param>
        /// <param name="inserted">Whether the extension was inserted or removed</param>
        public WiimoteExtensionChangedEventArgs(ExtensionType type, bool inserted)
        {
            ExtensionType = type;
            Inserted = inserted;
        }
    }
}