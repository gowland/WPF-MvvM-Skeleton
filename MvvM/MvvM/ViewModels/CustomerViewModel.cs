using System;
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
        /// <summary>
        /// Inintialize a new instance of CustomerViewModel class
        /// </summary>
        public CustomerViewModel()
        {
            var updateCommand = new Command(o => UpdateCustomer(o), o => CanUpdate(o));
            UpdateCommand = updateCommand;
            Customers = new ListCollectionView(new []
            {
                new Customer("Matze"),
                new Customer("Irwin"),
                new Customer("Fitzpatrick"),
            });
            Customers.CurrentChanged += (sender, args) => updateCommand.RaiseCanExecuteChanged();
        }

        public ICollectionView Customers { get; }

        /// <summary>
        /// Get the UpdateCommand for the ViewModel
        /// </summary>
        public ICommand UpdateCommand { get; }


        /// <summary>
        /// Get or set bool indicating if customer can be updated
        /// </summary>
        public bool CanUpdate(object parameter)
        {
            if (!(parameter is Customer selectedCustomer))
            {
                return false;
            }

            return !String.IsNullOrWhiteSpace(selectedCustomer.Name);
        }

        /// <summary>
        /// Save changes made to the Customer instance
        /// </summary>
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
    }
}
