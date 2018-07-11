namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class TextRun
    {
        public readonly string Text;
        public readonly System.Drawing.Font Font;
        public readonly GHIElectronics.TinyCLR.UI.Media.Color ForeColor;
        internal bool IsEndOfLine;
        protected int _width;
        protected int _height;

        private TextRun()
        {
        }

        public TextRun(string text, System.Drawing.Font font, GHIElectronics.TinyCLR.UI.Media.Color foreColor)
        {
            if ((text == null) || (text.Length == 0))
            {
                throw new ArgumentNullException("Text must be non-null and non-empty");
            }
            this.Text = text;
            this.Font = font ?? throw new ArgumentNullException("font must be non-null");
            this.ForeColor = foreColor;
        }

        internal bool Break(int availableWidth, out TextRun run1, out TextRun run2, bool emergencyBreak)
        {
            int length = -1;
            int startIndex = -1;
            bool flag = false;
            while (!flag)
            {
                int index = this.Text.IndexOf(' ', length + 1);
                flag = index == -1;
                if (!flag)
                {
                    int num4;
                    int num5;
                    this.Font.ComputeExtent(this.Text.Substring(0, index), out num4, out num5);
                    flag = num4 >= availableWidth;
                    if (num4 == availableWidth)
                    {
                        length = index;
                    }
                }
                if (flag)
                {
                    if (length < 0)
                    {
                        if (!emergencyBreak)
                        {
                            TextRun run;
                            run2 = (TextRun) (run = null);
                            run1 = run;
                            return false;
                        }
                        length = this.EmergencyBreak(availableWidth);
                        startIndex = length;
                    }
                    else
                    {
                        startIndex = length + 1;
                    }
                }
                else
                {
                    length = index;
                }
            }
            char[] trimChars = new char[] { ' ' };
            string text = this.Text.Substring(0, length).TrimEnd(trimChars);
            run1 = null;
            if (text.Length > 0)
            {
                run1 = new TextRun(text, this.Font, this.ForeColor);
            }
            run2 = null;
            if (startIndex < this.Text.Length)
            {
                char[] chArray2 = new char[] { ' ' };
                string str2 = this.Text.Substring(startIndex).TrimStart(chArray2);
                if (str2.Length > 0)
                {
                    run2 = new TextRun(str2, this.Font, this.ForeColor);
                }
            }
            return true;
        }

        private int EmergencyBreak(int width)
        {
            int num2;
            int length = this.Text.Length;
            do
            {
                int num3;
                this.Font.ComputeExtent(this.Text.Substring(0, --length), out num2, out num3);
            }
            while ((num2 >= width) && (length > 1));
            return length;
        }

        public void GetSize(out int width, out int height)
        {
            if (this._width == 0)
            {
                this.Font.ComputeExtent(this.Text, out this._width, out this._height);
            }
            width = this._width;
            height = this._height;
        }

        public static TextRun EndOfLine
        {
            get
            {
                return new TextRun { IsEndOfLine = true };
            }
        }
    }
}

