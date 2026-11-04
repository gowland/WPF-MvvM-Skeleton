using System.Windows.Input;

namespace MvvMCore.Commands;

public class Command(Action<object?> executeAction, Func<object?, bool> canExecuteFunc) : ICommand
{
    public bool CanExecute(object? parameter) => canExecuteFunc(parameter);

    public void Execute(object? parameter) => executeAction(parameter);

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;
}

public class Command<T>(Action<T> executeAction, Func<T, bool> canExecuteFunc) : ICommand
{
    public bool CanExecute(object? parameter) => AsT(parameter, out var value) && canExecuteFunc(value);

    public void Execute(object? parameter)
    {
        if (AsT(parameter, out var value))
        {
            executeAction(value);
        }
    }

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;

    private static bool AsT(object? parameter, out T value)
    {
        if (parameter is T converted)
        {
            value = converted;
            return true;
        }

        try
        {
            if (parameter is not null && Convert.ChangeType(parameter, typeof(T)) is T changed)
            {
                value = changed;
                return true;
            }
        }
        catch (Exception ex) when (ex is InvalidCastException or FormatException or OverflowException)
        {
        }

        value = default!;
        return false;
    }
}
