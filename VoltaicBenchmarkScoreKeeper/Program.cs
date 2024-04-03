//Variables and Arrays
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using VoltaicBenchmarkScoreKeeper.Enums;

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
                int[][] scores = GetData();
                if (scores == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to get scores online!");
                    break;
                }
                WriteScoreOnline(benchmarks, scores);
                break;
            case "4":
                ChangeUsername();
                break;
            case "5":
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
    Console.WriteLine("1.Input scores manually");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("2.Show scores");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine("3.Get scores online");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("4.Change username");
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("5.Reset scores");
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

    sw.WriteLine($"{benchmarkTier[index]}:{input} - {CheckRank(benchmarkTier[0], benchmarkTier[index], input)}");
}

string CheckRank(string benchmarkTier, string scenario, int input)
{
    string rank = "";

    switch (benchmarkTier)
    {
        case "NOVICE:":
            switch (scenario)
            {
                case "VT ANGLESHOT NOVICE":
                    if (input < 400) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 460) rank = ScenarioRank.Iron.ToString();
                    else if (input < 520) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 650) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 650) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT WAVESHOT NOVICE":
                    if (input < 300) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 370) rank = ScenarioRank.Iron.ToString();
                    else if (input < 440) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 560) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 560) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT SIXSHOT NOVICE":
                    if (input < 600) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 700) rank = ScenarioRank.Iron.ToString();
                    else if (input < 800) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 1040) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 1040) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT MULTISHOT 90 NOVICE":
                    if (input < 750) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 850) rank = ScenarioRank.Iron.ToString();
                    else if (input < 950) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 1200) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 1200) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT SUAVETRACK NOVICE":
                    if (input < 1000) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1300) rank = ScenarioRank.Iron.ToString();
                    else if (input < 1600) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 1900) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 1900) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT STEADYTRACK NOVICE":
                    if (input < 1250) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1550) rank = ScenarioRank.Iron.ToString();
                    else if (input < 1850) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 2150) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 2150) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT PILLTRACK NOVICE":
                    if (input < 1650) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2050) rank = ScenarioRank.Iron.ToString();
                    else if (input < 2450) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 2850) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 2850) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT AXITRACK NOVICE":
                    if (input < 2600) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2900) rank = ScenarioRank.Iron.ToString();
                    else if (input < 3200) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 3500) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 3500) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT SPHERESWITCH NOVICE":
                    if (input < 1500) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1600) rank = ScenarioRank.Iron.ToString();
                    else if (input < 1700) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 2000) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 2000) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT SKYSWITCH NOVICE":
                    if (input < 1800) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2000) rank = ScenarioRank.Iron.ToString();
                    else if (input < 2150) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 2500) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 2500) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT ANGLESWITCH NOVICE":
                    if (input < 1000) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1200) rank = ScenarioRank.Iron.ToString();
                    else if (input < 1500) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 1700) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 1700) rank = ScenarioRank.Gold.ToString();
                    break;
                case "VT ARCSWITCH NOVICE":
                    if (input < 900) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1000) rank = ScenarioRank.Iron.ToString();
                    else if (input < 1100) rank = ScenarioRank.Bronze.ToString();
                    else if (input < 1400) rank = ScenarioRank.Silver.ToString();
                    else if (input >= 1400) rank = ScenarioRank.Gold.ToString();
                    break;
                default:
                    break;
            }
            break;
        case "INTERMEDIATE:":
            switch (scenario)
            {
                case "VT ANGLESHOT INTERMEDIATE":
                    if (input < 660) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 720) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 800) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 900) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 900) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT WAVESHOT INTERMEDIATE":
                    if (input < 530) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 590) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 670) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 750) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 750) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT FIVESHOT INTERMEDIATE":
                    if (input < 1020) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1120) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 1220) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 1320) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 1320) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT MULTISHOT 120 INTERMEDIATE":
                    if (input < 1170) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1270) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 1370) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 1490) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 1490) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT SUAVETRACK INTERMEDIATE":
                    if (input < 2600) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2900) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 3200) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 3500) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 3500) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT STEADYTRACK INTERMEDIATE":
                    if (input < 2200) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2500) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 2800) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 3100) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 3100) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT PILLTRACK INTERMEDIATE":
                    if (input < 2000) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2350) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 2750) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 3150) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 3150) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT AXITRACK INTERMEDIATE":
                    if (input < 2150) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2450) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 2750) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 2900) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 2900) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT SPHERESWITCH INTERMEDIATE":
                    if (input < 1700) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1900) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 2050) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 2200) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 2200) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT SKYSWITCH INTERMEDIATE":
                    if (input < 2350) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2550) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 2700) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 2900) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 2900) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT DODGESWITCH INTERMEDIATE":
                    if (input < 2100) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2250) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 2400) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 2550) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 2550) rank = ScenarioRank.Master.ToString();
                    break;
                case "VT ARCSWITCH INTERMEDIATE":
                    if (input < 1700) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1830) rank = ScenarioRank.Platinum.ToString();
                    else if (input < 1930) rank = ScenarioRank.Diamond.ToString();
                    else if (input < 2000) rank = ScenarioRank.Jade.ToString();
                    else if (input >= 2000) rank = ScenarioRank.Master.ToString();
                    break;
                default:
                    break;
            }
            break;
        case "ADVANCED:":
            switch (scenario)
            {
                case "VT ANGLESHOT ADVANCED":
                    if (input < 850) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1020) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 1100) rank = ScenarioRank.Nova.ToString();
                    else if (input < 1300) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 1300) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT WAVESHOT ADVANCED":
                    if (input < 860) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1030) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 1120) rank = ScenarioRank.Nova.ToString();
                    else if (input < 1375) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 1375) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT ARCSHOT ADVANCED":
                    if (input < 900) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1050) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 1200) rank = ScenarioRank.Nova.ToString();
                    else if (input < 1400) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 1400) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT FOURSHOT WIDE ADVANCED":
                    if (input < 1550) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1650) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 1750) rank = ScenarioRank.Nova.ToString();
                    else if (input < 1875) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 1875) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT THREESHOT ADVANCED":
                    if (input < 1250) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1350) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 1450) rank = ScenarioRank.Nova.ToString();
                    else if (input < 1560) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 1560) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT MULTISHOT 180 ADVANCED":
                    if (input < 1490) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 1580) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 1670) rank = ScenarioRank.Nova.ToString();
                    else if (input < 1800) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 1800) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT SUAVETRACK ADVANCED":
                    if (input < 3530) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 4000) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 4150) rank = ScenarioRank.Nova.ToString();
                    else if (input < 4450) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 4450) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT STEADYTRACK ADVANCED":
                    if (input < 3000) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 3550) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 3900) rank = ScenarioRank.Nova.ToString();
                    else if (input < 4350) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 4350) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT MINITRACK ADVANCED":
                    if (input < 2450) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 3000) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 3400) rank = ScenarioRank.Nova.ToString();
                    else if (input < 3700) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 3700) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT BLINKTRACK ADVANCED":
                    if (input < 2500) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2800) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 3000) rank = ScenarioRank.Nova.ToString();
                    else if (input < 3385) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 3385) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT PILLTRACK ADVANCED":
                    if (input < 3200) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 3500) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 3800) rank = ScenarioRank.Nova.ToString();
                    else if (input < 4000) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 4000) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT AXITRACK ADVANCED":
                    if (input < 2250) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2500) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 2800) rank = ScenarioRank.Nova.ToString();
                    else if (input < 3300) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 3300) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT SWAYSWITCH ADVANCED":
                    if (input < 2670) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2870) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 3070) rank = ScenarioRank.Nova.ToString();
                    else if (input < 3350) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 3350) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT SPHERESWITCH ADVANCED":
                    if (input < 2370) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2570) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 2650) rank = ScenarioRank.Nova.ToString();
                    else if (input < 2825) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 2825) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT SKYSWITCH ADVANCED":
                    if (input < 2950) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 3250) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 3365) rank = ScenarioRank.Nova.ToString();
                    else if (input < 3525) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 3525) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT DODGESWITCH ADVANCED":
                    if (input < 2680) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2980) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 3130) rank = ScenarioRank.Nova.ToString();
                    else if (input < 3275) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 3275) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT ANGLESWITCH ADVANCED":
                    if (input < 2100) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2400) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 2600) rank = ScenarioRank.Nova.ToString();
                    else if (input < 2825) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 2825) rank = ScenarioRank.Celestial.ToString();
                    break;
                case "VT ARCSWITCH ADVANCED":
                    if (input < 2000) rank = ScenarioRank.Unranked.ToString();
                    else if (input < 2250) rank = ScenarioRank.GrandMaster.ToString();
                    else if (input < 2450) rank = ScenarioRank.Nova.ToString();
                    else if (input < 2750) rank = ScenarioRank.Astra.ToString();
                    else if (input >= 2750) rank = ScenarioRank.Celestial.ToString();
                    break;
                default:
                    break;
            }
            break;
        default:
            break;
    }
    return rank;
}

void WriteScoreOnline(string[][] benchmarks, int[][] onlineScores)
{
    Console.WriteLine("");
    using (StreamWriter sw = new StreamWriter(path, true))
    {
        for (int j = 0; j < benchmarks.Length; j++)
        {
            sw.WriteLine(benchmarks[j][0]);
            sw.WriteLine(DateTime.Now.ToString());

            for (int i = 0; i < benchmarks[j].Length-1; i++)
            {
                sw.WriteLine($"{benchmarks[j][i + 1]}:{onlineScores[j][i]} - {CheckRank(benchmarks[j][0], benchmarks[j][i + 1], onlineScores[j][i])}");
            }
            sw.WriteLine("");

        }
    }
}

int[][]? GetData()
{
    ChromeOptions options = new ChromeOptions();
    options.AddArgument("--headless"); // Run in headless mode (without opening a browser window)

    // Check username
    string usernamePath = Path.Combine(Directory.GetCurrentDirectory(), "username.txt");
    string username = "";
    if (!File.Exists(usernamePath))
    {
        Console.WriteLine("Enter username:");
        username = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(usernamePath))
        {
            sw.WriteLine(username);
            sw.Close();
        }
    }
    else username = File.ReadAllText(usernamePath);

    // Initialize Chrome driver
    using (var driver = new ChromeDriver(options))
    {
        // Navigate to the webpage
        driver.Navigate().GoToUrl($"https://app.voltaic.gg/u/{username}");

        // Wait for the page to fully render
        Thread.Sleep(7000);

        string data = "";

        IReadOnlyCollection<IWebElement> tabs = driver.FindElements(By.CssSelector(".q-tabs__content .q-tab"));

        foreach (var tab in tabs)
        {
            // Find child element with class "q-tab__label" and click it
            IWebElement labelElement = tab.FindElement(By.CssSelector(".q-tab__label"));
            labelElement.Click();

            // Find the specified div element
            var divElements = driver.FindElements(By.CssSelector("div[data-v-1501af5c][style*=\"grid-area:\"][style*=\"background: linear-gradient\"][style*=\"color:\"]"));

            // Get the text content of the div element and append it to the string
            foreach (var div in divElements)
            {
                string textContent = div.Text;

                if (textContent.Length >= 1 && textContent.Length <= 4)
                {
                    data += textContent.Trim() + " ";
                }
            }
            // Ensures data loading correctly before writing it to string by adding delay
            Thread.Sleep(1000);
        }

        // Save data to text file
        string path = Path.Combine(Directory.GetCurrentDirectory(), "voltaic_scores.txt");
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine(data);
        sw.Close();

        if (File.Exists(path))
        {
            string scorePath = Path.Combine(Directory.GetCurrentDirectory(), "voltaic_scores.txt");
            var scoresData = File.ReadAllText(scorePath);
            string[] numarray = scoresData.Split(" ");

            int[] a = new int[12];
            int[] b = new int[12];
            int[] c = new int[18];

            for (int i = 0; i < 12; i++)
            {
                a[i] = int.Parse(numarray[i]);
            }

            for (int i = 0; i < 12; i++)
            {
                b[i] = int.Parse(numarray[i + 12]);
            }

            for (int i = 0; i < 18; i++)
            {
                c[i] = int.Parse(numarray[i + 24]);
            }

            int[][] scores = { a, b, c };
            return scores;
        }
        else return null;
    }
}

void ChangeUsername()
{
    string usernamePath = Path.Combine(Directory.GetCurrentDirectory(), "username.txt");
    string username = "";
    if (!File.Exists(usernamePath))
    {
        Console.WriteLine("Enter username:");
        username = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(usernamePath))
        {
            sw.WriteLine(username);
            sw.Close();
        }
    }
    else
    {
        File.Delete(usernamePath);
        Console.WriteLine("Enter username:");
        username = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(usernamePath))
        {
            sw.WriteLine(username);
            sw.Close();
        }
    }
}