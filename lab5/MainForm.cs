using System;
using System.Drawing;
using System.Windows.Forms;

public class MainForm : Form
{

    private PictureBox picOriginal;
    private PictureBox picModified;

    private Bitmap originalBitmap;
    private Bitmap modifiedBitmap;

    private Button btnLoad;
    private Button btnAnalyzePixel;
    private Button btnProcess;
    private Button btnAnalyzeRange;
    private Button btnClear;

    private TextBox txtPixelX;
    private TextBox txtPixelY;
    private TextBox txtBrightness;
    private TextBox txtStartX;
    private TextBox txtEndX;
    private TextBox txtStartY;
    private TextBox txtEndY;

    private Label lblPixelX;
    private Label lblPixelY;
    private Label lblBrightness;
    private Label lblRange;
    private Label lblResult;

    private DataGridView dataGrid;
    private ListBox listBox;

    private ComboBox comboFilter;

    private OpenFileDialog fileDialog;

    public MainForm()
    {
        BuildForm();
    }

    private void BuildForm()
    {
        this.Text = "Lab 5 - Bitmap Processing";
        this.Width = 1100;
        this.Height = 750;

        fileDialog = new OpenFileDialog();
        fileDialog.Filter = "Images|*.bmp;*.jpg;*.png;*.jpeg";

        btnLoad = new Button();
        btnLoad.Text = "Load Image";
        btnLoad.Location = new Point(10, 10);
        btnLoad.Size = new Size(120, 30);
        btnLoad.Click += BtnLoad_Click;

        btnProcess = new Button();
        btnProcess.Text = "Process Image";
        btnProcess.Location = new Point(140, 10);
        btnProcess.Size = new Size(120, 30);
        btnProcess.Enabled = false;
        btnProcess.Click += BtnProcess_Click;

        btnClear = new Button();
        btnClear.Text = "Clear Data";
        btnClear.Location = new Point(270, 10);
        btnClear.Size = new Size(100, 30);
        btnClear.Click += BtnClear_Click;

        picOriginal = new PictureBox();
        picOriginal.Location = new Point(10, 50);
        picOriginal.Size = new Size(400, 400);
        picOriginal.BorderStyle = BorderStyle.FixedSingle;
        picOriginal.BackColor = Color.LightGray;

        picModified = new PictureBox();
        picModified.Location = new Point(420, 50);
        picModified.Size = new Size(400, 400);
        picModified.BorderStyle = BorderStyle.FixedSingle;
        picModified.BackColor = Color.LightGray;

        lblPixelX = new Label();
        lblPixelX.Text = "X:";
        lblPixelX.Location = new Point(10, 460);
        lblPixelX.Size = new Size(20, 20);

        txtPixelX = new TextBox();
        txtPixelX.Location = new Point(35, 457);
        txtPixelX.Size = new Size(50, 20);
        txtPixelX.Text = "0";

        lblPixelY = new Label();
        lblPixelY.Text = "Y:";
        lblPixelY.Location = new Point(95, 460);
        lblPixelY.Size = new Size(20, 20);

        txtPixelY = new TextBox();
        txtPixelY.Location = new Point(120, 457);
        txtPixelY.Size = new Size(50, 20);
        txtPixelY.Text = "0";

        btnAnalyzePixel = new Button();
        btnAnalyzePixel.Text = "Analyze Pixel";
        btnAnalyzePixel.Location = new Point(180, 455);
        btnAnalyzePixel.Size = new Size(100, 25);
        btnAnalyzePixel.Enabled = false;
        btnAnalyzePixel.Click += BtnAnalyzePixel_Click;

        lblRange = new Label();
        lblRange.Text = "Range X:";
        lblRange.Location = new Point(10, 490);
        lblRange.Size = new Size(50, 20);

        txtStartX = new TextBox();
        txtStartX.Location = new Point(65, 487);
        txtStartX.Size = new Size(40, 20);
        txtStartX.Text = "0";

        Label lblDash1 = new Label();
        lblDash1.Text = "-";
        lblDash1.Location = new Point(110, 490);

        txtEndX = new TextBox();
        txtEndX.Location = new Point(120, 487);
        txtEndX.Size = new Size(40, 20);
        txtEndX.Text = "10";

        Label lblRangeY = new Label();
        lblRangeY.Text = "Y:";
        lblRangeY.Location = new Point(170, 490);
        lblRangeY.Size = new Size(20, 20);

        txtStartY = new TextBox();
        txtStartY.Location = new Point(195, 487);
        txtStartY.Size = new Size(40, 20);
        txtStartY.Text = "0";

        Label lblDash2 = new Label();
        lblDash2.Text = "-";
        lblDash2.Location = new Point(240, 490);

        txtEndY = new TextBox();
        txtEndY.Location = new Point(250, 487);
        txtEndY.Size = new Size(40, 20);
        txtEndY.Text = "10";

        btnAnalyzeRange = new Button();
        btnAnalyzeRange.Text = "Analyze Range";
        btnAnalyzeRange.Location = new Point(300, 485);
        btnAnalyzeRange.Size = new Size(100, 25);
        btnAnalyzeRange.Enabled = false;
        btnAnalyzeRange.Click += BtnAnalyzeRange_Click;

        Label lblFilter = new Label();
        lblFilter.Text = "Filter:";
        lblFilter.Location = new Point(420, 460);
        lblFilter.Size = new Size(40, 20);

        comboFilter = new ComboBox();
        comboFilter.Location = new Point(465, 457);
        comboFilter.Size = new Size(120, 25);
        comboFilter.Items.AddRange(new string[] {
            "Brightness", "Invert", "Grayscale", "Sepia"
        });
        comboFilter.SelectedIndex = 0;

        lblBrightness = new Label();
        lblBrightness.Text = "Value:";
        lblBrightness.Location = new Point(595, 460);
        lblBrightness.Size = new Size(40, 20);

        txtBrightness = new TextBox();
        txtBrightness.Location = new Point(640, 457);
        txtBrightness.Size = new Size(50, 20);
        txtBrightness.Text = "30";

        dataGrid = new DataGridView();
        dataGrid.Location = new Point(10, 520);
        dataGrid.Size = new Size(400, 150);
        dataGrid.AllowUserToAddRows = false;
        dataGrid.ReadOnly = true;
        SetupDataGrid();

        listBox = new ListBox();
        listBox.Location = new Point(420, 520);
        listBox.Size = new Size(400, 150);
        listBox.Font = new Font("Consolas", 8);

        lblResult = new Label();
        lblResult.Location = new Point(10, 680);
        lblResult.Size = new Size(800, 30);
        lblResult.Text = "Ready";

        this.Controls.Add(btnLoad);
        this.Controls.Add(btnProcess);
        this.Controls.Add(btnClear);
        this.Controls.Add(picOriginal);
        this.Controls.Add(picModified);
        this.Controls.Add(lblPixelX);
        this.Controls.Add(txtPixelX);
        this.Controls.Add(lblPixelY);
        this.Controls.Add(txtPixelY);
        this.Controls.Add(btnAnalyzePixel);
        this.Controls.Add(lblRange);
        this.Controls.Add(txtStartX);
        this.Controls.Add(lblDash1);
        this.Controls.Add(txtEndX);
        this.Controls.Add(lblRangeY);
        this.Controls.Add(txtStartY);
        this.Controls.Add(lblDash2);
        this.Controls.Add(txtEndY);
        this.Controls.Add(btnAnalyzeRange);
        this.Controls.Add(lblFilter);
        this.Controls.Add(comboFilter);
        this.Controls.Add(lblBrightness);
        this.Controls.Add(txtBrightness);
        this.Controls.Add(dataGrid);
        this.Controls.Add(listBox);
        this.Controls.Add(lblResult);
    }

    private void SetupDataGrid()
    {
        dataGrid.Columns.Clear();
        dataGrid.Columns.Add("X", "X");
        dataGrid.Columns.Add("Y", "Y");
        dataGrid.Columns.Add("R", "R");
        dataGrid.Columns.Add("G", "G");
        dataGrid.Columns.Add("B", "B");
        dataGrid.Columns.Add("Bright", "Brightness");

        foreach (DataGridViewColumn col in dataGrid.Columns)
        {
            col.Width = 60;
        }
    }

    private void BtnLoad_Click(object sender, EventArgs e)
    {
        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                originalBitmap = new Bitmap(fileDialog.FileName);
                picOriginal.Image = originalBitmap;

                btnAnalyzePixel.Enabled = true;
                btnProcess.Enabled = true;
                btnAnalyzeRange.Enabled = true;

                ClearData();
                lblResult.Text = $"Loaded: {originalBitmap.Width}x{originalBitmap.Height} pixels";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }

    private void BtnAnalyzePixel_Click(object sender, EventArgs e)
    {
        if (originalBitmap == null) return;

        if (int.TryParse(txtPixelX.Text, out int x) && int.TryParse(txtPixelY.Text, out int y))
        {
            if (x >= 0 && y >= 0 && x < originalBitmap.Width && y < originalBitmap.Height)
            {
                Color pixel = originalBitmap.GetPixel(x, y);
                int brightness = (pixel.R + pixel.G + pixel.B) / 3;

                dataGrid.Rows.Clear();
                dataGrid.Rows.Add(x, y, pixel.R, pixel.G, pixel.B, brightness);

                listBox.Items.Clear();
                listBox.Items.Add($"Pixel ({x},{y}):");
                listBox.Items.Add($"  RGB: ({pixel.R}, {pixel.G}, {pixel.B})");
                listBox.Items.Add($"  Brightness: {brightness}");

                lblResult.Text = $"Pixel ({x},{y}): R={pixel.R}, G={pixel.G}, B={pixel.B}";
            }
        }
    }

    private void BtnAnalyzeRange_Click(object sender, EventArgs e)
    {
        if (originalBitmap == null) return;

        try
        {
            int startX = int.Parse(txtStartX.Text);
            int endX = int.Parse(txtEndX.Text);
            int startY = int.Parse(txtStartY.Text);
            int endY = int.Parse(txtEndY.Text);

            startX = Math.Max(0, Math.Min(startX, originalBitmap.Width - 1));
            endX = Math.Max(startX, Math.Min(endX, originalBitmap.Width - 1));
            startY = Math.Max(0, Math.Min(startY, originalBitmap.Height - 1));
            endY = Math.Max(startY, Math.Min(endY, originalBitmap.Height - 1));

            dataGrid.Rows.Clear();
            listBox.Items.Clear();

            int count = 0;
            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    Color pixel = originalBitmap.GetPixel(x, y);
                    int brightness = (pixel.R + pixel.G + pixel.B) / 3;

                    dataGrid.Rows.Add(x, y, pixel.R, pixel.G, pixel.B, brightness);
                    if (count < 50)
                    {
                        listBox.Items.Add($"({x},{y}): R={pixel.R} G={pixel.G} B={pixel.B}");
                    }
                    count++;
                }
            }

            lblResult.Text = $"Analyzed {count} pixels in range";
        }
        catch (FormatException)
        {
            MessageBox.Show("Enter valid numbers!");
        }
    }

    private void BtnProcess_Click(object sender, EventArgs e)
    {
        if (originalBitmap == null) return;

        modifiedBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

        int brightnessValue = 0;
        if (comboFilter.SelectedIndex == 0)
        {
            if (!int.TryParse(txtBrightness.Text, out brightnessValue))
                brightnessValue = 0;
        }

        for (int y = 0; y < originalBitmap.Height; y++)
        {
            for (int x = 0; x < originalBitmap.Width; x++)
            {
                Color original = originalBitmap.GetPixel(x, y);
                Color modified = ApplyFilter(original, brightnessValue);
                modifiedBitmap.SetPixel(x, y, modified);
            }
        }

        picModified.Image = modifiedBitmap;
        lblResult.Text = $"Applied {comboFilter.SelectedItem} filter";
    }

    private Color ApplyFilter(Color original, int brightnessValue)
    {
        int r = original.R;
        int g = original.G;
        int b = original.B;

        switch (comboFilter.SelectedIndex)
        {
            case 0:
                r = Clamp(r + brightnessValue);
                g = Clamp(g + brightnessValue);
                b = Clamp(b + brightnessValue);
                break;
            case 1:
                r = 255 - r;
                g = 255 - g;
                b = 255 - b;
                break;
            case 2:
                int gray = (r + g + b) / 3;
                r = g = b = gray;
                break;
            case 3:
                int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);
                r = Clamp(tr);
                g = Clamp(tg);
                b = Clamp(tb);
                break;
        }

        return Color.FromArgb(r, g, b);
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
        lblResult.Text = "Data cleared";
    }

    private void ClearData()
    {
        dataGrid.Rows.Clear();
        listBox.Items.Clear();
    }

    private int Clamp(int value)
    {
        return Math.Max(0, Math.Min(255, value));
    }

    [STAThread]
    static void Main()
    {
        Application.Run(new MainForm());
    }
}