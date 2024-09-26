using System.Drawing;

namespace Gorner
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
            this.components = new System.ComponentModel.Container();
            this.Graphic = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // Graphic
            // 
            this.Graphic.AutoSize = true;
            this.Graphic.Location = new System.Drawing.Point(12, 12);
            this.Graphic.Name = "Graphic";
            this.Graphic.ScrollGrace = 0D;
            this.Graphic.ScrollMaxX = 0D;
            this.Graphic.ScrollMaxY = 0D;
            this.Graphic.ScrollMaxY2 = 0D;
            this.Graphic.ScrollMinX = 0D;
            this.Graphic.ScrollMinY = 0D;
            this.Graphic.ScrollMinY2 = 0D;
            this.Graphic.Size = new System.Drawing.Size(760, 454);
            this.Graphic.TabIndex = 0;
            this.Graphic.UseExtendedPrintDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 501);
            this.Controls.Add(this.Graphic);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl Graphic;
        
    }
}

