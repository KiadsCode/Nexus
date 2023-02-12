namespace Nexus.Framework.Input
{
    public struct GamePadCapabilities
    {
        internal GamePadCapabilities(ref XINPUT_CAPABILITIES caps, ErrorCodes result)
        {
            IsConnected = result != ErrorCodes.NotConnected;
            _caps = caps;
        }

        public GamePadType GamePadType
        {
            get
            {
                if (_caps.Type == 3) return (GamePadType)((_caps.Type << 8) | _caps.SubType);
                return (GamePadType)_caps.SubType;
            }
        }

        public bool IsConnected { get; }

        public bool HasAButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.A) != 0;

        public bool HasBackButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.Back) != 0;

        public bool HasBButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.B) != 0;

        public bool HasDPadDownButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.Down) != 0;

        public bool HasDPadLeftButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.Left) != 0;

        public bool HasDPadRightButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.Right) != 0;

        public bool HasDPadUpButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.Up) != 0;

        public bool HasLeftShoulderButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.LeftShoulder) != 0;

        public bool HasLeftStickButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.LeftThumb) != 0;

        public bool HasRightShoulderButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.RightShoulder) != 0;

        public bool HasRightStickButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.RightThumb) != 0;

        public bool HasStartButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.Start) != 0;

        public bool HasXButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.X) != 0;

        public bool HasYButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.Y) != 0;

        public bool HasBigButton => (ushort)(_caps.GamePad.Buttons & ButtonValues.BigButton) != 0;

        public bool HasLeftXThumbStick => _caps.GamePad.ThumbLX != 0;

        public bool HasLeftYThumbStick => _caps.GamePad.ThumbLY != 0;

        public bool HasRightXThumbStick => _caps.GamePad.ThumbRX != 0;

        public bool HasRightYThumbStick => _caps.GamePad.ThumbRY != 0;

        public bool HasLeftTrigger => _caps.GamePad.LeftTrigger != 0;

        public bool HasRightTrigger => _caps.GamePad.RightTrigger != 0;

        public bool HasLeftVibrationMotor => _caps.Vibration.LeftMotorSpeed != 0;

        public bool HasRightVibrationMotor => _caps.Vibration.RightMotorSpeed != 0;

        public bool HasVoiceSupport => (_caps.Flags & 4) != 0;

        private readonly XINPUT_CAPABILITIES _caps;
    }
}