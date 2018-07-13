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
  <Reference>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Domain.dll</Reference>
  <Namespace>Filevine.Common</Namespace>
  <Namespace>Filevine.Common.Cache.Interfaces</Namespace>
  <Namespace>Filevine.Common.Cache.Objects</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
</Query>

var savedReportID = 13;
var userID = 1452;

var query = SavedReports
				.Where(r => 
					r.ReportUsers.Any(u => u.UserID == userID) 
					|| r.ReportOrgs.Any(o => o.Org.Members.Any(m => m.UserID == userID)) && r.ID == savedReportID
				)
				.Select(r => new { ThisReport = r, ThisUsersSettings = r.UserSettings.FirstOrDefault(s => s.UserID == userID) })
				.ToList()
				
				.Select(r => new SavedReportListItemResponse
				{
					ID = r.ThisReport.ID,
					DisplayName = r.ThisReport.DisplayName,
					IsFavorite = (r.ThisUsersSettings?.IsFavorite ?? false) && !(r.ThisUsersSettings?.IsHidden ?? false),
					IsHidden = r.ThisUsersSettings?.IsHidden ?? false,
					AutoReportTimeOfDay = r.ThisUsersSettings?.AutoReportTimeOfDay ?? AutoReportTimeOfDay.Off,
					AutoReportScheduleSettings = r.ThisUsersSettings?.AutoReportScheduleSettings.ToList()
                            .ConvertAll(input => new AutoReportScheduleSettingsResponse
                                {
                                    ID = input.ID,
                                    Schedule = input.Schedule,
                                    ScheduleType = input.ScheduleType
                                })
                            ?? new List<AutoReportScheduleSettingsResponse>()
				})
				
				.FirstOrDefault()
				;

query.Dump();