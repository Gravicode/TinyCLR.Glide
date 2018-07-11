namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using System;
    using System.Runtime.InteropServices;

    public abstract class ContentControl : Control
    {
        protected ContentControl()
        {
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            UIElement child = this.Child;
            if (child != null)
            {
                child.Measure(availableWidth, availableHeight);
                child.GetDesiredSize(out desiredWidth, out desiredHeight);
            }
            else
            {
                desiredWidth = desiredHeight = 0;
            }
        }

        public UIElement Child
        {
            get
            {
                if (base.LogicalChildren.Count > 0)
                {
                    return base._logicalChildren[0];
                }
                return null;
            }
            set
            {
                base.VerifyAccess();
                base.LogicalChildren.Clear();
                base.LogicalChildren.Add(value);
            }
        }
    }
}

