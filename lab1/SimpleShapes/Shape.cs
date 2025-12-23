using System.Drawing;

namespace SimpleShapes
{
    public abstract class Shape
    {
        protected Color foregroundColor;
        protected Color backgroundColor;

        public Shape(Color fgColor, Color bgColor)
        {
            foregroundColor = fgColor;
            backgroundColor = bgColor;
        }

        public abstract void Draw(Graphics g);
        public abstract void Erase(Graphics g);
        public abstract void SetPosition(Point position);
    }
}