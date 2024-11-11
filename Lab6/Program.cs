
while (true)
{
    string a = Console.ReadLine();
    Console.WriteLine(MathOperationsService.Calculate(a.ToRPN()));
}