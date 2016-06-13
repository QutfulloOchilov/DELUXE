using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deluxe.Framework
{
    public class OpenPageEventArgs : EventArgs
    {
        public object Page { get; set; }

        public OpenPageEventArgs(object page)
        {
            this.Page = page;
        }
    }

    public static class OpenPageManager
    {
        #region OpenPageManagerEvent Event
        public delegate void OpenPageManagerEventHendler(OpenPageEventArgs e);

        public static event OpenPageManagerEventHendler OpenPage;
        public static void OnMessageManagerEvent(object page)
        {
            OpenPage?.Invoke(new OpenPageEventArgs(page));
        }

        #endregion
    }
}
