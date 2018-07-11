namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using System;
    using System.Runtime.InteropServices;

    public class Panel : UIElement
    {
        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            desiredWidth = desiredHeight = 0;
            UIElementCollection elements = base._logicalChildren;
            if (elements != null)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    int num3;
                    int num4;
                    UIElement element1 = elements[i];
                    element1.Measure(availableWidth, availableHeight);
                    element1.GetDesiredSize(out num3, out num4);
                    desiredWidth = Math.Max(desiredWidth, num3);
                    desiredHeight = Math.Max(desiredHeight, num4);
                }
            }
        }

        public UIElementCollection Children
        {
            get
            {
                return base.LogicalChildren;
            }
        }
    }
}

