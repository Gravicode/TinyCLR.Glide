namespace System.Xml
{
    using System;
    using System.Runtime.CompilerServices;

    public class XmlNameTable
    {
        private XmlNameTable_Entry[] _entries;
        private int _count;
        private int _mask = 0x1f;
        private int _hashCodeRandomizer;
        private object _tmp;

        public XmlNameTable()
        {
            this._entries = new XmlNameTable_Entry[this._mask + 1];
            this._hashCodeRandomizer = new Random().Next();
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public virtual extern string Add(string key);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public virtual extern string Get(string value);
    }
}

