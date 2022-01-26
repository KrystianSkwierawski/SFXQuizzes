using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Files;

public class SFXFileBuilder : ISFXFileBuilder
{
    private readonly ILogger<SFXFileBuilder> _logger;

    public SFXFileBuilder(ILogger<SFXFileBuilder> logger)
    {
        _logger = logger;
    }

    public async Task SaveSFXs(IList<IFormFile> files, string id)
    {
        foreach (var file in files)
        {
            try
            {
                if (file is null)
                    throw new NullReferenceException();

                string directory = Path.Combine("./wwwroot", "assets", "SFXs", id);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string filePath = Path.Combine(directory, file.FileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(fileStream);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "An error occurred while saving file. It might happen when testing with an empty file.");
            }
        }
    }

    public Task RemoveSFXs()
    {
        throw new NotImplementedException();
    }
}

