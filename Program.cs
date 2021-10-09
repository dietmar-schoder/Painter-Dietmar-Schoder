using Painter_Dietmar_Schoder.Painters.Dice;
using Painter_Dietmar_Schoder.Tools;

namespace Painter_Dietmar_Schoder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\decision tree\1721 Frankfurt\",
                "Product_001.png", 2280, 1282, enlargeFactor: 2), new DicePainter002(diceSizeFactor: 16)).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\decision tree\1721 Frankfurt\",
            //    "Product_001.png", 365, 100, enlargeFactor: 16), new DicePainter001(diceSizeFactor: 2)).Apply();
        }
    }
}
