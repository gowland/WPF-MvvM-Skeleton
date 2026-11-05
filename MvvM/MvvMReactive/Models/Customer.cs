using ReactiveUI;

namespace MvvMReactive.Models;

public class Customer(string name) : ReactiveObject
{
    private string _name = name;
    private int _updates;

    public int Updates
    {
        get => _updates;
        set => this.RaiseAndSetIfChanged(ref _updates, value);
    }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
}
