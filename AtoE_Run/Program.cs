using TagLib;

Console.WriteLine("Please Enter Path");
String? Path = Console.ReadLine();

while (Path == null || Path == string.Empty)
{
    Console.WriteLine("Path Cannot Be Null Or Empty");
    Console.WriteLine("Please Enter A Valid Path or 0 to exit");
    Path = Console.ReadLine();
    if (Path == "0")
    {
        Environment.Exit(0);
    }
}

while (!Directory.Exists(Path))
{
    Console.WriteLine("Path Dose Not Exists");
    Console.WriteLine("Please Enter A Valid Path or 0 to exit");
    Path = Console.ReadLine();
    if (Path == "0")
    {
        Environment.Exit(0);
    }
}

logic();



async void logic()
{
    String[] Files = Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories)
        .Where(x => x.ToLower().Contains("mp3") || x.ToLower().Contains("wav")).ToArray();

    for (int i = 0; i < Files.Length; i++)
    {
        _ = await ClearTags(Files[i]);
    }
}




Task<bool> ClearTags(String FileEntity)
{
    try
    {
        FileEntity = FileEntity.Replace(@"\\", @"/");
        var tffile = TagLib.File.Create(FileEntity);
        tffile.RemoveTags(TagTypes.AllTags);
        tffile.Save();
        
    }
    catch (Exception e)
    {
        Console.WriteLine($"{FileEntity} {e.Message}");
        String s = System.IO.File.ReadAllText(@"D:\Tags.txt");
        using (StreamWriter writer = new(@"D:\Tags.txt"))
        {

            writer.Write($"{s}\n\n{FileEntity}\n{e.Message}");
        }

    }
    ArToEn(FileEntity);
    return Task.FromResult(true);
}

void ArToEn(String FileEntity)
{
    AtoEFunctions.ArCharacters xyz = new();
    String? NewPath = FileEntity;
    if (xyz.ArToEnChar(FileEntity, out NewPath))
    {
        if (NewPath != null)
        {
            try
            {
                System.IO.File.Move(FileEntity, NewPath);
                Console.WriteLine(NewPath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{FileEntity} {e.Message}");
                String s = System.IO.File.ReadAllText(@"D:\ARtEN.txt");
                using (StreamWriter writer = new(@"D:\ARtEN.txt"))
                {

                    writer.Write($"{s}\n\n{FileEntity}\n{NewPath}\n{e.Message}");
                }
            }
        }
    }
}