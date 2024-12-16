using Raylib_cs;

namespace HelloWorld;

class program
{
    public static void Main()
    {
        oyun oyun = new oyun();
        oyun.StartGame();
        oyun.UpdateGame();
        oyun.EndGame();
    }
}