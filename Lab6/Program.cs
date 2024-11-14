using Lab6;

var historyService = new HistoryService("./history.txt");

while (true)
{
    string expression = Console.ReadLine();
    if (expression.Equals("next", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine(historyService.GetNext());
    }
    else if (expression.Equals("current", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine(historyService.GetCurrent());
    }
    else if (expression.Equals("previous", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine(historyService.GetPrevious());
    }
    else
    {
        try
        {
            var result = MathOperationsService.Calculate(expression.ToRPN());
            historyService.Add(expression + " = " + result);
            Console.WriteLine(result);
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception _)
        {
            Console.WriteLine("Ошибка в набранном выражении!");
        }
    }
}