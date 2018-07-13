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
  <Namespace>Filevine.Common.Cache.Interfaces</Namespace>
  <Namespace>Filevine.Common.Cache.Objects</Namespace>
  <Namespace>Filevine.Common.Exceptions</Namespace>
  <Namespace>Filevine.Common.Push</Namespace>
  <Namespace>Filevine.Data.Repositories</Namespace>
  <Namespace>Filevine.Data.Repositories.Interfaces</Namespace>
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
var schedules = new List<int> { 0, 2, 4, 5 };

var scheduleSettings = AutoReportScheduleSettings
	.Where(arss => arss.ScheduleType == scheduleType && schedules.Any(sch => sch == arss.Schedule))
	.ToList();

//scheduleSettings.Dump();

var x = scheduleSettings.Select(s => new { s.ScheduleType, s.Schedule })
	.Except(dbSchedList.Select(sl => new { sl.ScheduleType, sl.Schedule})); //.Count() == 0;

var y = scheduleSettings.Select(s => s.Schedule).Except(dbSchedList.Select(sl => sl.Schedule));

x.Dump();
y.Dump();