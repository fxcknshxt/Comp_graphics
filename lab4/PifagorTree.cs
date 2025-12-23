using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private PictureBox pictureBox;
        private Button btnDraw;
        private TrackBar trackBarDepth;
        private Label lblDepth;
        private int recursionDepth = 10;

        public Form1()
        {
            InitializeComponents();
            SetupForm();
            DrawTree();
        }

        private void InitializeComponents()
        {
            this.Text = "Фрактал: Дерево Пифагора";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            pictureBox = new PictureBox
            {
                Location = new Point(10, 50),
                Size = new Size(760, 600),
                BackColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };

            btnDraw = new Button
            {
                Text = "Нарисовать дерево",
                Location = new Point(10, 10),
                Size = new Size(120, 30),
                BackColor = Color.LightGreen
            };
            btnDraw.Click += BtnDraw_Click;

            trackBarDepth = new TrackBar
            {
                Location = new Point(140, 10),
                Size = new Size(200, 30),
                Minimum = 1,
                Maximum = 15,
                Value = recursionDepth,
                TickFrequency = 1
            };
            trackBarDepth.Scroll += TrackBarDepth_Scroll;

            lblDepth = new Label
            {
                Text = $"Глубина рекурсии: {recursionDepth}",
                Location = new Point(350, 10),
                Size = new Size(150, 30),
                ForeColor = Color.White,
                BackColor = Color.DarkBlue
            };

            this.Controls.AddRange(new Control[]
            {
                pictureBox, btnDraw, trackBarDepth, lblDepth
            });
        }

        private void SetupForm()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            DrawTree();
        }

        private void TrackBarDepth_Scroll(object sender, EventArgs e)
        {
            recursionDepth = trackBarDepth.Value;
            lblDepth.Text = $"Глубина рекурсии: {recursionDepth}";
            DrawTree();
        }

        private void DrawTree()
        {
            if (pictureBox.Width <= 0 || pictureBox.Height <= 0)
                return;

            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                PointF startPoint = new PointF(pictureBox.Width / 2, pictureBox.Height - 50);
                float startLength = 100;
                float startAngle = -90;

                DrawBranch(g, startPoint, startLength, startAngle, recursionDepth);
            }

            if (pictureBox.Image != null)
                pictureBox.Image.Dispose();
            pictureBox.Image = bmp;
        }

        private void DrawBranch(Graphics g, PointF startPoint, float length, float angle, int depth)
        {
            if (depth <= 0 || length < 1)
                return;

            float angleRad = angle * (float)Math.PI / 180;
            PointF endPoint = new PointF(
                startPoint.X + length * (float)Math.Cos(angleRad),
                startPoint.Y + length * (float)Math.Sin(angleRad)
            );

            int greenValue = Math.Min(255, 100 + depth * 20);
            int blueValue = Math.Min(255, 50 + depth * 15);
            using (Pen pen = new Pen(Color.FromArgb(150, 100, greenValue, blueValue), depth * 0.5f))
            {
                g.DrawLine(pen, startPoint, endPoint);
            }

            DrawBranch(g, endPoint, length * 0.75f, angle - 25, depth - 1);
            DrawBranch(g, endPoint, length * 0.75f, angle + 25, depth - 1);

            if (depth > 3 && new Random().Next(0, 100) > 50)
            {
                DrawBranch(g, endPoint, length * 0.5f, angle + 10, depth - 2);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}