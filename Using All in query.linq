<Query Kind="Statements">
  <Connection>
    <ID>2f1a4320-4d25-41e8-b80f-4fceedf63f2f</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Data.EF.dll</CustomAssemblyPath>
    <CustomTypeName>Filevine.Data.FilevineContext</CustomTypeName>
    <AppConfigPath>C:\GitHub\filevine\Filevine\Web.config</AppConfigPath>
    <DisplayName>FilevineContext</DisplayName>
  </Connection>
  <Reference>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Common.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Data.EF.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Domain.dll</Reference>
  <Namespace>Filevine.Common</Namespace>
  <Namespace>Filevine.Data.Repositories</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
</Query>

var dbSchedList = new List<AutoReportScheduleSettings>
{
	new AutoReportScheduleSettings { ID = 1, ScheduleType = AutoReportScheduleType.Weekly, Schedule = 0 },
	new AutoReportScheduleSettings { ID = 3, ScheduleType = AutoReportScheduleType.Weekly, Schedule = 2 },
	new AutoReportScheduleSettings { ID = 5, ScheduleType = AutoReportScheduleType.Weekly, Schedule = 4 },
	new AutoReportScheduleSettings { ID = 6, ScheduleType = AutoReportScheduleType.Weekly, Schedule = 5 }
};

//dbSchedList.Dump();

var scheduleType = AutoReportScheduleType.Weekly;
var schedules = new List<int> { 0, 2, 4 };

//var scheduleSettings = AutoReportScheduleSettings
//	.Where(ar => ar.ScheduleType == scheduleType 
//			&& schedules.Any(sch => sch == ar.Schedule))
//	//.All(a => a.ScheduleType == AutoReportScheduleType.Monthly)
//	;
//
//scheduleSettings.Dump();

// Another test of the All method on an empty list.
var empty = new List<AutoReportScheduleSettings>();

// The All() method succeeds if the list is empty! We don't want that!
var all = empty.All(e => e.ScheduleType == scheduleType
					&& schedules.Any(s => s == e.Schedule));
all.Dump();

// Verify the list is not empty BEFORE checking if all elements match!
var none = empty.Any() 
			&& empty.All(e => e.ScheduleType == scheduleType
					&& schedules.Any(s => s == e.Schedule));
none.Dump();

var notAll = dbSchedList.All(e => e.ScheduleType == scheduleType
					&& schedules.Any(s => s == e.Schedule));
notAll.Dump();