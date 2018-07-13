<Query Kind="Program" />

void Main()
{
	var mySubClass = new SubClass { Id = 7, Name = "Cindy", Dynamic = "" };
	TestBaseClassMethod(mySubClass);
}

// Define other methods and classes here
public void TestBaseClassMethod(BaseClass testClass = null)
{
	var subClass = testClass as SubClass;
	Console.WriteLine($"SubClass Id = {subClass?.Id}");
}

public abstract class BaseClass
{}

public class SubClass : BaseClass
{
	public int Id { get; set; }
	public string Name { get; set; }
	public dynamic Dynamic { get; set; }
}