<Query Kind="Program">
  <Connection>
    <ID>2f1a4320-4d25-41e8-b80f-4fceedf63f2f</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Data.EF.dll</CustomAssemblyPath>
    <CustomTypeName>Filevine.Data.FilevineContext</CustomTypeName>
    <AppConfigPath>C:\GitHub\filevine\Filevine\Web.config</AppConfigPath>
    <DisplayName>FilevineContext</DisplayName>
  </Connection>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Common.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Domain.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Services.dll</Reference>
  <Namespace>Filevine.Common.Cache.Objects</Namespace>
  <Namespace>Filevine.Common.Cache.Interfaces</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
  <Namespace>Filevine.Services</Namespace>
</Query>

void Main()
{
	AutoMapperConfig.Initialize();

	var projectID = 169676;

	// Get ProjectLink elements from database
	var projectLinks = CustomData.OfType<ProjectLinkCustomDataElement>()
			.Where(e => e.ProjectID == projectID)
			.Select(p => new
			{
				Project = p.Project,
				ProjectLink = p.ProjectLink,
				CustomSection = p.CustomSection,
				CustomField = p.CustomField,
				CollectionItemGuid = p.CollectionItemGuid
			})
			.Union(
				CustomData.OfType<ProjectLinkListCustomDataElement>()
					.SelectMany(p => p.ProjectLinkListItems)
					.Where(e => e.ProjectID == projectID)
					.Select(p => new 
					{
						Project = p.ProjectLinkListCustomDataElement.Project,
						ProjectLink = p.ProjectLink,
						CustomSection = p.ProjectLinkListCustomDataElement.CustomSection,
						CustomField = p.ProjectLinkListCustomDataElement.CustomField,
						CollectionItemGuid = p.CollectionItemGuid
					}))
			.ToList()
			.Select(e => new ProjectRelatedProject
			{
				RelatedProject = AutoMapper.Mapper.Map<Project, ProjectGlance>(e.ProjectLink),
				Project = AutoMapper.Mapper.Map<Project, ProjectGlance>(e.Project),
				SectionKey = e.CustomSection.InternalName,
				SectionName = e.CustomSection.Name,
				FieldKey = e.CustomField.InternalName,
				FieldName = e.CustomField.Name,
				CollectionItemGuid = e.CollectionItemGuid
			})
			.ToList();

	//projectLinks.Dump();

	// Convert to response objects, complete with Breadcrumbs list
	var responses = projectLinks
		.Select(p => new ProjectRelatedProjectResponse
		{
			RelatedProject = p.RelatedProject,
			Breadcrumbs = getBreadcrumbs(p.Project.OrgName, p.Project.ProjectOrClientName, p.Project.IsArchived, p.Project.ID, p.Project.ClientPictureUrl,
				p.SectionKey, p.SectionName, p.GetLastBreadcrumbs)
		});

	responses.Dump();
}

// Define other methods and classes here
public class ProjectRelatedProject
{
	// Use to make Breadcrumb
	public ProjectGlance Project { get; set; }
	public string SectionKey { get; set; }
	public string SectionName { get; set; }
	public string FieldKey { get; set; }
	public string FieldName { get; set; }
	public Guid? CollectionItemGuid { get; set; }

	// Related Projects from Project Links
	public ProjectGlance RelatedProject { get; set; }

	public IEnumerable<Breadcrumb> GetLastBreadcrumbs(string sectionLink)
	{
		var crumbs = new List<Breadcrumb>();
		var itemID = string.Empty;

		// If this is a collection section, show that in the breadcrumbs
		if (CollectionItemGuid.HasValue && CollectionItemGuid.Value != default(Guid))
		{
			itemID = CollectionItemGuid.Value.ToString("D");

			crumbs.Add(new Breadcrumb
			{
				Title = $"{SectionName} Item",
				FaClass = "fa-th-large",
				LinkUrl = $"{sectionLink}?itemID={itemID}"
			});
		}

		if (!string.IsNullOrWhiteSpace(FieldKey))
		{
			crumbs.Add(new Breadcrumb
			{
				Title = FieldName,
				FaClass = "fa-code-fork",
				LinkUrl = $"{sectionLink}?itemID={itemID}&field={FieldKey}"
			});
		}

		return crumbs;
	}
}

public class ProjectRelatedProjectResponse
{
	//public ProjectGlance Project { get; set; }				// we need this for breadcrumbs of where this project came from
	public ProjectGlance RelatedProject { get; set; }       // we need this for project card
	public IEnumerable<Breadcrumb> Breadcrumbs { get; set; }
}

//public static IEnumerable<Breadcrumb> getBreadcrumbs(ProjectRelatedProject projectInfo)
public static IEnumerable<Breadcrumb> getBreadcrumbs(string orgName, string projectName, bool isArchived, int projectID, string pictureUrl,
	string sectionKey, string sectionName, Func<string, IEnumerable<Breadcrumb>> getLastBreadcrumbs)
{
	//ICustomMetaCache metaCache;

	var sectionIcon = "fa-anchor"; // metaCache.GetSection(projectInfo.SectionKey?.Icon;

	//	var parent = projectInfo.Project;
	return Breadcrumb.GetBreadcrumbs(orgName, projectName, isArchived, projectID, pictureUrl, $"custom/{sectionKey}", sectionName, sectionIcon, getLastBreadcrumbs);
}

