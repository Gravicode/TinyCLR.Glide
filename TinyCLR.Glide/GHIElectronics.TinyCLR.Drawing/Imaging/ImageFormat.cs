// Decompiled with JetBrains decompiler
// Type: System.Drawing.Imaging.ImageFormat
// Assembly: GHIElectronics.TinyCLR.Drawing, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FB94FDB-AA94-43DE-9FD5-9C0BE64863BE
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.Drawing.dll

namespace System.Drawing.Imaging
{
    public sealed class ImageFormat
    {
        private static ImageFormat memoryBMP = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 170,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat bmp = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 171,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat emf = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 172,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat wmf = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 173,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat jpeg = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 174,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat png = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 175,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat gif = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 176,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat tiff = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 177,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat exif = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 178,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat photoCD = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 179,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat flashPIX = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 180,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));
        private static ImageFormat icon = new ImageFormat(new Guid(new byte[16]
        {
      (byte) 181,
      (byte) 60,
      (byte) 107,
      (byte) 185,
      (byte) 40,
      (byte) 7,
      (byte) 211,
      (byte) 17,
      (byte) 157,
      (byte) 123,
      (byte) 0,
      (byte) 0,
      (byte) 248,
      (byte) 30,
      (byte) 243,
      (byte) 46
        }));

        public ImageFormat(Guid guid)
        {
            this.Guid = guid;
        }

        public Guid Guid { get; }

        public static ImageFormat MemoryBmp
        {
            get
            {
                return ImageFormat.memoryBMP;
            }
        }

        public static ImageFormat Bmp
        {
            get
            {
                return ImageFormat.bmp;
            }
        }

        public static ImageFormat Emf
        {
            get
            {
                return ImageFormat.emf;
            }
        }

        public static ImageFormat Wmf
        {
            get
            {
                return ImageFormat.wmf;
            }
        }

        public static ImageFormat Gif
        {
            get
            {
                return ImageFormat.gif;
            }
        }

        public static ImageFormat Jpeg
        {
            get
            {
                return ImageFormat.jpeg;
            }
        }

        public static ImageFormat Png
        {
            get
            {
                return ImageFormat.png;
            }
        }

        public static ImageFormat Tiff
        {
            get
            {
                return ImageFormat.tiff;
            }
        }

        public static ImageFormat Exif
        {
            get
            {
                return ImageFormat.exif;
            }
        }

        public static ImageFormat Icon
        {
            get
            {
                return ImageFormat.icon;
            }
        }

        public override bool Equals(object o)
        {
            ImageFormat imageFormat;
            if ((imageFormat = o as ImageFormat) != null)
                return imageFormat.Guid == this.Guid;
            return false;
        }

        public override int GetHashCode()
        {
            return this.Guid.GetHashCode();
        }

        public override string ToString()
        {
            if (this == ImageFormat.memoryBMP)
                return "MemoryBMP";
            if (this == ImageFormat.bmp)
                return "Bmp";
            if (this == ImageFormat.emf)
                return "Emf";
            if (this == ImageFormat.wmf)
                return "Wmf";
            if (this == ImageFormat.gif)
                return "Gif";
            if (this == ImageFormat.jpeg)
                return "Jpeg";
            if (this == ImageFormat.png)
                return "Png";
            if (this == ImageFormat.tiff)
                return "Tiff";
            if (this == ImageFormat.exif)
                return "Exif";
            if (this == ImageFormat.icon)
                return "Icon";
            return "[ImageFormat: " + (object)this.Guid + "]";
        }
    }
}
