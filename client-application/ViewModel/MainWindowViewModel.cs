namespace AkkodisLabcamp.ViewModel;

internal class MainWindowViewModel : BaseViewModel
{
    private readonly string CONNECTION_STRING = ConfigurationManager.AppSettings["ConnectionString"] ?? "";
    private readonly string QUEUE_NAME = ConfigurationManager.AppSettings["QueueName"] ?? "";

    private ServiceBusClient? _client;
    private ServiceBusProcessor? _processor;

    private bool _lightStatus = false;
    public bool LightStatus
    {
        get { return _lightStatus; }
        set 
        {   
            if (_lightStatus == value)
                return;
            
            _lightStatus = value;
            OnPropertyChanged();
        }
    }

    private bool _temperatureStatus = true;
    public bool TemperatureStatus
    {
        get { return _temperatureStatus; }
        set
        {
            if (_temperatureStatus == value)
                return;

            _temperatureStatus = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Handle received messages.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        
        try
        {
            //deserialize message
            ApplianceStatusMessage? message = JsonSerializer.Deserialize<ApplianceStatusMessage>(body);
           
            if (message != null)
            {
                if (message.light != null)
                    LightStatus = message.light == "ON";

                if (message.temperature != null)
                    TemperatureStatus = message.temperature == "ON";
            }

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    /// <summary>
    /// Handle any errors when receiving messages.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

    internal void InitServiceBus()
    {
        try
        {
            // initialize client, receiver and processor
            _client = new(CONNECTION_STRING);
            _processor = _client.CreateProcessor(QUEUE_NAME, new ServiceBusProcessorOptions());

            // add handler to process messages
            _processor.ProcessMessageAsync += MessageHandler;
            // add handler to process any errors
            _processor.ProcessErrorAsync += ErrorHandler;

            Task.Run(async () =>
            {
                // start processing 
                await _processor.StartProcessingAsync();
            });
        }
        catch (Exception ex)
        {
            //TODO: Show message error
            Console.WriteLine(ex.ToString());
            Environment.Exit(-1);
        }
    }
}
