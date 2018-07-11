namespace GHIElectronics.TinyCLR.UI
{
    using System;
    using System.Drawing;
    using System.Resources;

    internal class Resources
    {
        private static System.Resources.ResourceManager manager;

        internal static System.Drawing.Bitmap GetBitmap(BitmapResources id)
        {
            return (System.Drawing.Bitmap) ResourceManager.GetObject((short) id);
        }

        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (manager == null)
                {
                    manager = new System.Resources.ResourceManager("GHIElectronics.TinyCLR.UI.Resources", typeof(Resources).Assembly);
                }
                return manager;
            }
        }

        [Serializable]
        internal enum BitmapResources : short
        {
            Keyboard_Numbers = -14062,
            Keyboard_Lowercase = -10522,
            Keyboard_Symbols = 0x616,
            Keyboard_Uppercase = 0x5354
        }
    }
}

