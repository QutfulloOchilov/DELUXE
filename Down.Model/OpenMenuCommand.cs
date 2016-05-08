using Dawn.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dawn.Model
{
    public class OpenMenuCommand : BaseCommand
    {
        private MenuItem menuItem;

        public OpenMenuCommand(MenuItem menuItem)
        {
            this.menuItem = menuItem;
        }


        public MenuItem MenuItem
        {
            get { return menuItem; }
            set { menuItem = value; }
        }

        public override void Execute(object parameter)
        {
            OpenPageManager.OnMessageManagerEvent(MenuItem.Content);
        }
    }
}
