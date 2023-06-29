using Emgu.CV;
using Emgu.CV.Aruco;

using System.CommandLine;

namespace testing;

public class Program {
    public static int Main(string[] args)
    {

        var fileOption = new Option<FileInfo?>(
            name: "--file",
            description: "The file to read and display on the console.");

        var delayOption = new Option<int>(
            name: "--delay",
            description: "Delay between lines, specified as milliseconds per character in a line.",
            getDefaultValue: () => 42);

        var fgcolorOption = new Option<ConsoleColor>(
            name: "--fgcolor",
            description: "Foreground color of text displayed on the console.",
            getDefaultValue: () => ConsoleColor.White);

        var lightModeOption = new Option<bool>(
            name: "--light-mode",
            description: "Background color of text displayed on the console: default is black, light mode is white.");


        var xSizeOption = new Option<int>(
            name: "-x",
            description: "The number of tiles in the x direction"
        );

        var ySizeOption = new Option<int>(
            name: "-y",
            description: "The number of tiles in the y direction"
        );


        var rootCommand = new RootCommand("Sample app for System.CommandLine");
        //rootCommand.AddOption(fileOption);

        var readCommand = new Command("read", "Read and display the file.")
            {
                fileOption,
                delayOption,
                fgcolorOption,
                lightModeOption
            };
        rootCommand.AddCommand(readCommand);


        var arucoCommand = new Command("aruco", "Create a single aruco marker");

        rootCommand.AddCommand(arucoCommand);

        var arucoBoardCommand = new Command("board", "Create a grid of aruco markers")
        {
            xSizeOption,
            ySizeOption
        };

        rootCommand.AddCommand(arucoBoardCommand);

        var charucoCommand = new Command("charuco", "Create a charuco board")
        {
            xSizeOption,
            ySizeOption
        };

        rootCommand.AddCommand(charucoCommand);

        // readCommand.SetHandler(async (file, delay, fgcolor, lightMode) =>
        //     {
        //         await ReadFile(file!, delay, fgcolor, lightMode);
        //     },
        //     fileOption, delayOption, fgcolorOption, lightModeOption);

        return rootCommand.InvokeAsync(args).Result;


        

        // command.AddOption(xSizeOption);
        // command.AddOption(ySizeOption);

        //Mat? output = generateGridBoard();

        // Mat output = new Mat();

        

        // if (output is not null)
        // {
        //     CvInvoke.Imshow("output", output);
        //     CvInvoke.WaitKey(0);
        //     CvInvoke.DestroyAllWindows();
        // }
    }

    internal static async Task ReadFile(
            FileInfo file, int delay, ConsoleColor fgColor, bool lightMode)
    {
        Console.BackgroundColor = lightMode ? ConsoleColor.White : ConsoleColor.Black;
        Console.ForegroundColor = fgColor;
        List<string> lines = File.ReadLines(file.FullName).ToList();
        foreach (string line in lines)
        {
            Console.WriteLine(line);
            await Task.Delay(delay * line.Length);
        };
    }

    public static Mat? generateGridBoard()
    {
        Dictionary dict = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50);

        int xSize = 1;
        int ySize = 1;

        float squareLength = 1.0f;
        float markerLength = 0.8f;

        if (!(markerLength < squareLength))
        {
            return null;
        }

        GridBoard board = new GridBoard(xSize, ySize, squareLength, markerLength, dict);

        Mat image = new Mat();

        System.Drawing.Size imageSize = new System.Drawing.Size(500, 500);

        ArucoInvoke.GenerateImage(board, imageSize, image);

        return image;
    }


    public static Mat? generateCharBoard()
    {
        Dictionary dict = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50);

        int xSize = 2;
        int ySize = 2;

        float squareLength = 1.0f;
        float markerLength = 0.8f;

        if (!(markerLength < squareLength))
        {
            return null;
        }

        CharucoBoard board = new CharucoBoard(xSize, ySize, squareLength, markerLength, dict);

        Mat image = new Mat();

        System.Drawing.Size imageSize = new System.Drawing.Size(500, 500);

        ArucoInvoke.GenerateImage(board, imageSize, image);

        return image;
    }
}