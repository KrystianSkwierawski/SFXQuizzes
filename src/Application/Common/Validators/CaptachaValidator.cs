using Newtonsoft.Json.Linq;

namespace Application.Common.Validators;

public static class CaptachaValidator
{
    public static async Task<bool> BeValid(string captacha, CancellationToken cancellationToken)
    {
        if (Environment.GetEnvironmentVariable("RunningTests") == "true")
            return true;

        using HttpClient client = new();

        string url = "https://www.google.com/recaptcha/api/siteverify?secret=6LdbmkEeAAAAAN1tg83pP-Pg0eSyyoa3AZQ7Yqxb&response=" + captacha;
        using HttpResponseMessage res = await client.GetAsync(url);

        using HttpContent content = res.Content;

        string data = await content.ReadAsStringAsync();
        JObject dataObj = JObject.Parse(data);

        bool isValid = (dataObj["success"]?.Value<bool>() == true);

        return isValid;
    }
}

