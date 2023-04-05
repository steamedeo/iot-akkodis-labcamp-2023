from iotc.aio import IoTCClient
from iotc import (
    IOTCConnectType,
    IOTCLogLevel,
)
import os
import asyncio
import configparser
import sys
import serial

"""
Get the necessary configuration
"""
config = configparser.ConfigParser()
config.read(os.path.join(os.path.dirname(__file__), "credentials.ini"))
if config["DEFAULT"].getboolean("Local"):
    sys.path.insert(0, "src")
device_id = config["1khqavx3sro"]["DeviceId"]
scope_id = config["1khqavx3sro"]["ScopeId"]
key = config["1khqavx3sro"]["DeviceKey"]


def initialize_client():
    """
    Function to initialize the client
    """
    client = IoTCClient(
        device_id,
        scope_id,
        IOTCConnectType.IOTC_CONNECT_DEVICE_KEY,
        key
    )
    client.set_log_level(IOTCLogLevel.IOTC_LOGGING_ALL)


async def main():
    """
    Main function to connect and send messages
    """
    client = initialize_client()
    await client.connect()

    s = serial.Serial("/dev/ttyACM0", 9600)

    while not client.terminated():
        if client.is_connected():
            try:
                message_from_arduino = s.readline().decode("utf-8")
                print("Received message from arduino: " + message_from_arduino)

                print("Sending message to IoT Hub...")
                await client.send_telemetry(message_from_arduino)
                print("Message successfully sent!")
            except KeyboardInterrupt:
                s.close()
        await asyncio.sleep(3)


if __name__ == "__main__":
    asyncio.run(main())
