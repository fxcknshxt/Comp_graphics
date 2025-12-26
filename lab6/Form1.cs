using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OpenGL_GLControl
{
    public partial class Form1 : Form
    {
        bool loaded = false;
        double angle = 0;
        double dangle = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loaded = true;
            SetupViewport(glControl1);    // установка вида
            comboBox1.SelectedIndex = 0;  // выбор оси для вращения (X)
            numericUpDown1.Value = 3;     // начальный угол вращения
        }

        private void SetupViewport(GLControl glControl)
        {
            float aspectRatio = (float)glControl.Width / (float)glControl.Height;
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,    // 45 градусов
                aspectRatio,
                1f,
                5000000000);
            GL.MultMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;

            // Очистка экрана
            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f); // цвет фона
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Настройка камеры
            Matrix4 modelview = Matrix4.LookAt(
                new Vector3(-300, 300, 200),  // позиция камеры
                new Vector3(0, 0, 0),         // точка взгляда
                new Vector3(0, 0, 1));        // направление "вверх"

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            // Вращение вокруг выбранной оси
            switch (comboBox1.Text)
            {
                case "X": GL.Rotate(angle, 1, 0, 0); break;
                case "Y": GL.Rotate(angle, 0, 1, 0); break;
                case "Z": GL.Rotate(angle, 0, 0, 1); break;
            }

            Vector3 clr1 = new Vector3(1.0f, 1.0f, 0.0f); // желтый
            Vector3 clr2 = new Vector3(1.0f, 0.0f, 0.0f); // красный
            Vector3 clr3 = new Vector3(0.2f, 0.9f, 1.0f); // голубой

            // ===== РИСУЕМ ОСИ =====

            // Ось X (желтая)
            GL.Begin(BeginMode.Lines);
            GL.Color3(clr1);
            GL.Vertex3(-300.0f, 0.0f, 0.0f);
            GL.Vertex3(300.0f, 0.0f, 0.0f);
            GL.End();

            // Ось Y (красная)
            GL.Begin(BeginMode.Lines);
            GL.Color3(clr2);
            GL.Vertex3(0.0f, -300.0f, 0.0f);
            GL.Vertex3(0.0f, 300.0f, 0.0f);
            GL.End();

            // Ось Z (голубая)
            GL.Begin(BeginMode.Lines);
            GL.Color3(clr3);
            GL.Vertex3(0.0f, 0.0f, -300f);
            GL.Vertex3(0.0f, 0.0f, 300.0f);
            GL.End();

            // ===== РИСУЕМ 3D ФИГУРЫ =====

            float z = 40f;

            // 1. Треугольник (из инструкции)
            GL.Begin(BeginMode.Triangles);
            GL.Color3(clr1);
            GL.Vertex3(-100.0f, -100.0f, z);
            GL.Color3(clr2);
            GL.Vertex3(100.0f, -100.0f, z);
            GL.Color3(clr3);
            GL.Vertex3(0.0f, 100.0f, z);
            GL.End();

            // 2. Куб (добавляем к заданию)
            DrawCube(0, 0, 100, 50);

            // 3. Пирамида
            DrawPyramid(150, 0, 100, 40);

            // 4. Сфера
            DrawSphere(-150, 0, 100, 30);

            glControl1.SwapBuffers();
        }

        // ===== ФУНКЦИИ РИСОВАНИЯ 3D ФИГУР =====

        private void DrawCube(float x, float y, float z, float size)
        {
            float s = size / 2;

            // Передняя грань
            GL.Begin(BeginMode.Quads);
            GL.Color3(1.0f, 0.0f, 0.0f); // Красный
            GL.Vertex3(x - s, y - s, z + s);
            GL.Vertex3(x + s, y - s, z + s);
            GL.Vertex3(x + s, y + s, z + s);
            GL.Vertex3(x - s, y + s, z + s);
            GL.End();

            // Задняя грань
            GL.Begin(BeginMode.Quads);
            GL.Color3(0.0f, 1.0f, 0.0f); // Зеленый
            GL.Vertex3(x - s, y - s, z - s);
            GL.Vertex3(x - s, y + s, z - s);
            GL.Vertex3(x + s, y + s, z - s);
            GL.Vertex3(x + s, y - s, z - s);
            GL.End();

            // Верхняя грань
            GL.Begin(BeginMode.Quads);
            GL.Color3(0.0f, 0.0f, 1.0f); // Синий
            GL.Vertex3(x - s, y + s, z - s);
            GL.Vertex3(x - s, y + s, z + s);
            GL.Vertex3(x + s, y + s, z + s);
            GL.Vertex3(x + s, y + s, z - s);
            GL.End();

            // Нижняя грань
            GL.Begin(BeginMode.Quads);
            GL.Color3(1.0f, 1.0f, 0.0f); // Желтый
            GL.Vertex3(x - s, y - s, z - s);
            GL.Vertex3(x + s, y - s, z - s);
            GL.Vertex3(x + s, y - s, z + s);
            GL.Vertex3(x - s, y - s, z + s);
            GL.End();

            // Левая грань
            GL.Begin(BeginMode.Quads);
            GL.Color3(1.0f, 0.0f, 1.0f); // Фиолетовый
            GL.Vertex3(x - s, y - s, z - s);
            GL.Vertex3(x - s, y - s, z + s);
            GL.Vertex3(x - s, y + s, z + s);
            GL.Vertex3(x - s, y + s, z - s);
            GL.End();

            // Правая грань
            GL.Begin(BeginMode.Quads);
            GL.Color3(0.0f, 1.0f, 1.0f); // Голубой
            GL.Vertex3(x + s, y - s, z - s);
            GL.Vertex3(x + s, y + s, z - s);
            GL.Vertex3(x + s, y + s, z + s);
            GL.Vertex3(x + s, y - s, z + s);
            GL.End();
        }

        private void DrawPyramid(float x, float y, float z, float size)
        {
            float s = size;

            // Основание (квадрат)
            GL.Begin(BeginMode.Quads);
            GL.Color3(0.8f, 0.4f, 0.0f); // Оранжевый
            GL.Vertex3(x - s, y - s, z);
            GL.Vertex3(x + s, y - s, z);
            GL.Vertex3(x + s, y + s, z);
            GL.Vertex3(x - s, y + s, z);
            GL.End();

            // Боковые грани (треугольники)
            GL.Begin(BeginMode.Triangles);
            GL.Color3(0.6f, 0.8f, 0.2f); // Салатовый

            // Передняя грань
            GL.Vertex3(x - s, y - s, z);
            GL.Vertex3(x + s, y - s, z);
            GL.Vertex3(x, y, z + s * 2);

            // Правая грань
            GL.Vertex3(x + s, y - s, z);
            GL.Vertex3(x + s, y + s, z);
            GL.Vertex3(x, y, z + s * 2);

            // Задняя грань
            GL.Vertex3(x + s, y + s, z);
            GL.Vertex3(x - s, y + s, z);
            GL.Vertex3(x, y, z + s * 2);

            // Левая грань
            GL.Vertex3(x - s, y + s, z);
            GL.Vertex3(x - s, y - s, z);
            GL.Vertex3(x, y, z + s * 2);
            GL.End();
        }

        private void DrawSphere(float x, float y, float z, float radius)
        {
            int segments = 16;
            int rings = 8;

            GL.Color3(1.0f, 0.5f, 0.0f); // Оранжевый

            for (int i = 0; i < rings; i++)
            {
                double lat0 = Math.PI * (-0.5 + (double)(i) / rings);
                double z0 = Math.Sin(lat0);
                double zr0 = Math.Cos(lat0);

                double lat1 = Math.PI * (-0.5 + (double)(i + 1) / rings);
                double z1 = Math.Sin(lat1);
                double zr1 = Math.Cos(lat1);

                GL.Begin(BeginMode.QuadStrip);
                for (int j = 0; j <= segments; j++)
                {
                    double lng = 2 * Math.PI * (double)(j) / segments;
                    double sinLng = Math.Sin(lng);
                    double cosLng = Math.Cos(lng);

                    GL.Vertex3(
                        x + radius * cosLng * zr0,
                        y + radius * sinLng * zr0,
                        z + radius * z0);

                    GL.Vertex3(
                        x + radius * cosLng * zr1,
                        y + radius * sinLng * zr1,
                        z + radius * z1);
                }
                GL.End();
            }
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded) return;
            SetupViewport(glControl1);
        }

        // ===== ОБРАБОТЧИКИ КНОПОК =====

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            dangle = (double)numericUpDown1.Value;
            angle += dangle;
            glControl1.Invalidate();
        }

        private void buttonAngleZero_Click(object sender, EventArgs e)
        {
            angle = 0;
            glControl1.Invalidate();
        }
    }
}