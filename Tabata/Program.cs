using System.Runtime.InteropServices;

void StartTimer()
{
    TabataTimer time = new TabataTimer();

    time.PrintMenu();
}

StartTimer();


class TabataTimer
{
    //Needed for setting console window into foreground
    [DllImport("kernel32.dll", ExactSpelling = true)]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    //variables
    int repititions;

    void BringConsoleToFront()
    {

        SetForegroundWindow(GetConsoleWindow());
        ShowWindow(GetConsoleWindow(), 9);
    }

    void TimerLoopOutput(int min, int reps, string text)
    {
        for (int minutes = min; minutes >= 0; minutes--)
        {
            for (int seconds = 59; seconds >= 0; seconds--)
            {
                Console.WriteLine(text);
                Console.WriteLine("Minutes: " + minutes + " Seconds: " + seconds);
                Console.WriteLine("Repititions Left: " + reps);
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    public void PrintMenu()
    {
        Console.WriteLine("-----Menu-----");
        Console.WriteLine("1. Simple Timer(Optional Points:reptition count timer 25-5)");
        Console.WriteLine("2. More Options Simple Timer(Optional Points:repitition count - naming of the routines 25-5");
        Console.WriteLine("3. advanced timer(Optional Points:repition count - naming - time of routines");
        Console.Write("Selection: ");

        int SelectionNumber;

        SelectionNumber = Convert.ToInt32(Console.ReadLine());

        switch (SelectionNumber)
        {
            case 1:
                PrintFirstMenu();
                break;
            case 2:
                PrintSecondMenu();
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    //Starting point of the Program where optional stuff could be set
    void PrintFirstMenu()
    {

        Console.WriteLine("Welcome to the Tabata timer!");
        Console.WriteLine("How often do you want to repeat your session?");
        Console.Write("Repitition Count: ");
        repititions = Convert.ToInt32(Console.ReadLine());

        PrintTimer();
    }

    void PrintSecondMenu()
    {
        Console.WriteLine("Welcome to the Tabata timer!");
        Console.WriteLine("How often do you want to repeat your session?");
        Console.Write("Repitition Count: ");
        repititions = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Workname: ");
        string workName = Console.ReadLine();

        Console.WriteLine("Pausename: ");
        string pauseName = Console.ReadLine();

        PrintTimer(workName,pauseName);
    }

    void PrintTimer(string workName = "Work",string pauseName = "Pause")
    {
        while (repititions > 0)
        {
            TimerLoopOutput(25, repititions, "---" + workName + "---");
            BringConsoleToFront();
            TimerLoopOutput(5, repititions, "---" + pauseName + "---");
            repititions--;
        }
    }
}