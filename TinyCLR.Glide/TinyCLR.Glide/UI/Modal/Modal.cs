////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Copyright (c) GHI Electronics, LLC.
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
//using Microsoft.SPOT;
//using Microsoft.SPOT.Presentation.Media;
using GHI.Glide.Display;
using System.Drawing;
using TinyCLR.Glide.Properties;

namespace GHI.Glide.UI
{
    /// <summary>
    /// The Modal component displays a box of text to instruct and inform the user.
    /// </summary>
    public class Modal : DisplayObjectContainer
    {
        /// <summary>
        /// Creates a new Modal.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="alpha">Alpha</param>
        /// <param name="x">X-axis position.</param>
        /// <param name="y">Y-axis position.</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public Modal(string name, ushort alpha, int x, int y, int width, int height)
        {
            Name = name;
            Alpha = alpha;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Renders the Modal onto it's parent container's graphics.
        /// </summary>
        public override void Render()
        {
            int x = Parent.X + X;
            int y = Parent.Y + Y;

            Parent.Graphics.Scale9Image(x, y, Width, Height, BackImage.GetInternalBitmap(), TitlebarHeight, Alpha);
            Parent.Graphics.DrawTextInRect(Title, x + 10, y + ((TitlebarHeight - TitleFont.Height) / 2), Width - 20, TitleFont.Height, System.Drawing.Internal.Bitmap.DT_AlignmentLeft, TitleFontColor, TitleFont);

            base.Render();
        }

        /// <summary>
        /// Background image.
        /// </summary>
        public Bitmap BackImage = Resources.GetBitmap(Resources.BitmapResources.Modal);

        /// <summary>
        /// Titlebar height.
        /// </summary>
        public int TitlebarHeight = 34;

        /// <summary>
        /// Title
        /// </summary>
        public string Title = String.Empty;

        /// <summary>
        /// Title font.
        /// </summary>
        public Font TitleFont = Resources.GetFont(Resources.FontResources.droid_reg12);

        /// <summary>
        /// Title font color.
        /// </summary>
        public Color TitleFontColor = TinyCLR.Glide.Ext.Colors.White;
    }
}