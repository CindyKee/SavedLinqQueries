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

// This does not work at all! Flawed query!

var scheduleType = AutoReportScheduleType.Weekly;
var timeOfDay = AutoReportTimeOfDay.Mornings;
var schedules = new List<int> { 4, 5, 6 };

var savedReportUserSettings = AutoReportScheduleSettings
	.Where(arss => arss.ScheduleType == scheduleType
			&& schedules.Any(s => s == arss.Schedule)
		)
	.SelectMany(arss => arss.SavedReportUserSettings)
	.Include(s => s.SavedReport)
//	.ToList()
//	.Dump();
	

//var fromAutoReportSettings = savedReportUserSettings
	.Where(srus => srus.AutoReportTimeOfDay == timeOfDay
			&& (
				srus.SavedReport.ReportUsers.Any(ru => ru.UserID == srus.UserID)
				|| srus.SavedReport.ReportOrgs.Any(o => o.Org.Members.Any(m => m.UserID == srus.UserID)))
			)
	.ToList()
	.Dump();
	
//SavedReports
//	.Where
//	(
//		sr => //sr.ID == 2224 &&
//			(sr.ReportUsers.Any(ru => ru.UserID == 1452)
//				|| sr.ReportOrgs.Any(o => o.Org.Members.Any(m => m.UserID == 1452)))
//	)

//SavedReportUserShares
//	.Where(s => s.UserID == 1452)
	//.Where(s => s.SavedReportID == 2224 && s.UserID == 1452)
	
//SavedReportOrgShares
//	.Where(s => s.SavedReportID == 2224 && s.Org.Members.Any(m => m.UserID == 1452))
//