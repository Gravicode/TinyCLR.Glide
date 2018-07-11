using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace TinyApp.GHI_PINS
{
    public interface GadgeteerBoard
    {
        GadgeteerSocket GetSocketByNumber(int SocketNumber);
    }
}
