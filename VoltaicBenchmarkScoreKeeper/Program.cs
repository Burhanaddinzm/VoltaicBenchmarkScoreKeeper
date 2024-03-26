//Variables and Arrays
string[] noviceBenhchmarks =
    {
    "NOVICE:",
    "VT ANGLESHOT NOVICE",
    "VT WAVESHOT NOVICE",
    "VT SIXSHOT NOVICE",
    "VT MULTISHOT 90 NOVICE",
    "VT SUAVETRACK NOVICE",
    "VT STEADYTRACK NOVICE",
    "VT PILLTRACK NOVICE",
    "VT AXITRACK NOVICE",
    "VT SPHERESWITCH NOVICE",
    "VT SKYSWITCH NOVICE",
    "VT ANGLESWITCH NOVICE",
    "VT ARCSWITCH NOVICE"
    };

string[] intermediateBenchmarks =
    {
    "INTERMEDIATE:",
    "VT ANGLESHOT INTERMEDIATE",
    "VT WAVESHOT INTERMEDIATE",
    "VT FIVESHOT INTERMEDIATE",
    "VT MULTISHOT 120 INTERMEDIATE",
    "VT SUAVETRACK INTERMEDIATE",
    "VT STEADYTRACK INTERMEDIATE",
    "VT PILLTRACK INTERMEDIATE",
    "VT AXITRACK INTERMEDIATE",
    "VT SPHERESWITCH INTERMEDIATE",
    "VT SKYSWITCH INTERMEDIATE",
    "VT DODGESWITCH INTERMEDIATE",
    "VT ARCSWITCH INTERMEDIATE"
    };

string[] advancedBenchmarks =
    {
    "ADVANCED:",
    "VT ANGLESHOT ADVANCED",
    "VT WAVESHOT ADVANCED",
    "VT ARCSHOT ADVANCED",
    "VT FOURSHOT WIDE ADVANCED",
    "VT THREESHOT ADVANCED",
    "VT MULTISHOT 180 ADVANCED",
    "VT SUAVETRACK ADVANCED",
    "VT STEADYTRACK ADVANCED",
    "VT MINITRACK ADVANCED",
    "VT BLINKTRACK ADVANCED",
    "VT PILLTRACK ADVANCED",
    "VT AXITRACK ADVANCED",
    "VT SWAYSWITCH ADVANCED",
    "VT SPHERESWITCH ADVANCED",
    "VT SKYSWITCH ADVANCED",
    "VT DODGESWITCH ADVANCED",
    "VT ANGLESWITCH ADVANCED",
    "VT ARCSWITCH ADVANCED"
    };

string[][] benchmarks =
{
    noviceBenhchmarks, intermediateBenchmarks, advancedBenchmarks
};

string fileName = "Score_Keeper.txt";
string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

//Main app function
RunApp(path, benchmarks);

//Functions
void RunApp(string path, string[][] benchmarks)
{
    ShowMainMenu();
    string? request = Console.ReadLine();

    while (request != "0")
    {
        Console.ResetColor();
        switch (request)
        {
            case "1":
                GetCorrespondingBenchmark(benchmarks);
                break;
            case "2":
                ShowScores(path);
                break;
            case "3":
                DeleteScores(path);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Option!");
                Console.WriteLine("");
                break;
        }
        Console.ResetColor();
        ShowMainMenu();
        request = Console.ReadLine();
        Console.WriteLine("");

    }
}

void ShowMainMenu()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("1.Select benchmark tier");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("2.Show scores");
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("3.Reset scores");
    Console.ResetColor();
    Console.WriteLine("0.Close application");
}

void ShowTierMenu()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Select benchmark tier:");
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("1.Novice");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("2.Intermediate");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("3.Advanced");
    Console.ResetColor();
    Console.WriteLine("0.Back");
}

void ShowScores(string path)
{
    if (File.Exists(path))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(File.ReadAllText(path));
        Console.WriteLine("");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You haven't added any scores yet!");
        Console.WriteLine("");
    }
}

void DeleteScores(string path)
{
    if (File.Exists(path))
    {
        File.Delete(path);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Scores successfully reset!");
        Console.WriteLine("");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The scores have already been reset!");
        Console.WriteLine("");
    }
}

void GetCorrespondingBenchmark(string[][] benchmarks)
{
    Console.WriteLine("");
    ShowTierMenu();
    string? request = Console.ReadLine();

    while (request != "0")
    {
        Console.ResetColor();
        switch (request)
        {
            case "1":
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                WriteScore(benchmarks[0]);
                break;
            case "2":
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                WriteScore(benchmarks[1]);
                break;
            case "3":
                Console.ForegroundColor = ConsoleColor.Magenta;
                WriteScore(benchmarks[2]);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Option!");
                Console.WriteLine("");
                break;
        }
        Console.ResetColor();
        ShowTierMenu();
        request = Console.ReadLine();
        Console.WriteLine("");
    }
}

void WriteScore(string[] benchmarkTier)
{
    Console.WriteLine("");
    using (StreamWriter sw = new StreamWriter(path, true))
    {
        sw.WriteLine(benchmarkTier[0]);
        sw.WriteLine(DateTime.Now.ToString());

        for (int i = 1; i < benchmarkTier.Length; i++)
        {
            TakeUserInput(sw, benchmarkTier, i);
        }

        sw.WriteLine("");
    }
}

void TakeUserInput(StreamWriter sw, string[] benchmarkTier, int index)
{
    Console.WriteLine($"{benchmarkTier[index]}:");

    int.TryParse(Console.ReadLine(), out int input);

    sw.WriteLine($"{benchmarkTier[index]}:{input}");
}