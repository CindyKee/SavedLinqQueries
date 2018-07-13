<Query Kind="Statements" />

var list1 = new List<(string, int)> { ("zero", 0), ("one", 1), ("two", 2), ("three", 3), ("four", 4)};
var list2 = new List<(string, int)> { ("two", 2), ("three", 3), ("four", 4), ("five", 5), ("six", 6), ("seven", 7), ("two", 12) };

//// What we want:
//var mynewItems = list2.Except(list1);
//mynewItems.Dump();
//list1.Except(list2).Dump();

//foreach (var item in list2)
//{
////	if (list1.All(x => x.Item1 != "two" && x.Item2 != 12))
//		if (list1.All(x => !(x.Item1 == item.Item1 && x.Item2 == item.Item2)))
//	{
//		Console.WriteLine($"Except: ({item.Item1}, {item.Item2})");
//	}
//}

// from updateToValueOf logic:
var newItems = list2.Where(l => list1.All(li => !(li.Item1 == l.Item1 && li.Item2 == l.Item2)));
newItems.Dump();
var removeItems = list1.Where(l => list2.All(li => !(li.Item1 == l.Item1 && li.Item2 == l.Item2)));
removeItems.Dump();

// Verify a specific property is unique across the list/set
(list2.GroupBy(l => l.Item1).Count() == list2.Count()).Dump();
