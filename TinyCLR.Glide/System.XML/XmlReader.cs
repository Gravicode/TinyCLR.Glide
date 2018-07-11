// Decompiled with JetBrains decompiler
// Type: System.Xml.XmlReader
// Assembly: System.Xml, Version=4.3.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 333D9AE1-C644-4FBA-B9E6-1778C5F29BED
// Assembly location: C:\Program Files (x86)\Microsoft .NET Micro Framework\v4.3\Assemblies\le\System.Xml.dll

using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Xml
{
    public class XmlReader : IDisposable
    {
        private static readonly string Xml = "xml";
        private static readonly string Xmlns = "xmlns";
        private static readonly string XmlNamespace = "http://www.w3.org/XML/1998/namespace";
        private static readonly string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";
        private const int BufferSize = 1024;
        private const uint HasValueBitmap = 508;
        private const uint IsTextualNodeBitmap = 204;
        private byte[] _buffer;
        private int _offset;
        private int _length;
        private Stack _xmlNodes;
        private Stack _namespaces;
        private Stack _xmlSpaces;
        private Stack _xmlLangs;
        private XmlNodeType _nodeType;
        private string _value;
        private bool _isEmptyElement;
        private ArrayList _attributes;
        private XmlNameTable _nameTable;
        private object _state;
        private bool _isDone;
        private int _currentAttribute;
        private Stream _stream;
        private ReadState _readState;

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void Initialize(uint settings);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int ReadInternal(uint options);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern bool StringRefEquals(string str1, string str2);

        public virtual XmlNodeType NodeType
        {
            get
            {
                if (this._currentAttribute < 0)
                    return this._nodeType;
                return XmlNodeType.Attribute;
            }
        }

        public virtual string Name
        {
            get
            {
                if (this._currentAttribute >= 0)
                    return ((XmlReader_XmlAttribute)this._attributes[this._currentAttribute]).Name;
                return ((XmlReader_XmlNode)this._xmlNodes.Peek()).Name;
            }
        }

        public virtual string LocalName
        {
            get
            {
                if (this._currentAttribute >= 0)
                    return ((XmlReader_XmlAttribute)this._attributes[this._currentAttribute]).LocalName;
                return ((XmlReader_XmlNode)this._xmlNodes.Peek()).LocalName;
            }
        }

        public virtual string NamespaceURI
        {
            get
            {
                if (this._currentAttribute >= 0)
                    return ((XmlReader_XmlAttribute)this._attributes[this._currentAttribute]).NamespaceURI;
                return ((XmlReader_XmlNode)this._xmlNodes.Peek()).NamespaceURI;
            }
        }

        public virtual string Prefix
        {
            get
            {
                if (this._currentAttribute >= 0)
                    return ((XmlReader_XmlAttribute)this._attributes[this._currentAttribute]).Prefix;
                return ((XmlReader_XmlNode)this._xmlNodes.Peek()).Prefix;
            }
        }

        public virtual bool HasValue
        {
            get
            {
                return 0L != (508L & (long)(1 << (int)(this.NodeType & (XmlNodeType)31)));
            }
        }

        public virtual string Value
        {
            get
            {
                if (this._currentAttribute >= 0)
                    return ((XmlReader_XmlAttribute)this._attributes[this._currentAttribute]).Value;
                return this._value;
            }
        }

        public virtual int Depth
        {
            get
            {
                if (this._currentAttribute >= 0)
                    return this._xmlNodes.Count;
                return this._xmlNodes.Count - 1;
            }
        }

        public virtual bool IsEmptyElement
        {
            get
            {
                if (this._currentAttribute != -1)
                    return false;
                return this._isEmptyElement;
            }
        }

        public virtual XmlSpace XmlSpace
        {
            get
            {
                return (XmlSpace)this._xmlSpaces.Peek();
            }
        }

        public virtual string XmlLang
        {
            get
            {
                return (string)this._xmlLangs.Peek();
            }
        }

        public virtual int AttributeCount
        {
            get
            {
                return this._attributes.Count;
            }
        }

        public virtual string GetAttribute(string name)
        {
            int attribute = this.FindAttribute(name);
            if (attribute >= 0)
                return ((XmlReader_XmlAttribute)this._attributes[attribute]).Value;
            return (string)null;
        }

        private int FindAttribute(string name)
        {
            name = this._nameTable.Get(name);
            if (name != null)
            {
                int count = this._attributes.Count;
                for (int index = 0; index < count; ++index)
                {
                    if (object.ReferenceEquals((object)name, (object)((XmlReader_XmlAttribute)this._attributes[index]).Name))
                        return index;
                }
            }
            return -1;
        }

        private int FindAttribute(string localName, string namespaceURI)
        {
            localName = this._nameTable.Get(localName);
            namespaceURI = this._nameTable.Get(namespaceURI);
            if (localName != null && namespaceURI != null)
            {
                int count = this._attributes.Count;
                for (int index = 0; index < count; ++index)
                {
                    XmlReader_XmlAttribute attribute = (XmlReader_XmlAttribute)this._attributes[index];
                    if (object.ReferenceEquals((object)localName, (object)attribute.LocalName) && object.ReferenceEquals((object)namespaceURI, (object)attribute.NamespaceURI))
                        return index;
                }
            }
            return -1;
        }

        public virtual string GetAttribute(string localName, string namespaceURI)
        {
            int attribute = this.FindAttribute(localName, namespaceURI);
            if (attribute >= 0)
                return ((XmlReader_XmlAttribute)this._attributes[attribute]).Value;
            return (string)null;
        }

        public virtual string GetAttribute(int i)
        {
            return ((XmlReader_XmlAttribute)this._attributes[i]).Value;
        }

        public virtual bool MoveToAttribute(string name)
        {
            int attribute = this.FindAttribute(name);
            if (attribute < 0)
                return false;
            this._currentAttribute = attribute;
            return true;
        }

        public virtual bool MoveToAttribute(string localName, string namespaceURI)
        {
            int attribute = this.FindAttribute(localName, namespaceURI);
            if (attribute < 0)
                return false;
            this._currentAttribute = attribute;
            return true;
        }

        public virtual void MoveToAttribute(int i)
        {
            if (i < 0 || i >= this._attributes.Count)
                throw new ArgumentOutOfRangeException();
            this._currentAttribute = i;
        }

        public virtual bool MoveToFirstAttribute()
        {
            if (this._attributes.Count <= 0)
                return false;
            this._currentAttribute = 0;
            return true;
        }

        public virtual bool MoveToNextAttribute()
        {
            if (this._currentAttribute + 1 >= this._attributes.Count)
                return false;
            ++this._currentAttribute;
            return true;
        }

        public virtual bool MoveToElement()
        {
            if (this._currentAttribute < 0)
                return false;
            this._currentAttribute = -1;
            return true;
        }

        public virtual bool Read()
        {
            if (this._readState == ReadState.Initial)
                this._readState = ReadState.Interactive;
            if (this._readState == ReadState.Interactive)
            {
                if (this._currentAttribute >= 0)
                {
                    if (this.MoveToNextAttribute())
                        return true;
                    this._currentAttribute = -1;
                }
                if (this._length == 0)
                {
                    this._offset = 0;
                    this._length = this._stream.Read(this._buffer, 0, 1024);
                    if (this._length == 0)
                    {
                        if (this._isDone)
                        {
                            this.CleanUp(ReadState.EndOfFile);
                            return false;
                        }
                        this._readState = ReadState.Error;
                        throw new XmlException(XmlException.XmlExceptionErrorCode.UnexpectedEOF);
                    }
                }
                int num1;
                while (true)
                {
                    num1 = this.ReadInternal(0U);
                    switch (num1)
                    {
                        case -805306368:
                            goto label_13;
                        case -788529152:
                            int offset = this._offset + this._length;
                            int num2 = this._stream.Read(this._buffer, offset, 1024 - offset);
                            if (num2 != 0)
                            {
                                this._length += num2;
                                continue;
                            }
                            goto label_15;
                        default:
                            goto label_19;
                    }
                }
                label_13:
                return true;
                label_15:
                this.ReadInternal(0U);
                if (this._isDone)
                {
                    this.CleanUp(ReadState.EndOfFile);
                    return false;
                }
                this._readState = ReadState.Error;
                throw new XmlException(XmlException.XmlExceptionErrorCode.UnexpectedEOF);
                label_19:
                this._readState = ReadState.Error;
                throw new XmlException((XmlException.XmlExceptionErrorCode)num1);
            }
            return false;
        }

        public virtual bool EOF
        {
            get
            {
                return this._readState == ReadState.EndOfFile;
            }
        }

        public virtual void Close()
        {
            this.CleanUp(ReadState.Closed);
            this._stream.Close();
        }

        private void CleanUp(ReadState readState)
        {
            this._xmlSpaces.Clear();
            this._xmlLangs.Clear();
            this._xmlNodes.Clear();
            this._attributes.Clear();
            this._namespaces.Clear();
            this._value = string.Empty;
            this._isEmptyElement = false;
            this._nodeType = XmlNodeType.None;
            this._currentAttribute = -1;
            this._readState = readState;
            this._xmlSpaces.Push((object)0);
            this._xmlLangs.Push((object)string.Empty);
            this._xmlNodes.Push((object)new XmlReader_XmlNode());
        }

        public virtual ReadState ReadState
        {
            get
            {
                return this._readState;
            }
        }

        public virtual void Skip()
        {
            if (this._readState != ReadState.Interactive)
                return;
            if (this._currentAttribute >= 0)
                this._currentAttribute = -1;
            if (this._nodeType == XmlNodeType.Element && !this._isEmptyElement)
            {
                int count = this._xmlNodes.Count;
                do
                    ;
                while (this.Read() && count < this._xmlNodes.Count);
                if (this._nodeType != XmlNodeType.EndElement)
                    return;
                this.Read();
            }
            else
                this.Read();
        }

        public virtual XmlNameTable NameTable
        {
            get
            {
                return this._nameTable;
            }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public virtual extern string LookupNamespace(string prefix);

        public virtual string ReadString()
        {
            if (this._readState != ReadState.Interactive)
                return "";
            if (this._currentAttribute >= 0)
                this._currentAttribute = -1;
            if (this._nodeType == XmlNodeType.Element)
            {
                if (this._isEmptyElement)
                    return "";
                if (!this.Read())
                    throw new InvalidOperationException();
                if (this._nodeType == XmlNodeType.EndElement)
                    return "";
            }
            string str = "";
            while ((204L & (long)(1 << (int)(this._nodeType & (XmlNodeType)31))) != 0L)
            {
                str += this._value;
                if (!this.Read())
                    throw new InvalidOperationException();
            }
            return str;
        }

        public virtual XmlNodeType MoveToContent()
        {
            if (this._currentAttribute >= 0)
            {
                this._currentAttribute = -1;
                return this._nodeType;
            }
            do
            {
                switch (this._nodeType)
                {
                    case XmlNodeType.Element:
                    case XmlNodeType.Text:
                    case XmlNodeType.CDATA:
                    case XmlNodeType.EndElement:
                        return this._nodeType;
                    default:
                        continue;
                }
            }
            while (this.Read());
            return XmlNodeType.None;
        }

        public virtual void ReadStartElement()
        {
            if (this.MoveToContent() != XmlNodeType.Element)
                throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            this.Read();
        }

        public virtual void ReadStartElement(string name)
        {
            if (this.MoveToContent() != XmlNodeType.Element)
                throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            if (!XmlReader.StringRefEquals(((XmlReader_XmlNode)this._xmlNodes.Peek()).Name, name))
                throw new XmlException(XmlException.XmlExceptionErrorCode.ElementNotFound);
            this.Read();
        }

        public virtual void ReadStartElement(string localname, string ns)
        {
            if (this.MoveToContent() != XmlNodeType.Element)
                throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            XmlReader_XmlNode xmlReaderXmlNode = (XmlReader_XmlNode)this._xmlNodes.Peek();
            if (!XmlReader.StringRefEquals(xmlReaderXmlNode.LocalName, localname) || !XmlReader.StringRefEquals(xmlReaderXmlNode.NamespaceURI, ns))
                throw new XmlException(XmlException.XmlExceptionErrorCode.ElementNotFound);
            this.Read();
        }

        public virtual string ReadElementString()
        {
            string str = "";
            if (this.MoveToContent() != XmlNodeType.Element)
                throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            if (!this._isEmptyElement)
            {
                str = this.ReadString();
                if (this._nodeType != XmlNodeType.EndElement)
                    throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            }
            this.Read();
            return str;
        }

        public virtual string ReadElementString(string name)
        {
            string str = "";
            if (this.MoveToContent() != XmlNodeType.Element)
                throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            if (!XmlReader.StringRefEquals(((XmlReader_XmlNode)this._xmlNodes.Peek()).Name, name))
                throw new XmlException(XmlException.XmlExceptionErrorCode.ElementNotFound);
            if (!this._isEmptyElement)
            {
                str = this.ReadString();
                if (this._nodeType != XmlNodeType.EndElement)
                    throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            }
            this.Read();
            return str;
        }

        public virtual string ReadElementString(string localname, string ns)
        {
            string str = "";
            if (this.MoveToContent() != XmlNodeType.Element)
                throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            XmlReader_XmlNode xmlReaderXmlNode = (XmlReader_XmlNode)this._xmlNodes.Peek();
            if (!XmlReader.StringRefEquals(xmlReaderXmlNode.LocalName, localname) || !XmlReader.StringRefEquals(xmlReaderXmlNode.NamespaceURI, ns))
                throw new XmlException(XmlException.XmlExceptionErrorCode.ElementNotFound);
            if (!this._isEmptyElement)
            {
                str = this.ReadString();
                if (this._nodeType != XmlNodeType.EndElement)
                    throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            }
            this.Read();
            return str;
        }

        public virtual void ReadEndElement()
        {
            if (this.MoveToContent() != XmlNodeType.EndElement)
                throw new XmlException(XmlException.XmlExceptionErrorCode.InvalidNodeType);
            this.Read();
        }

        public virtual bool IsStartElement()
        {
            return this.MoveToContent() == XmlNodeType.Element;
        }

        public virtual bool IsStartElement(string name)
        {
            if (this.MoveToContent() == XmlNodeType.Element)
                return XmlReader.StringRefEquals(((XmlReader_XmlNode)this._xmlNodes.Peek()).Name, name);
            return false;
        }

        public virtual bool IsStartElement(string localname, string ns)
        {
            if (this.MoveToContent() != XmlNodeType.Element)
                return false;
            XmlReader_XmlNode xmlReaderXmlNode = (XmlReader_XmlNode)this._xmlNodes.Peek();
            if (XmlReader.StringRefEquals(xmlReaderXmlNode.LocalName, localname))
                return XmlReader.StringRefEquals(xmlReaderXmlNode.NamespaceURI, ns);
            return false;
        }

        public virtual bool ReadToFollowing(string name)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentException();
            name = this._nameTable.Add(name);
            if (this._currentAttribute >= 0)
                this._currentAttribute = -1;
            while (this.Read())
            {
                if (this._nodeType == XmlNodeType.Element && object.ReferenceEquals((object)((XmlReader_XmlNode)this._xmlNodes.Peek()).Name, (object)name))
                    return true;
            }
            return false;
        }

        public virtual bool ReadToFollowing(string localName, string namespaceURI)
        {
            if (localName == null || localName.Length == 0 || namespaceURI == null)
                throw new ArgumentException();
            localName = this.NameTable.Add(localName);
            namespaceURI = this.NameTable.Add(namespaceURI);
            if (this._currentAttribute >= 0)
                this._currentAttribute = -1;
            while (this.Read())
            {
                if (this._nodeType == XmlNodeType.Element)
                {
                    XmlReader_XmlNode xmlReaderXmlNode = (XmlReader_XmlNode)this._xmlNodes.Peek();
                    if (object.ReferenceEquals((object)xmlReaderXmlNode.LocalName, (object)localName) && object.ReferenceEquals((object)xmlReaderXmlNode.NamespaceURI, (object)namespaceURI))
                        return true;
                }
            }
            return false;
        }

        public virtual bool ReadToDescendant(string name)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentException();
            if (this._currentAttribute >= 0)
                return false;
            int count = this._xmlNodes.Count;
            if (this._nodeType != XmlNodeType.Element)
            {
                if (this._readState != ReadState.Initial)
                    return false;
                --count;
            }
            else if (this._isEmptyElement)
                return false;
            name = this._nameTable.Add(name);
            while (this.Read() && this._xmlNodes.Count > count)
            {
                if (this._nodeType == XmlNodeType.Element && object.ReferenceEquals((object)name, (object)((XmlReader_XmlNode)this._xmlNodes.Peek()).Name))
                    return true;
            }
            return false;
        }

        public virtual bool ReadToDescendant(string localName, string namespaceURI)
        {
            if (localName == null || localName.Length == 0 || namespaceURI == null)
                throw new ArgumentException();
            if (this._currentAttribute >= 0)
                return false;
            int count = this._xmlNodes.Count;
            if (this._nodeType != XmlNodeType.Element)
            {
                if (this._readState != ReadState.Initial)
                    return false;
                --count;
            }
            else if (this._isEmptyElement)
                return false;
            localName = this._nameTable.Add(localName);
            namespaceURI = this._nameTable.Add(namespaceURI);
            while (this.Read() && this._xmlNodes.Count > count)
            {
                if (this._nodeType == XmlNodeType.Element)
                {
                    XmlReader_XmlNode xmlReaderXmlNode = (XmlReader_XmlNode)this._xmlNodes.Peek();
                    if (object.ReferenceEquals((object)localName, (object)xmlReaderXmlNode.LocalName) && object.ReferenceEquals((object)namespaceURI, (object)xmlReaderXmlNode.NamespaceURI))
                        return true;
                }
            }
            return false;
        }

        public virtual bool ReadToNextSibling(string name)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentException();
            name = this.NameTable.Add(name);
            do
            {
                this.Skip();
                if (this._nodeType == XmlNodeType.Element && object.ReferenceEquals((object)name, (object)((XmlReader_XmlNode)this._xmlNodes.Peek()).Name))
                    return true;
            }
            while (this._nodeType != XmlNodeType.EndElement && this._readState != ReadState.EndOfFile);
            return false;
        }

        public virtual bool ReadToNextSibling(string localName, string namespaceURI)
        {
            if (localName == null || localName.Length == 0 || namespaceURI == null)
                throw new ArgumentException();
            localName = this._nameTable.Add(localName);
            namespaceURI = this._nameTable.Add(namespaceURI);
            do
            {
                this.Skip();
                if (this._nodeType == XmlNodeType.Element)
                {
                    XmlReader_XmlNode xmlReaderXmlNode = (XmlReader_XmlNode)this._xmlNodes.Peek();
                    if (object.ReferenceEquals((object)this.LocalName, (object)xmlReaderXmlNode.LocalName) && object.ReferenceEquals((object)namespaceURI, (object)xmlReaderXmlNode.NamespaceURI))
                        return true;
                }
            }
            while (this._nodeType != XmlNodeType.EndElement && this._readState != ReadState.EndOfFile);
            return false;
        }

        public virtual bool HasAttributes
        {
            get
            {
                return this._attributes.Count > 0;
            }
        }

        public void Dispose()
        {
            if (this.ReadState == ReadState.Closed)
                return;
            this.Close();
        }

        private XmlReader()
        {
        }

        public static XmlReader Create(Stream input)
        {
            return XmlReader.Create(input, (XmlReaderSettings)null);
        }

        public static XmlReader Create(Stream input, XmlReaderSettings settings)
        {
            if (input == null)
                throw new ArgumentNullException();
            if (settings == null)
                settings = new XmlReaderSettings();
            XmlReader xmlReader = new XmlReader();
            xmlReader._xmlSpaces = new Stack();
            xmlReader._xmlLangs = new Stack();
            xmlReader._xmlNodes = new Stack();
            xmlReader._attributes = new ArrayList();
            xmlReader._namespaces = new Stack();
            xmlReader._value = string.Empty;
            xmlReader._nodeType = XmlNodeType.None;
            xmlReader._isEmptyElement = false;
            xmlReader._isDone = false;
            xmlReader._buffer = new byte[1024];
            xmlReader._offset = 0;
            xmlReader._length = 0;
            xmlReader._stream = input;
            xmlReader._readState = ReadState.Initial;
            xmlReader._nameTable = settings.NameTable == null ? new XmlNameTable() : settings.NameTable;
            xmlReader._xmlNodes.Push((object)new XmlReader_XmlNode());
            xmlReader._xmlSpaces.Push((object)0);
            xmlReader._xmlLangs.Push((object)string.Empty);
            xmlReader._namespaces.Push((object)new XmlReader_NamespaceEntry()
            {
                Prefix = string.Empty,
                NamespaceURI = string.Empty
            });
            xmlReader._namespaces.Push((object)new XmlReader_NamespaceEntry()
            {
                Prefix = xmlReader._nameTable.Add(XmlReader.Xml),
                NamespaceURI = xmlReader._nameTable.Add(XmlReader.XmlNamespace)
            });
            xmlReader._namespaces.Push((object)new XmlReader_NamespaceEntry()
            {
                Prefix = xmlReader._nameTable.Add(XmlReader.Xmlns),
                NamespaceURI = xmlReader._nameTable.Add(XmlReader.XmlnsNamespace)
            });
            xmlReader._currentAttribute = -1;
            xmlReader.Initialize(settings.GetSettings());
            return xmlReader;
        }
    }
}
