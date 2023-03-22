## Akkodis Labcamp Client Application

This is a client desktop application developed in c# with .net to simulate a smart home. For this labcamp this is used to view the automation effects () deriving from the IoT devices sensors and the messages received from the Azure cloud.

### Setup

Create a file named App.config with the structure below, to allow the application to connect to the Service Bus and receive status messages.

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="ConnectionString" value="<Service Bus Connection String>"/>
		<add key="QueueName" value="<Service Bus Queue Name>"/>
	</appSettings>
</configuration>
```

### Message received from Service Bus

{
"temperature": "ON/OFF",
"light": "ON/OFF"
}
