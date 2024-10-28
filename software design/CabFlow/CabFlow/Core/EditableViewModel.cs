using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabFlow.Core
{
    public abstract class EditableViewModel: ViewModel
    {
        private bool _isEditing;

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                OnPropertyChanged();
                _isEditing = value;
            }
        }
    }
}
