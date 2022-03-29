using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var currencyDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currencyDirectory, "stores");
var salesFiles = FindFiles(storesDirectory);
var salesTotalaDir = Path.Combine(currencyDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalaDir);
var salesTotal = CalculateSalesTotal(salesFiles);
File.AppendAllText(Path.Combine(salesTotalaDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

IEnumerable<string> FindFiles(string folderName){
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName,"*",SearchOption.AllDirectories);

    foreach(var file in foundFiles){
        var extension = Path.GetExtension(file);
        //The file name will contain the full path, so only check the end of it
        if(extension == ".json"){
            salesFiles.Add(file);
        }
    }
    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles){
    double salesTotal = 0;

    foreach(var file in salesFiles){
        //Read the contents of the file
        string salesJson = File.ReadAllText(file);

        //Parse the contents as JSON
        SalesData ? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        //Add the amount found in the Total field to salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

record SalesData (double Total);