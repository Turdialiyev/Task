using CsvHelper.Configuration.Attributes;

namespace Task.Models;

public class XlsxModel
{
    [Name("PersonName")]
    public string? PersonName { get; set; }
    
    [Name("Age")]
    public string? Age { get; set; }
    
    [Name("Pet 1")]
    public string?  Pet1 { get; set; }
   
    [Name("Pet 1 Type")]
    public string?  Type1 { get; set; }

    [Name("Pet 2")]
    public string? Pet2 { get; set; }
   
    [Name("Pet 2 Type")]
    public string? Typ2 { get; set; }
   
    [Name("Pet 3")]
    public string? Pet3 { get; set; }
    
    [Name("Pet 3 Type")]
    public string? Type3 { get; set; }
}