using Painter_Dietmar_Schoder.Painters;
using Painter_Dietmar_Schoder.Tools;
using System.Drawing;

namespace Painter_Dietmar_Schoder
{
    public class Recipe : IRecipe
    {
        private readonly ICanvas _canvas;
        private readonly IPainter _painter;
        private readonly Color _background;
        private readonly Color _signColor1;
        private readonly Color _signColor2;

        public Recipe(ICanvas canvas, IPainter painter, Color background, Color signColor1, Color signColor2)
        {
            _canvas = canvas;
            _painter = painter;
            _background = background;
            _signColor1 = signColor1;
            _signColor2 = signColor2;
        }

        public Recipe(ICanvas canvas, IPainter painter, Color background)
            : this(canvas, painter, background, Color.Black, Color.White) { }

        public Recipe(ICanvas canvas, IPainter painter, Color signColor1, Color signColor2)
            : this(canvas, painter, Color.White, signColor1, signColor2) { }

        public Recipe(ICanvas canvas, IPainter painter)
            : this(canvas, painter, Color.White, Color.Black, Color.White) { }

        public void Apply()
        {
            if (string.IsNullOrEmpty(_canvas.BackgroundImageFileName))
            {
                _canvas.FillWithBrush(new SolidBrush(_background));
            }
            else
            {
                _canvas.FillWithImage(_canvas.BackgroundImageFileName);
            }
            _painter.DrawOnCanvas(_canvas);
            _canvas.SignDrawing(_signColor1, _signColor2);
            _canvas.Save();
        }
    }
}
