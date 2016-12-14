namespace GreatHorseInterface
{
    partial class MainForm
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
            this.dataGridViewBase = new System.Windows.Forms.DataGridView();
            this.VesselCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8Account = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.U8AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxBase = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxVMOS = new System.Windows.Forms.GroupBox();
            this.groupBoxAMOS = new System.Windows.Forms.GroupBox();
            this.btnImportToU8 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBase)).BeginInit();
            this.groupBoxBase.SuspendLayout();
            this.groupBoxVMOS.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewBase
            // 
            this.dataGridViewBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VesselCode,
            this.U8Account,
            this.U8AccountName,
            this.Year});
            this.dataGridViewBase.Location = new System.Drawing.Point(6, 20);
            this.dataGridViewBase.Name = "dataGridViewBase";
            this.dataGridViewBase.RowTemplate.Height = 23;
            this.dataGridViewBase.Size = new System.Drawing.Size(491, 471);
            this.dataGridViewBase.TabIndex = 1;
            this.dataGridViewBase.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewBase_CellValidating);
            this.dataGridViewBase.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBase_CellEndEdit);
            // 
            // VesselCode
            // 
            this.VesselCode.HeaderText = "船舶号";
            this.VesselCode.Name = "VesselCode";
            // 
            // U8Account
            // 
            this.U8Account.HeaderText = "U8帐套号";
            this.U8Account.Name = "U8Account";
            // 
            // U8AccountName
            // 
            this.U8AccountName.HeaderText = "U8帐套名称";
            this.U8AccountName.Name = "U8AccountName";
            this.U8AccountName.ReadOnly = true;
            // 
            // Year
            // 
            this.Year.HeaderText = "年份";
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            // 
            // groupBoxBase
            // 
            this.groupBoxBase.Controls.Add(this.btnSave);
            this.groupBoxBase.Controls.Add(this.dataGridViewBase);
            this.groupBoxBase.Location = new System.Drawing.Point(12, 4);
            this.groupBoxBase.Name = "groupBoxBase";
            this.groupBoxBase.Size = new System.Drawing.Size(503, 557);
            this.groupBoxBase.TabIndex = 2;
            this.groupBoxBase.TabStop = false;
            this.groupBoxBase.Text = "基础档案";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(383, 497);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 54);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBoxVMOS
            // 
            this.groupBoxVMOS.Controls.Add(this.label1);
            this.groupBoxVMOS.Controls.Add(this.btnImportToU8);
            this.groupBoxVMOS.Location = new System.Drawing.Point(538, 12);
            this.groupBoxVMOS.Name = "groupBoxVMOS";
            this.groupBoxVMOS.Size = new System.Drawing.Size(236, 275);
            this.groupBoxVMOS.TabIndex = 3;
            this.groupBoxVMOS.TabStop = false;
            this.groupBoxVMOS.Text = "VMOS";
            // 
            // groupBoxAMOS
            // 
            this.groupBoxAMOS.Location = new System.Drawing.Point(538, 293);
            this.groupBoxAMOS.Name = "groupBoxAMOS";
            this.groupBoxAMOS.Size = new System.Drawing.Size(236, 249);
            this.groupBoxAMOS.TabIndex = 4;
            this.groupBoxAMOS.TabStop = false;
            this.groupBoxAMOS.Text = "AMOS";
            // 
            // btnImportToU8
            // 
            this.btnImportToU8.Location = new System.Drawing.Point(111, 20);
            this.btnImportToU8.Name = "btnImportToU8";
            this.btnImportToU8.Size = new System.Drawing.Size(107, 48);
            this.btnImportToU8.TabIndex = 0;
            this.btnImportToU8.Text = "应付发票导入";
            this.btnImportToU8.UseVisualStyleBackColor = true;
            this.btnImportToU8.Click += new System.EventHandler(this.btnImportToU8_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "VMOS => U8";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.groupBoxAMOS);
            this.Controls.Add(this.groupBoxVMOS);
            this.Controls.Add(this.groupBoxBase);
            this.Name = "MainForm";
            this.Text = "伟马U8二次开发接口";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBase)).EndInit();
            this.groupBoxBase.ResumeLayout(false);
            this.groupBoxVMOS.ResumeLayout(false);
            this.groupBoxVMOS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBase;
        private System.Windows.Forms.GroupBox groupBoxBase;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBoxVMOS;
        private System.Windows.Forms.GroupBox groupBoxAMOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn VesselCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn U8Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.Button btnImportToU8;
        private System.Windows.Forms.Label label1;

    }
}

