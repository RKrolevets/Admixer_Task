using ThreeInaRow;

public class Program
{
    public static void Main()
    {
        Field matrix = new ();
        Console.WriteLine("This is the instance matrix");
        matrix.Print();

        while (matrix.FindReplaceDuplicates())
        {
            Console.WriteLine("This is the matrix after finding duplicates:");
            matrix.Print();
        }
        Console.WriteLine("The last matrix is the final matrix without duplicates");

    }
}
