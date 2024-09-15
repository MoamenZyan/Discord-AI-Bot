using Discord;
using Discord.WebSocket;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

namespace MyProgram;

public class MyProgram
{
    // Environment Variables
    private static string _AI_API_KEY = "AI API KEY";
    private static string _REQUEST_URL = "YOUR REST REQUEST URL";
    private static string _DISCORD_API_KEY = "YOUR DISCORD APP API KEY";

    // Clients Instances
    private static readonly DiscordSocketClient _discordClient = new DiscordSocketClient();
    private static readonly HttpClient _httpClient = new HttpClient();

    // Container To Save Last Reponses => (Allow Interactive Chat)
    private static List<string> _responses = new List<string>();
    public static async Task Main(string[] args)
    {
        // Adding API Key Header
        _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", _AI_API_KEY);

        // Subscribing To Certain Events
        _discordClient.Log += Log; ;
        _discordClient.MessageReceived += MessageReceivedAsync;

        // Starting Discord Bot
        await _discordClient.LoginAsync(TokenType.Bot, _DISCORD_API_KEY);
        await _discordClient.StartAsync();


        // Make The Program Work Until Closes
        await Task.Delay(-1);
    }

    // Logging Service
    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    // On Message Received Event
    private static async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Author.IsBot)
        {
            var modelResponse = CreateResponse("model", message.Content);
            _responses.Add(modelResponse);

            return;
        }

        try
        {
            var userReponse = CreateResponse("user", message.Content);
            _responses.Add(userReponse);

            await message.Channel.SendMessageAsync(await SendAiMessage());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await message.Channel.SendMessageAsync("Error in gemini :(");
        }
    }

    // Sending Message To AI Model
    private static async Task<string> SendAiMessage()
    {
        var requestbody = new
        {
            contents = new[]
            {
                _responses.Select(x => JsonConvert.DeserializeObject<dynamic>(x)).ToList()
            },
            generationConfig = new
            {
                maxOutputTokens = 800
            }
        };

        var contentType = new StringContent(JsonConvert.SerializeObject(requestbody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_REQUEST_URL, contentType);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error in gemini"); // Handle Error If Applicable

        return JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync())!.candidates[0].content.parts[0].text;
    }

    // Create Response Object And Serialize It => To Store It In The Responses Container
    private static string CreateResponse(string type, string content)
    {
        var response = new
        {
            role = type,
            parts = new[]
            {
                new
                {
                    text = content,
                }
            }
        };

        return JsonConvert.SerializeObject(response);
    }
}
