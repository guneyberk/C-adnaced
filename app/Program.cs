//Inspect contents of assembly (types, methods, properties)
//Not directly referenced
//Bypass accses modifiers
using System.Reflection; 
public class Program{
    public static void Main(string[] args)
    {
        Console.WriteLine(typeof(Program).Assembly.GetTypes()); 
    }
}

public class Exercise{
    public class Player{
        private void NewFunc()
        {
            Console.WriteLine("NewFunc");
        }
    }
    private class Player2{}
    private class Player3{}
}