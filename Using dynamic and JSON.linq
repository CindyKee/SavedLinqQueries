<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

string result = @"{""AppointmentID"":463236,""Message"":""Successfully Appointment Booked"",""Success"":true,""MessageCode"":200,""isError"":false,""Exception"":null,""ReturnedValue"":null}";
dynamic d = JsonConvert.DeserializeObject<dynamic>(result);
Console.WriteLine($"d = {d}");

string message = d.Message;
int code = d.MessageCode;

// Copying without just passing the same reference
//dynamic anotherD = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(d));
//d.ReturnedValue = "Changed";
//Console.WriteLine($"d after change = {d}");
//Console.WriteLine($"other one = {anotherD}");

dynamic expando = new ExpandoObject();
expando.JsonField = "testData";
expando.newField = "dynamically added";
var testExpando = (ExpandoObject)expando;
Console.WriteLine($"Verify equal: {expando.Equals(testExpando)}");

dynamic dynamicJson = JsonConvert.DeserializeObject<dynamic>("{ 'JsonField': 'testData'}");
dynamicJson.newField = "dynamically added";

Console.WriteLine($"expando: {JsonConvert.SerializeObject(expando)}");
Console.WriteLine($"expando.TestField: {expando.JsonField}");
Console.WriteLine($"dynamicJson: {dynamicJson}");
Console.WriteLine($"dynamicJson.TestField: {dynamicJson.JsonField}");
foreach (var element in (IDictionary<string,object>)expando)
{
	Console.WriteLine($"{element.Key}: {element.Value}");
}