using Deluxe.Framework;
using Deluxe.Framework.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections;

namespace Deluxe.Windows.Controls
{
    public class EntityDataGrid : DataGrid
    {

        public NewRowCollection<EntityBase> NewRowCollection
        {
            get { return (NewRowCollection<EntityBase>)GetValue(NewRowCollectionProperty); }
            set { SetValue(NewRowCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewRowCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewRowCollectionProperty =
            DependencyProperty.Register(nameof(NewRowCollection), typeof(NewRowCollection<EntityBase>), typeof(EntityDataGrid), new PropertyMetadata(null));


        public EntityDataGrid()
        {
            NewRowCollection = new NewRowCollection<EntityBase>();
            SetBinding(ItemsSourceProperty, new Binding(nameof(NewRowCollection)) { Source = this });
        }

    }
}
