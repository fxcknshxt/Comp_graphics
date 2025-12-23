using System.Drawing;

namespace SimpleShapes
{
    public class Ellipse : Shape
    {
        public Point Center { get; set; }
        public int RadiusX { get; set; }
        public int RadiusY { get; set; }
        public float BorderWidth { get; set; }
        public Color FillColor { get; set; }
        public string Label { get; set; }
        public Font LabelFont { get; set; }

        public Ellipse(Point center, int radiusX, int radiusY,
                      Color borderColor, Color fillColor, Color bgColor)
            : base(borderColor, bgColor)
        {
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
            BorderWidth = 1;
            FillColor = fillColor;
            Label = "";
            LabelFont = new Font("Arial", 10);
        }

        public override void Draw(Graphics g)
        {
            Rectangle rect = new Rectangle(
                Center.X - RadiusX,
                Center.Y - RadiusY,
                RadiusX * 2,
                RadiusY * 2);

            using (Brush brush = new SolidBrush(FillColor))
            {
                g.FillEllipse(brush, rect);
            }

            using (Pen pen = new Pen(foregroundColor, BorderWidth))
            {
                g.DrawEllipse(pen, rect);
            }

            if (!string.IsNullOrEmpty(Label))
            {
                using (Brush textBrush = new SolidBrush(foregroundColor))
                {
                    SizeF textSize = g.MeasureString(Label, LabelFont);
                    PointF textPosition = new PointF(
                        Center.X - textSize.Width / 2,
                        Center.Y - textSize.Height / 2);
                    g.DrawString(Label, LabelFont, textBrush, textPosition);
                }
            }
        }

        public override void Erase(Graphics g)
        {
            Rectangle rect = new Rectangle(
                Center.X - RadiusX,
                Center.Y - RadiusY,
                RadiusX * 2,
                RadiusY * 2);

            using (Brush brush = new SolidBrush(backgroundColor))
            {
                g.FillEllipse(brush, rect);
            }

            using (Pen pen = new Pen(backgroundColor, BorderWidth))
            {
                g.DrawEllipse(pen, rect);
            }

            if (!string.IsNullOrEmpty(Label))
            {
                SizeF textSize = g.MeasureString(Label, LabelFont);
                RectangleF textRect = new RectangleF(
                    Center.X - textSize.Width / 2,
                    Center.Y - textSize.Height / 2,
                    textSize.Width,
                    textSize.Height);
                g.FillRectangle(new SolidBrush(backgroundColor), textRect);
            }
        }

        public override void SetPosition(Point position)
        {
            Center = position;
        }
    }
}