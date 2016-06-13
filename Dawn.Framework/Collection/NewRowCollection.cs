using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deluxe.Framework.Collection
{
    public class NewRowCollection<T> : ObservableNotifyPropertyChangedCollection<T> where T : EntityBase
    {
        public NewRowCollection()
        {
            
        }

        /// <summary>
        /// The entity that represents the new row
        /// </summary>
        public EntityBase NewRow { get; private set; }

        void SetNewRow()
        {
            NewRow = (T)Activator.CreateInstance(typeof(T));

            NewRow.PropertyChanged += NewRow_PropertyChanged;

            Add((T)(object)NewRow);
        }

        void NewRow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NewRow.PropertyChanged -= NewRow_PropertyChanged;

            SetNewRow();
        }

    }
}
