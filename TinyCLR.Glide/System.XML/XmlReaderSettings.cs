namespace System.Xml
{
    using System;

    public class XmlReaderSettings
    {
        internal const uint NativeIgnoreWhitespace = 1;
        internal const uint NativeIgnoreProcessingInstructions = 2;
        internal const uint NativeIgnoreComments = 4;
        public XmlNameTable NameTable = null;
        public bool IgnoreWhitespace = false;
        public bool IgnoreProcessingInstructions = false;
        public bool IgnoreComments = false;

        internal uint GetSettings()
        {
            uint num = 0;
            if (this.IgnoreWhitespace)
            {
                num |= 1;
            }
            if (this.IgnoreProcessingInstructions)
            {
                num |= 2;
            }
            if (this.IgnoreComments)
            {
                num |= 4;
            }
            return num;
        }
    }
}

