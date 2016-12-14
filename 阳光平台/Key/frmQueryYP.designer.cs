namespace SHYSInterface
{
    partial class frmQueryYP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryYP));
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtTip = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cboCondition = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboRelation = new System.Windows.Forms.ComboBox();
            this.tlsubCancel = new System.Windows.Forms.ToolStripButton();
            this.tlsubQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlsubClear = new System.Windows.Forms.ToolStripButton();
            this.tlsubOK = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvShow
            // 
            this.dgvShow.AllowUserToAddRows = false;
            this.dgvShow.AllowUserToDeleteRows = false;
            this.dgvShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvShow.Location = new System.Drawing.Point(0, 85);
            this.dgvShow.MultiSelect = false;
            this.dgvShow.Name = "dgvShow";
            this.dgvShow.ReadOnly = true;
            this.dgvShow.RowHeadersWidth = 28;
            this.dgvShow.RowTemplate.Height = 23;
            this.dgvShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShow.Size = new System.Drawing.Size(641, 366);
            this.dgvShow.TabIndex = 11;
            this.dgvShow.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShow_CellDoubleClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // txtTip
            // 
            this.txtTip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtTip.Location = new System.Drawing.Point(0, 434);
            this.txtTip.Name = "txtTip";
            this.txtTip.ReadOnly = true;
            this.txtTip.Size = new System.Drawing.Size(641, 21);
            this.txtTip.TabIndex = 10;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(251, 21);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(215, 21);
            this.txtValue.TabIndex = 2;
            // 
            // cboCondition
            // 
            this.cboCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCondition.FormattingEnabled = true;
            this.cboCondition.Items.AddRange(new object[] {
            "",
            "[1]药品编码",
            "[2]药品名称",
            "[3]统编代码"});
            this.cboCondition.Location = new System.Drawing.Point(11, 21);
            this.cboCondition.Name = "cboCondition";
            this.cboCondition.Size = new System.Drawing.Size(131, 20);
            this.cboCondition.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Controls.Add(this.cboRelation);
            this.groupBox1.Controls.Add(this.cboCondition);
            this.groupBox1.Location = new System.Drawing.Point(0, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 53);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件";
            // 
            // cboRelation
            // 
            this.cboRelation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRelation.FormattingEnabled = true;
            this.cboRelation.Items.AddRange(new object[] {
            "",
            "[1]等于",
            "[2]包含"});
            this.cboRelation.Location = new System.Drawing.Point(154, 21);
            this.cboRelation.Name = "cboRelation";
            this.cboRelation.Size = new System.Drawing.Size(82, 20);
            this.cboRelation.TabIndex = 1;
            // 
            // tlsubCancel
            // 
            this.tlsubCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsubCancel.Image")));
            this.tlsubCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsubCancel.Name = "tlsubCancel";
            this.tlsubCancel.Size = new System.Drawing.Size(49, 22);
            this.tlsubCancel.Text = "取消";
            this.tlsubCancel.Click += new System.EventHandler(this.tlsubCancel_Click);
            // 
            // tlsubQuery
            // 
            this.tlsubQuery.Image = ((System.Drawing.Image)(resources.GetObject("tlsubQuery.Image")));
            this.tlsubQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsubQuery.Name = "tlsubQuery";
            this.tlsubQuery.Size = new System.Drawing.Size(49, 22);
            this.tlsubQuery.Text = "查询";
            this.tlsubQuery.Click += new System.EventHandler(this.tlsubQuery_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsubQuery,
            this.tlsubClear,
            this.toolStripSeparator1,
            this.tlsubOK,
            this.tlsubCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(641, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlsubClear
            // 
            this.tlsubClear.Image = ((System.Drawing.Image)(resources.GetObject("tlsubClear.Image")));
            this.tlsubClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsubClear.Name = "tlsubClear";
            this.tlsubClear.Size = new System.Drawing.Size(97, 22);
            this.tlsubClear.Text = "清空查询条件";
            this.tlsubClear.Click += new System.EventHandler(this.tlsubClear_Click);
            // 
            // tlsubOK
            // 
            this.tlsubOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsubOK.Image")));
            this.tlsubOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsubOK.Name = "tlsubOK";
            this.tlsubOK.Size = new System.Drawing.Size(49, 22);
            this.tlsubOK.Text = "确定";
            this.tlsubOK.Click += new System.EventHandler(this.tlsubOK_Click);
            // 
            // frmQueryYP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(641, 455);
            this.Controls.Add(this.dgvShow);
            this.Controls.Add(this.txtTip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQueryYP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询药品";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txtTip;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cboCondition;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboRelation;
        private System.Windows.Forms.ToolStripButton tlsubCancel;
        private System.Windows.Forms.ToolStripButton tlsubQuery;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlsubClear;
        private System.Windows.Forms.ToolStripButton tlsubOK;


    }
}