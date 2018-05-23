using MvvMCore.ViewModels;

namespace MvvM.Models
{
    public class Customer : NotifyPropertyChangedBase
    {
        private string _name;
        private int _updates;

        public Customer(string customerName)
        {
            Name = customerName;
        }

        public int Updates
        {
            get => _updates;
            set => SetProperty(ref _updates, value);
        }

        public string Name
        {
            get => _name;
            set
            {
                // TODO: Use base class setter method
                _name = value; 
                OnPropertyChanged();
            }
        }
    }
}
