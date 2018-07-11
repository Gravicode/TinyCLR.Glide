// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.WindowManager
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.UI.Controls;
using GHIElectronics.TinyCLR.UI.Input;
using GHIElectronics.TinyCLR.UI.Media;

namespace GHIElectronics.TinyCLR.UI
{
    public class WindowManager : Canvas
    {
        public static WindowManager Instance;
        private PostRenderEventHandler _postRenderHandler;

        public DisplayController DisplayController { get; }

        private WindowManager(DisplayController displayController)
        {
            this.DisplayController = displayController;
            WindowManager.Instance = this;
            this._flags |= UIElement.Flags.IsVisibleCache;
            this.Measure(524287, 524287);
            int width;
            int height;
            this.GetDesiredSize(out width, out height);
            this.Arrange(0, 0, width, height);
        }

        internal static WindowManager EnsureInstance(DisplayController displayController)
        {
            if (WindowManager.Instance == null)
                new WindowManager(displayController)._flags |= UIElement.Flags.ShouldPostRender;
            return WindowManager.Instance;
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            base.MeasureOverride(availableWidth, availableHeight, out desiredWidth, out desiredHeight);
            desiredWidth = (int)this.DisplayController.ActiveSettings.Width;
            desiredHeight = (int)this.DisplayController.ActiveSettings.Height;
        }

        internal void SetTopMost(Window window)
        {
            UIElementCollection logicalChildren = this.LogicalChildren;
            if (this.IsTopMost(window))
                return;
            logicalChildren.Remove((UIElement)window);
            logicalChildren.Add((UIElement)window);
        }

        internal bool IsTopMost(Window window)
        {
            int num = this.LogicalChildren.IndexOf((UIElement)window);
            if (num >= 0)
                return num == this.LogicalChildren.Count - 1;
            return false;
        }

        protected internal override void OnChildrenChanged(UIElement added, UIElement removed, int indexAffected)
        {
            base.OnChildrenChanged(added, removed, indexAffected);
            UIElementCollection logicalChildren = this.LogicalChildren;
            int index = logicalChildren.Count - 1;
            if (added != null && indexAffected == index && added.Visibility == Visibility.Visible)
            {
                Buttons.Focus(added);
                TouchCapture.Capture(added);
            }
            if (removed == null || !this.IsFocused || index < 0)
                return;
            Buttons.Focus(logicalChildren[index]);
            TouchCapture.Capture(logicalChildren[index]);
        }

        public event PostRenderEventHandler PostRender
        {
            add
            {
                this._postRenderHandler += value;
            }
            remove
            {
                this._postRenderHandler -= value;
            }
        }

        protected internal override void RenderRecursive(DrawingContext dc)
        {
            base.RenderRecursive(dc);
            PostRenderEventHandler postRenderHandler = this._postRenderHandler;
            if (postRenderHandler == null)
                return;
            postRenderHandler(dc);
        }
    }
}
