namespace GHIElectronics.TinyCLR.UI
{
    using System;

    public class PropertyChangedEventArgs
    {
        public readonly string Property;
        public readonly object OldValue;
        public readonly object NewValue;

        public PropertyChangedEventArgs(string property, object oldValue, object newValue)
        {
            this.Property = property;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}

