﻿namespace AlgorithmTest
{
    partial class SymbolSelectorFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SymbolSelectorFrm));
            this.axSymbologyControl = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ptbPreview = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOutlineColor = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.nudAngle = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.lblOutlineColor = new System.Windows.Forms.Label();
            this.lblAngle = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMoreSymbols = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStripMoreSymbol = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            this.SuspendLayout();
            // 
            // axSymbologyControl
            // 
            this.axSymbologyControl.Location = new System.Drawing.Point(12, 12);
            this.axSymbologyControl.Name = "axSymbologyControl";
            this.axSymbologyControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl.OcxState")));
            this.axSymbologyControl.Size = new System.Drawing.Size(260, 396);
            this.axSymbologyControl.TabIndex = 0;
            this.axSymbologyControl.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.axSymbologyControl_OnDoubleClick);
            this.axSymbologyControl.OnMouseDown += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnMouseDownEventHandler(this.axSymbologyControl_OnMouseDown);
            this.axSymbologyControl.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl_OnItemSelected);
            this.axSymbologyControl.OnStyleClassChanged += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnStyleClassChangedEventHandler(this.axSymbologyControl_OnStyleClassChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ptbPreview);
            this.groupBox1.Location = new System.Drawing.Point(278, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // ptbPreview
            // 
            this.ptbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptbPreview.Location = new System.Drawing.Point(3, 17);
            this.ptbPreview.Name = "ptbPreview";
            this.ptbPreview.Size = new System.Drawing.Size(166, 80);
            this.ptbPreview.TabIndex = 0;
            this.ptbPreview.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOutlineColor);
            this.groupBox2.Controls.Add(this.btnColor);
            this.groupBox2.Controls.Add(this.nudAngle);
            this.groupBox2.Controls.Add(this.nudWidth);
            this.groupBox2.Controls.Add(this.nudSize);
            this.groupBox2.Controls.Add(this.lblOutlineColor);
            this.groupBox2.Controls.Add(this.lblAngle);
            this.groupBox2.Controls.Add(this.lblWidth);
            this.groupBox2.Controls.Add(this.lblSize);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(278, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 180);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // btnOutlineColor
            // 
            this.btnOutlineColor.Location = new System.Drawing.Point(118, 134);
            this.btnOutlineColor.Name = "btnOutlineColor";
            this.btnOutlineColor.Size = new System.Drawing.Size(48, 23);
            this.btnOutlineColor.TabIndex = 9;
            this.btnOutlineColor.Text = "...";
            this.btnOutlineColor.UseVisualStyleBackColor = true;
            this.btnOutlineColor.Click += new System.EventHandler(this.btnOutlineColor_Click);
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(118, 25);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(48, 23);
            this.btnColor.TabIndex = 8;
            this.btnColor.Text = "...";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // nudAngle
            // 
            this.nudAngle.Location = new System.Drawing.Point(46, 107);
            this.nudAngle.Name = "nudAngle";
            this.nudAngle.Size = new System.Drawing.Size(120, 21);
            this.nudAngle.TabIndex = 7;
            this.nudAngle.ValueChanged += new System.EventHandler(this.nudAngle_ValueChanged);
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(46, 80);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(120, 21);
            this.nudWidth.TabIndex = 6;
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // nudSize
            // 
            this.nudSize.Location = new System.Drawing.Point(46, 54);
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(120, 21);
            this.nudSize.TabIndex = 5;
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // lblOutlineColor
            // 
            this.lblOutlineColor.AutoSize = true;
            this.lblOutlineColor.Location = new System.Drawing.Point(7, 141);
            this.lblOutlineColor.Name = "lblOutlineColor";
            this.lblOutlineColor.Size = new System.Drawing.Size(59, 12);
            this.lblOutlineColor.TabIndex = 4;
            this.lblOutlineColor.Text = "外框颜色:";
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(7, 112);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(35, 12);
            this.lblAngle.TabIndex = 3;
            this.lblAngle.Text = "角度:";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(7, 83);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 12);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "宽度:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(7, 56);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(35, 12);
            this.lblSize.TabIndex = 1;
            this.lblSize.Text = "大小:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色:";
            // 
            // btnMoreSymbols
            // 
            this.btnMoreSymbols.Location = new System.Drawing.Point(369, 313);
            this.btnMoreSymbols.Name = "btnMoreSymbols";
            this.btnMoreSymbols.Size = new System.Drawing.Size(75, 23);
            this.btnMoreSymbols.TabIndex = 3;
            this.btnMoreSymbols.Text = "更多符号";
            this.btnMoreSymbols.UseVisualStyleBackColor = true;
            this.btnMoreSymbols.Click += new System.EventHandler(this.btnMoreSymbols_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(369, 342);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(369, 371);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Styles 文件|*.ServerStyle";
            // 
            // contextMenuStripMoreSymbol
            // 
            this.contextMenuStripMoreSymbol.Name = "contextMenuStripMoreSymbol";
            this.contextMenuStripMoreSymbol.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStripMoreSymbol.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStripMoreSymbol_ItemClicked);
            // 
            // SymbolSelectorFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 422);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnMoreSymbols);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.axSymbologyControl);
            this.Name = "SymbolSelectorFrm";
            this.Text = "符号选择器";
            this.Load += new System.EventHandler(this.SymbolSelectorFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox ptbPreview;
        private System.Windows.Forms.Label lblOutlineColor;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAngle;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnOutlineColor;
        private System.Windows.Forms.Button btnMoreSymbols;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMoreSymbol;
    }
}