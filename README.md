# Discord-AI-Bot

## Overview

A simple Discord bot that integrates with Gemini AI to provide intelligent responses within Discord channels. Built using the Discord.NET library, this bot interacts with users, processes their messages through the Gemini AI API, and sends back relevant responses.

## Technologies

- **Gemini AI**: Provides AI-driven responses.
- **Discord.NET**: Library used to interface with the Discord API and manage bot interactions.

## Features

- **Message Handling**: Listens to user messages in Discord channels.
- **AI Integration**: Uses Gemini AI to generate responses based on user input.
- **Error Handling**: Handles exceptions and provides error messages if the AI service fails.

## Setup

### Prerequisites

- .NET SDK (version 5.0 or later)
- A Discord bot token (create a bot application in the [Discord Developer Portal](https://discord.com/developers/applications))
- An API key for Gemini AI

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/discord-ai-bot.git
   cd discord-ai-bot
   ```

2. **Install Dependencies**

   ```bash
   dotnet restore
   ```

3. **Configure Your Bot**
   - Update the Program.cs file with your Discord bot token and Gemini API key.
   - Set your environment variables or directly input them into the code.

   ```bash
   private const string _DISCORD_API_KEY = "your_discord_bot_token";
   private const string _AI_API_KEY = "your_gemini_api_key";
   private static string _REQUEST_URL = "YOUR REST REQUEST URL";
   ```

4. **Run the Bot**

   ```bash
   dotnet run
   ```

## Screenshot
![DiscordBot](https://github.com/user-attachments/assets/cde2e9f2-5db0-4d46-acbd-41a3c2e49070)


## Conclusion
Creating the Discord-AI-Bot was a fantastic experience, especially since it was my first time working with AI and Discord development tools. Integrating Gemini AI with Discord.NET has been a great learning journey, showing how powerful these technologies can be when combined.

I hope this bot helps you see the potential of using AI in Discord. Feel free to build on it and share your feedback!
