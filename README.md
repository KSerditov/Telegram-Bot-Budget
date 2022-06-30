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

## Setup Google Sheet

## Create Telegram bot
Use @bot_father to create new Telegram bot, then specify access token in appsettings.json as "TelegramToken".

## Setup hosting for production use
