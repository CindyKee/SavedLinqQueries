<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
	string json = @"{
	  'Department': 'Furniture',
	  'JobTitle': 'Carpenter',
	  'FirstName': 'John',
	  'LastName': 'Joinery',
	  'BirthDate': '1983-02-02T00:00:00'
	}";

	Person person = JsonConvert.DeserializeObject<Employee>(json, new PersonConverter());
	
	Console.WriteLine(person.GetType().Name);
	// Employee
	
	person.Dump();
	
	Employee employee = (Employee)person;

	Console.WriteLine($"Department: {employee.Department}\nJob Title: {employee.JobTitle}");
	// Carpenter
}

// Define other methods and classes here
public class Person
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime Birthdate { get; set; }
}

public class Employee : Person 
{
	public string Department { get; set; }
	public string JobTitle { get; set; }
}

public class PersonConverter : CustomCreationConverter<Person>
{
	public override Person Create(Type objectType)
	{
		objectType.FullName.Dump();
		return (Person)Activator.CreateInstance(objectType); //new Employee();
	}
}