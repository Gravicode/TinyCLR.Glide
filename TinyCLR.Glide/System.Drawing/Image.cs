namespace System.Drawing
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;

    [Serializable]
    public abstract class Image : MarshalByRefObject, ICloneable, IDisposable
    {
        internal Graphics data;
        private bool disposed;

        protected Image()
        {
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.data.callFromImage = false;
                this.data.Dispose();
                this.disposed = true;
            }
        }

        ~Image()
        {
            this.Dispose(false);
        }

        public static Image FromStream(Stream stream)
        {
            return new Bitmap(stream);
        }

        public void Save(Stream stream, ImageFormat format)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            if (format != ImageFormat.MemoryBmp)
            {
                throw new ArgumentException("Only MemoryBmp supported.");
            }
            byte[] bitmap = this.data.surface.GetBitmap();
            stream.Seek(0L, SeekOrigin.Begin);
            stream.Write(bitmap, 0, bitmap.Length);
        }

        public int Width
        {
            get
            {
                return this.data.Width;
            }
        }

        public int Height
        {
            get
            {
                return this.data.Height;
            }
        }
    }
}

