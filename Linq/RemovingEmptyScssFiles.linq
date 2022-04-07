<Query Kind="Program" />

void Main()
{
	var files = Directory.GetFiles(@"D:\Source\V4\V4%20App\", "*.scss", SearchOption.AllDirectories);
	IList<string> emptyFiles = new List<string>();
	Regex comments = new Regex(@"/\*(.|\n)*?\*/");
	
	//Get all empty files or files with only comments
	foreach(string file in files)
	{
		var str = comments.Replace(File.ReadAllText(file), "");
		str = str.Trim();
		if(string.IsNullOrEmpty(str))
		{
			emptyFiles.Add(file);
		}
	}
	
	Console.WriteLine(emptyFiles.Count);
	
	foreach(string filePath in emptyFiles)
	{
		var fileName = Path.GetFileName(filePath);
		var directoryName = Path.GetDirectoryName(filePath);

		var tsFiles = Directory.GetFiles(directoryName, "*.ts");
		foreach (var tsFile in tsFiles)
		{
			string tempPath = Path.GetTempFileName();
			try
			{
				using (var reader = new StreamReader(tsFile))
				{
					using (var writer = new StreamWriter(File.OpenWrite(tempPath)))
					{
						while (!reader.EndOfStream)
						{
							var line = reader.ReadLine();
							if (!line.Contains(fileName))
							{
								writer.WriteLine(line);
							}
						}
					}
				}
				if (File.Exists(tempPath))
				{
					File.Delete(tsFile);
					File.Move(tempPath, tsFile);
				}
				File.Delete(filePath);
			}
			catch (Exception ex)
			{
				Console.WriteLine(filePath);
			}
		}
	}
}

// You can define other methods, fields, classes and namespaces here