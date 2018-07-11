namespace GHIElectronics.TinyCLR.UI
{
    using System;

    internal class RouteItem
    {
        internal object _target;
        internal RoutedEventHandlerInfo _routedEventHandlerInfo;

        internal RouteItem(object target, RoutedEventHandlerInfo routedEventHandlerInfo)
        {
            this._target = target;
            this._routedEventHandlerInfo = routedEventHandlerInfo;
        }

        public bool Equals(RouteItem routeItem)
        {
            return ((routeItem._target == this._target) && (routeItem._routedEventHandlerInfo == this._routedEventHandlerInfo));
        }

        public override bool Equals(object o)
        {
            return this.Equals((RouteItem) o);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(RouteItem routeItem1, RouteItem routeItem2)
        {
            return routeItem1.Equals(routeItem2);
        }

        public static bool operator !=(RouteItem routeItem1, RouteItem routeItem2)
        {
            return !routeItem1.Equals(routeItem2);
        }

        internal object Target
        {
            get
            {
                return this._target;
            }
        }
    }
}

