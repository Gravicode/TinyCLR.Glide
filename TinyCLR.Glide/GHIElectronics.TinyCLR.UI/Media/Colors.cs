// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Media.Colors
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

namespace GHIElectronics.TinyCLR.UI.Media
{
    public sealed class Colors
    {
        public static Color Transparent { get; } = Color.FromArgb((byte)0, byte.MaxValue, byte.MaxValue, byte.MaxValue);

        public static Color Black { get; } = Color.FromArgb(byte.MaxValue, (byte)0, (byte)0, (byte)0);

        public static Color White { get; } = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

        public static Color Gray { get; } = Color.FromArgb(byte.MaxValue, (byte)128, (byte)128, (byte)128);

        public static Color Red { get; } = Color.FromArgb(byte.MaxValue, byte.MaxValue, (byte)0, (byte)0);

        public static Color Green { get; } = Color.FromArgb(byte.MaxValue, (byte)0, (byte)128, (byte)0);

        public static Color Blue { get; } = Color.FromArgb(byte.MaxValue, (byte)0, (byte)0, byte.MaxValue);

        public static Color Yellow { get; } = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)0);

        public static Color Purple { get; } = Color.FromArgb(byte.MaxValue, (byte)128, (byte)0, (byte)128);

        public static Color Teal { get; } = Color.FromArgb(byte.MaxValue, (byte)0, (byte)128, (byte)128);
    }
}
