namespace GraphicsOpenGL
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.segmentButton = new System.Windows.Forms.Button();
            this.TriangleButton = new System.Windows.Forms.Button();
            this.QuadrangleButton = new System.Windows.Forms.Button();
            this.PentagonButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.Location = new System.Drawing.Point(2, 1);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(644, 509);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.openGLControl1_KeyPress);
            this.openGLControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseClick);
            // 
            // segmentButton
            // 
            this.segmentButton.Location = new System.Drawing.Point(670, 19);
            this.segmentButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.segmentButton.Name = "segmentButton";
            this.segmentButton.Size = new System.Drawing.Size(100, 30);
            this.segmentButton.TabIndex = 1;
            this.segmentButton.Text = "Отрезок";
            this.segmentButton.UseVisualStyleBackColor = true;
            this.segmentButton.Click += new System.EventHandler(this.segmentButtonClick);
            // 
            // TriangleButton
            // 
            this.TriangleButton.Location = new System.Drawing.Point(670, 53);
            this.TriangleButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TriangleButton.Name = "TriangleButton";
            this.TriangleButton.Size = new System.Drawing.Size(100, 30);
            this.TriangleButton.TabIndex = 2;
            this.TriangleButton.Text = "Треугольник";
            this.TriangleButton.UseVisualStyleBackColor = true;
            this.TriangleButton.Click += new System.EventHandler(this.triangleButtonClick);
            // 
            // QuadrangleButton
            // 
            this.QuadrangleButton.Location = new System.Drawing.Point(670, 106);
            this.QuadrangleButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.QuadrangleButton.Name = "QuadrangleButton";
            this.QuadrangleButton.Size = new System.Drawing.Size(100, 32);
            this.QuadrangleButton.TabIndex = 3;
            this.QuadrangleButton.Text = "Четырехугольник";
            this.QuadrangleButton.UseVisualStyleBackColor = true;
            this.QuadrangleButton.Click += new System.EventHandler(this.quadrangleButtonClick);
            // 
            // PentagonButton
            // 
            this.PentagonButton.Location = new System.Drawing.Point(670, 155);
            this.PentagonButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PentagonButton.Name = "PentagonButton";
            this.PentagonButton.Size = new System.Drawing.Size(100, 32);
            this.PentagonButton.TabIndex = 4;
            this.PentagonButton.Text = "Многоугольник";
            this.PentagonButton.UseVisualStyleBackColor = true;
            this.PentagonButton.Click += new System.EventHandler(this.pentagonButtonClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(689, 206);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(81, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Рисование";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(843, 170);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 32);
            this.button5.TabIndex = 6;
            this.button5.Text = "Построить";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(670, 297);
            this.button7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(206, 32);
            this.button7.TabIndex = 8;
            this.button7.Text = "Вращение вокруг сцены";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(670, 239);
            this.button6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 32);
            this.button6.TabIndex = 9;
            this.button6.Text = "Пьедестал";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(670, 352);
            this.button8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(206, 32);
            this.button8.TabIndex = 10;
            this.button8.Text = "Вращение вокруг центрального куба";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(670, 405);
            this.button9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(206, 32);
            this.button9.TabIndex = 11;
            this.button9.Text = "Вращение вокруг своей оси";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(835, 206);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(118, 74);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Проекция";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(4, 48);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(116, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Ортографическая";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(4, 26);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(104, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Перспективная";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 509);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.PentagonButton);
            this.Controls.Add(this.QuadrangleButton);
            this.Controls.Add(this.TriangleButton);
            this.Controls.Add(this.segmentButton);
            this.Controls.Add(this.openGLControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button segmentButton;
        private System.Windows.Forms.Button TriangleButton;
        private System.Windows.Forms.Button QuadrangleButton;
        private System.Windows.Forms.Button PentagonButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

