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
  <Reference>C:\GitHub\filevine\Filevine.Common\bin\Debug\Filevine.Common.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine.Domain\bin\Filevine.Domain.dll</Reference>
  <Namespace>Filevine.Common</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
</Query>

var reportToUpdate = SavedReportUserSettings
						.Include(s => s.SavedReport)
						.Include(s => s.AutoReportScheduleSettings)
						.First(s => s.UserID == 1452
								&& s.AutoReportTimeOfDay == AutoReportTimeOfDay.Off
								&& !s.AutoReportScheduleSettings.Any()
								&&
								(
									s.SavedReport.ReportUsers.Any(u => u.UserID == s.UserID)
									||
									s.SavedReport.ReportOrgs.Any(o => o.Org.Members.Any(m => m.UserID == s.UserID))
								)
						);

reportToUpdate.Dump();


var sched = AutoReportScheduleSettings
	.Where(a => a.ScheduleType == Filevine.Common.AutoReportScheduleType.Monthly && new List<int> { 7, 14, 21, 28 }.Any(d => d == a.Schedule))
//	.Where(a => a.ScheduleType == Filevine.Common.AutoReportScheduleType.Yearly && new List<int> { 6, 12 }.Any(d => d == a.Schedule))
	.ToList();
sched.Dump();

reportToUpdate.AutoReportTimeOfDay = AutoReportTimeOfDay.Mornings;
//sched.ForEach(s => reportToUpdate.AutoReportScheduleSettings.Add(s));
((List<AutoReportScheduleSettings>)reportToUpdate.AutoReportScheduleSettings).AddRange(sched);
reportToUpdate.Dump();

//SaveChanges();

	
