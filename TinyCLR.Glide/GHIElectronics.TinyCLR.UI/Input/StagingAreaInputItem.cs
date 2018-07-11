namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;
    using System.Collections;

    public class StagingAreaInputItem
    {
        public readonly InputEventArgs Input;
        private Hashtable _table;

        internal StagingAreaInputItem(InputEventArgs input, StagingAreaInputItem promote)
        {
            this.Input = input;
            if ((promote != null) && (promote._table != null))
            {
                this._table = (Hashtable) promote._table.Clone();
            }
        }

        public object GetData(object key)
        {
            if (this._table == null)
            {
                return null;
            }
            return this._table[key];
        }

        public void SetData(object key, object value)
        {
            if (this._table == null)
            {
                this._table = new Hashtable();
            }
            this._table[key] = value;
        }
    }
}

