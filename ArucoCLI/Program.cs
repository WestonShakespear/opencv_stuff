using Emgu.CV;
using Emgu.CV.Aruco;

namespace testing;

public class Program {
    public static void Main(string[] args)
    {
        // Mat? output = generateBoard();

        Mat output = new Mat();

        Dictionary dict = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50);

        CvE

        if (output is not null)
        {
            CvInvoke.Imshow("output", output);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
        }
    }


    public static Mat? generateBoard()
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