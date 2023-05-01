#include <ArduinoJson.h>

#define CALIBRATION_DURATION 5000
#define TELEMETRY_DELAY 5000
#define AREF_VOLTAGE 3.3

#define TEMPERATURE_PIN A1
#define LIGHT_PIN A0

void setup()
{
  analogReference(EXTERNAL);
  Serial.begin(9600);
}

void loop()
{
  delay(TELEMETRY_DELAY);

  int temperatureSensorValue = analogRead(TEMPERATURE_PIN);
  int lightSensorValue = analogRead(LIGHT_PIN);
  sendTelemetryMessage(temperatureSensorValue, lightSensorValue);
}

float convertSensorValueToTemperature(int sensorValue)
{
  float voltage = sensorValue * AREF_VOLTAGE / 1024.0;
  float temperature = (voltage - 0.5) * 100.0;

  return temperature;
}

void sendTelemetryMessage(int temperatureSensorValue, int lightSensorValue)
{
  const int capacity = JSON_OBJECT_SIZE(2);

  StaticJsonDocument<capacity> doc;
  doc["temperature"] = convertSensorValueToTemperature(temperatureSensorValue);
  doc["lightLevel"] = lightSensorValue;

  char output[256];
  serializeJson(doc, output);
  Serial.println(output);
}
