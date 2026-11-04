using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MvvM.Models;
using MvvMCore.Commands;
using MvvMCore.ViewModels;

namespace MvvM.ViewModels;

internal class CustomerViewModel : ViewModelBase
{
    private readonly ObservableCollection<Customer> _customers =
    [
        new("Matze"),
        new("Irwin"),
        new("Fitzpatrick"),
    ];

    /// <summary>
    /// Initialize a new instance of CustomerViewModel class
    /// </summary>
    public CustomerViewModel()
    {
        var updateCommand = new Command(UpdateCustomer, CanUpdate);
        UpdateCommand = updateCommand;

        AddNewUserCommand = new Command<string>(AddNewUser, IsValidUserName);

        Customers = new ListCollectionView(_customers);
        Customers.CurrentChanged += (_, _) => updateCommand.RaiseCanExecuteChanged();
    }

    public ICollectionView Customers { get; }

    public ICommand UpdateCommand { get; }

    public ICommand AddNewUserCommand { get; }

    public bool CanUpdate(object? parameter) =>
        parameter is Customer selectedCustomer && IsValidUserName(selectedCustomer.Name);

    public void UpdateCustomer(object? parameter)
    {
        if (!CanUpdate(parameter))
        {
            Console.WriteLine("May not save");
            return;
        }

        ((Customer)parameter!).Updates++;
    }

    private void AddNewUser(string name) => _customers.Add(new Customer(name));

    private static bool IsValidUserName(string? name) => !string.IsNullOrWhiteSpace(name);
}
