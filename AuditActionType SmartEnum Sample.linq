<Query Kind="Program">
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Domain.dll</Reference>
  <NuGetReference>Ardalis.SmartEnum</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Ardalis.SmartEnum</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var action = AuditActionType.Update;
	action.Dump();
	Console.WriteLine($"Test the ToString() override of the SmartEnum: [{action}]");
	
	
	var fromValue = AuditActionType.FromValue(1);
	fromValue.Dump();
	
	var fromString = AuditActionType.FromName("Delete");
	fromString.Dump();
	
	var logItems = new List<AuditLogItemTest>
	{
		new AuditLogItemTest { Action = AuditActionType.Update, ActionDetails = "ChainDate.Update" },
		new AuditLogItemTest { Action = AuditActionType.Delete, ActionDetails = "ChainDate.Delete" },
		new AuditLogItemTest { Action = AuditActionType.Create, ActionDetails = "ChainDate.Create" },
		new AuditLogItemTest { Action = AuditActionType.Update, ActionDetails = "ChainDate.Update" },
		new AuditLogItemTest { Action = AuditActionType.Undelete, ActionDetails = "ChainDate.Undelete" },
		new AuditLogItemTest { Action = AuditActionType.Read, ActionDetails = "ChainDate.Read" },
	};

	//logItems.Dump();

	logItems.Where(i => i.Action == AuditActionType.Update)
		.Select(i => new { i.Action.Name }).Dump();
}

// Define other methods and classes here
public class AuditLogItemTest
{
	public AuditActionType Action { get; set; }
	public string ActionDetails { get; set; }
}

public class AuditActionType : Ardalis.SmartEnum.SmartEnum<AuditActionType, int>
{
	public static AuditActionType Create = new AuditActionType(1, "Create");
	public static AuditActionType Read = new AuditActionType(2, "Read");
	public static AuditActionType Update = new AuditActionType(3, "Update");
	public static AuditActionType Delete = new AuditActionType(4, "Delete");
	public static AuditActionType Execute = new AuditActionType(5, "Execute");
	public static AuditActionType Undelete = new AuditActionType(6, "Undelete");

	protected AuditActionType(int value, string name) : base(name, value)
	{ }
}