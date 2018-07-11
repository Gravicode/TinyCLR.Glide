namespace GHIElectronics.TinyCLR.UI
{
    using System;

    public class RoutedEventHandlerInfo
    {
        internal RoutedEventHandler _handler;
        internal bool _handledEventsToo;

        internal RoutedEventHandlerInfo(RoutedEventHandler handler, bool handledEventsToo)
        {
            this._handler = handler;
            this._handledEventsToo = handledEventsToo;
        }

        public bool Equals(RoutedEventHandlerInfo handlerInfo)
        {
            return ((this._handler == handlerInfo._handler) && (this._handledEventsToo == handlerInfo._handledEventsToo));
        }

        public override bool Equals(object obj)
        {
            RoutedEventHandlerInfo handlerInfo = obj as RoutedEventHandlerInfo;
            if (handlerInfo == null)
            {
                return false;
            }
            return this.Equals(handlerInfo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(RoutedEventHandlerInfo handlerInfo1, RoutedEventHandlerInfo handlerInfo2)
        {
            return handlerInfo1.Equals(handlerInfo2);
        }

        public static bool operator !=(RoutedEventHandlerInfo handlerInfo1, RoutedEventHandlerInfo handlerInfo2)
        {
            return !handlerInfo1.Equals(handlerInfo2);
        }

        public RoutedEventHandler Handler
        {
            get
            {
                return this._handler;
            }
        }

        public bool InvokeHandledEventsToo
        {
            get
            {
                return this._handledEventsToo;
            }
        }
    }
}

