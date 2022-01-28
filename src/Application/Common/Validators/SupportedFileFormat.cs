namespace Application.Common.Validators;

public static class SupportedFileFormat
{
    public static bool HasSupportedFileFormat(string fileName)
    {
        string[] supportedFormats = { "mp3", "wav", "ogg" };

        string format = fileName.Split(".")[1].ToLower();
        return supportedFormats.Contains(format);
    }
}

