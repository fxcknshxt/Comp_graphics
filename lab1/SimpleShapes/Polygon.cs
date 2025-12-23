using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleShapes
{
    public class Polygon : Shape
    {
        public Point Center { get; set; }
        public int Sides { get; set; }
        public int Radius { get; set; }
        public float BorderWidth { get; set; }
        public Color FillColor { get; set; }
        public PointF[] CustomPoints { get; set; }
        public bool IsRegular { get; set; }

        public Polygon(Point center, int sides, int radius,
                      Color borderColor, Color fillColor, Color bgColor)
            : base(borderColor, bgColor)
        {
            Center = center;
            Sides = sides;
            Radius = radius;
            BorderWidth = 1;
            FillColor = fillColor;
            IsRegular = true;
            CustomPoints = null;
        }

        public Polygon(PointF[] points, Color borderColor, Color fillColor, Color bgColor)
            : base(borderColor, bgColor)
        {
            CustomPoints = points;
            BorderWidth = 1;
            FillColor = fillColor;
            IsRegular = false;
        }

        private PointF[] CalculateRegularPolygonPoints()
        {
            PointF[] points = new PointF[Sides];
            double angle = 2 * Math.PI / Sides;

            for (int i = 0; i < Sides; i++)
            {
                double currentAngle = i * angle;
                points[i] = new PointF(
                    Center.X + Radius * (float)Math.Cos(currentAngle),
                    Center.Y + Radius * (float)Math.Sin(currentAngle));
            }

            return points;
        }

        public override void Draw(Graphics g)
        {
            PointF[] points = IsRegular ? CalculateRegularPolygonPoints() : CustomPoints;

            if (points == null || points.Length < 3)
                return;

            using (Brush brush = new SolidBrush(FillColor))
            {
                g.FillPolygon(brush, points);
            }

            using (Pen pen = new Pen(foregroundColor, BorderWidth))
            {
                g.DrawPolygon(pen, points);
            }
        }

        public override void Erase(Graphics g)
        {
            PointF[] points = IsRegular ? CalculateRegularPolygonPoints() : CustomPoints;

            if (points == null || points.Length < 3)
                return;

            using (Brush brush = new SolidBrush(backgroundColor))
            {
                g.FillPolygon(brush, points);
            }

            using (Pen pen = new Pen(backgroundColor, BorderWidth))
            {
                g.DrawPolygon(pen, points);
            }
        }

        public override void SetPosition(Point position)
        {
            if (IsRegular)
            {
                Center = position;
            }
        }
    }
}