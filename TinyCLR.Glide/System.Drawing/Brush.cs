namespace System.Drawing
{
    using System;

    public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
    {
        protected Brush()
        {
        }

        public abstract object Clone();
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~Brush()
        {
            this.Dispose(false);
        }
    }
}

