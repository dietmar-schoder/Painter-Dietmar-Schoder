using Painter_Dietmar_Schoder.Painters.Square;
using Painter_Dietmar_Schoder.Tools;
using System;
using System.Drawing;

namespace Painter_Dietmar_Schoder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree\",
                "Tree043.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
                new TreePainter003(), Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree\",
            //    "Tree010_Amazing.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreePainter002(), Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree\",
            //    "Tree007.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 105, 2280 - 45, 105)),
            //    new TreePainter001(), Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\time\",
            //    "LondonTime.png", 2280, 2280, enlargeFactor: 1, now, new Rectangle(0, 2280 - 105, 2280 - 100, 105)),
            //    new LondonTime(squareSize: 90, now)).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\decision tree\1721 Frankfurt\",
            //    "Product_001.png", 2280, 1282, enlargeFactor: 2), new DicePainter002(diceSizeFactor: 16)).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\decision tree\1721 Frankfurt\",
            //    "Product_001.png", 365, 100, enlargeFactor: 16), new DicePainter001(diceSizeFactor: 2)).Apply();
        }
    }
}
