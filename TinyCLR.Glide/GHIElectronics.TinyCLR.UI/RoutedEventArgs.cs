namespace GHIElectronics.TinyCLR.UI
{
    using System;

    public class RoutedEventArgs : EventArgs
    {
        internal GHIElectronics.TinyCLR.UI.RoutedEvent _routedEvent;
        internal object _source;
        private object _originalSource;
        private Flags _flags;

        public RoutedEventArgs()
        {
        }

        public RoutedEventArgs(GHIElectronics.TinyCLR.UI.RoutedEvent routedEvent) : this(routedEvent, null)
        {
        }

        public RoutedEventArgs(GHIElectronics.TinyCLR.UI.RoutedEvent routedEvent, object source)
        {
            this._routedEvent = routedEvent;
            this._source = this._originalSource = source;
        }

        internal void InvokeHandler(RouteItem routeItem)
        {
            RoutedEventHandlerInfo info = routeItem._routedEventHandlerInfo;
            if (!this.Handled || info._handledEventsToo)
            {
                RoutedEventHandler handler = info._handler;
                this._flags |= Flags.InvokingHandler;
                try
                {
                    handler(routeItem._target, this);
                }
                finally
                {
                    this._flags &= ~Flags.InvokingHandler;
                }
            }
        }

        protected virtual void OnSetSource(object source)
        {
        }

        public GHIElectronics.TinyCLR.UI.RoutedEvent RoutedEvent
        {
            get
            {
                return this._routedEvent;
            }
            set
            {
                if ((this._flags & Flags.InvokingHandler) != ((Flags) 0))
                {
                    throw new InvalidOperationException();
                }
                this._routedEvent = value;
            }
        }

        public bool Handled
        {
            get
            {
                return ((this._flags & Flags.Handled) > ((Flags) 0));
            }
            set
            {
                if (this._routedEvent == null)
                {
                    throw new InvalidOperationException();
                }
                this._flags |= Flags.Handled;
            }
        }

        public object Source
        {
            get
            {
                return this._source;
            }
            set
            {
                if ((this._flags & Flags.InvokingHandler) != ((Flags) 0))
                {
                    throw new InvalidOperationException();
                }
                if (this._routedEvent == null)
                {
                    throw new InvalidOperationException();
                }
                object source = value;
                if ((this._source == null) && (this._originalSource == null))
                {
                    this._source = this._originalSource = source;
                    this.OnSetSource(source);
                }
                else if (this._source != source)
                {
                    this._source = source;
                    this.OnSetSource(source);
                }
            }
        }

        public object OriginalSource
        {
            get
            {
                return this._originalSource;
            }
        }

        [Flags]
        private enum Flags : uint
        {
            Handled = 1,
            InvokingHandler = 2
        }
    }
}

