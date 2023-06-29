using Emgu.CV;
using Emgu.CV.Aruco;
using static Emgu.CV.Aruco.Dictionary;

using System.CommandLine;

namespace testing;

public class Program {
    public static int Main(string[] args)
    {

        var rootCommand = new RootCommand("Sample app for System.CommandLine");

        var imageSizeOption = new Option<int>(
            name: "--size",
            description: "Set the pixel ratio used for the output image"
        );

        var xSizeOption = new Option<int>(
            name: "-x",
            description: "The number of tiles in the x direction"
        );

        var ySizeOption = new Option<int>(
            name: "-y",
            description: "The number of tiles in the y direction"
        );

        var dictOption = new Option<string>(
            name: "--dict",
            description: "Selects the dictionary to use",
            getDefaultValue: () => "4_50"
        );

        var squareLengthOption = new Option<float>(
            name: "--square-length",
            description: "Set the size of blank squares"
        );

        var markerLengthOption = new Option<float>(
            name: "--marker-length",
            description: "Set the size of marker squares"
        );



        var arucoCommand = new Command("aruco", "Create a single aruco marker")
            {
                imageSizeOption,
                dictOption
            };

        arucoCommand.SetHandler(async (dict) =>
            {
                await RunAruco(dict);
            },
            dictOption);

        rootCommand.AddCommand(arucoCommand);


        var arucoBoardCommand = new Command("board", "Create a grid of aruco markers")
        {
            imageSizeOption,
            dictOption,
            xSizeOption,
            ySizeOption,
            squareLengthOption,
            markerLengthOption
        };

        arucoBoardCommand.SetHandler(async (dict, x, y, squareLength, markerLength) =>
            {
                await RunGridBoard(dict, x, y, squareLength, markerLength);
            },
            dictOption, xSizeOption, ySizeOption, squareLengthOption, markerLengthOption);

        rootCommand.AddCommand(arucoBoardCommand);

        var charucoCommand = new Command("charuco", "Create a charuco board")
        {
            dictOption,
            xSizeOption,
            ySizeOption
        };

        rootCommand.AddCommand(charucoCommand);

        return rootCommand.InvokeAsync(args).Result;
    }



    internal static async Task RunAruco(string dict)
    {
        await Task.Run(() => {
            PredefinedDictionaryName? useDict = parseDictionaryFromString(dict);

            if (useDict is not null)
            {
                Mat? image = generateGridBoard(1, 1, 1.0f, 0.8f, (PredefinedDictionaryName)useDict);
                output(image, "name", false); 
            } 
        });   
    }

    internal static async Task RunGridBoard(string dict, int x, int y, float squareLength, float markerLength)
    {
        await Task.Run(() => {
            PredefinedDictionaryName? useDict = parseDictionaryFromString(dict);

            if (useDict is not null)
            {
                Mat? image = generateGridBoard(x, y, squareLength, markerLength, (PredefinedDictionaryName)useDict);
                output(image, "name", false); 
            } 
        });
    }

    public static PredefinedDictionaryName? parseDictionaryFromString(string dict)
    {
        Dictionary<string, PredefinedDictionaryName> dictList = new Dictionary<string, PredefinedDictionaryName>()
        {
            {"4_50", Dictionary.PredefinedDictionaryName.Dict4X4_50},
            {"4_100", Dictionary.PredefinedDictionaryName.Dict4X4_100}
        };

        if (!dictList.ContainsKey(dict))
        {
            return null;
        } 
        Console.WriteLine("Using {0} dictionary", dict);
        return dictList[dict];
    }


    public static void output(Mat? image, string name, bool save)
    {
        if (image is not null)
        {
            CvInvoke.Imshow(name, image);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
        }
    }

    public static Mat? generateGridBoard(
        int xSize, int ySize,
        float squareLength, float markerLength,
        PredefinedDictionaryName dictName)
    {
        Dictionary dict = new Dictionary(dictName);

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