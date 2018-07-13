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
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Search.dll</Reference>
  <Namespace>Filevine.Domain.Entities</Namespace>
  <Namespace>Filevine.Search.Objects</Namespace>
</Query>

void Main()
{
	var offset = 5;
	var limit = 10;
	var isReindexing = false;
	var personIds = new List<int> { 295937, 461442, 503938, 503944 };
	
	var filteredIds = personIds.Take(10);//.Skip(offset).Take(limit);
	//filteredIds.Dump();
	
	// _queryBasePerson()
	var query = CustomData
					.OfType<PersonCustomDataElement>()
					.AsNoTracking()
					.Where(e => e.CustomField != null &&
								e.CustomSection != null &&
								!e.CustomSection.IsHidden && e.Project != null && e.Person != null);
	//query.Take(10).Dump();
	
	var queryBasePerson = isReindexing ? query : query.Where(e => filteredIds.Contains(e.PersonID));
	//queryBase.Dump();

	// _queryBasePersonList()
	var queryList = CustomData
					.OfType<PersonListCustomDataElement>()
					.AsNoTracking()
					.Where(e => e.CustomField != null &&
								e.CustomSection != null &&
								!e.CustomSection.IsHidden && e.Project != null && e.PersonListItems.Any(i => i.Person != null));
	//queryList.Take(10).Dump();

	var queryBasePersonList = isReindexing ? queryList : queryList.Where(e => e.PersonListItems.Any(i => filteredIds.Contains(i.PersonID)));
	//queryBasePersonList.Dump();

	// test concat
	var all = queryBasePerson
		.Select(p => p.Key)
		.Concat(queryBasePersonList.Select(l => l.Key))
		.OrderBy(p => p)
		.Skip(offset)
		.Take(limit);
//	all.Count().Dump();
	Console.WriteLine("Keys to find from queries:");
	all.Dump();
		
//	var dist = all.Distinct();
//	dist.Count().Dump();
	
	
	// GetNextChunkFromDb
	var chunk = queryBasePerson
		.Select(e => new Result
		{
			Key = e.Key,
			PersonID = e.PersonID,
			FirstName =  e.Person.FirstName,
			LastName = e.Person.LastName,
			FullName = e.Person.Fullname,
			Phones = e.Person.Phones.Select(h => new SearchableNestedPhone { RawNumber = h.RawNumber, Number = h.Number })
		})
		.Where(e => all.Contains(e.Key))
//		.OrderBy(e => e.Key)
//		.Skip(offset)
//		.Take(limit)
		.ToList();
	chunk.Dump();
	
	var chunk2 = queryBasePersonList
			.SelectMany(e => e.PersonListItems.Select(i => new Result
			{
				Key = e.Key,
				PersonID = i.PersonID,
				FirstName = i.Person.FirstName,
				LastName = i.Person.LastName,
				FullName = i.Person.Fullname,
				Phones = i.Person.Phones.Select(h => new SearchableNestedPhone { RawNumber = h.RawNumber, Number = h.Number })
			}))
			.Where(e => all.Contains(e.Key))
//			.OrderBy(e => e.Key)
//			.Skip(offset)
//			.Take(limit)
			.ToList();
	//chunk2.Dump();
		
	var final = chunk.Concat(chunk2).ToList();
	final.Dump();
//	}

	Console.WriteLine($"Return the final chunk with {chunk.Count()} items.");
}

public class Result
{
	public string Key { get; set; }
	public int PersonID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string FullName { get; set; }
	public IEnumerable<SearchableNestedPhone> Phones { get; set; }
}
// Define other methods and classes here
