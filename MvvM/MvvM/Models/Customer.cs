using MvvMCore.ViewModels;

namespace MvvM.Models;

public class Customer(string customerName) : NotifyPropertyChangedBase
{
    private string _name = customerName;
    private int _updates;

    public int Updates
    {
        get => _updates;
        set => SetProperty(ref _updates, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}
