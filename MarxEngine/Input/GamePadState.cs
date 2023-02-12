using System.Globalization;

namespace Nexus.Framework.Input
{
    public struct GamePadState
    {
        public GamePadState(GamePadThumbSticks thumbSticks, GamePadTriggers triggers, GamePadButtons buttons,
            GamePadDPad dPad)
        {
            PacketNumber = 0;
            IsConnected = true;
            _thumbs = thumbSticks;
            _triggers = triggers;
            _buttons = buttons;
            _dpad = dPad;
            _state = default;
            FillInternalState();
        }

        public GamePadState(Vector2 leftThumbStick, Vector2 rightThumbStick, float leftTrigger, float rightTrigger,
            params Buttons[] buttons)
        {
            PacketNumber = 0;
            IsConnected = true;
            _thumbs = new GamePadThumbSticks(leftThumbStick, rightThumbStick);
            _triggers = new GamePadTriggers(leftTrigger, rightTrigger);
            Buttons buttons2 = 0;
            if (buttons != null)
                for (var i = 0; i < buttons.Length; i++)
                    buttons2 |= buttons[i];
            _buttons = new GamePadButtons(buttons2);
            _dpad = default;
            _dpad._down = (buttons2 & Input.Buttons.DPadDown) != 0 ? ButtonState.Pressed : ButtonState.Released;
            _dpad._up = (buttons2 & Input.Buttons.DPadUp) != 0 ? ButtonState.Pressed : ButtonState.Released;
            _dpad._left = (buttons2 & Input.Buttons.DPadLeft) != 0 ? ButtonState.Pressed : ButtonState.Released;
            _dpad._right = (buttons2 & Input.Buttons.DPadRight) != 0 ? ButtonState.Pressed : ButtonState.Released;
            _state = default;
            FillInternalState();
        }

        private void FillInternalState()
        {
            _state.PacketNumber = 0;
            if (Buttons.A == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.A;
            if (Buttons.B == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.B;
            if (Buttons.X == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.X;
            if (Buttons.Y == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.Y;
            if (Buttons.Back == ButtonState.Pressed)
                _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.Back;
            if (Buttons.LeftShoulder == ButtonState.Pressed)
                _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.LeftShoulder;
            if (Buttons.LeftStick == ButtonState.Pressed)
                _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.LeftThumb;
            if (Buttons.RightShoulder == ButtonState.Pressed)
                _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.RightShoulder;
            if (Buttons.RightStick == ButtonState.Pressed)
                _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.RightThumb;
            if (Buttons.Start == ButtonState.Pressed)
                _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.Start;
            if (Buttons.BigButton == ButtonState.Pressed)
                _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.BigButton;
            if (DPad.Up == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.Up;
            if (DPad.Down == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.Down;
            if (DPad.Right == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.Right;
            if (DPad.Left == ButtonState.Pressed) _state.GamePad.Buttons = _state.GamePad.Buttons | ButtonValues.Left;
            _state.GamePad.LeftTrigger = (byte)(_triggers._left * 255f);
            _state.GamePad.RightTrigger = (byte)(_triggers._right * 255f);
            _state.GamePad.ThumbLX = (short)(_thumbs._left.X * 32767f);
            _state.GamePad.ThumbLY = (short)(_thumbs._left.Y * 32767f);
            _state.GamePad.ThumbRX = (short)(_thumbs._right.X * 32767f);
            _state.GamePad.ThumbRY = (short)(_thumbs._right.Y * 32767f);
        }

        internal GamePadState(ref XINPUT_STATE pState, ErrorCodes result, GamePadDeadZone deadZoneMode)
        {
            _state = pState;
            IsConnected = result != ErrorCodes.NotConnected;
            PacketNumber = pState.PacketNumber;
            _buttons._a = (ushort)(pState.GamePad.Buttons & ButtonValues.A) == 4096
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._b = (ushort)(pState.GamePad.Buttons & ButtonValues.B) == 8192
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._x = (ushort)(pState.GamePad.Buttons & ButtonValues.X) == 16384
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._y = (ushort)(pState.GamePad.Buttons & ButtonValues.Y) == 32768
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._start = (ushort)(pState.GamePad.Buttons & ButtonValues.Start) == 16
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._back = (ushort)(pState.GamePad.Buttons & ButtonValues.Back) == 32
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._leftStick = (ushort)(pState.GamePad.Buttons & ButtonValues.LeftThumb) == 64
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._rightStick = (ushort)(pState.GamePad.Buttons & ButtonValues.RightThumb) == 128
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._leftShoulder = (ushort)(pState.GamePad.Buttons & ButtonValues.LeftShoulder) == 256
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._rightShoulder = (ushort)(pState.GamePad.Buttons & ButtonValues.RightShoulder) == 512
                ? ButtonState.Pressed
                : ButtonState.Released;
            _buttons._bigButton = (ushort)(pState.GamePad.Buttons & ButtonValues.BigButton) == 2048
                ? ButtonState.Pressed
                : ButtonState.Released;
            _triggers._left = GamePadDeadZoneUtils.ApplyTriggerDeadZone(pState.GamePad.LeftTrigger, deadZoneMode);
            _triggers._right = GamePadDeadZoneUtils.ApplyTriggerDeadZone(pState.GamePad.RightTrigger, deadZoneMode);
            _thumbs._left =
                GamePadDeadZoneUtils.ApplyLeftStickDeadZone(pState.GamePad.ThumbLX, pState.GamePad.ThumbLY,
                    deadZoneMode);
            _thumbs._right =
                GamePadDeadZoneUtils.ApplyRightStickDeadZone(pState.GamePad.ThumbRX, pState.GamePad.ThumbRY,
                    deadZoneMode);
            _dpad._down = (ushort)(pState.GamePad.Buttons & ButtonValues.Down) == 2
                ? ButtonState.Pressed
                : ButtonState.Released;
            _dpad._up = (ushort)(pState.GamePad.Buttons & ButtonValues.Up) == 1
                ? ButtonState.Pressed
                : ButtonState.Released;
            _dpad._left = (ushort)(pState.GamePad.Buttons & ButtonValues.Left) == 4
                ? ButtonState.Pressed
                : ButtonState.Released;
            _dpad._right = (ushort)(pState.GamePad.Buttons & ButtonValues.Right) == 8
                ? ButtonState.Pressed
                : ButtonState.Released;
        }

        public GamePadButtons Buttons => _buttons;

        public GamePadDPad DPad => _dpad;

        public bool IsConnected { get; }

        public int PacketNumber { get; }

        public GamePadThumbSticks ThumbSticks => _thumbs;

        public GamePadTriggers Triggers => _triggers;

        public bool IsButtonDown(Buttons button)
        {
            var buttons = (Buttons)(_state.GamePad.Buttons & (ButtonValues.A | ButtonValues.B | ButtonValues.Back |
                                                              ButtonValues.Down | ButtonValues.Left |
                                                              ButtonValues.LeftShoulder | ButtonValues.LeftThumb |
                                                              ButtonValues.Right | ButtonValues.RightShoulder |
                                                              ButtonValues.RightThumb | ButtonValues.Start |
                                                              ButtonValues.Up | ButtonValues.X | ButtonValues.Y |
                                                              ButtonValues.BigButton));
            if ((button & Input.Buttons.LeftThumbstickLeft) == Input.Buttons.LeftThumbstickLeft &&
                GamePadDeadZoneUtils.ApplyLeftStickDeadZone(_state.GamePad.ThumbLX, _state.GamePad.ThumbLY,
                    GamePadDeadZone.IndependentAxes).X < 0f) buttons |= Input.Buttons.LeftThumbstickLeft;
            if ((button & Input.Buttons.LeftThumbstickRight) == Input.Buttons.LeftThumbstickRight &&
                GamePadDeadZoneUtils.ApplyLeftStickDeadZone(_state.GamePad.ThumbLX, _state.GamePad.ThumbLY,
                    GamePadDeadZone.IndependentAxes).X > 0f) buttons |= Input.Buttons.LeftThumbstickRight;
            if ((button & Input.Buttons.LeftThumbstickDown) == Input.Buttons.LeftThumbstickDown &&
                GamePadDeadZoneUtils.ApplyLeftStickDeadZone(_state.GamePad.ThumbLX, _state.GamePad.ThumbLY,
                    GamePadDeadZone.IndependentAxes).Y < 0f) buttons |= Input.Buttons.LeftThumbstickDown;
            if ((button & Input.Buttons.LeftThumbstickUp) == Input.Buttons.LeftThumbstickUp && GamePadDeadZoneUtils
                    .ApplyLeftStickDeadZone(_state.GamePad.ThumbLX, _state.GamePad.ThumbLY,
                        GamePadDeadZone.IndependentAxes).Y > 0f) buttons |= Input.Buttons.LeftThumbstickUp;
            if ((button & Input.Buttons.RightThumbstickLeft) == Input.Buttons.RightThumbstickLeft &&
                GamePadDeadZoneUtils.ApplyRightStickDeadZone(_state.GamePad.ThumbRX, _state.GamePad.ThumbRY,
                    GamePadDeadZone.IndependentAxes).X < 0f) buttons |= Input.Buttons.RightThumbstickLeft;
            if ((button & Input.Buttons.RightThumbstickRight) == Input.Buttons.RightThumbstickRight &&
                GamePadDeadZoneUtils.ApplyRightStickDeadZone(_state.GamePad.ThumbRX, _state.GamePad.ThumbRY,
                    GamePadDeadZone.IndependentAxes).X > 0f) buttons |= Input.Buttons.RightThumbstickRight;
            if ((button & Input.Buttons.RightThumbstickDown) == Input.Buttons.RightThumbstickDown &&
                GamePadDeadZoneUtils.ApplyRightStickDeadZone(_state.GamePad.ThumbRX, _state.GamePad.ThumbRY,
                    GamePadDeadZone.IndependentAxes).Y < 0f) buttons |= Input.Buttons.RightThumbstickDown;
            if ((button & Input.Buttons.RightThumbstickUp) == Input.Buttons.RightThumbstickUp && GamePadDeadZoneUtils
                    .ApplyRightStickDeadZone(_state.GamePad.ThumbRX, _state.GamePad.ThumbRY,
                        GamePadDeadZone.IndependentAxes).Y > 0f) buttons |= Input.Buttons.RightThumbstickUp;
            if ((button & Input.Buttons.LeftTrigger) == Input.Buttons.LeftTrigger &&
                GamePadDeadZoneUtils.ApplyTriggerDeadZone(_state.GamePad.LeftTrigger, GamePadDeadZone.IndependentAxes) >
                0f) buttons |= Input.Buttons.LeftTrigger;
            if ((button & Input.Buttons.RightTrigger) == Input.Buttons.RightTrigger &&
                GamePadDeadZoneUtils.ApplyTriggerDeadZone(_state.GamePad.RightTrigger,
                    GamePadDeadZone.IndependentAxes) > 0f) buttons |= Input.Buttons.RightTrigger;
            return (button & buttons) == button;
        }

        public bool IsButtonUp(Buttons button)
        {
            return !IsButtonDown(button);
        }

        public override bool Equals(object obj)
        {
            return obj != null && !(obj.GetType() != GetType()) && this == (GamePadState)obj;
        }

        public override int GetHashCode()
        {
            return _thumbs.GetHashCode() ^ _triggers.GetHashCode() ^ _buttons.GetHashCode() ^
                   IsConnected.GetHashCode() ^ _dpad.GetHashCode() ^ PacketNumber.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{IsConnected:{0}}}", IsConnected);
        }

        public static bool operator ==(GamePadState left, GamePadState right)
        {
            return left.IsConnected == right.IsConnected && left.PacketNumber == right.PacketNumber &&
                   left._thumbs == right._thumbs && left._triggers == right._triggers &&
                   left._buttons == right._buttons && left._dpad == right._dpad;
        }

        public static bool operator !=(GamePadState left, GamePadState right)
        {
            return left.IsConnected != right.IsConnected || left.PacketNumber != right.PacketNumber ||
                   left._thumbs != right._thumbs || left._triggers != right._triggers ||
                   left._buttons != right._buttons || left._dpad != right._dpad;
        }

        private const int _normalButtonMask = 64511;

        private GamePadThumbSticks _thumbs;

        private GamePadTriggers _triggers;

        private GamePadButtons _buttons;

        private GamePadDPad _dpad;

        private XINPUT_STATE _state;
    }
}