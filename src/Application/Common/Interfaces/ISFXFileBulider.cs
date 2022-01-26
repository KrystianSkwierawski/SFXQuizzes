using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces;

public interface ISFXFileBulider
{
    public Task SaveSFXs(IList<IFormFile> files, string id);
    public Task RemoveSFXs();

}

