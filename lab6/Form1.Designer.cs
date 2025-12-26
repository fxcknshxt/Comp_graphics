namespace OpenGL_GLControl
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.GroupBox groupBoxDrawing;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonAngleZero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAngleZero = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBoxDrawing = new System.Windows.Forms.GroupBox();
            this.glControl1 = new OpenTK.GLControl();
            this.groupBoxControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBoxDrawing.SuspendLayout();
            this.SuspendLayout();

            // groupBoxControls
            this.groupBoxControls.Controls.Add(this.label2);
            this.groupBoxControls.Controls.Add(this.label1);
            this.groupBoxControls.Controls.Add(this.buttonAngleZero);
            this.groupBoxControls.Controls.Add(this.buttonRotate);
            this.groupBoxControls.Controls.Add(this.numericUpDown1);
            this.groupBoxControls.Controls.Add(this.comboBox1);
            this.groupBoxControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxControls.Location = new System.Drawing.Point(0, 0);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(800, 100);
            this.groupBoxControls.TabIndex = 0;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Управление";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Угол вращения:";

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ось вращ.:";

            // buttonAngleZero
            this.buttonAngleZero.Location = new System.Drawing.Point(450, 25);
            this.buttonAngleZero.Name = "buttonAngleZero";
            this.buttonAngleZero.Size = new System.Drawing.Size(100, 30);
            this.buttonAngleZero.TabIndex = 3;
            this.buttonAngleZero.Text = "Angle = 0";
            this.buttonAngleZero.UseVisualStyleBackColor = true;
            this.buttonAngleZero.Click += new System.EventHandler(this.buttonAngleZero_Click);

            // buttonRotate
            this.buttonRotate.Location = new System.Drawing.Point(330, 25);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(100, 30);
            this.buttonRotate.TabIndex = 2;
            this.buttonRotate.Text = "Rotate";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.buttonRotate_Click);

            // numericUpDown1
            this.numericUpDown1.Location = new System.Drawing.Point(200, 50);
            this.numericUpDown1.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            this.numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] { 3, 0, 0, 0 });

            // comboBox1
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "X", "Y", "Z" });
            this.comboBox1.Location = new System.Drawing.Point(20, 50);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 21);
            this.comboBox1.TabIndex = 0;

            // groupBoxDrawing
            this.groupBoxDrawing.Controls.Add(this.glControl1);
            this.groupBoxDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDrawing.Location = new System.Drawing.Point(0, 100);
            this.groupBoxDrawing.Name = "groupBoxDrawing";
            this.groupBoxDrawing.Size = new System.Drawing.Size(800, 500);
            this.groupBoxDrawing.TabIndex = 1;
            this.groupBoxDrawing.TabStop = false;
            this.groupBoxDrawing.Text = "Область рисования";

            // glControl1
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(3, 16);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(794, 481);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.groupBoxDrawing);
            this.Controls.Add(this.groupBoxControls);
            this.Name = "Form1";
            this.Text = "OpenGL GLControl - Лаба 6";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBoxDrawing.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}