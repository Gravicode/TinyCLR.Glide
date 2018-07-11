namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    public class TextRunCollection : ICollection, IEnumerable
    {
        private TextFlow _textFlow;
        private ArrayList _textRuns;

        internal TextRunCollection(TextFlow textFlow)
        {
            this._textFlow = textFlow;
            this._textRuns = new ArrayList();
        }

        public int Add(TextRun textRun)
        {
            if (textRun == null)
            {
                throw new ArgumentNullException("textRun");
            }
            this._textFlow.InvalidateMeasure();
            return this._textRuns.Add(textRun);
        }

        public int Add(string text, Font font, GHIElectronics.TinyCLR.UI.Media.Color foreColor)
        {
            return this.Add(new TextRun(text, font, foreColor));
        }

        public void Clear()
        {
            this._textRuns.Clear();
            this._textFlow.InvalidateMeasure();
        }

        public bool Contains(TextRun run)
        {
            return this._textRuns.Contains(run);
        }

        public void CopyTo(Array array, int index)
        {
            this._textRuns.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this._textRuns.GetEnumerator();
        }

        public int IndexOf(TextRun run)
        {
            return this._textRuns.IndexOf(run);
        }

        public void Insert(int index, TextRun run)
        {
            this._textRuns.Insert(index, run);
            this._textFlow.InvalidateMeasure();
        }

        public void Remove(TextRun run)
        {
            this._textRuns.Remove(run);
            this._textFlow.InvalidateMeasure();
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this._textRuns.Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            this._textRuns.RemoveAt(index);
            this._textFlow.InvalidateMeasure();
        }

        public int Count
        {
            get
            {
                return this._textRuns.Count;
            }
        }

        public TextRun this[int index]
        {
            get
            {
                return (TextRun) this._textRuns[index];
            }
            set
            {
                this._textRuns[index] = value;
                this._textFlow.InvalidateMeasure();
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return null;
            }
        }
    }
}

