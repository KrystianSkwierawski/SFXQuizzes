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

    public async Task SaveSFXs(IList<IFormFile> files, string quizId)
    {
        if (files is null || files.Count() == 0)
            throw new ArgumentNullException(nameof(files));

        foreach (var file in files)
        {
            try
            {
                if (file is null)
                    throw new ArgumentNullException(nameof(file));

                string directory = Path.Combine("./wwwroot", "assets", "SFXs", quizId);

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

    public async Task RemoveSFXs(string quizId)
    {
        string directory = Path.Combine("./wwwroot", "assets", "SFXs", quizId);

        if (!Directory.Exists(directory))
            return;

        Directory.Delete(directory, true);
    }
}

