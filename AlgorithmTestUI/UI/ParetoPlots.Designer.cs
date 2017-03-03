namespace AlgorithmTest
{
    partial class FormPareto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPareto));
            this.axTChartParetoPlots = new AxTeeChart.AxTChart();
            ((System.ComponentModel.ISupportInitialize)(this.axTChartParetoPlots)).BeginInit();
            this.SuspendLayout();
            // 
            // axTChartParetoPlots
            // 
            this.axTChartParetoPlots.Enabled = true;
            this.axTChartParetoPlots.Location = new System.Drawing.Point(0, 1);
            this.axTChartParetoPlots.Name = "axTChartParetoPlots";
            this.axTChartParetoPlots.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChartParetoPlots.OcxState")));
            this.axTChartParetoPlots.Size = new System.Drawing.Size(664, 363);
            this.axTChartParetoPlots.TabIndex = 0;
            // 
            // FormPareto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 362);
            this.Controls.Add(this.axTChartParetoPlots);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(680, 400);
            this.MinimumSize = new System.Drawing.Size(680, 400);
            this.Name = "FormPareto";
            this.Text = "情景分析-帕累托图";
            this.Load += new System.EventHandler(this.FormPareto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTChartParetoPlots)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxTeeChart.AxTChart axTChartParetoPlots;
    }
}

