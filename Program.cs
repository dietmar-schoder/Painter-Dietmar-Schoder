using Painter_Dietmar_Schoder.Painters.Square;
using Painter_Dietmar_Schoder.Tools;
using System;
using System.Drawing;

namespace Painter_Dietmar_Schoder
{
    public class Program
    {
        public static void Main()
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree10\Leon0002.jpg",
                @$"C:\Users\dietm\OneDrive\Bilder\random\tree10\",
                "Tree1066.png", 3000, 2000, enlargeFactor: 2, now, new Rectangle(0, 2000 - 40, 3000 - 20, 40)),
                //{ BackgroundImageFileName = @"C:\Users\dietm\OneDrive\Bilder\random\tree6\DSC05140.jpg" },
                new TreeInputPainter008(), Color.FromArgb(255, 127, 127, 127), Color.Black, Color.White).Apply();

            //var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree8\IMG_20211009_135042_1140.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree8\",
            //    "Tree914.png", 1140, 2028, enlargeFactor: 4, now, new Rectangle(0, 2028 - 40, 1140 - 20, 40)),
            //    //{ BackgroundImageFileName = @"C:\Users\dietm\OneDrive\Bilder\random\tree6\DSC05140.jpg" },
            //    new TreeInputPainter006(), Color.FromArgb(255, 0, 0, 0), Color.Teal, Color.White).Apply();

            //var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree7\",
            //    "Tree829.png", 2280, 4000, enlargeFactor: 2, now, new Rectangle(0, 4000 - 50, 2280 - 20, 40)),
            //    //{ BackgroundImageFileName = @"C:\Users\dietm\OneDrive\Bilder\random\tree6\DSC05140.jpg" },
            //    new TreePainter010(), Color.White, Color.Black, Color.White).Apply();

            //var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree6\DSC05140_COLOR.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree6\",
            //    "Tree796.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    //{ BackgroundImageFileName = @"C:\Users\dietm\OneDrive\Bilder\random\tree6\DSC05140.jpg" },
            //    new TreeInputPainter005(), Color.FromArgb(255, 0, 0, 0), Color.White, Color.Gray).Apply();

            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree6\DSC05140_COLOR.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree6\",
            //    "Tree772.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85))
            //    { BackgroundImageFileName = @"C:\Users\dietm\OneDrive\Bilder\random\tree6\DSC05140.jpg" },
            //    new TreeInputPainter005(), Color.FromArgb(255, 16, 16, 16), Color.White, Color.Gray).Apply();

            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree5\",
            //    "Tree861.png", 2280, 1282, enlargeFactor: 8, now, new Rectangle(0, 1282 - 20, 2280 - 9, 22)),
            //    new TreePainter009(), Color.White, Color.Black, Color.FromArgb(255, 200, 200, 200)).Apply();

            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree5\",
            //    "Tree842.png", 2280, 1282, enlargeFactor: 4, now, new Rectangle(0, 1282 - 20, 2280 - 9, 22)),
            //    new TreePainter008(), Color.White, Color.Black, Color.FromArgb(255, 200, 200, 200)).Apply();

            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree5\",
            //    "Tree820.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreePainter007(), Color.White, Color.Black, Color.FromArgb(255, 200, 200, 200)).Apply();

            // Daniel Finch abstract
            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree3\DSC04611.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree4\",
            //    "Tree734.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreeInputPainter005(), Color.FromArgb(255, 16, 16, 16), Color.White, Color.Gray).Apply();

            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree3\DSC04611.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree4\",
            //    "Tree713.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreeInputPainter004(), Color.Black, Color.Black, Color.DarkSlateGray).Apply();


            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree3\DSC04611.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree3\",
            //    "Tree699.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreeInputPainter003(), Color.Black, Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree2\DSC04611.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree2\",
            //    "Tree510.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreeInputPainter002(), Color.White, Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree2\DSC04611.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree2\",
            //    "Tree507.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreeInputPainter001(), Color.Black, Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree2\DSC04611.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree2\",
            //    "Tree503.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreeInputPainter001(), Color.Black, Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree2\",
            //    "Tree428.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreePainter006(), Color.White, Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree2\",
            //    "Tree407.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreePainter005(), Color.White, Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@"C:\Users\dietm\OneDrive\Bilder\random\tree2\DSC04611-2.jpg",
            //    @$"C:\Users\dietm\OneDrive\Bilder\random\tree2\",
            //    "Tree305.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreeInputPainter001(), Color.Black, Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree2\",
            //    "Tree203.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreePainter004(), Color.Black, Color.White).Apply();
            //new Recipe(new Canvas(@$"C:\Users\dietm\OneDrive\Bilder\random\tree\",
            //    "Tree043.png", 2280, 1282, enlargeFactor: 1, now, new Rectangle(0, 1282 - 85, 2280 - 35, 85)),
            //    new TreePainter003(), Color.Black, Color.White).Apply();
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
