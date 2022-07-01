# Telegram-Bot-Budget
Telegram bot that stores transactions to preconfigured Google Sheet spreadsheet

# Intended Usage
This bot allows you to store your spendings into single Google Spreadsheet right from Telegram chat.
Every person who wants to use same spreadsheet (for example, members of one family) should initiate chat with same bot.

When bot works, use "/start" command to initiate filling out new spending or just enter amount of money spent.

Here is an example of interaction with bot:

<img src="https://user-images.githubusercontent.com/3009597/176690263-bab437b1-2347-4bd5-8060-d6c5bf996fcf.png" alt="drawing" width="200"/>

"/link" command allows you to open spreadsheet to see all records and aggregated data.

# Setting environment
## Setup Google Service account
1. Go to https://console.cloud.google.com/apis/ and log in under your account
2. Create new project
3. Click "+ Enable API and Services" and select "Google Sheets API"
4. Then navigate back to "Enabled APIs & services" and click "Google Sheets API" to manage it
5. Select "Credentials" tab, click "Create Credentials" - "Service account" and proceed. Note e-mail address created for service account
6. Then open service account and navigate to "Keys" tab. Add new JSON key and save new key file. It should be placed as "telegrambudget-key.json" file to allow interaction with Google Sheet API

## Setup Google Sheet
Here is suggested template. First 4 columns on Sheet1 sheet should remain as is together with sheet name.
https://docs.google.com/spreadsheets/d/190d27ABVJYG4ZKMPNmdoLKTXNyEKYMWvmtiTGvg5L_M/
1. Copy template to your account
2. Give edit access to google service account
3. Give view access to everyone who wants to see statistics

## Create Telegram bot
Use @bot_father to create new Telegram bot, then specify access token in appsettings.json as "TelegramToken".
appsettings.sample.json is provided for reference as an example.

## Setup for production use
Bot can be deployed to any platforms that is compatible with .NET Core.
For example:
1. install dotnet-runtime-6.0 package for Ubuntu server:
```
apt-get install -y aspnetcore-runtime-6.0
```
2. publish project:
```
dotnet publish -r linux-x64 --self-contained false -p:PublishSingleFile=true
```
3. copy bin\Debug\net6.0\linux-x64\publish content to target server
4. make telegrambudgetbot file executable and run it:
```
chmod +x telegrambudgetbot
./telegrambudgetbot
```
5. you can also setup it as a service using telegram.service file. Put it under /etc/systemd/system and edit paths according to your environment.
Then use the following commands one by one to enable and launch service:
```
systemctl daemon-reload
systemctl enable telegram.service
systemctl start telegram.service
```