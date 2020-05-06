using Data;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Client.ViewModel
{
    public class CategoryViewModel : ViewModelBase, IEditableObject
    {

        private int _id;
        private string _name;

        private bool _isDirty = false;
        private CategoryViewModel _backup;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler EditEnded;

        public void BeginEdit()
        {
            if (!_isDirty)
            {
                _backup = (CategoryViewModel)MemberwiseClone();
                _isDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (_isDirty)
            {
                Id = _backup.Id;
                Name = _backup.Name;
                _isDirty = false;
                _backup = null;
            }
        }

        public void EndEdit()
        {
            if (_isDirty)
            {
                EditEnded?.Invoke(this, EventArgs.Empty);
                _isDirty = false;
                _backup = null;
            }
        }

        public static explicit operator CategoryViewModel(CategoryDTO dto) => new CategoryViewModel
        {
            Id = dto.Id,
            Name = dto.Name
        };

        public static explicit operator CategoryDTO(CategoryViewModel vm) => new CategoryDTO
        {
            Id = vm.Id,
            Name = vm.Name
        };
    }

    public class ListValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            CategoryViewModel list = (value as BindingGroup).Items[0] as CategoryViewModel;
            if (string.IsNullOrEmpty(list.Name))
            {
                return new ValidationResult(false,
                    "Name cannot be empty.");
            }
            else if (list.Name.Length > 30)
            {
                return new ValidationResult(false,
                    "Name cannot be longer than 30 characters.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
