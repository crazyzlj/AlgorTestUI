namespace AlgorithmTest
{
    partial class Hydrograph
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hydrograph));
            this.axTChartHydrograph = new AxTeeChart.AxTChart();
            this.labelChartTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axTChartHydrograph)).BeginInit();
            this.SuspendLayout();
            // 
            // axTChartHydrograph
            // 
            this.axTChartHydrograph.Enabled = true;
            this.axTChartHydrograph.Location = new System.Drawing.Point(27, 54);
            this.axTChartHydrograph.MaximumSize = new System.Drawing.Size(574, 281);
            this.axTChartHydrograph.MinimumSize = new System.Drawing.Size(574, 281);
            this.axTChartHydrograph.Name = "axTChartHydrograph";
            this.axTChartHydrograph.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChartHydrograph.OcxState")));
            this.axTChartHydrograph.Size = new System.Drawing.Size(574, 281);
            this.axTChartHydrograph.TabIndex = 0;
            // 
            // labelChartTitle
            // 
            this.labelChartTitle.AutoSize = true;
            this.labelChartTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChartTitle.Location = new System.Drawing.Point(242, 30);
            this.labelChartTitle.Name = "labelChartTitle";
            this.labelChartTitle.Size = new System.Drawing.Size(0, 19);
            this.labelChartTitle.TabIndex = 2;
            // 
            // Hydrograph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 362);
            this.Controls.Add(this.labelChartTitle);
            this.Controls.Add(this.axTChartHydrograph);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(645, 400);
            this.MinimumSize = new System.Drawing.Size(645, 400);
            this.Name = "Hydrograph";
            this.Text = "Hydrograph";
            this.Load += new System.EventHandler(this.Hydrograph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTChartHydrograph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxTeeChart.AxTChart axTChartHydrograph;
        private System.Windows.Forms.Label labelChartTitle;
    }
}

