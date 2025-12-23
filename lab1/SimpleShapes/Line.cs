using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleShapes
{
    public class Line : Shape
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public float Width { get; set; }
        public DashStyle LineStyle { get; set; }
        public LineCap StartCap { get; set; }
        public LineCap EndCap { get; set; }

        public Line(Point start, Point end, float width, Color color, Color bgColor)
            : base(color, bgColor)
        {
            StartPoint = start;
            EndPoint = end;
            Width = width;
            LineStyle = DashStyle.Solid;
            StartCap = LineCap.Round;
            EndCap = LineCap.Round;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(foregroundColor, Width))
            {
                pen.DashStyle = LineStyle;
                pen.StartCap = StartCap;
                pen.EndCap = EndCap;
                g.DrawLine(pen, StartPoint, EndPoint);
            }
        }

        public override void Erase(Graphics g)
        {
            using (Pen pen = new Pen(backgroundColor, Width))
            {
                pen.DashStyle = LineStyle;
                pen.StartCap = StartCap;
                pen.EndCap = EndCap;
                g.DrawLine(pen, StartPoint, EndPoint);
            }
        }

        public override void SetPosition(Point position)
        {
            int dx = position.X - StartPoint.X;
            int dy = position.Y - StartPoint.Y;
            StartPoint = position;
            EndPoint = new Point(EndPoint.X + dx, EndPoint.Y + dy);
        }
    }
}