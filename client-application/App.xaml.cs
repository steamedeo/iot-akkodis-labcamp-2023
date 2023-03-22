namespace AkkodisLabcamp;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindowViewModel viewModel = new();
        viewModel.InitServiceBus();

        MainWindow view = new()
        {
            DataContext = viewModel
        };
        view.Show();
    }
}
