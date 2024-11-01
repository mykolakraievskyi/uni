using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CabFlow.Core
{
    public abstract class EditableViewModel: ViewModel
    {
        public EditableViewModel()
        {
            ChangeEditCommand = new RelayCommand(execute: x =>
            {
                if (IsEditing)
                {
                    SaveData();
                }
                IsEditing = !IsEditing;

            }, canExecute: x => true);

            CancelCommand = new RelayCommand(execute: x =>
            {
                Cancel();
                IsEditing = false;
            });
        }

        private bool _isEditing = false;

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                OnPropertyChanged();
                _isEditing = value;
            }
        }

        public ICommand ChangeEditCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public abstract void SaveData();
        public abstract void Cancel();

    }
}
