using System.Xml.Linq;
using ClosedXML.Excel;

namespace Task.Services;
public class FileHelper : IFileHelper
{
    public bool ValidateFile(IFormFile file)
    {
        var defineCsvOrXlsxFile = DefineCsvOrXlsxFile(file);

        if ((defineCsvOrXlsxFile.ToLower() == "csv" || defineCsvOrXlsxFile.ToLower() == "xlsx"))
            return true;

        return false;
    }
    public Tuple<string, string> WriteFileAsync(IFormFile file)
    {
        var fileFormat = DefineCsvOrXlsxFile(file);
        var textFile = "";
        if (fileFormat.ToLower() == "csv")
        {
            using (StreamReader reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() != -1)
                {
                    var line = reader.ReadLine();
                    textFile += line + ".";
                }
            }
            textFile = textFile.Substring(0, (textFile.Count()-1));
        }
        else
        {
             using (XLWorkbook excelWorkbook = new XLWorkbook(file.OpenReadStream()))
        {
            var xlWorksheet = excelWorkbook.Worksheet(1);
            var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

            var col = range.ColumnCount();
            var row = range.RowCount();
            
            for (var i = 1; i <= row; i++)
            {
                for (int j = 1; j <= col; j++)
                {
                    var column = xlWorksheet.Cell(i, j);
                    textFile += column.Value.ToString();
                    if (j != col)
                        textFile += ",";
                }
                if (i != row)
                    textFile += ".";
            }
        }
        }
        return Tuple.Create(fileFormat, textFile)!;
    }
    public XElement GetFileXElement(string model)
    {
        List<List<string>> result = new List<List<string>>();
        string xml = model;
        var eachRow = xml.Split('.').ToList();
        eachRow.RemoveAt(0);

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
    private string DefineCsvOrXlsxFile(IFormFile file)
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