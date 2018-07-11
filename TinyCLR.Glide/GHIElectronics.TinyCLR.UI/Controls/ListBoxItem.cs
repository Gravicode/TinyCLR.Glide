namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI.Input;
    using System;

    public class ListBoxItem : ContentControl
    {
        private bool _isSelectable = true;
        private ListBox _listBox;

        protected internal virtual void OnIsSelectedChanged(bool isSelected)
        {
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            if (this.IsSelectable)
            {
                this._listBox.SelectedItem = this;
            }
        }

        internal void SetListBox(ListBox listbox)
        {
            this._listBox = listbox;
            if (this.IsSelected && !this.IsSelectable)
            {
                this._listBox.SelectedIndex = -1;
            }
        }

        public bool IsSelected
        {
            get
            {
                return ((this._listBox != null) && (this._listBox.SelectedItem == this));
            }
        }

        public bool IsSelectable
        {
            get
            {
                return this._isSelectable;
            }
            set
            {
                base.VerifyAccess();
                if (this._isSelectable != value)
                {
                    this._isSelectable = value;
                    if (!value && this.IsSelected)
                    {
                        this._listBox.SelectedIndex = -1;
                    }
                }
            }
        }
    }
}

