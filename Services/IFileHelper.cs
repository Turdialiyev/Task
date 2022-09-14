using System.Xml.Linq;

namespace Task.Services;

public interface IFileHelper
{
    bool ValidateFile(IFormFile file);
    Tuple<string, string> WriteFileAsync(IFormFile file);
    XElement GetFileXElement(string model);
}