namespace GHIElectronics.TinyCLR.UI
{
    using System;

    public sealed class RoutedEvent
    {
        private string _name;
        internal GHIElectronics.TinyCLR.UI.RoutingStrategy _routingStrategy;
        private Type _handlerType;
        private int _globalIndex;
        private static int _eventCount;

        public RoutedEvent(string name, GHIElectronics.TinyCLR.UI.RoutingStrategy routingStrategy, Type handlerType)
        {
            this._name = name;
            this._routingStrategy = routingStrategy;
            this._handlerType = handlerType;
            Type type = typeof(GlobalLock);
            lock (type)
            {
                if (_eventCount >= 0x7fffffff)
                {
                    throw new InvalidOperationException("too many events");
                }
                this._globalIndex = _eventCount++;
            }
        }

        public override string ToString()
        {
            return this._name;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public GHIElectronics.TinyCLR.UI.RoutingStrategy RoutingStrategy
        {
            get
            {
                return this._routingStrategy;
            }
        }

        public Type HandlerType
        {
            get
            {
                return this._handlerType;
            }
        }

        internal int GlobalIndex
        {
            get
            {
                return this._globalIndex;
            }
        }

        private class GlobalLock
        {
        }
    }
}

