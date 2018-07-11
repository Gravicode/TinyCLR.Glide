namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public static class TouchCapture
    {
        private static UIElement _captureElement;

        public static bool Capture(UIElement element)
        {
            return Capture(element, CaptureMode.Element);
        }

        public static bool Capture(UIElement element, CaptureMode mode)
        {
            if (mode != CaptureMode.None)
            {
                if (element == null)
                {
                    throw new ArgumentException();
                }
                if (!IsMainWindowChild(element))
                {
                    throw new ArgumentException();
                }
                if (mode == CaptureMode.SubTree)
                {
                    throw new NotImplementedException();
                }
                if (mode == CaptureMode.Element)
                {
                    _captureElement = element;
                }
            }
            else
            {
                _captureElement = null;
            }
            return true;
        }

        private static bool IsMainWindowChild(UIElement element)
        {
            UIElement mainWindow = Application.Current.MainWindow;
            while (element != null)
            {
                if (element == mainWindow)
                {
                    return true;
                }
                element = element.Parent;
            }
            return false;
        }

        public static UIElement Captured
        {
            get
            {
                return _captureElement;
            }
        }
    }
}

