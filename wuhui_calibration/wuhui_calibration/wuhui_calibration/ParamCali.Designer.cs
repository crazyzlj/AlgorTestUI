namespace TestDemo
{
    partial class FormWuhui_calibration
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
            this.dataGridViewParamsCali = new System.Windows.Forms.DataGridView();
            this.ColumnParasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnInitialValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParamsCali)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewParamsCali
            // 
            this.dataGridViewParamsCali.AllowUserToAddRows = false;
            this.dataGridViewParamsCali.AllowUserToDeleteRows = false;
            this.dataGridViewParamsCali.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewParamsCali.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParamsCali.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnParasName,
            this.ColumnInitialValue,
            this.ColumnMin,
            this.ColumnMax});
            this.dataGridViewParamsCali.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewParamsCali.Name = "dataGridViewParamsCali";
            this.dataGridViewParamsCali.ReadOnly = true;
            this.dataGridViewParamsCali.RowTemplate.Height = 23;
            this.dataGridViewParamsCali.Size = new System.Drawing.Size(605, 338);
            this.dataGridViewParamsCali.TabIndex = 0;
            // 
            // ColumnParasName
            // 
            this.ColumnParasName.HeaderText = "参数名";
            this.ColumnParasName.Name = "ColumnParasName";
            this.ColumnParasName.ReadOnly = true;
            this.ColumnParasName.ToolTipText = "ColumnParasName";
            // 
            // ColumnInitialValue
            // 
            this.ColumnInitialValue.HeaderText = "初始值";
            this.ColumnInitialValue.Name = "ColumnInitialValue";
            this.ColumnInitialValue.ReadOnly = true;
            // 
            // ColumnMin
            // 
            this.ColumnMin.HeaderText = "最小值";
            this.ColumnMin.Name = "ColumnMin";
            this.ColumnMin.ReadOnly = true;
            // 
            // ColumnMax
            // 
            this.ColumnMax.HeaderText = "最大值";
            this.ColumnMax.Name = "ColumnMax";
            this.ColumnMax.ReadOnly = true;
            // 
            // FormWuhui_calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 362);
            this.Controls.Add(this.dataGridViewParamsCali);
            this.Name = "FormWuhui_calibration";
            this.Text = "参数率定结果";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParamsCali)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewParamsCali;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnParasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInitialValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMax;
    }
}

