using System.Drawing;

namespace Painter_Dietmar_Schoder.Tools
{
    public interface ICanvas
    {
        int Width { get; set; }
        int Height { get; set; }
        int EnlargeFactor { get; set; }
        Bitmap Bitmap { get; set; }

        void FillWithBrush(Brush brush);

        void SignDrawing(Color color1, Color color2);

        void Save();
    }
}
