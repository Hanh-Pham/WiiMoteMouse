using System.Threading;
using WiiRemote.WiiMoteLib;

namespace WiiRemote
{
    class Program
    {
        private static WiiMote wm = new WiiMote();
        private static State state = new State();
        private static int _moveSpeed = 3;

        static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        static void Main()
        {
            wm.Connect();
            wm.SetReportType(InputReport.IRAccel, true);
            wm.WiimoteChanged += Wm_WiimoteChanged;

            _quitEvent.WaitOne();
            wm.Disconnect();
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
                MoveMouse(e.WiimoteState.NunchukState);
                MouseClick(e.WiimoteState.NunchukState);
            }
            //else
            //{
            MoveMouse(e.WiimoteState.ButtonState);
            MouseClick(e.WiimoteState.ButtonState);
            //}


            state.ButtonState = e.WiimoteState.ButtonState;
            state.NunchukState = e.WiimoteState.NunchukState;
        }

        private static void MouseClick(ButtonState buttonState)
        {
            if (!state.ButtonState.A && buttonState.A)
            {
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown | MouseOperations.MouseEventFlags.LeftUp);
            }

            if (!state.ButtonState.B && buttonState.B)
            {
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown | MouseOperations.MouseEventFlags.RightUp);
            }
        }
        private static void MouseClick(NunchukState nunchukState)
        {
            if (!state.NunchukState.Z && nunchukState.Z)
            {
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown | MouseOperations.MouseEventFlags.LeftUp);
            }

        }
        private static void MoveMouse(ButtonState buttonState)
        {
            var position = MouseOperations.GetCursorPosition();

            if (buttonState.Left)
            {
                MouseOperations.SetCursorPosition(position.X - _moveSpeed, position.Y);
            }

            if (buttonState.Right)
            {
                MouseOperations.SetCursorPosition(position.X + _moveSpeed, position.Y);
            }

            if (buttonState.Up)
            {
                MouseOperations.SetCursorPosition(position.X, position.Y - _moveSpeed);
            }

            if (buttonState.Down)
            {
                MouseOperations.SetCursorPosition(position.X, position.Y + _moveSpeed);
            }
        }
        private static void MoveMouse(NunchukState nunchukState)
        {
            var position = MouseOperations.GetCursorPosition();
            var joyStickX = nunchukState.Joystick.X;
            var joyStickY = nunchukState.Joystick.Y;
            if (joyStickY > 0.05 || joyStickY < -0.05 || joyStickX > 0.05 || joyStickX < -0.05)
            {
                var posX = joyStickX > 0.1 ? position.X + _moveSpeed : position.X - _moveSpeed;
                var posY = joyStickY > 0.1 ? position.Y - _moveSpeed : position.Y + _moveSpeed;
                MouseOperations.SetCursorPosition(posX, posY);
            }
        }
    }
}
