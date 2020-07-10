using System;
using WiiRemote.WiiMoteLib;

namespace WiiRemote
{
    public class WiiMoteMouse
    {
        private State _state;
        private int _moveSpeed;
        public WiiMoteMouse(State state, int moveSpeed = 3)
        {
            _state = state;
            _moveSpeed = moveSpeed;
        }
        public void MouseClick(ButtonState buttonState)
        {
            CheckAndExecuteClick(_state.ButtonState.A, buttonState.A, MouseOperations.ClickLeft);
            CheckAndExecuteClick(_state.ButtonState.B, buttonState.B, MouseOperations.ClickRight);
        }
        public void MouseClick(NunchukState nunchukState)
        {
            CheckAndExecuteClick(_state.NunchukState.Z, nunchukState.Z, MouseOperations.ClickLeft);
            CheckAndExecuteClick(_state.NunchukState.C, nunchukState.C, MouseOperations.ClickRight);
        }

        public void MoveMouse(ButtonState buttonState)
        {
            var position = MouseOperations.GetCursorPosition();
            var posX = position.X;
            var posY = position.Y;
            if (buttonState.Left)
            {
                posX -= _moveSpeed;
            }

            if (buttonState.Right)
            {
                posX += _moveSpeed;
            }

            if (buttonState.Up)
            {
                posY -= _moveSpeed;
            }

            if (buttonState.Down)
            {
                posY += _moveSpeed;
            }
            MouseOperations.SetCursorPosition(posX, posY);
        }
        public void MoveMouse(NunchukState nunchukState)
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

        public void SetState(ButtonState buttonState, NunchukState nunchukState)
        {
            _state.ButtonState = buttonState;
            _state.NunchukState = nunchukState;
        }

        private void CheckAndExecuteClick(bool prevState, bool currentState, Action actionToExecute)
        {
            if (!prevState && currentState)
            {
                actionToExecute();
            }
        }
    }
}