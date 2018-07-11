namespace System.Xml
{
    using System;

    public enum XmlNodeType
    {
        None,
        Element,
        Attribute,
        Text,
        CDATA,
        ProcessingInstruction,
        Comment,
        Whitespace,
        SignificantWhitespace,
        EndElement,
        XmlDeclaration
    }
}

