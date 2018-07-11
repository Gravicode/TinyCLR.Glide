// Decompiled with JetBrains decompiler
// Type: System.Drawing.Color
// Assembly: GHIElectronics.TinyCLR.Drawing, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FB94FDB-AA94-43DE-9FD5-9C0BE64863BE
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.Drawing.dll

using System.Diagnostics;
using System.Text;

namespace System.Drawing
{
    [DebuggerDisplay("{NameAndARGBValue}")]
    [Serializable]
    public struct Color
    {
        public static readonly Color Empty = new Color();
        private const int ARGBAlphaShift = 24;
        private const int ARGBRedShift = 0;
        private const int ARGBGreenShift = 8;
        private const int ARGBBlueShift = 16;
        internal readonly long value;

        public static Color Transparent { get; } = Color.FromArgb(0, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);

        public static Color Black { get; } = Color.FromArgb((int)byte.MaxValue, 0, 0, 0);

        public static Color White { get; } = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue);

        public static Color Gray { get; } = Color.FromArgb((int)byte.MaxValue, 128, 128, 128);

        public static Color Red { get; } = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 0, 0);

        public static Color Green { get; } = Color.FromArgb((int)byte.MaxValue, 0, 128, 0);

        public static Color Blue { get; } = Color.FromArgb((int)byte.MaxValue, 0, 0, (int)byte.MaxValue);

        public static Color Yellow { get; } = Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, 0);

        public static Color Purple { get; } = Color.FromArgb((int)byte.MaxValue, 128, 0, 128);

        public static Color Teal { get; } = Color.FromArgb((int)byte.MaxValue, 0, 128, 128);
       
        public static readonly Color Brown = new Color(2763429);
        public static readonly Color Cyan = new Color(16776960);
        public static readonly Color DarkGray = new Color(11119017);
    
        public static readonly Color LightGray = new Color(13882323);
        public static readonly Color Magenta = new Color(16711935);
        public static readonly Color Orange = new Color(42495);
        
        /// <summary>
        /// Fushia
        /// </summary>
        public static readonly Color Fuchsia = Color.FromArgb(255, 0, 255);
        
        public Color(long value)
        {
            this.value = value;
        }

        public byte R
        {
            get
            {
                return (byte)((ulong)this.value & (ulong)byte.MaxValue);
            }
        }

        public byte G
        {
            get
            {
                return (byte)((ulong)(this.value >> 8) & (ulong)byte.MaxValue);
            }
        }

        public byte B
        {
            get
            {
                return (byte)((ulong)(this.value >> 16) & (ulong)byte.MaxValue);
            }
        }

        public byte A
        {
            get
            {
                return (byte)((ulong)(this.value >> 24) & (ulong)byte.MaxValue);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        private string NameAndARGBValue
        {
            get
            {
                return string.Format("ARGB=({0}, {1}, {2}, {3})", (object)this.A, (object)this.R, (object)this.R, (object)this.B);
            }
        }

        public string Name
        {
            get
            {
                return this.value.ToString("x");
            }
        }

        private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
        {
            return (long)(uint)((int)red | (int)green << 8 | (int)blue << 16 | (int)alpha << 24) & (long)uint.MaxValue;
        }

        public static Color FromArgb(int argb)
        {
            return new Color((long)argb & (long)uint.MaxValue);
        }

        public static Color FromArgb(int red, int green, int blue)
        {
            return Color.FromArgb((int)byte.MaxValue, red, green, blue);
        }

        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            if (alpha < 0 || alpha > (int)byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(alpha));
            if (red < 0 || red > (int)byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(red));
            if (green < 0 || green > (int)byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(green));
            if (blue < 0 || blue > (int)byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(blue));
            return new Color(Color.MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue));
        }

        public static Color FromArgb(int alpha, Color baseColor)
        {
            if (alpha < 0 || alpha > (int)byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(alpha));
            return new Color(Color.MakeArgb((byte)alpha, baseColor.R, baseColor.G, baseColor.B));
        }

        public float GetBrightness()
        {
            double num1 = (double)this.R / (double)byte.MaxValue;
            float num2 = (float)this.G / (float)byte.MaxValue;
            float num3 = (float)this.B / (float)byte.MaxValue;
            float num4 = (float)num1;
            float num5 = (float)num1;
            if ((double)num2 > (double)num4)
                num4 = num2;
            if ((double)num3 > (double)num4)
                num4 = num3;
            if ((double)num2 < (double)num5)
                num5 = num2;
            if ((double)num3 < (double)num5)
                num5 = num3;
            return (float)(((double)num4 + (double)num5) / 2.0);
        }

        public float GetHue()
        {
            if ((int)this.R == (int)this.G && (int)this.G == (int)this.B)
                return 0.0f;
            float num1 = (float)this.R / (float)byte.MaxValue;
            float num2 = (float)this.G / (float)byte.MaxValue;
            float num3 = (float)this.B / (float)byte.MaxValue;
            float num4 = 0.0f;
            float num5 = num1;
            float num6 = num1;
            if ((double)num2 > (double)num5)
                num5 = num2;
            if ((double)num3 > (double)num5)
                num5 = num3;
            if ((double)num2 < (double)num6)
                num6 = num2;
            if ((double)num3 < (double)num6)
                num6 = num3;
            float num7 = num5 - num6;
            if ((double)num1 == (double)num5)
                num4 = (num2 - num3) / num7;
            else if ((double)num2 == (double)num5)
                num4 = (float)(2.0 + ((double)num3 - (double)num1) / (double)num7);
            else if ((double)num3 == (double)num5)
                num4 = (float)(4.0 + ((double)num1 - (double)num2) / (double)num7);
            float num8 = num4 * 60f;
            if ((double)num8 < 0.0)
                num8 += 360f;
            return num8;
        }

        public float GetSaturation()
        {
            double num1 = (double)this.R / (double)byte.MaxValue;
            float num2 = (float)this.G / (float)byte.MaxValue;
            float num3 = (float)this.B / (float)byte.MaxValue;
            float num4 = 0.0f;
            float num5 = (float)num1;
            float num6 = (float)num1;
            if ((double)num2 > (double)num5)
                num5 = num2;
            if ((double)num3 > (double)num5)
                num5 = num3;
            if ((double)num2 < (double)num6)
                num6 = num2;
            if ((double)num3 < (double)num6)
                num6 = num3;
            if ((double)num5 != (double)num6)
                num4 = ((double)num5 + (double)num6) / 2.0 > 0.5 ? (float)(((double)num5 - (double)num6) / (2.0 - (double)num5 - (double)num6)) : (float)(((double)num5 - (double)num6) / ((double)num5 + (double)num6));
            return num4;
        }

        public int ToArgb()
        {
            return (int)this.value;
        }

        internal int ToRgb()
        {
            return (int)this.value & 16777215;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(32);
            stringBuilder.Append(this.GetType().Name);
            stringBuilder.Append(" [");
            stringBuilder.Append("A=");
            stringBuilder.Append(this.A);
            stringBuilder.Append(", R=");
            stringBuilder.Append(this.R);
            stringBuilder.Append(", G=");
            stringBuilder.Append(this.G);
            stringBuilder.Append(", B=");
            stringBuilder.Append(this.B);
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        public static bool operator ==(Color left, Color right)
        {
            return left.value == right.value;
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Color)
                return this.value == ((Color)obj).value;
            return false;
        }

        public uint ToNativeColor()
        {
            return (uint)((this.R | (this.G << 8)) | (this.B << 0x10));
        }

        internal ushort ToNativeAlpha()
        {
            return (ushort)this.A;
        }
    }
}
