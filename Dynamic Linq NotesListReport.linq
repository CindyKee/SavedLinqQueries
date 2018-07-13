<Query Kind="Statements">
  <Connection>
    <ID>8e8f57af-dc46-460c-963f-0c9ba1d23698</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Data.EF.dll</CustomAssemblyPath>
    <CustomTypeName>Filevine.Data.FilevineContext</CustomTypeName>
    <AppConfigPath>C:\GitHub\filevine\Filevine\Web.config</AppConfigPath>
    <DisplayName>Filevine</DisplayName>
  </Connection>
  <Reference>C:\GitHub\filevine\Filevine.Services\bin\EntityFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Entity.dll</Reference>
  <Reference>C:\GitHub\filevine\System.Linq.Dynamic\Src\System.Linq.Dynamic\bin\Debug\System.Linq.Dynamic.dll</Reference>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Linq.Dynamic</Namespace>
  <IncludePredicateBuilder>true</IncludePredicateBuilder>
</Query>

var userId = 1452;
//var select = $"Note.FeedPins.Any(UserID == {userId})";
var select = $"FeedPins.Any(UserID == {userId})";
var expression = $"{select} AS pinnedToMyFeed";
var selectStatement = "new (Note.ID AS NoteID, Note.Body AS Body, " + expression + ")";
selectStatement.Dump();

var db = new FilevineContext();
var query = from project in db.Projects
			join note in db.Notes on project.ID equals note.ProjectID
			where note.AuthorID == 1502
			orderby note.ID
			select new
			{
				ProjectId = project.ID,
				NoteId = note.ID,
				FeedPins = note.FeedPins//.Where(p => p.NoteID == note.ID)
			};

//query.ToString().Dump();
query.ToList().Dump();

//var finalQuery = query.Select(n => new {n.ID, n.AuthorID, PinnedToMyProject = n.FeedPins.Any(f => f.UserID == userId)});
//"new (Note.FeedPins.Any(UserID == 1452) AS pinnedToMyFeed)"
//var newQuery = query.Select("new (Note.ID AS NoteID)");
//newQuery.ToString().Dump();
//newQuery.ToListAsync().Result.Dump();

//var finalQuery = query.Select(selectStatement);
//finalQuery.ToString().Dump();

//finalQuery.ToListAsync().Result.Dump();

//var mynote = db.Notes.First(n => n.ID == 2240708);

//mynote.Dump();
//
//mynote.FeedPins.Any(f => f.UserID == 1357).Dump();
//