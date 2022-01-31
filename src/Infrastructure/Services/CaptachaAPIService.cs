using Application.Common.Interfaces;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services;

public class CaptachaAPIService : ICaptachaAPIService
{
    public async Task<JObject> GetResponseAsync(string captacha)
    {
        using HttpClient client = new();

        string url = "https://www.google.com/recaptcha/api/siteverify?secret=6LdbmkEeAAAAAN1tg83pP-Pg0eSyyoa3AZQ7Yqxb&response=" + captacha;
        using HttpResponseMessage response = await client.GetAsync(url);

        using HttpContent content = response.Content;

        string data = await content.ReadAsStringAsync();
        JObject dataObj = JObject.Parse(data);

        return dataObj;
    }
}

