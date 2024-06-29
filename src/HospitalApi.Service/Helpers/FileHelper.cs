using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace Tenge.Service.Helpers;

public class FileHelper
{
    public static async Task<(string Path, string Name)> CreateFileAsync(IFormFile file)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, "Photos");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fileName = MakeFileName(file.FileName);
        var fullPath = Path.Combine(directoryPath, fileName);

        var stream = File.Create(fullPath);
        await file.CopyToAsync(stream);
        stream.Close();

        return (fullPath, fileName);
    }

    private static string MakeFileName(string fileName)
    {
        string fileExtension = Path.GetExtension(fileName);
        string guid = Guid.NewGuid().ToString();
        return $"{guid}{fileName}";
    }
}
