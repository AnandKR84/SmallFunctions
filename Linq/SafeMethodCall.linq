<Query Kind="Program" />

void Main()
{
	int result = SafeCall(()=>{
		return CallMethod(40, 20);
	});		
	
	Console.WriteLine(default(int));
	Console.WriteLine(result);
}

T SafeCall<T>(Func<T> p)
{
	try{
	 	return p();
	}
	catch(Exception ex)
	{
		Console.WriteLine(ex.ToString());
	}
	return default(T);
}

int CallMethod(int a, int b)
{
	return a + b;
}

// You can define other methods, fields, classes and namespaces here