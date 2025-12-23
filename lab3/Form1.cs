using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private Chart chart1;
        private Button btnPlot;
        private TextBox txtFunction1, txtFunction2;
        private Label lblFunction1, lblFunction2;

        public Form1()
        {
            InitializeCustomComponents();
            SetupChart();
            AddSampleFunctions();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Построение графиков функций";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblFunction1 = new Label
            {
                Text = "Функция 1:",
                Location = new Point(20, 20),
                Size = new Size(70, 25)
            };

            lblFunction2 = new Label
            {
                Text = "Функция 2:",
                Location = new Point(20, 50),
                Size = new Size(70, 25)
            };

            txtFunction1 = new TextBox
            {
                Location = new Point(100, 20),
                Size = new Size(200, 25),
                Text = "sin(x)"
            };

            txtFunction2 = new TextBox
            {
                Location = new Point(100, 50),
                Size = new Size(200, 25),
                Text = "x^2"
            };

            btnPlot = new Button
            {
                Text = "Построить графики",
                Location = new Point(320, 35),
                Size = new Size(120, 30),
                BackColor = Color.LightBlue
            };
            btnPlot.Click += BtnPlot_Click;

            chart1 = new Chart();
            chart1.Location = new Point(20, 100);
            chart1.Size = new Size(840, 550);


            this.Controls.AddRange(new Control[]
            {
                lblFunction1, lblFunction2,
                txtFunction1, txtFunction2,
                btnPlot,
                chart1
            });
        }

        private void SetupChart()
        {
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();

            chart1.Titles.Add("Графики функций");
            chart1.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            ChartArea chartArea = new ChartArea("MainArea");
            chartArea.AxisX.Title = "Ось X";
            chartArea.AxisY.Title = "Ось Y";
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.Crossing = 0;
            chartArea.AxisY.Crossing = 0;

            chart1.ChartAreas.Add(chartArea);

            Legend legend = new Legend();
            legend.Docking = Docking.Bottom;
            chart1.Legends.Add(legend);
        }

        private void AddSampleFunctions()
        {
            PlotFunction("sin(x)", Color.Red, MarkerStyle.Circle);
            PlotFunction("x^2", Color.Blue, MarkerStyle.Square);
        }

        private void BtnPlot_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            if (!string.IsNullOrWhiteSpace(txtFunction1.Text))
                PlotFunction(txtFunction1.Text, Color.Red, MarkerStyle.Circle);

            if (!string.IsNullOrWhiteSpace(txtFunction2.Text))
                PlotFunction(txtFunction2.Text, Color.Blue, MarkerStyle.Square);
        }

        private void PlotFunction(string functionName, Color color, MarkerStyle markerStyle)
        {
            Series series = new Series();
            series.Name = functionName;
            series.ChartType = SeriesChartType.Line;
            series.Color = color;
            series.BorderWidth = 2;

            series.MarkerStyle = markerStyle;
            series.MarkerSize = 8;
            series.MarkerColor = color;
            series.MarkerBorderColor = Color.Black;

            double xMin = -5;
            double xMax = 5;
            double step = 0.1;

            for (double x = xMin; x <= xMax; x += step)
            {
                try
                {
                    double y = CalculateFunction(functionName, x);
                    series.Points.AddXY(x, y);
                }
                catch
                {
                }
            }
            chart1.Series.Add(series);
        }

        private double CalculateFunction(string functionName, double x)
        {
            functionName = functionName.ToLower().Replace(" ", "");

            if (functionName.Contains("sin"))
            {
                return Math.Sin(x);
            }
            else if (functionName.Contains("cos"))
            {
                return Math.Cos(x);
            }
            else if (functionName.Contains("x^2") || functionName.Contains("x*x"))
            {
                return x * x;
            }
            else if (functionName.Contains("x^3"))
            {
                return x * x * x;
            }
            else if (functionName == "x")
            {
                return x;
            }
            else if (functionName.Contains("exp") || functionName.Contains("e^x"))
            {
                return Math.Exp(x);
            }
            else
            {
                try
                {
                    string expr = functionName.Replace("x", x.ToString());
                    return Convert.ToDouble(new System.Data.DataTable().Compute(expr, null));
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}