using System.ComponentModel.Design;
using System.Windows;

namespace AkkodisLabcamp;

public partial class MainWindow : Window
{
    private const string LIGHT_OFF_FILENAME = "chandelier-off.png";
    private const string LIGHT_ON_FILENAME = "chandelier-on.png";
    private const string TEMPERATURE_OFF_FILENAME = "cold.png";
    private const string TEMPERATURE_ON_FILENAME = "hot.png";
    private const string BOOKSHELF_FILENAME = "bookshelf.png";
    private const string BOOKSHELF_HIGH_FILENAME = "bookshelf-high.png";
    private const string COUCH_FILENAME = "couch.png";
    private const string DOOR_FILENAME = "glass.png";

    public MainWindow()
    {
        InitializeComponent();

        /*
         logic to manage the light status update
         */
        string lightOffImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", LIGHT_OFF_FILENAME);
        DataTrigger lightOffDataTrigger = new()
        {
            Binding = new Binding() { Path = new PropertyPath("LightStatus") },
            Value = "False"
        };
        lightOffDataTrigger.Setters.Add(new Setter()
        {
            Property = Image.SourceProperty,
            Value = new BitmapImage(new Uri(lightOffImagePath))
        });

        string lightOnImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", LIGHT_ON_FILENAME);
        DataTrigger lightOnDataTrigger = new()
        {
            Binding = new Binding() { Path = new PropertyPath("LightStatus") },
            Value = "True"
        };
        lightOnDataTrigger.Setters.Add(new Setter()
        {
            Property = Image.SourceProperty,
            Value = new BitmapImage(new Uri(lightOnImagePath))
        });

        Style lightStyle = new() { TargetType = typeof(Image) };
        lightStyle.Triggers.Add(lightOffDataTrigger);
        lightStyle.Triggers.Add(lightOnDataTrigger);
        LightImage.Style = lightStyle;

        /*
         logic to manage the temperature status update
         */
        string temperatureOnImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", TEMPERATURE_ON_FILENAME);
        DataTrigger temperatureOnDataTrigger = new()
        {
            Binding = new Binding() { Path = new PropertyPath("TemperatureStatus") },
            Value = "True"
        };
        temperatureOnDataTrigger.Setters.Add(new Setter()
        {
            Property = Image.SourceProperty,
            Value = new BitmapImage(new Uri(temperatureOnImagePath))
        });

        string temperatureOffImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", TEMPERATURE_OFF_FILENAME);
        DataTrigger temperatureOffDataTrigger = new()
        {
            Binding = new Binding() { Path = new PropertyPath("TemperatureStatus") },
            Value = "False"
        };
        temperatureOffDataTrigger.Setters.Add(new Setter()
        {
            Property = Image.SourceProperty,
            Value = new BitmapImage(new Uri(temperatureOffImagePath))
        });

        Style temperatureStyle = new() { TargetType = typeof(Image) };
        temperatureStyle.Triggers.Add(temperatureOnDataTrigger);
        temperatureStyle.Triggers.Add(temperatureOffDataTrigger);
        TemperatureImage.Style = temperatureStyle;

        /*
         show the static content
         */
        string couchImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", COUCH_FILENAME);
        CouchImage.Source = new BitmapImage(new Uri(couchImagePath));

        string bookshelfImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", BOOKSHELF_FILENAME);
        BookshelfImage.Source = new BitmapImage(new Uri(bookshelfImagePath));

        string bookshelfHighImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", BOOKSHELF_HIGH_FILENAME);
        BookshelfHighImage.Source = new BitmapImage(new Uri(bookshelfHighImagePath));

        string doorImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", DOOR_FILENAME);
        DoorImage.Source = new BitmapImage(new Uri(doorImagePath));
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string messageBoxText = "Icons designed by Freepik from Flaticon.";
        string caption = "Credits";
        MessageBoxButton button = MessageBoxButton.OK;
        MessageBoxImage icon = MessageBoxImage.None;
        MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
    }
}
