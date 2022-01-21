using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files;

public class SFXFileBulidercs : ISFXFileBulider
{
    private IWebHostEnvironment _hostEnvironment;

    public SFXFileBulidercs(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public async Task SaveSFXs(IFormFileCollection files, string id)
    {
        foreach (var file in files)
        {
            if (file is null)
                throw new NullReferenceException();

            string directory = Path.Combine(_hostEnvironment.WebRootPath, "assets", "SFXs", id);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string filePath = Path.Combine(directory, file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);
        }
    }

    public Task RemoveSFXs()
    {
        throw new NotImplementedException();
    }   
}

