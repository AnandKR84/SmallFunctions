<Query Kind="Program" />

void Main()
{
	var files = Directory.GetFiles(@"D:\Source\V4\V4%20App\apps", "*.*", SearchOption.AllDirectories).ToList();
	files = files.Where(x=>!(x.EndsWith("*.png")||x.EndsWith(".gitkeep")||x.EndsWith(".browserslistrc"))).ToList();
	Regex comments1 = new Regex(@"[ |\t]//.*?\n");
	Regex comments2 = new Regex(@"[ |\t]/\*(.|\n)*?\*/");

	foreach (var file in files)
	{
		var fileContent = File.ReadAllText(file);
		string afterRemoval = comments1.Replace(fileContent, "\n");
		afterRemoval = comments2.Replace(afterRemoval, "\n");
		try
		{
			File.WriteAllText(file, afterRemoval);
		}
		catch(UnauthorizedAccessException ex)
		{
			Debug.WriteLine(ex.Message);
		}
	}
}

// You can define other methods, fields, classes and namespaces here