// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var salesFiles = FindFiles("stores");

foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}
Console.WriteLine("directorio actual:");
Console.WriteLine(Directory.GetCurrentDirectory());

Console.WriteLine("home");
string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

// separador de direcciones de la clase Path: Path.DirectorySeparator
Console.WriteLine("separador indentificado con la clase Path:");
Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");

//La clase Path funciona con el concepto de rutas de acceso de archivos y 
//carpetas, que son simplemente cadenas. Puede usar la clase Path para crear 
//e forma automática rutas correctas para sistemas operativos específicos.
//
//Por ejemplo, si quiere obtener la ruta de acceso a la carpeta stores/201, 
//puede usar la función Path.Combine para ello.
Console.WriteLine("Path.Combine:");
Console.WriteLine(Path.Combine("stores","201"));

//La clase Path también puede indicarle la extensión de un nombre 
//de archivo. Si tiene un archivo y quiere saber si es JSON, puede 
//usar la función Path.GetExtension.
Console.WriteLine("Path.GetExtension");
Console.WriteLine(Path.GetExtension("sales.json"));

//La clase Path contiene muchos métodos diferentes que realizan 
//diversas acciones. Si quiere obtener el máximo de información 
//posible sobre un directorio o un archivo, use la clase DirectoryInfo 
//o FileInfo, respectivamente.
string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";

FileInfo info = new FileInfo(fileName);

Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine} Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}"); // And many more

Console.WriteLine("Directory.GetCUrrentDirectory");

//En el código de Program.cs , se pasa la ubicación 
//estática de la carpeta stores. Cambiaremos ese código 
//para que use el valor de Directory.GetCurrentDirectory
// en lugar de pasar un nombre de carpeta estática.

var currentDirectory = Directory.GetCurrentDirectory();
//Este código usa el método Directory.GetCurrentDirectory 
//para obtener la ruta del directorio actual y almacenarla 
//n una nueva variable currentDirectory
Console.WriteLine(currentDirectory);

var storesDirectory = Path.Combine(currentDirectory, "stores");
// Este código usa el método Path.Combine para crear la ruta de 
//acceso completa al directorio de almacenes y almacenarla en 
//una nueva variable storesDirectory

var salesFiles2 = FindFiles4extension("stores");

foreach (var file in salesFiles2)
{
    Console.WriteLine(file);
}

IEnumerable<string> FindFiles4extension(string folderName)
{
    List<string> salesFiles2 = new List<string>();

    var foundFiles2 = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles2)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles2.Add(file);
        }
    }

    return salesFiles2;
}

//crear un directorio :

Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "stores","201","newDir"));

//ver si existe un directorio, use el método Directory.Exists:

//bool doesDirectoryExist = Directory.Exists(filePath);

//Se pueden crear archivos mediante el método File.WriteAllText. 
//Este método toma una ruta de acceso al archivo y los datos que se 
//van a escribir en él. Si el archivo ya existe, se sobrescribirá.

//Por ejemplo, este código crea un archivo denominado greeting.txt con el texto "Hola mundo" dentro:

File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "greeting.txt"), "Hello World!");

// ceracion de directorio salesTotals
currentDirectory = Directory.GetCurrentDirectory();
storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

salesFiles = FindFiles(storesDirectory);

File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);

//Los archivos se leen a través del método ReadAllText de la clase File.

File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");

// método JsonConvert.DeserializeObject de la del paquete Newtonsoft.Json

//var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
//var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
//
// Console.WriteLine(salesData.Total);
//
//var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
//File.WriteAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", data.Total.ToString());
//
//File.AppendAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{data.Total}{Environment.NewLine}");
//
//class SalesTotal
//{
//  public double Total { get; set; }
//}
 currentDirectory = Directory.GetCurrentDirectory();
 storesDirectory = Path.Combine(currentDirectory, "stores");

 salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);   

 salesFiles = FindFiles3(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

IEnumerable<string> FindFiles3(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;
    
    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);
    
        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
    
        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }
    
    return salesTotal;
}

record SalesData (double Total);



//Directory.EnumerateDirectories y Directory.EnumerateFiles aceptan un 
//parámetro que permite especificar una condición de búsqueda para los nombres 
//que se van a devolver y un parámetro para recorrer de forma recursiva todos los 
//directorios secundarios.
//
//System.Environment.SpecialFolder es una enumeración que le permite acceder a carpetas
// definidas por el sistema, como el perfil de usuario o de escritorio, de una manera 
// multiplataforma sin tener que memorizar la ruta de acceso exacta de cada sistema operativo.
//
//En caso de que sea necesario analizar otros tipos de archivo, consulte los paquetes disponibles 
//en nuget.org. Por ejemplo, puede usar el paquete CsvHelper para los archivos .csv.
