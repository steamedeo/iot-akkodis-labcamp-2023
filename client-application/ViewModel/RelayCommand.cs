namespace AkkodisLabcamp.ViewModel;

internal class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object?>? _predicate;

    public event EventHandler? CanExecuteChanged 
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public RelayCommand(Action<object?> execute, Predicate<object?>? predicate = null)
    {
        _execute = execute;
        _predicate = predicate;
    }

    public bool CanExecute(object? parameter) => _predicate?.Invoke(parameter) ?? true;

    public void Execute(object? parameter) => _execute(parameter);
}
