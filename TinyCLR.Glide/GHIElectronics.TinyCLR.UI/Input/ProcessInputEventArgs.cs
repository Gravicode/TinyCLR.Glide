namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;
    using System.Collections;

    public class ProcessInputEventArgs : NotifyInputEventArgs
    {
        internal ProcessInputEventArgs(StagingAreaInputItem input) : base(input)
        {
        }

        public StagingAreaInputItem PeekInput()
        {
            Stack stack = InputManager.CurrentInputManager._currentStagingStack;
            if (stack.Count > 0)
            {
                return (StagingAreaInputItem) stack.Peek();
            }
            return null;
        }

        public StagingAreaInputItem PopInput()
        {
            Stack stack = InputManager.CurrentInputManager._currentStagingStack;
            if (stack.Count > 0)
            {
                return (StagingAreaInputItem) stack.Pop();
            }
            return null;
        }

        public StagingAreaInputItem PushInput(StagingAreaInputItem input)
        {
            InputManager.CurrentInputManager._currentStagingStack.Push(input);
            return input;
        }

        public StagingAreaInputItem PushInput(InputEventArgs input, StagingAreaInputItem promote)
        {
            StagingAreaInputItem item = new StagingAreaInputItem(input, promote);
            InputManager.CurrentInputManager._currentStagingStack.Push(item);
            return item;
        }
    }
}

