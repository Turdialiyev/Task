using System.IO.Compression;
using System.Xml.Linq;
using Aspose.Cells;
using Ganss.Excel;
using IronXL;
using Newtonsoft.Json;

namespace Task.Services;
public class FileHelper : IFileHelper
{
    public bool ValidateFile(IFormFile file)
    {
        var defineCsvOrXlsxFile = DefineCsvOrXlsxFile(file);

        if ((file.Length > 0) && (file.Name == "file") && (defineCsvOrXlsxFile.ToLower() == "csv" || defineCsvOrXlsxFile.ToLower() == "xlsx"))
            return true;

        return false;
    }
    public Tuple<string, string> WriteFileAsync(IFormFile file)
    {
        var fileFormat = FileHelper.DefineCsvOrXlsxFile(file);
        var filename = DateTime.Now.ToString("yyyy'-'MM'-'dd'-'hh'-'mm'-'ss");

        var textFile = "";
        if (fileFormat.ToLower() == "csv")
        {
            using (StreamReader reader = new StreamReader(file.OpenReadStream()))
            {
                reader.ReadLine();
                while (reader.Peek() != -1)
                {
                    var line = reader.ReadLine();
                    textFile += line + ".";
                }
            }
        }
        else
        {
            using (StreamReader sr = new StreamReader(file.OpenReadStream()))
            {
                string line;
                string[] columns = null;
                while ((line = sr.ReadLine()) != null)
                {
                    columns = line.Split(',');
                    //now columns array has a ll data of column in a row!
                    //like:
                    string col1 = columns[0]; //and so on..
                }
            }
        }
        return Tuple.Create(filename, textFile)!;
    }
    public XElement GetFileXElement(string model)
    {
        List<List<string>> result = new List<List<string>>();
        string xml = model;
        var eachRow = xml.Split('.').ToList();
        eachRow.RemoveAt(eachRow.Count - 1);

        foreach (var item in eachRow)
        {
            var list = new List<string>();
            var ar = item.Split(',').ToList();
            foreach (var d in ar)
                if (!(d.Equals("-")))
                    list.Add(d);

            result.Add(list);
        }

        XElement xmlPeople = new XElement("people");

        foreach (var item in result)
        {
            XElement xmlPersons = new XElement("person");

            for (int i = 0; i < 1; i++)
            {
                xmlPersons.Add(new XAttribute("name", item[i]), new XAttribute("age", item[i + 1]));

                if (item.Count() > 2)
                {
                    XElement xmlPets = new XElement("pets");
                    for (int j = 2; j < item.Count(); j += 2)
                    {
                        XElement xmlPet = new XElement("pet");
                        xmlPet.Add(new XAttribute("name", item[j]), new XAttribute("type", item[j + 1]));
                        xmlPets.Add(xmlPet);
                    }
                    xmlPersons.Add(xmlPets);
                }
            }
            xmlPeople.Add(xmlPersons);
        }

        return xmlPeople;
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
}