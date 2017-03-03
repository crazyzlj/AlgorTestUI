namespace TestDemo
{
    partial class SubProc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubProc));
            this.axTChartSubProcRuntime = new AxTeeChart.AxTChart();
            this.tabControlSubProc = new System.Windows.Forms.TabControl();
            this.tabPageSubProcRuntime = new System.Windows.Forms.TabPage();
            this.tabPageSubProcSpeedup = new System.Windows.Forms.TabPage();
            this.tabPageSubProcEfficiency = new System.Windows.Forms.TabPage();
            this.axTChartSubProcSpeedup = new AxTeeChart.AxTChart();
            this.axTChartSubProcEfficiency = new AxTeeChart.AxTChart();
            this.buttonRuntimeAll = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axTChartSubProcRuntime)).BeginInit();
            this.tabControlSubProc.SuspendLayout();
            this.tabPageSubProcRuntime.SuspendLayout();
            this.tabPageSubProcSpeedup.SuspendLayout();
            this.tabPageSubProcEfficiency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTChartSubProcSpeedup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChartSubProcEfficiency)).BeginInit();
            this.SuspendLayout();
            // 
            // axTChartSubProcRuntime
            // 
            this.axTChartSubProcRuntime.Enabled = true;
            this.axTChartSubProcRuntime.Location = new System.Drawing.Point(1, 2);
            this.axTChartSubProcRuntime.Name = "axTChartSubProcRuntime";
            this.axTChartSubProcRuntime.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChartSubProcRuntime.OcxState")));
            this.axTChartSubProcRuntime.Size = new System.Drawing.Size(652, 331);
            this.axTChartSubProcRuntime.TabIndex = 0;
            // 
            // tabControlSubProc
            // 
            this.tabControlSubProc.Controls.Add(this.tabPageSubProcRuntime);
            this.tabControlSubProc.Controls.Add(this.tabPageSubProcSpeedup);
            this.tabControlSubProc.Controls.Add(this.tabPageSubProcEfficiency);
            this.tabControlSubProc.Location = new System.Drawing.Point(1, 1);
            this.tabControlSubProc.Name = "tabControlSubProc";
            this.tabControlSubProc.SelectedIndex = 0;
            this.tabControlSubProc.Size = new System.Drawing.Size(661, 360);
            this.tabControlSubProc.TabIndex = 1;
            // 
            // tabPageSubProcRuntime
            // 
            this.tabPageSubProcRuntime.Controls.Add(this.buttonRuntimeAll);
            this.tabPageSubProcRuntime.Controls.Add(this.axTChartSubProcRuntime);
            this.tabPageSubProcRuntime.Location = new System.Drawing.Point(4, 22);
            this.tabPageSubProcRuntime.Name = "tabPageSubProcRuntime";
            this.tabPageSubProcRuntime.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSubProcRuntime.Size = new System.Drawing.Size(653, 334);
            this.tabPageSubProcRuntime.TabIndex = 0;
            this.tabPageSubProcRuntime.Text = "运行时间";
            this.tabPageSubProcRuntime.UseVisualStyleBackColor = true;
            // 
            // tabPageSubProcSpeedup
            // 
            this.tabPageSubProcSpeedup.Controls.Add(this.button1);
            this.tabPageSubProcSpeedup.Controls.Add(this.axTChartSubProcSpeedup);
            this.tabPageSubProcSpeedup.Location = new System.Drawing.Point(4, 22);
            this.tabPageSubProcSpeedup.Name = "tabPageSubProcSpeedup";
            this.tabPageSubProcSpeedup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSubProcSpeedup.Size = new System.Drawing.Size(653, 334);
            this.tabPageSubProcSpeedup.TabIndex = 1;
            this.tabPageSubProcSpeedup.Text = "加速比";
            this.tabPageSubProcSpeedup.UseVisualStyleBackColor = true;
            // 
            // tabPageSubProcEfficiency
            // 
            this.tabPageSubProcEfficiency.Controls.Add(this.button2);
            this.tabPageSubProcEfficiency.Controls.Add(this.axTChartSubProcEfficiency);
            this.tabPageSubProcEfficiency.Location = new System.Drawing.Point(4, 22);
            this.tabPageSubProcEfficiency.Name = "tabPageSubProcEfficiency";
            this.tabPageSubProcEfficiency.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSubProcEfficiency.Size = new System.Drawing.Size(653, 334);
            this.tabPageSubProcEfficiency.TabIndex = 2;
            this.tabPageSubProcEfficiency.Text = "并行效率";
            this.tabPageSubProcEfficiency.UseVisualStyleBackColor = true;
            // 
            // axTChartSubProcSpeedup
            // 
            this.axTChartSubProcSpeedup.Enabled = true;
            this.axTChartSubProcSpeedup.Location = new System.Drawing.Point(0, 2);
            this.axTChartSubProcSpeedup.Name = "axTChartSubProcSpeedup";
            this.axTChartSubProcSpeedup.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChartSubProcSpeedup.OcxState")));
            this.axTChartSubProcSpeedup.Size = new System.Drawing.Size(653, 331);
            this.axTChartSubProcSpeedup.TabIndex = 1;
            // 
            // axTChartSubProcEfficiency
            // 
            this.axTChartSubProcEfficiency.Enabled = true;
            this.axTChartSubProcEfficiency.Location = new System.Drawing.Point(0, 2);
            this.axTChartSubProcEfficiency.Name = "axTChartSubProcEfficiency";
            this.axTChartSubProcEfficiency.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTChartSubProcEfficiency.OcxState")));
            this.axTChartSubProcEfficiency.Size = new System.Drawing.Size(653, 331);
            this.axTChartSubProcEfficiency.TabIndex = 1;
            // 
            // buttonRuntimeAll
            // 
            this.buttonRuntimeAll.Location = new System.Drawing.Point(593, 6);
            this.buttonRuntimeAll.Name = "buttonRuntimeAll";
            this.buttonRuntimeAll.Size = new System.Drawing.Size(54, 37);
            this.buttonRuntimeAll.TabIndex = 1;
            this.buttonRuntimeAll.Text = "反选";
            this.buttonRuntimeAll.UseVisualStyleBackColor = true;
            this.buttonRuntimeAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "反选";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(596, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "反选";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SubProc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 362);
            this.Controls.Add(this.tabControlSubProc);
            this.MaximumSize = new System.Drawing.Size(680, 400);
            this.MinimumSize = new System.Drawing.Size(680, 400);
            this.Name = "SubProc";
            this.Text = "SubProcess";
            this.Load += new System.EventHandler(this.SubProc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axTChartSubProcRuntime)).EndInit();
            this.tabControlSubProc.ResumeLayout(false);
            this.tabPageSubProcRuntime.ResumeLayout(false);
            this.tabPageSubProcSpeedup.ResumeLayout(false);
            this.tabPageSubProcEfficiency.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTChartSubProcSpeedup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTChartSubProcEfficiency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxTeeChart.AxTChart axTChartSubProcRuntime;
        private System.Windows.Forms.TabControl tabControlSubProc;
        private System.Windows.Forms.TabPage tabPageSubProcRuntime;
        private System.Windows.Forms.TabPage tabPageSubProcSpeedup;
        private AxTeeChart.AxTChart axTChartSubProcSpeedup;
        private System.Windows.Forms.TabPage tabPageSubProcEfficiency;
        private AxTeeChart.AxTChart axTChartSubProcEfficiency;
        private System.Windows.Forms.Button buttonRuntimeAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

