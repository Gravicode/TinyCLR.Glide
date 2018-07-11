namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using System;
    using System.Runtime.InteropServices;

    public class Canvas : Panel
    {
        private const int Edge_Left = 1;
        private const int Edge_Right = 2;
        private const int Edge_Top = 4;
        private const int Edge_Bottom = 8;
        private const int Edge_LeftRight = 3;
        private const int Edge_TopBottom = 12;

        protected override void ArrangeOverride(int arrangeWidth, int arrangeHeight)
        {
            base.VerifyAccess();
            UIElementCollection elements = base._logicalChildren;
            if (elements != null)
            {
                int count = elements.Count;
                for (int i = 0; i < count; i++)
                {
                    int num3;
                    int num4;
                    UIElement element = elements[i];
                    element.GetDesiredSize(out num3, out num4);
                    UIElement.Pair pair = element._anchorInfo;
                    if (pair != null)
                    {
                        int num5 = pair._status;
                        element.Arrange(((num5 & 2) != 0) ? ((arrangeWidth - num3) - pair._first) : pair._first, ((num5 & 8) != 0) ? ((arrangeHeight - num4) - pair._second) : pair._second, num3, num4);
                    }
                    else
                    {
                        element.Arrange(0, 0, num3, num4);
                    }
                }
            }
        }

        private static int GetAnchorValue(UIElement e, int edge)
        {
            UIElement.Pair pair = e._anchorInfo;
            if ((pair == null) || ((pair._status & edge) == 0))
            {
                return 0;
            }
            if ((edge & 3) == 0)
            {
                return pair._second;
            }
            return pair._first;
        }

        public static int GetBottom(UIElement e)
        {
            return GetAnchorValue(e, 8);
        }

        public static int GetLeft(UIElement e)
        {
            return GetAnchorValue(e, 1);
        }

        public static int GetRight(UIElement e)
        {
            return GetAnchorValue(e, 2);
        }

        public static int GetTop(UIElement e)
        {
            return GetAnchorValue(e, 4);
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            UIElementCollection elements = base._logicalChildren;
            if (elements != null)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    elements[i].Measure(0x7ffff, 0x7ffff);
                }
            }
            desiredWidth = 0;
            desiredHeight = 0;
        }

        private static void SetAnchorValue(UIElement e, int edge, int val)
        {
            e.VerifyAccess();
            UIElement.Pair pair = e._anchorInfo;
            if (pair == null)
            {
                pair = new UIElement.Pair();
                e._anchorInfo = pair;
            }
            if ((edge & 3) != 0)
            {
                pair._first = val;
                pair._status &= -4;
            }
            else
            {
                pair._second = val;
                pair._status &= -13;
            }
            pair._status |= edge;
            if (e.Parent != null)
            {
                e.Parent.InvalidateArrange();
            }
        }

        public static void SetBottom(UIElement e, int bottom)
        {
            SetAnchorValue(e, 8, bottom);
        }

        public static void SetLeft(UIElement e, int left)
        {
            SetAnchorValue(e, 1, left);
        }

        public static void SetRight(UIElement e, int right)
        {
            SetAnchorValue(e, 2, right);
        }

        public static void SetTop(UIElement e, int top)
        {
            SetAnchorValue(e, 4, top);
        }
    }
}

