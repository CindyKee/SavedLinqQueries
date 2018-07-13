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

var timeOfDay = AutoReportTimeOfDay.Mornings;

var today = DateTime.UtcNow.AddHours(-7);
var dayOfWeek = today.DayOfWeek;
var dayOfMonth = today.Date.Day;
var month = today.Month;


//var scheduleType = AutoReportScheduleType.Weekly;
//var schedules = new List<int> { 4, 5, 6 };

// Get the schedules that should run today for each schedule type.
var settings = AutoReportScheduleSettings
	.Where(ar => (ar.ScheduleType == AutoReportScheduleType.Weekly && ar.Schedule == (int)dayOfWeek)
			|| (ar.ScheduleType == AutoReportScheduleType.Monthly && ar.Schedule == dayOfMonth)
			|| (ar.ScheduleType == AutoReportScheduleType.Yearly && ar.Schedule == month))
	.Dump();


var fromAutoReportSchedule = SavedReportUserSettings
                .Include(s => s.SavedReport)
				.Include(s => s.AutoReportScheduleSettings)
				.Where(s => s.AutoReportTimeOfDay == timeOfDay
						&& s.AutoReportScheduleSettings.Any(ar =>
								(ar.ScheduleType == AutoReportScheduleType.Weekly && ar.Schedule == (int)dayOfWeek)
							|| (ar.ScheduleType == AutoReportScheduleType.Monthly && ar.Schedule == dayOfMonth)
							|| (ar.ScheduleType == AutoReportScheduleType.Yearly && ar.Schedule == month && dayOfMonth == 1)
						)
						&&
						(
                            s.SavedReport.ReportUsers.Any(u => u.UserID == s.UserID)
                            ||
                            s.SavedReport.ReportOrgs.Any(o => o.Org.Members.Any(m => m.UserID == s.UserID))
                        )
            	).ToList();

fromAutoReportSchedule.Dump();