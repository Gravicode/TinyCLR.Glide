namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Input;
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class TextFlow : UIElement
    {
        public TextRunCollection TextRuns;
        internal ArrayList _lineCache;
        internal GHIElectronics.TinyCLR.UI.Media.TextAlignment _alignment;
        internal int _currentLine;
        internal GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle _scrollingStyle;

        public TextFlow()
        {
            this.TextRuns = new TextRunCollection(this);
        }

        internal bool LineScroll(bool up)
        {
            if (this._lineCache != null)
            {
                if (up && (this._currentLine > 0))
                {
                    this._currentLine--;
                    base.Invalidate();
                    return true;
                }
                if (!up && (this._currentLine < (this._lineCache.Count - 1)))
                {
                    this._currentLine++;
                    base.Invalidate();
                    return true;
                }
            }
            return false;
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            desiredWidth = availableWidth;
            desiredHeight = availableHeight;
            if (availableWidth > 0)
            {
                this._lineCache = this.SplitLines(availableWidth);
                int num = 0;
                for (int i = this._lineCache.Count - 1; i >= 0; i--)
                {
                    num += ((TextLine) this._lineCache[i]).Height;
                }
                desiredHeight = num;
            }
        }

        protected override void OnButtonDown(ButtonEventArgs e)
        {
            if ((e.Button == HardwareButton.Up) || (e.Button == HardwareButton.Down))
            {
                bool up = e.Button == HardwareButton.Up;
                switch (this._scrollingStyle)
                {
                    case GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.First:
                        e.Handled = this.LineScroll(up);
                        break;

                    case GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.PageByPage:
                        e.Handled = this.PageScroll(up);
                        return;
                }
            }
        }

        public override void OnRender(DrawingContext dc)
        {
            if ((this._lineCache != null) && (this._lineCache.Count != 0))
            {
                int num3;
                int num4;
                int count = this._lineCache.Count;
                int num2 = 0;
                base.GetRenderSize(out num3, out num4);
                for (int i = this._currentLine; i < count; i++)
                {
                    int num6;
                    TextLine line = (TextLine) this._lineCache[i];
                    if ((num2 + line.Height) > num4)
                    {
                        break;
                    }
                    TextRun[] runs = line.Runs;
                    switch (this._alignment)
                    {
                        case GHIElectronics.TinyCLR.UI.Media.TextAlignment.Left:
                            num6 = 0;
                            break;

                        case GHIElectronics.TinyCLR.UI.Media.TextAlignment.Center:
                            num6 = (num3 - line.Width) >> 1;
                            break;

                        case GHIElectronics.TinyCLR.UI.Media.TextAlignment.Right:
                            num6 = num3 - line.Width;
                            break;

                        default:
                            throw new NotSupportedException();
                    }
                    for (int j = 0; j < runs.Length; j++)
                    {
                        int num8;
                        int num9;
                        TextRun run = runs[j];
                        run.GetSize(out num8, out num9);
                        int y = (num2 + line.Baseline) - run.Font.Ascent;
                        dc.DrawText(run.Text, run.Font, run.ForeColor, num6, y);
                        num6 += num8;
                    }
                    num2 += line.Height;
                }
            }
        }

        internal bool PageScroll(bool up)
        {
            if (this._lineCache == null)
            {
                return false;
            }
            int num = this._currentLine;
            int count = this._lineCache.Count;
            int num3 = base._renderHeight;
            int num4 = 0;
            if (!up)
            {
                while (num < count)
                {
                    TextLine line2 = (TextLine) this._lineCache[num];
                    num4 += line2.Height;
                    if (num4 > num3)
                    {
                        break;
                    }
                    num++;
                }
            }
            else
            {
                while (num > 0)
                {
                    num--;
                    TextLine line = (TextLine) this._lineCache[num];
                    num4 += line.Height;
                    if (num4 > num3)
                    {
                        num++;
                        break;
                    }
                }
                goto Label_008D;
            }
            if (num == count)
            {
                num = count - 1;
            }
        Label_008D:
            if (this._currentLine != num)
            {
                this._currentLine = num;
                base.Invalidate();
                return true;
            }
            return false;
        }

        internal ArrayList SplitLines(int availableWidth)
        {
            int num = 0;
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.TextRuns.Count; i++)
            {
                list.Add(this.TextRuns[i]);
            }
            ArrayList list2 = new ArrayList();
            ArrayList runs = new ArrayList();
            while (list.Count > 0)
            {
                bool flag = false;
                TextRun run = (TextRun) list[0];
                list.RemoveAt(0);
                if (run.IsEndOfLine)
                {
                    flag = true;
                }
                else
                {
                    int num3;
                    int num4;
                    run.GetSize(out num3, out num4);
                    num += num3;
                    runs.Add(run);
                    if (num > availableWidth)
                    {
                        bool emergencyBreak = runs.Count == 1;
                        if (run.Text.Length > 1)
                        {
                            TextRun run2;
                            TextRun run3;
                            runs.Remove(run);
                            if (run.Break(num3 - (num - availableWidth), out run2, out run3, emergencyBreak))
                            {
                                if (run2 != null)
                                {
                                    runs.Add(run2);
                                }
                                if (run3 != null)
                                {
                                    list.Insert(0, run3);
                                }
                            }
                            else if (!emergencyBreak)
                            {
                                list.Insert(0, run);
                            }
                        }
                        else if (!emergencyBreak)
                        {
                            runs.Remove(run);
                            list.Insert(0, run);
                        }
                        flag = true;
                    }
                    if ((num >= availableWidth) || (list.Count == 0))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    int height = 0;
                    int baseline = 0;
                    int count = runs.Count;
                    if (count > 0)
                    {
                        for (int j = 0; j < count; j++)
                        {
                            Font font = ((TextRun) runs[j]).Font;
                            int num9 = font.Height + font.ExternalLeading;
                            if (num9 > height)
                            {
                                height = num9;
                                baseline = font.Ascent;
                            }
                        }
                        list2.Add(new TextLine(runs, height, baseline));
                    }
                    else
                    {
                        height = (list2.Count > 0) ? ((TextLine) list2[list2.Count - 1]).Height : 10;
                        list2.Add(new TextLine(height));
                    }
                    runs.Clear();
                    num = 0;
                }
            }
            return list2;
        }

        public GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle ScrollingStyle
        {
            get
            {
                return this._scrollingStyle;
            }
            set
            {
                base.VerifyAccess();
                if ((value < GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.First) || (value > GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.PageByPage))
                {
                    throw new ArgumentOutOfRangeException("ScrollingStyle", "Invalid Enum");
                }
                this._scrollingStyle = value;
            }
        }

        public GHIElectronics.TinyCLR.UI.Media.TextAlignment TextAlignment
        {
            get
            {
                return this._alignment;
            }
            set
            {
                base.VerifyAccess();
                this._alignment = value;
                base.Invalidate();
            }
        }

        public int TopLine
        {
            get
            {
                return this._currentLine;
            }
            set
            {
                base.VerifyAccess();
                object obj1 = this._lineCache[value];
                this._currentLine = value;
                base.Invalidate();
            }
        }

        public int LineCount
        {
            get
            {
                return this._lineCache.Count;
            }
        }

        internal class TextLine
        {
            public const int DefaultLineHeight = 10;
            public TextRun[] Runs;
            public int Baseline;
            public int Height;
            private int _width;

            public TextLine(int height)
            {
                this.Runs = new TextRun[0];
                this.Height = height;
            }

            public TextLine(ArrayList runs, int height, int baseline)
            {
                this.Runs = (TextRun[]) runs.ToArray(typeof(TextRun));
                this.Baseline = baseline;
                this.Height = height;
            }

            public int Width
            {
                get
                {
                    if (this._width == 0)
                    {
                        int num = 0;
                        for (int i = this.Runs.Length - 1; i >= 0; i--)
                        {
                            int num3;
                            int num4;
                            this.Runs[i].GetSize(out num3, out num4);
                            num += num3;
                        }
                        this._width = num;
                    }
                    return this._width;
                }
            }
        }
    }
}

