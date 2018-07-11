namespace GHIElectronics.TinyCLR.UI
{
    using GHIElectronics.TinyCLR.UI.Controls;
    using GHIElectronics.TinyCLR.UI.Media;
    using GHIElectronics.TinyCLR.UI.Threading;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Window : ContentControl
    {
        private GHIElectronics.TinyCLR.UI.SizeToContent _sizeToContent;
        private WindowManager _windowManager;

        public Window()
        {
            if (WindowManager.Instance == null)
            {
                throw new InvalidOperationException();
            }
            this._windowManager = WindowManager.Instance;
            base._background = new SolidColorBrush(Colors.White);
            base.Visibility = Visibility.Collapsed;
            this._windowManager.Children.Add(this);
            Application current = Application.Current;
            if (current != null)
            {
                if (current.Dispatcher.Thread == Dispatcher.CurrentDispatcher.Thread)
                {
                    current.WindowsInternal.Add(this);
                    if (current.MainWindow == null)
                    {
                        current.MainWindow = this;
                    }
                }
                else
                {
                    current.NonAppWindowsInternal.Add(this);
                }
            }
        }

        protected override void ArrangeOverride(int arrangeWidth, int arrangeHeight)
        {
            UIElement element;
            UIElementCollection logicalChildren = base.LogicalChildren;
            if ((logicalChildren.Count > 0) && ((element = logicalChildren[0]) != null))
            {
                element.Arrange(0, 0, arrangeWidth, arrangeHeight);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Close()
        {
            Application current = Application.Current;
            if (current != null)
            {
                current.WindowsInternal.Remove(this);
                current.NonAppWindowsInternal.Remove(this);
            }
            if (this._windowManager != null)
            {
                this._windowManager.Children.Remove(this);
                this._windowManager = null;
            }
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            UIElementCollection logicalChildren = base.LogicalChildren;
            if (logicalChildren.Count > 0)
            {
                UIElement element = logicalChildren[0];
                if (element != null)
                {
                    element.Measure(availableWidth, availableHeight);
                    element.GetDesiredSize(out desiredWidth, out desiredHeight);
                    return;
                }
            }
            desiredWidth = availableWidth;
            desiredHeight = availableHeight;
        }

        public GHIElectronics.TinyCLR.UI.SizeToContent SizeToContent
        {
            get
            {
                return this._sizeToContent;
            }
            set
            {
                base.VerifyAccess();
                this._sizeToContent = value;
            }
        }

        public int Top
        {
            get
            {
                return Canvas.GetTop(this);
            }
            set
            {
                base.VerifyAccess();
                Canvas.SetTop(this, value);
            }
        }

        public int Left
        {
            get
            {
                return Canvas.GetLeft(this);
            }
            set
            {
                base.VerifyAccess();
                Canvas.SetLeft(this, value);
            }
        }

        public bool Topmost
        {
            get
            {
                return this._windowManager.IsTopMost(this);
            }
            set
            {
                base.VerifyAccess();
                this._windowManager.SetTopMost(this);
            }
        }
    }
}

