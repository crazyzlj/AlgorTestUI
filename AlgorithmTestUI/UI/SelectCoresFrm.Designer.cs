namespace AlgorithmTest
{
    partial class SelectCoresFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCoresFrm));
            this.checkedListBoxCores = new System.Windows.Forms.CheckedListBox();
            this.buttonSelectCores = new System.Windows.Forms.Button();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBoxCores
            // 
            this.checkedListBoxCores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBoxCores.CheckOnClick = true;
            this.checkedListBoxCores.ColumnWidth = 50;
            this.checkedListBoxCores.FormattingEnabled = true;
            this.checkedListBoxCores.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32"});
            this.checkedListBoxCores.Location = new System.Drawing.Point(42, 19);
            this.checkedListBoxCores.MultiColumn = true;
            this.checkedListBoxCores.Name = "checkedListBoxCores";
            this.checkedListBoxCores.Size = new System.Drawing.Size(168, 34);
            this.checkedListBoxCores.TabIndex = 7;
            this.checkedListBoxCores.ThreeDCheckBoxes = true;
            this.checkedListBoxCores.UseTabStops = false;
            // 
            // buttonSelectCores
            // 
            this.buttonSelectCores.Location = new System.Drawing.Point(135, 68);
            this.buttonSelectCores.Name = "buttonSelectCores";
            this.buttonSelectCores.Size = new System.Drawing.Size(75, 30);
            this.buttonSelectCores.TabIndex = 8;
            this.buttonSelectCores.Text = "确定";
            this.buttonSelectCores.UseVisualStyleBackColor = true;
            this.buttonSelectCores.Click += new System.EventHandler(this.buttonSelectCores_Click);
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(42, 68);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(75, 30);
            this.buttonSelectAll.TabIndex = 9;
            this.buttonSelectAll.Text = "全选";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // SelectCoresFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 107);
            this.Controls.Add(this.buttonSelectAll);
            this.Controls.Add(this.buttonSelectCores);
            this.Controls.Add(this.checkedListBoxCores);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectCoresFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectCoresFrm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.CheckedListBox checkedListBoxCores;
        private System.Windows.Forms.Button buttonSelectCores;
        private System.Windows.Forms.Button buttonSelectAll;
    }
}