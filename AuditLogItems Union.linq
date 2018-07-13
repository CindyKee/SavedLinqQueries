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
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Services.dll</Reference>
  <NuGetReference>AutoMapper</NuGetReference>
  <Namespace>AutoMapper</Namespace>
  <Namespace>AutoMapper.QueryableExtensions</Namespace>
  <Namespace>Filevine.Common</Namespace>
  <Namespace>Filevine.Services</Namespace>
  <Namespace>Filevine.Services.Reports.ReportRunners</Namespace>
</Query>

AutoMapperConfig.Initialize();

var userID = 1452;		// 1187 for a non-admin user; 1022 for admin but not God user

AuditLogItems
	// All users can see all of their logs, regardless of log type
	.Where(a => a.UserID == userID)

	.Union(AuditLogItems
		// Org Admins can see all of the logs affecting their org
		.Where(a => a.AffectedOrg.Members.Any(m => m.UserID == userID && m.AccessLevel == Filevine.Common.AccessLevel.Admin) && a.LogType >= AuditLogType.Advanced)
	)
	
	.Union(AuditLogItems
		// Project Admins can see all of the logs affecting their project
		.Where(a => a.AffectedProject.Permissions.Any(m => m.UserID == userID && m.AccessLevel == AccessLevel.Admin) && a.LogType >= AuditLogType.Advanced)
	)

//	.Union(AuditLogItems
//		// Filevine Auditors can see ALL logs
//		.Where(a => a.AffectedProject.Permissions.Any(m => m.UserID == userID && m.AccessLevel == AccessLevel.Auditor))
//	)
	.ProjectTo<AuditForReport>()
	.Select(a => new { a.ID, a.LogType, a.TimeStamp, a.Summary, a.Action, a.UserID, a.DetailsJson })
	.OrderByDescending(a => a.TimeStamp)
	.Take(20)
//	.Count()
	.Dump();
