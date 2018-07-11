namespace GHIElectronics.TinyCLR.UI.Media
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class SolidColorBrush : Brush
    {
        public GHIElectronics.TinyCLR.UI.Media.Color Color;
        private const int c_YMinBit = 0x40000000;
        private const int c_XValueMask = 0x3fffffff;

        public SolidColorBrush(GHIElectronics.TinyCLR.UI.Media.Color color)
        {
            this.Color = color;
        }

        private int Abs(int a)
        {
            if (a < 0)
            {
                return -a;
            }
            return a;
        }

        internal override void RenderEllipse(Bitmap bmp, Pen pen, int x, int y, int xRadius, int yRadius)
        {
            GHIElectronics.TinyCLR.UI.Media.Color color = (pen != null) ? pen.Color : Colors.Transparent;
            ushort thickness = 0;
            if (pen != null)
            {
                thickness = pen.Thickness;
            }
            bmp.DrawEllipse(color, thickness, x, y, xRadius, yRadius, this.Color, 0, 0, this.Color, 0, 0, base.Opacity);
        }

        internal override void RenderPolygon(Bitmap bmp, Pen outline, int[] pts)
        {
            int num = pts.Length / 2;
            if ((num >= 3) && (base.Opacity != 0))
            {
                int index = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                LineSegment[] segmentArray = new LineSegment[num];
                int[] numArray = new int[num];
                index = 0;
                while (index < num)
                {
                    segmentArray[index] = new LineSegment { processedPts = 0 };
                    numArray[index] = 0;
                    segmentArray[index].x1 = pts[index * 2];
                    segmentArray[index].y1 = pts[(index * 2) + 1];
                    if (index < (num - 1))
                    {
                        segmentArray[index].x2 = pts[(index + 1) * 2];
                        segmentArray[index].y2 = pts[((index + 1) * 2) + 1];
                    }
                    else
                    {
                        segmentArray[index].x2 = pts[0];
                        segmentArray[index].y2 = pts[1];
                    }
                    if (segmentArray[index].y2 < segmentArray[index].y1)
                    {
                        this.Swap(ref segmentArray[index].y2, ref segmentArray[index].y1);
                        this.Swap(ref segmentArray[index].x2, ref segmentArray[index].x1);
                    }
                    segmentArray[index].dx = this.Abs(segmentArray[index].x2 - segmentArray[index].x1);
                    segmentArray[index].dy = this.Abs(segmentArray[index].y2 - segmentArray[index].y1);
                    segmentArray[index].cx = segmentArray[index].x1;
                    segmentArray[index].e = 0;
                    if (segmentArray[index].dx < segmentArray[index].dy)
                    {
                        segmentArray[index].highSlope = true;
                    }
                    else
                    {
                        segmentArray[index].highSlope = false;
                    }
                    if (segmentArray[index].x2 > segmentArray[index].x1)
                    {
                        segmentArray[index].ix = 1;
                    }
                    else
                    {
                        segmentArray[index].ix = -1;
                    }
                    if (index == 0)
                    {
                        num4 = segmentArray[index].y1;
                        num5 = segmentArray[index].y2;
                    }
                    else
                    {
                        if (segmentArray[index].y1 < num4)
                        {
                            num4 = segmentArray[index].y1;
                        }
                        if (segmentArray[index].y2 > num5)
                        {
                            num5 = segmentArray[index].y2;
                        }
                    }
                    index++;
                }
                for (num3 = num4; num3 <= num5; num3++)
                {
                    int num6 = 0;
                    index = 0;
                    while (index < num)
                    {
                        numArray[index] = 0x7fffffff;
                        index++;
                    }
                    index = 0;
                    while (index < num)
                    {
                        if ((num3 >= segmentArray[index].y1) && (num3 <= segmentArray[index].y2))
                        {
                            int cx;
                            bool flag;
                            LineSegment segment2 = segmentArray[index];
                            segment2.processedPts++;
                            if (segmentArray[index].dy != 0)
                            {
                                if (segmentArray[index].highSlope)
                                {
                                    if (num3 == segmentArray[index].y1)
                                    {
                                        segmentArray[index].cx = segmentArray[index].x1;
                                    }
                                    else if (num3 == segmentArray[index].y2)
                                    {
                                        segmentArray[index].cx = segmentArray[index].x2;
                                    }
                                    else
                                    {
                                        LineSegment segment3 = segmentArray[index];
                                        segment3.e += segmentArray[index].dx;
                                        if ((segmentArray[index].e << 1) >= segmentArray[index].dy)
                                        {
                                            LineSegment segment4 = segmentArray[index];
                                            segment4.cx += segmentArray[index].ix;
                                            LineSegment segment5 = segmentArray[index];
                                            segment5.e -= segmentArray[index].dy;
                                        }
                                    }
                                }
                                else if (num3 == segmentArray[index].y1)
                                {
                                    segmentArray[index].cx = segmentArray[index].x1;
                                }
                                else if (num3 == segmentArray[index].y2)
                                {
                                    segmentArray[index].cx = segmentArray[index].x2;
                                }
                                else
                                {
                                    do
                                    {
                                        LineSegment segment6 = segmentArray[index];
                                        segment6.cx += segmentArray[index].ix;
                                        LineSegment segment7 = segmentArray[index];
                                        segment7.e += segmentArray[index].dy;
                                    }
                                    while ((segmentArray[index].e << 1) < segmentArray[index].dx);
                                    LineSegment segment8 = segmentArray[index];
                                    segment8.e -= segmentArray[index].dx;
                                }
                            }
                            int num8 = 0x7fffffff;
                            bool flag2 = false;
                            if (segmentArray[index].dy == 0)
                            {
                                cx = segmentArray[index].x1;
                                flag = true;
                                num8 = segmentArray[index].x2;
                                flag2 = true;
                                if (num8 < 0)
                                {
                                    num8 = 0;
                                }
                            }
                            else
                            {
                                cx = segmentArray[index].cx;
                                flag = segmentArray[index].processedPts == 1;
                            }
                            if (cx < 0)
                            {
                                cx = 0;
                            }
                            int num9 = -1;
                            int num10 = (num8 == 0x7fffffff) ? num8 : -1;
                            int num11 = 0;
                            num6 = 0;
                            while (num6 < num)
                            {
                                int num13 = numArray[num6] & 0x3fffffff;
                                bool flag3 = (numArray[num6] & 0x40000000) > 0;
                                if (num9 == -1)
                                {
                                    if ((num13 == cx) && (flag3 != flag))
                                    {
                                        num9 = 0x7fffffff;
                                    }
                                    else if (num13 > cx)
                                    {
                                        num9 = num6 + num11;
                                        num11++;
                                    }
                                }
                                if (num10 == -1)
                                {
                                    if ((num13 == num8) && (flag3 != flag2))
                                    {
                                        num10 = 0x7fffffff;
                                    }
                                    else if (num13 > num8)
                                    {
                                        num10 = num6 + num11;
                                        num11++;
                                    }
                                }
                                if (((num9 != -1) && (num10 != -1)) && (numArray[num6] == 0x7fffffff))
                                {
                                    break;
                                }
                                num6++;
                            }
                            int num12 = (num9 < num10) ? num9 : num10;
                            num11 = ((num10 == 0x7fffffff) || (num9 == 0x7fffffff)) ? 1 : 2;
                            num6 += num11 - 1;
                            if (num6 >= numArray.Length)
                            {
                                num6 = numArray.Length - 1;
                            }
                            while (num6 >= num12)
                            {
                                if (num6 == num9)
                                {
                                    numArray[num6] = flag ? (cx | 0x40000000) : cx;
                                    num11--;
                                }
                                else if (num6 == num10)
                                {
                                    numArray[num6] = flag2 ? (num8 | 0x40000000) : num8;
                                    num11--;
                                }
                                else
                                {
                                    if (num6 < num11)
                                    {
                                        break;
                                    }
                                    numArray[num6] = numArray[num6 - num11];
                                }
                                num6--;
                            }
                        }
                        index++;
                    }
                    for (index = 0; index < (numArray.Length - 1); index += 2)
                    {
                        int num14 = numArray[index] & 0x3fffffff;
                        int num15 = numArray[index + 1] & 0x3fffffff;
                        if ((num14 == 0x3fffffff) || (num15 == 0x3fffffff))
                        {
                            break;
                        }
                        bmp.DrawLine(this.Color, 1, num14, num3, num15, num3);
                    }
                }
            }
        }

        internal override void RenderRectangle(Bitmap bmp, Pen pen, int x, int y, int width, int height)
        {
            GHIElectronics.TinyCLR.UI.Media.Color outlineColor = (pen != null) ? pen.Color : Colors.Transparent;
            ushort outlineThickness = 0;
            if (pen != null)
            {
                outlineThickness = pen.Thickness;
            }
            bmp.DrawRectangle(outlineColor, outlineThickness, x, y, width, height, 0, 0, this.Color, 0, 0, this.Color, 0, 0, base.Opacity);
        }

        private void Swap(ref int a, ref int b)
        {
            int num = a;
            a = b;
            b = num;
        }

        private class LineSegment
        {
            public int x1;
            public int y1;
            public int x2;
            public int y2;
            public int dx;
            public int dy;
            public int cx;
            public int e;
            public bool highSlope;
            public int ix;
            public int processedPts;
        }
    }
}

