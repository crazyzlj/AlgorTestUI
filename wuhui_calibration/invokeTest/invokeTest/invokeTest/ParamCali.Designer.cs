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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewParamsCali = new System.Windows.Forms.DataGridView();
            this.ColumnParasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnInitialValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripDraw = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemHydroQ = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHydroS = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParamsCali)).BeginInit();
            this.contextMenuStripDraw.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewParamsCali
            // 
            this.dataGridViewParamsCali.AllowUserToAddRows = false;
            this.dataGridViewParamsCali.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewParamsCali.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnParasName,
            this.ColumnInitialValue,
            this.ColumnMin,
            this.ColumnMax});
            this.dataGridViewParamsCali.Location = new System.Drawing.Point(12, 39);
            this.dataGridViewParamsCali.Name = "dataGridViewParamsCali";
            this.dataGridViewParamsCali.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dataGridViewParamsCali.RowTemplate.Height = 23;
            this.dataGridViewParamsCali.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewParamsCali.Size = new System.Drawing.Size(640, 311);
            this.dataGridViewParamsCali.TabIndex = 0;
            this.dataGridViewParamsCali.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewParamsCali_CellMouseDown);
            // 
            // ColumnParasName
            // 
            this.ColumnParasName.Frozen = true;
            this.ColumnParasName.HeaderText = "参数名";
            this.ColumnParasName.Name = "ColumnParasName";
            this.ColumnParasName.ReadOnly = true;
            this.ColumnParasName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnParasName.ToolTipText = "ColumnParasName";
            // 
            // ColumnInitialValue
            // 
            this.ColumnInitialValue.HeaderText = "初始值";
            this.ColumnInitialValue.Name = "ColumnInitialValue";
            this.ColumnInitialValue.ReadOnly = true;
            this.ColumnInitialValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnInitialValue.ToolTipText = "ColumnInitialValue";
            // 
            // ColumnMin
            // 
            this.ColumnMin.HeaderText = "最小值";
            this.ColumnMin.Name = "ColumnMin";
            this.ColumnMin.ReadOnly = true;
            this.ColumnMin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnMin.ToolTipText = "ColumnMin";
            // 
            // ColumnMax
            // 
            this.ColumnMax.HeaderText = "最大值";
            this.ColumnMax.Name = "ColumnMax";
            this.ColumnMax.ReadOnly = true;
            this.ColumnMax.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnMax.ToolTipText = "ColumnMax";
            // 
            // contextMenuStripDraw
            // 
            this.contextMenuStripDraw.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHydroQ,
            this.toolStripMenuItemHydroS});
            this.contextMenuStripDraw.Name = "contextMenuStripDraw";
            this.contextMenuStripDraw.Size = new System.Drawing.Size(161, 48);
            // 
            // toolStripMenuItemHydroQ
            // 
            this.toolStripMenuItemHydroQ.Name = "toolStripMenuItemHydroQ";
            this.toolStripMenuItemHydroQ.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemHydroQ.Text = "绘制流量过程线";
            this.toolStripMenuItemHydroQ.Click += new System.EventHandler(this.toolStripMenuItemHydroQ_Click);
            // 
            // toolStripMenuItemHydroS
            // 
            this.toolStripMenuItemHydroS.Name = "toolStripMenuItemHydroS";
            this.toolStripMenuItemHydroS.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemHydroS.Text = "绘制泥沙过程线";
            this.toolStripMenuItemHydroS.Click += new System.EventHandler(this.toolStripMenuItemHydroS_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(503, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "说明：1、参数率定的是针对输入数据的乘积系数；2、初始值-99为标识该参数为空间分布参数";
            // 
            // FormWuhui_calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 362);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewParamsCali);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(680, 400);
            this.MinimumSize = new System.Drawing.Size(680, 400);
            this.Name = "FormWuhui_calibration";
            this.Text = "参数率定结果";
            this.Load += new System.EventHandler(this.FormWuhui_calibration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParamsCali)).EndInit();
            this.contextMenuStripDraw.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewParamsCali;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDraw;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHydroQ;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHydroS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnParasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInitialValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMax;
        private System.Windows.Forms.Label label1;
    }
}

