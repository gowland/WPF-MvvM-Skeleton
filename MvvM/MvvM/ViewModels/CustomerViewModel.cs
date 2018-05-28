using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MvvM.Models;
using MvvMCore.Commands;
using MvvMCore.ViewModels;

namespace MvvM.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Customer> _listCollectionView;

        /// <summary>
        /// Inintialize a new instance of CustomerViewModel class
        /// </summary>
        public CustomerViewModel()
        {
            var updateCommand = new Command(UpdateCustomer, CanUpdate);
            UpdateCommand = updateCommand;

            AddNewUserCommand = new Command(name => AddNewUser((string)name), name => IsValidUserName((string)name));

            _listCollectionView = new ObservableCollection<Customer>(new []
            {
                new Customer("Matze"),
                new Customer("Irwin"),
                new Customer("Fitzpatrick"),
            });

            Customers = new ListCollectionView(_listCollectionView);
            Customers.CurrentChanged += (sender, args) => updateCommand.RaiseCanExecuteChanged();
        }

        public ICollectionView Customers { get; }

        public ICommand UpdateCommand { get; }

        public ICommand AddNewUserCommand { get; }

        public bool CanUpdate(object parameter)
        {
            if (!(parameter is Customer selectedCustomer))
            {
                return false;
            }

            return IsValidUserName(selectedCustomer.Name);
        }

        public void UpdateCustomer(object parameter)
        {
            if (!CanUpdate(parameter))
            {
                Console.WriteLine("May not save");
            }

            if (parameter is Customer selectedCustomer)
            {
                selectedCustomer.Updates++;
            }

            Console.WriteLine("Save failed");
        }

        private void AddNewUser(string name)
        {
            _listCollectionView.Add(new Customer(name));
        }

        private static bool IsValidUserName(string name)
        {
            return !String.IsNullOrWhiteSpace(name);
        }
    }
}
