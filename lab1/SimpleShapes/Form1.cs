using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleShapes
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Color backgroundColor = Color.White;
        private List<Shape> shapes = new List<Shape>();
        private Shape currentShape;
        private PictureBox pictureBox1;

        public Form1()
        {
            InitializeComponent();

            CreateControls();
        }

        private void CreateControls()
        {
            this.Text = "Рисование простых фигур";
            this.Size = new Size(900, 700);

            GroupBox controlGroup = new GroupBox
            {
                Text = "Управление",
                Location = new Point(10, 10),
                Size = new Size(250, 400)
            };

            Button btnEnableGraphics = new Button
            {
                Text = "Включить графику",
                Location = new Point(10, 20),
                Size = new Size(220, 30)
            };
            btnEnableGraphics.Click += BtnEnableGraphics_Click;

            Button btnDrawLine = new Button
            {
                Text = "Нарисовать линию",
                Location = new Point(10, 60),
                Size = new Size(220, 30)
            };
            btnDrawLine.Click += BtnDrawLine_Click;

            Button btnDrawEllipse = new Button
            {
                Text = "Нарисовать эллипс",
                Location = new Point(10, 100),
                Size = new Size(220, 30)
            };
            btnDrawEllipse.Click += BtnDrawEllipse_Click;

            Button btnDrawPolygon = new Button
            {
                Text = "Нарисовать многоугольник",
                Location = new Point(10, 140),
                Size = new Size(220, 30)
            };
            btnDrawPolygon.Click += BtnDrawPolygon_Click;

            Button btnEraseLast = new Button
            {
                Text = "Стереть последний",
                Location = new Point(10, 180),
                Size = new Size(220, 30)
            };
            btnEraseLast.Click += BtnEraseLast_Click;

            Button btnClearAll = new Button
            {
                Text = "Очистить все",
                Location = new Point(10, 220),
                Size = new Size(220, 30)
            };
            btnClearAll.Click += BtnClearAll_Click;

            controlGroup.Controls.AddRange(new Control[] {
                btnEnableGraphics,
                btnDrawLine,
                btnDrawEllipse,
                btnDrawPolygon,
                btnEraseLast,
                btnClearAll
            });

            pictureBox1 = new PictureBox
            {
                Location = new Point(270, 10),
                Size = new Size(600, 650),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pictureBox1.Paint += PictureBox1_Paint;


            this.Controls.Add(controlGroup);
            this.Controls.Add(pictureBox1);
        }

        private void BtnEnableGraphics_Click(object sender, EventArgs e)
        {
            if (pictureBox1 != null)
            {
                graphics = pictureBox1.CreateGraphics();
                graphics.Clear(backgroundColor);
                MessageBox.Show("Графика включена! Теперь можно рисовать фигуры.");
            }
        }

        private void BtnDrawLine_Click(object sender, EventArgs e)
        {
            if (graphics == null)
            {
                MessageBox.Show("Сначала включите графику!");
                return;
            }

            try
            {
                currentShape = new Line(
                    new Point(100, 100),
                    new Point(400, 300),
                    3,
                    Color.Blue,
                    backgroundColor);

                currentShape.Draw(graphics);
                shapes.Add(currentShape);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BtnDrawEllipse_Click(object sender, EventArgs e)
        {
            if (graphics == null)
            {
                MessageBox.Show("Сначала включите графику!");
                return;
            }

            try
            {
                Ellipse ellipse = new Ellipse(
                    new Point(300, 200),
                    100,
                    60,
                    Color.Red,
                    Color.Yellow,
                    backgroundColor);

                ellipse.Label = "Эллипс";
                currentShape = ellipse;
                currentShape.Draw(graphics);
                shapes.Add(currentShape);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BtnDrawPolygon_Click(object sender, EventArgs e)
        {
            if (graphics == null)
            {
                MessageBox.Show("Сначала включите графику!");
                return;
            }

            try
            {
                currentShape = new Polygon(
                    new Point(500, 300),
                    6,
                    80,
                    Color.Green,
                    Color.LightGreen,
                    backgroundColor);

                currentShape.Draw(graphics);
                shapes.Add(currentShape);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BtnEraseLast_Click(object sender, EventArgs e)
        {
            if (graphics == null || shapes.Count == 0)
            {
                MessageBox.Show("Нет фигур для стирания!");
                return;
            }

            Shape lastShape = shapes[shapes.Count - 1];
            lastShape.Erase(graphics);
            shapes.RemoveAt(shapes.Count - 1);
        }

        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            if (graphics == null) return;

            graphics.Clear(backgroundColor);
            shapes.Clear();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }
    }
}