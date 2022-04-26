# Chat-bot-challenge
Chatbot app is a challenge to develop an browser-based chat application;

## Technologies
This app uses ASP.NET Core 6.0 and Blazor Components on Front-end; It was built using some libraries as SignalR to comunicate in real time between clients and server and 
Microsoft Identity Server for authentication;

## Features
- ✅ Browser-based chat 
- N/A - Focus on backend
- ✅ Allow registered users logon and send messages only
- 🚧 Send commands with the following format: /stock=stock_code
- 🚧 Create a decouple bot that will receive the stock command
  - 🚧 Retrieve the stock quote from: https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv
  - 🚧 Send the response using a message broker (like RabBitMQ) as a post into the chat (in bot's name) like: APPL.US quote is $93.42 per share
- 🥚 Leave the messages ordered by timestamp
- 🥚 Unit tests

### Glossary

- ✅ Done
- 🚧 Under construction
- 🥚 Waiting
- N/A  Not applicable

## Application Architecture
The applications architectures was thinking about to improve scalability and decouple services. There are an API HUB (Chatbot.Api) and the chat client (Chatbot.App)
to avoid scale the entire application. Diving deeper there will be a service (stock bot) to retrieve information from another API (Third party) and post it into the hub back,
turning the stock respose into the chat;
The api hub should implements an database to store some messages whether it become necessary;

The code architecture followed was Clean Architecture + ddd (as soon as fast) tooking the advantage sharing some code between domains, 
as we could do sharing nuget/npm packages, like the Broadcast messages service, that should be resude by the stock bot.

## How to run
### Database
You need to configure your database connection for Identity Server and run the Update-Database command to apply Identity Server database's objects

### Configuration
There are two main applications, the server (provides SignalR Hub) and Client (provides user interface chat), on the client application (Chatbot.App) you need to configure
into appsettings.development.json the necessary keys to comunicate to each other:
```
  "ChatServer": {
    "Url": "https://localhost:7001/"
  }
```

### Starting 
Configure the startup project for multiple projects (solution configurations) and select both web projects and run!
