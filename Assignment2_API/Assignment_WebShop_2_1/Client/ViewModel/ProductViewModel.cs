using Data;
using System;
using System.ComponentModel;

namespace Client.ViewModel
{
    public class ProductViewModel : ViewModelBase, IEditableObject
    {
        private int _id;
        private string _manufacturer;
        private int _modelID;
        private int _price;
        private int _amount;
        private bool _available;
        private string _description;
        private byte[] _image;
        private CategoryDTO _category;

        private ProductViewModel _backup;        

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public byte[] Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnPropertyChanged();
            }
        }

        public int ModelID
        {
            get => _modelID;
            set
            {
                _modelID = value;
                OnPropertyChanged();
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public bool Available
        {
            get => _available;
            set
            {
                _available = value;
                OnPropertyChanged();
            }
        }

        public CategoryDTO Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }


        public bool IsDirty { get; private set; } = false;

        public string Error => string.Empty;

        public void BeginEdit()
        {
            if (!IsDirty)
            {
                _backup = (ProductViewModel)MemberwiseClone();
                IsDirty = true;
            }
        }

        public void CancelEdit()
        {
            if (IsDirty)
            {
                Id = _backup.Id;
                Manufacturer = _backup.Manufacturer;
                Description = _backup.Description;
                ModelID = _backup.ModelID;
                Image = _backup.Image;
                Category = _backup.Category;
                Available = _backup.Available;
                Amount = _backup.Amount;
                Price = _backup.Price;

                IsDirty = false;
                _backup = null;
            }
        }

        public void EndEdit()
        {
            if (IsDirty)
            {
                IsDirty = false;
                _backup = null;
            }
        }

        public static explicit operator ProductViewModel(ProductDTO dto) => new ProductViewModel
        {
            Id = dto.Id,
            Manufacturer = dto.Manufacturer,
            Description = dto.Description,
            ModelID = dto.ModelID,
            Image = dto.Image,
            Category = dto.Category,
            Available = dto.Available,
            Amount = dto.Amount,
            Price = dto.Price
        };
        public static explicit operator ProductDTO(ProductViewModel vm) => new ProductDTO
        {
            Id = vm.Id,
            Manufacturer = vm.Manufacturer,
            Description = vm.Description,
            ModelID = vm.ModelID,
            Image = vm.Image,
            Category = vm.Category,
            Available = vm.Available,
            Amount = vm.Amount,
            Price = vm.Price
        };

    }
}
