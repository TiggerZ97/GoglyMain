# Import Adafruit IO MQTT client.
from Adafruit_IO import *

ADAFRUIT_IO_USERNAME = "TiggerZ97"
ADAFRUIT_IO_KEY = "aio_wcOr66JRhjje6OL8avZzdhTvAZpL"

aio = Client(ADAFRUIT_IO_USERNAME,ADAFRUIT_IO_KEY)

try:
    test = aio.feeds('test')
except RequestError:
    print("feed not found")
    test_feed =Feed(name='test')
    test_feed = aio.create_feed(test_feed)

def getdata():
    print("Recibimos dato")
    data = aio.receive(test.key)