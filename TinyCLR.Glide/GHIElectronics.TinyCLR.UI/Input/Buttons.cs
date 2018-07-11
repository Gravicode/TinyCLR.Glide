namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class Buttons
    {
        public static readonly RoutedEvent PreviewButtonDownEvent = new RoutedEvent("PreviewButtonDown", RoutingStrategy.Tunnel, typeof(ButtonEventHandler));
        public static readonly RoutedEvent PreviewButtonUpEvent = new RoutedEvent("PreviewButtonUp", RoutingStrategy.Tunnel, typeof(ButtonEventHandler));
        public static readonly RoutedEvent ButtonDownEvent = new RoutedEvent("ButtonDown", RoutingStrategy.Bubble, typeof(ButtonEventHandler));
        public static readonly RoutedEvent ButtonUpEvent = new RoutedEvent("ButtonUp", RoutingStrategy.Bubble, typeof(ButtonEventHandler));
        public static readonly RoutedEvent GotFocusEvent = new RoutedEvent("GotFocus", RoutingStrategy.Bubble, typeof(FocusChangedEventHandler));
        public static readonly RoutedEvent LostFocusEvent = new RoutedEvent("LostFocus", RoutingStrategy.Bubble, typeof(FocusChangedEventHandler));

        public static UIElement Focus(UIElement element)
        {
            return PrimaryDevice.Focus(element);
        }

        public static ButtonState GetButtonState(HardwareButton button)
        {
            return PrimaryDevice.GetButtonState(button);
        }

        public static bool IsButtonDown(HardwareButton button)
        {
            return PrimaryDevice.IsButtonDown(button);
        }

        public static bool IsButtonHeld(HardwareButton button)
        {
            return PrimaryDevice.IsButtonHeld(button);
        }

        public static bool IsButtonUp(HardwareButton button)
        {
            return PrimaryDevice.IsButtonUp(button);
        }

        public static UIElement FocusedElement
        {
            get
            {
                return PrimaryDevice.Target;
            }
        }

        public static ButtonDevice PrimaryDevice
        {
            get
            {
                return InputManager.CurrentInputManager._buttonDevice;
            }
        }
    }
}

