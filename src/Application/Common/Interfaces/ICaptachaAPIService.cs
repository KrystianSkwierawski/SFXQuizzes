using Newtonsoft.Json.Linq;

namespace Application.Common.Interfaces;

public interface ICaptachaAPIService
{
    Task<JObject> GetResponseAsync(string captacha);
}

