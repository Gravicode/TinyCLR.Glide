// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Controls.ListBox
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.UI.Input;
using System;

namespace GHIElectronics.TinyCLR.UI.Controls
{
    public class ListBox : ContentControl
    {
        private int _selectedIndex = -1;
        internal ScrollViewer _scrollViewer;
        internal StackPanel _panel;
        private SelectionChangedEventHandler _selectionChanged;
        private ListBoxItemCollection _items;

        public ListBox()
        {
            this._panel = new StackPanel();
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Child = (UIElement)this._panel;
            this._scrollViewer = scrollViewer;
            this.LogicalChildren.Add((UIElement)this._scrollViewer);
        }

        public ListBoxItemCollection Items
        {
            get
            {
                this.VerifyAccess();
                if (this._items == null)
                    this._items = new ListBoxItemCollection(this, this._panel.Children);
                return this._items;
            }
        }

        public event SelectionChangedEventHandler SelectionChanged
        {
            add
            {
                this.VerifyAccess();
                this._selectionChanged += value;
            }
            remove
            {
                this.VerifyAccess();
                this._selectionChanged -= value;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this._selectedIndex;
            }
            set
            {
                this.VerifyAccess();
                if (this._selectedIndex == value)
                    return;
                if (value < -1)
                    throw new ArgumentOutOfRangeException(nameof(SelectedIndex));
                ListBoxItem listBoxItem = this._items == null || value < 0 || value >= this._items.Count ? (ListBoxItem)null : this._items[value];
                if (listBoxItem != null && !listBoxItem.IsSelectable)
                    throw new InvalidOperationException("Item is not selectable");
                this.SelectedItem?.OnIsSelectedChanged(false);
                SelectionChangedEventArgs args = new SelectionChangedEventArgs(this._selectedIndex, value);
                this._selectedIndex = value;
                listBoxItem?.OnIsSelectedChanged(true);
                SelectionChangedEventHandler selectionChanged = this._selectionChanged;
                if (selectionChanged == null)
                    return;
                selectionChanged((object)this, args);
            }
        }

        public ListBoxItem SelectedItem
        {
            get
            {
                if (this._items != null && this._selectedIndex >= 0 && this._selectedIndex < this._items.Count)
                    return this._items[this._selectedIndex];
                return (ListBoxItem)null;
            }
            set
            {
                this.VerifyAccess();
                int num = this.Items.IndexOf(value);
                if (num == -1)
                    return;
                this.SelectedIndex = num;
            }
        }

        public void ScrollIntoView(ListBoxItem item)
        {
            this.VerifyAccess();
            if (!this.Items.Contains(item))
                return;
            int x1;
            int y1;
            this._panel.GetLayoutOffset(out x1, out y1);
            int x2;
            int y2;
            item.GetLayoutOffset(out x2, out y2);
            int num1 = y2 + y1;
            int num2 = num1 + item._renderHeight;
            if (num2 > this._scrollViewer._renderHeight)
                this._scrollViewer.VerticalOffset -= this._scrollViewer._renderHeight - num2;
            if (num1 >= 0)
                return;
            this._scrollViewer.VerticalOffset += num1;
        }

        protected override void OnButtonDown(ButtonEventArgs e)
        {
            if (e.Button == HardwareButton.Down && this._selectedIndex < this.Items.Count - 1)
            {
                int index = this._selectedIndex + 1;
                while (index < this.Items.Count && !this.Items[index].IsSelectable)
                    ++index;
                if (index >= this.Items.Count)
                    return;
                this.SelectedIndex = index;
                this.ScrollIntoView(this.SelectedItem);
                e.Handled = true;
            }
            else
            {
                if (e.Button != HardwareButton.Up || this._selectedIndex <= 0)
                    return;
                int index = this._selectedIndex - 1;
                while (index >= 0 && !this.Items[index].IsSelectable)
                    --index;
                if (index < 0)
                    return;
                this.SelectedIndex = index;
                this.ScrollIntoView(this.SelectedItem);
                e.Handled = true;
            }
        }

        public event ScrollChangedEventHandler ScrollChanged
        {
            add
            {
                this._scrollViewer.ScrollChanged += value;
            }
            remove
            {
                this._scrollViewer.ScrollChanged -= value;
            }
        }

        public int HorizontalOffset
        {
            get
            {
                return this._scrollViewer.HorizontalOffset;
            }
            set
            {
                this._scrollViewer.HorizontalOffset = value;
            }
        }

        public int VerticalOffset
        {
            get
            {
                return this._scrollViewer.VerticalOffset;
            }
            set
            {
                this._scrollViewer.VerticalOffset = value;
            }
        }

        public int ExtentHeight
        {
            get
            {
                return this._scrollViewer.ExtentHeight;
            }
        }

        public int ExtentWidth
        {
            get
            {
                return this._scrollViewer.ExtentWidth;
            }
        }

        public ScrollingStyle ScrollingStyle
        {
            get
            {
                return this._scrollViewer.ScrollingStyle;
            }
            set
            {
                this._scrollViewer.ScrollingStyle = value;
            }
        }
    }
}
