namespace Diamond;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: Diamond.exe <character>");
            Console.WriteLine("Example: Diamond.exe E");

            return;
        }

        if (args[0].Length != 1)
        {
            Console.WriteLine("You specified an invalid character. Please input a single character in the range [A-Z].");
            return;
        }

        var diamondBuilder = new DiamondBuilder();

        try
        {
            string diamond = diamondBuilder.Build(args[0][0]);
            Console.Write(diamond);

        }
        catch (InvalidCharacterException)
        {
            Console.WriteLine("You specified an invalid character. The character must be in range [A-Z].");
        }
    }
}