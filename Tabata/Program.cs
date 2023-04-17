using System.Runtime.InteropServices;

//Program start 
TabataTimer time = new TabataTimer();
time.PrintMenu();


class TabataTimer
{
    //variables
    int repititions;

    //Needed for setting console window into foreground
    [DllImport("kernel32.dll", ExactSpelling = true)]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    void BringConsoleToFront()
    {

        SetForegroundWindow(GetConsoleWindow());
        ShowWindow(GetConsoleWindow(), 9);
    }

    //Main TimeLoop for the time output in the console
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

    //printing the menu to the user and letting select one of the options
    public void PrintMenu()
    {
        Console.WriteLine("-----Menu-----");
        Console.WriteLine("1. Simple Timer(Optional Points:reptition count timer 25-5)");
        Console.WriteLine("2. More Options Simple Timer(Optional Points:repitition count - naming of the routines 25-5");
        Console.WriteLine("3. advanced timer(Optional Points:repition count - naming - time of routines");
        Console.Write("Selection: ");

        var result = int.TryParse(Console.ReadLine(), out int SelectionNumber );

        if(result)
        {
            switch (SelectionNumber)
            {
                case 1:
                    PrintFirstMenu();
                    break;
                case 2:
                    PrintSecondMenu();
                    break;
                case 3:
                    PrintThirdMenu();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.Clear();
            PrintMenu();
        }
    }

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

    void PrintThirdMenu()
    {
        Console.WriteLine("Welcome to the Tabata timer!");
        Console.WriteLine("How often do you want to repeat your session?");
        Console.Write("Repitition Count: ");
        repititions = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Workname: ");
        string workName = Console.ReadLine();

        Console.WriteLine("Pausename: ");
        string pauseName = Console.ReadLine();

        Console.WriteLine("Worktime: ");
        int workMinutes = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("PauseMinutes");
        int pauseMinutes = Convert.ToInt32(Console.ReadLine());

        PrintTimer(workName, pauseName,workMinutes,pauseMinutes);
    }

    //uses all information gathered from the user and uses the main time method to let the work session get started
    void PrintTimer(string workName = "Work",string pauseName = "Pause",int workMinutes = 25, int pauseMinutes = 5)
    {
        while (repititions > 0)
        {
            TimerLoopOutput(workMinutes - 1, repititions, "---" + workName + "---");
            BringConsoleToFront();
            TimerLoopOutput(pauseMinutes - 1, repititions, "---" + pauseName + "---");
            repititions--;
        }
    }
}
