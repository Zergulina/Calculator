using Lab6;

var historyService = new HistoryService("./history.txt");
historyService.Add("aboba1");
historyService.Add("aboba2");
historyService.Add("aboba3");
historyService.Add("aboba4");
historyService.Add("aboba5");
Console.WriteLine(historyService.GetPrevious());
Console.WriteLine(historyService.GetPrevious());
Console.WriteLine(historyService.GetCurrent());
Console.WriteLine(historyService.GetNext());