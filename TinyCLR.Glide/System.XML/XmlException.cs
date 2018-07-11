namespace System.Xml
{
    using System;

    public class XmlException : Exception
    {
        public XmlException()
        {
        }

        public XmlException(string message) : base(message)
        {
        }

        public XmlException(XmlExceptionErrorCode errorCode)
        {
            base.m_HResult = (int) errorCode;
        }

        public XmlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public XmlExceptionErrorCode ErrorCode
        {
            get
            {
                return (XmlExceptionErrorCode) base.m_HResult;
            }
        }

        public enum XmlExceptionErrorCode
        {
            Others = -771751936,
            UnexpectedEOF = -754974720,
            BadNameChar = -738197504,
            UnknownEncoding = -721420288,
            UnexpectedToken = -704643072,
            TagMismatch = -687865856,
            UnexpectedEndTag = -671088640,
            BadAttributeChar = -654311424,
            MultipleRoots = -637534208,
            InvalidRootData = -620756992,
            XmlDeclNotFirst = -603979776,
            InvalidXmlDecl = -587202560,
            InvalidXmlSpace = -570425344,
            DupAttributeName = -553648128,
            InvalidCharacter = -536870912,
            CDATAEndInText = -520093696,
            InvalidCommentChars = -503316480,
            LimitExceeded = -486539264,
            BadOrUnsupportedEntity = -469762048,
            UndeclaredNamespace = -452984832,
            InvalidXmlPrefixMapping = -436207616,
            NamespaceDeclXmlXmlns = -419430400,
            InvalidPIName = -402653184,
            DTDIsProhibited = -385875968,
            EmptyName = -369098752,
            InvalidNodeType = -352321536,
            ElementNotFound = -335544320
        }

        internal enum XmlExceptionErrorCodeInternal
        {
            ReturnToManagedCode = -805306368,
            NeedMoreData = -788529152
        }
    }
}

