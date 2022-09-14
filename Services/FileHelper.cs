using System.IO.Compression;
using Aspose.Cells;
using IronXL;
using Newtonsoft.Json;

namespace Task.Services;
public class FileHelper : IFileHelper
{

    public bool ValidateFile(IFormFile file)
    {
        var defineCsvOrXlsxFile = DefineCsvOrXlsxFile(file);

        if ((file.Length > 0) && (file.Name == "file") && (defineCsvOrXlsxFile.ToLower() == "xlsx" || defineCsvOrXlsxFile.ToLower() == "csv"))
            return true;

        return false;
    }
    public async ValueTask<string> WriteFileAsync(IFormFile file)
    {
        var fileFormat = FileHelper.DefineCsvOrXlsxFile(file);
        var date = DateTime.Now.ToString("yyyy'-'MM'-'dd'-'hh'-'mm'-'ss");
        var filename = "import " + date + "." + fileFormat;

        var filePath = Path.Combine(FileFolder, filename);

        using var fileStream = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write);
        await file.CopyToAsync(fileStream);

        return filename;
    }
    private static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];

        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms.ToArray();
        }
    }
    public async ValueTask<FileStream?> GetFileByNameAsync(string filename)
    {
        var filePath = Path.Combine(FileFolder, filename);

        if (!File.Exists(filePath))
            return null;

        using var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        return file;
    }

    public static string DefineCsvOrXlsxFile(IFormFile file)
    {
        var reverseFileName = Reverse(file.FileName);
        var count = reverseFileName.Count();
        var index = reverseFileName.IndexOf(".");
        reverseFileName = reverseFileName.Substring(0, index);

        return Reverse(reverseFileName);

    }
    //Revers
    public static string Reverse(string fileName)
    {

        char[] charArray = fileName.ToCharArray();

        string reversedString = String.Empty;

        int length, index;
        length = charArray.Length - 1;
        index = length;

        while (index > -1)
        {

            reversedString = reversedString + charArray[index];
            index--;
        }

        return reversedString;
    }

    private static string FileFolder => Path.Combine(Directory.GetCurrentDirectory(), "data/files");
}