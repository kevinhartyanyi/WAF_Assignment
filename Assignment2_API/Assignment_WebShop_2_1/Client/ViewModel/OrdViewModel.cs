using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Client.ViewModel
{
    public class OrdViewModel : ViewModelBase
    {
        private int _id;
        private string _name;
        private string _email;
        public string _address;
        private string _phone;
        private bool _delivered;
        private List<BasketElemDTO> _orderedProducts;

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

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public bool Delivered
        {
            get => _delivered;
            set
            {
                _delivered = value;
                OnPropertyChanged();
            }
        }

        public List<BasketElemDTO> OrderedProducts
        {
            get => _orderedProducts;
            set
            {
                _orderedProducts = value;
                OnPropertyChanged();
            }
        }


        public static explicit operator OrdViewModel(OrderDTO dto) => new OrdViewModel
        {
            Id = dto.ID,
            Name = dto.UserName,
            Address = dto.Address,
            Phone = dto.PhoneNumber,
            Delivered = dto.Delivered,
            Email = dto.Email,
            OrderedProducts = dto.OrderedProducts
        };

        public static explicit operator OrderDTO(OrdViewModel vm) => new OrderDTO
        {
            ID = vm.Id,
            UserName = vm.Name,
            Address = vm.Address,
            PhoneNumber = vm.Phone,
            Delivered = vm.Delivered,
            Email = vm.Email,
            OrderedProducts = vm.OrderedProducts
        };
    }
}
