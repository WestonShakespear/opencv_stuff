using Emgu.CV;
using Emgu.CV.Aruco;

using System.CommandLine;

namespace testing;

public class Program {
    public static void Main(params string[] args)
    {
        RootCommand command = new RootCommand(
            "Generates Aruco Markers"
        );

        var xSizeOption = new Option<int>(
            name: "-x",
            description: "The number of tiles in the x direction"
        );

        Option ySizeOption = new Option<int>(
            name: "-y",
            description: "The number of tiles in the y direction"
        );

        command.AddOption(xSizeOption);
        command.AddOption(ySizeOption);

        //Mat? output = generateGridBoard();

        // Mat output = new Mat();

        

        // if (output is not null)
        // {
        //     CvInvoke.Imshow("output", output);
        //     CvInvoke.WaitKey(0);
        //     CvInvoke.DestroyAllWindows();
        // }
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