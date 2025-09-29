namespace SolveSLAU
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.graph = new ZedGraph.ZedGraphControl();
            this.start_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.matrixBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.vectorsBox = new System.Windows.Forms.RichTextBox();
            this.countx = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.valueEps = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.countx)).BeginInit();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.Location = new System.Drawing.Point(259, 27);
            this.graph.Name = "graph";
            this.graph.ScrollGrace = 0D;
            this.graph.ScrollMaxX = 0D;
            this.graph.ScrollMaxY = 0D;
            this.graph.ScrollMaxY2 = 0D;
            this.graph.ScrollMinX = 0D;
            this.graph.ScrollMinY = 0D;
            this.graph.ScrollMinY2 = 0D;
            this.graph.Size = new System.Drawing.Size(660, 446);
            this.graph.TabIndex = 0;
            this.graph.UseExtendedPrintDialog = true;
            // 
            // start_button
            // 
            this.start_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.start_button.Location = new System.Drawing.Point(95, 424);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(77, 49);
            this.start_button.TabIndex = 1;
            this.start_button.Text = "START";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите исходную матрицу";
            // 
            // matrixBox
            // 
            this.matrixBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.matrixBox.Location = new System.Drawing.Point(15, 104);
            this.matrixBox.Name = "matrixBox";
            this.matrixBox.Size = new System.Drawing.Size(227, 146);
            this.matrixBox.TabIndex = 5;
            this.matrixBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(12, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Введите начальное приближение";
            // 
            // vectorsBox
            // 
            this.vectorsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.vectorsBox.Location = new System.Drawing.Point(16, 285);
            this.vectorsBox.Name = "vectorsBox";
            this.vectorsBox.Size = new System.Drawing.Size(227, 61);
            this.vectorsBox.TabIndex = 7;
            this.vectorsBox.Text = "";
            // 
            // countx
            // 
            this.countx.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.countx.Location = new System.Drawing.Point(37, 43);
            this.countx.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.countx.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.countx.Name = "countx";
            this.countx.Size = new System.Drawing.Size(173, 27);
            this.countx.TabIndex = 8;
            this.countx.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label3.Location = new System.Drawing.Point(12, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Введите число неизвестных";
            // 
            // valueEps
            // 
            this.valueEps.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.valueEps.Location = new System.Drawing.Point(59, 370);
            this.valueEps.Name = "valueEps";
            this.valueEps.Size = new System.Drawing.Size(151, 26);
            this.valueEps.TabIndex = 10;
            this.valueEps.Text = "Точность";
            this.valueEps.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 500);
            this.Controls.Add(this.valueEps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.countx);
            this.Controls.Add(this.vectorsBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.matrixBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.graph);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.countx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl graph;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox matrixBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox vectorsBox;
        private System.Windows.Forms.NumericUpDown countx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox valueEps;
    }
}

