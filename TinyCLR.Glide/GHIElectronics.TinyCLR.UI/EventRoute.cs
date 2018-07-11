namespace GHIElectronics.TinyCLR.UI
{
    using System;
    using System.Collections;

    public sealed class EventRoute
    {
        internal GHIElectronics.TinyCLR.UI.RoutedEvent RoutedEvent;
        private ArrayList _routeItemList;

        public EventRoute(GHIElectronics.TinyCLR.UI.RoutedEvent routedEvent)
        {
            this.RoutedEvent = routedEvent ?? throw new ArgumentNullException("routedEvent");
            this._routeItemList = new ArrayList();
        }

        public void Add(object target, RoutedEventHandler handler, bool handledEventsToo)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            RouteItem item = new RouteItem(target, new RoutedEventHandlerInfo(handler, handledEventsToo));
            this._routeItemList.Add(item);
        }

        internal void InvokeHandlers(object source, RoutedEventArgs args)
        {
            if ((args.RoutedEvent.RoutingStrategy == RoutingStrategy.Bubble) || (args.RoutedEvent.RoutingStrategy == RoutingStrategy.Direct))
            {
                int num = 0;
                int count = this._routeItemList.Count;
                while (num < count)
                {
                    RouteItem routeItem = (RouteItem) this._routeItemList[num];
                    args.InvokeHandler(routeItem);
                    num++;
                }
            }
            else
            {
                int num4;
                for (int i = this._routeItemList.Count - 1; i >= 0; i = num4)
                {
                    object target = ((RouteItem) this._routeItemList[i]).Target;
                    num4 = i;
                    while (num4 >= 0)
                    {
                        if (((RouteItem) this._routeItemList[num4]).Target != target)
                        {
                            if ((num4 != i) || (i <= 0))
                            {
                                break;
                            }
                            i--;
                        }
                        num4--;
                    }
                    for (int j = num4 + 1; j <= i; j++)
                    {
                        RouteItem routeItem = (RouteItem) this._routeItemList[j];
                        args.InvokeHandler(routeItem);
                    }
                }
            }
        }
    }
}

