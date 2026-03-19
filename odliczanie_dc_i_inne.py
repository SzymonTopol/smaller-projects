import discord
from datetime import datetime
from googleapiclient.discovery import build

API_KLUCZ_YT = "APIYT"
API_KLUCZ_DC = "APIDC"
CHANNEL_ID = "UCK4TZpn1SEWWfKLT4Tl9r_g" #Brudo

intents=discord.Intents.default()
bot=discord.Client(intents=intents)

youtube = build("youtube","v3",developerKey=API_KLUCZ_YT)
request = youtube.channels().list(part="statistics", id=CHANNEL_ID)
response = request.execute()
stats = response["items"][0]["statistics"]
dane_yt = [stats['subscriberCount'],stats['viewCount'],stats['videoCount']]

roznica = datetime.now() - datetime(2022, 8, 22, 18, 39, 0)

def dodaj_kropki(liczba):  
    tekst = ""

    liczba = int(liczba)

    counter = 0

    while liczba >= 1:
        tekst += str(liczba%10)
        liczba //=10
        counter+=1
        if counter == 3 and liczba > 0:
            tekst += "."
            counter=0


    return tekst[::-1]

roznica = dodaj_kropki(roznica.days)
subs = dodaj_kropki(dane_yt[0])
views = dodaj_kropki(dane_yt[1])
videos = dodaj_kropki(dane_yt[2])

@bot.event
async def on_ready():
    print("Bot is ready")

    channel=bot.get_channel(977683477837258772)
    if channel:
        await channel.send("⚡Wiadomości lokalne różnica dni to: " + roznica + " dni \n✨Brudolina sztats sumery✨\n"+subs+" subów\n"+views+" wyświetleń\n" + videos+ " DI END c:")
        await bot.close()

bot.run(API_KLUCZ_DC)