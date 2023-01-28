namespace vendingMachine;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Please enter the sum to change:");
            var sum = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Please enter first change bank note:");
            var a = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Please enter second change bank note:");
            var b = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Please enter percent (if you want to use without percent please enter 0): ");
            var percent = Convert.ToUInt32(Console.ReadLine());

            var vendingMachine = new VendingMachine(sum: sum, a: a, b: b, percent: percent);

            var change = vendingMachine.Change();
            Console.WriteLine(change == null
                ? "Minimum bank note is higher than the sum"
                : $"Your will get: {a}x{change.Item1} and {b}x{change.Item2} with remainder: {change.Item3} for the sum: {sum}");
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter only positive numbers");
        }
    }
}