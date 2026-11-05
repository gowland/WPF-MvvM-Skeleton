using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using MvvMReactive.Models;
using ReactiveUI;
using RxVoid = ReactiveUI.Primitives.RxVoid;

namespace MvvMReactive.ViewModels;

internal class CustomerViewModel : ReactiveObject
{
    private readonly SourceList<Customer> _customers = new();
    private readonly ReadOnlyObservableCollection<Customer> _sortedCustomers;
    private Customer? _selectedCustomer;
    private string _newUserName = "New User Name";

    public CustomerViewModel()
    {
        _customers.AddRange([new("Matze"), new("Irwin"), new("Fitzpatrick")]);

        _customers.Connect()
            .Sort(SortExpressionComparer<Customer>.Ascending(c => c.Name))
            .Bind(out _sortedCustomers)
            .Subscribe();

        var canUpdate = this.WhenAnyValue(x => x.SelectedCustomer)
            .Select(customer => customer is not null && IsValidUserName(customer.Name));
        UpdateCommand = ReactiveCommand.Create(() => { SelectedCustomer!.Updates++; }, canUpdate);

        var canAdd = this.WhenAnyValue(x => x.NewUserName).Select(IsValidUserName);
        AddNewUserCommand = ReactiveCommand.Create(() => _customers.Add(new Customer(NewUserName)), canAdd);
    }

    public ReadOnlyObservableCollection<Customer> Customers => _sortedCustomers;

    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set => this.RaiseAndSetIfChanged(ref _selectedCustomer, value);
    }

    public string NewUserName
    {
        get => _newUserName;
        set => this.RaiseAndSetIfChanged(ref _newUserName, value);
    }

    public ReactiveCommand<RxVoid, RxVoid> UpdateCommand { get; }

    public ReactiveCommand<RxVoid, RxVoid> AddNewUserCommand { get; }

    private static bool IsValidUserName(string? name) => !string.IsNullOrWhiteSpace(name);
}
