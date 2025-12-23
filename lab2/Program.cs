using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using System;
using GL = OpenTK.Graphics.OpenGL.GL;
using BeginMode = OpenTK.Graphics.OpenGL.BeginMode;
using ClearBufferMask = OpenTK.Graphics.OpenGL.ClearBufferMask;
using EnableCap = OpenTK.Graphics.OpenGL.EnableCap;
using MatrixMode = OpenTK.Graphics.OpenGL.MatrixMode;

namespace Lab2
{
    class Game : GameWindow
    {
        Vector3 centre = new Vector3(0f, 0f, 4f);
        int znak = 1;
        float dx = 0.02f;
        float angle = 0.0f;
        float scale = 1.0f;

        Vector3[] squarePoints = new Vector3[4];
        Vector3[] squareColors = new Vector3[4];

        public Game() : base(800, 600, new GraphicsMode(), "Лаба 2: Преобразования OpenGL")
        {
            VSync = VSyncMode.On;

            float z = 4.0f;
            squarePoints[0] = new Vector3(-0.5f, -0.5f, z);
            squarePoints[1] = new Vector3(0.5f, -0.5f, z);
            squarePoints[2] = new Vector3(0.5f, 0.5f, z);
            squarePoints[3] = new Vector3(-0.5f, 0.5f, z);

            squareColors[0] = new Vector3(1.0f, 0.0f, 0.0f);
            squareColors[1] = new Vector3(0.0f, 1.0f, 0.0f);
            squareColors[2] = new Vector3(0.0f, 0.0f, 1.0f);
            squareColors[3] = new Vector3(1.0f, 1.0f, 0.0f);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4,
                Width / (float)Height,
                1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();

            centre.X += dx * znak;

            if ((centre.X < -1.5f) || (centre.X > 1.5f))
                znak = -znak;
            centre.Y = Function(centre.X);
            angle += 2.0f;
            if (angle > 360) angle -= 360;

            scale = 0.8f + 0.3f * (float)Math.Sin(centre.X * 2);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(BeginMode.Triangles);
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(1.0f, -1.0f, 4.0f);
            GL.Color3(0.2f, 0.9f, 1.0f);
            GL.Vertex3(0.0f, 1.0f, 4.0f);
            GL.End();

            GL.PushMatrix();


            GL.Translate(centre);
            GL.Rotate(angle, 0, 0, 1);
            GL.Scale(scale, scale, 1);

            DrawPolygon(squarePoints, squareColors, 4, BeginMode.Quads);

            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(0.5f, 0.5f, 4f);

            DrawRegularPolygon(
                Vector3.Zero,
                0.25f,
                new Vector3(0.5f, 0.8f, 0.2f),
                6,
                BeginMode.Polygon);

            GL.PopMatrix();

            GL.PushMatrix();
            GL.Translate(-1.0f, 0f, 4f);

            DrawRegularPolygon(
                Vector3.Zero,
                0.25f,
                new Vector3(0.8f, 0.2f, 0.5f),
                50,
                BeginMode.Polygon);

            GL.PopMatrix();

            SwapBuffers();
        }
        float Function(float x)
        {
            return x * x * 0.3f;
        }

        void DrawPolygon(Vector3[] points, Vector3[] colors, int n, BeginMode mode)
        {
            GL.Begin(mode);
            for (int i = 0; i < n; i++)
            {
                GL.Color3(colors[i]);
                GL.Vertex3(points[i]);
            }
            GL.End();
        }

        void DrawRegularPolygon(Vector3 center, float radius, Vector3 color,
                               int sides, BeginMode mode)
        {
            Vector3[] points = new Vector3[sides];
            Vector3[] colors = new Vector3[sides];

            for (int i = 0; i < sides; i++)
            {
                float angle = 2.0f * (float)Math.PI * i / sides;
                points[i] = new Vector3(
                    center.X + radius * (float)Math.Cos(angle),
                    center.Y + radius * (float)Math.Sin(angle),
                    center.Z);
                colors[i] = color;
            }

            DrawPolygon(points, colors, sides, mode);
        }

        [STAThread]
        static void Main()
        {
            using (Game game = new Game())
            {
                game.Run(60.0);
            }
        }
    }
}