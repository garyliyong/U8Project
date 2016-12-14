namespace Key
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnserach = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnedit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnshuaxin = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.客户ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客户MD5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客户名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客户类型ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.邮政编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.电话 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.传真 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.邮件 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.来源 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.删除标识 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtcusname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnserach,
            this.toolStripSeparator2,
            this.btnedit,
            this.toolStripSeparator1,
            this.btnshuaxin});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(327, 25);
            this.toolStrip1.TabIndex = 20;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnserach
            // 
            this.btnserach.Image = ((System.Drawing.Image)(resources.GetObject("btnserach.Image")));
            this.btnserach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnserach.Name = "btnserach";
            this.btnserach.Size = new System.Drawing.Size(49, 22);
            this.btnserach.Text = "查询";
            this.btnserach.Click += new System.EventHandler(this.btnserach_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnedit
            // 
            this.btnedit.Image = ((System.Drawing.Image)(resources.GetObject("btnedit.Image")));
            this.btnedit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(49, 22);
            this.btnedit.Text = "同步";
            this.btnedit.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnshuaxin
            // 
            this.btnshuaxin.Image = ((System.Drawing.Image)(resources.GetObject("btnshuaxin.Image")));
            this.btnshuaxin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnshuaxin.Name = "btnshuaxin";
            this.btnshuaxin.Size = new System.Drawing.Size(49, 22);
            this.btnshuaxin.Text = "刷新";
            this.btnshuaxin.Click += new System.EventHandler(this.btnshuaxin_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.客户ID,
            this.客户MD5,
            this.客户名称,
            this.客户类型ID,
            this.邮政编码,
            this.地址,
            this.电话,
            this.传真,
            this.邮件,
            this.来源,
            this.删除标识});
            this.dataGridView.Location = new System.Drawing.Point(0, 95);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(909, 376);
            this.dataGridView.TabIndex = 21;
            // 
            // 客户ID
            // 
            this.客户ID.HeaderText = "客户ID";
            this.客户ID.Name = "客户ID";
            this.客户ID.ReadOnly = true;
            // 
            // 客户MD5
            // 
            this.客户MD5.HeaderText = "客户MD5";
            this.客户MD5.Name = "客户MD5";
            this.客户MD5.Visible = false;
            // 
            // 客户名称
            // 
            this.客户名称.HeaderText = "客户名称";
            this.客户名称.Name = "客户名称";
            this.客户名称.ReadOnly = true;
            // 
            // 客户类型ID
            // 
            this.客户类型ID.HeaderText = "客户类型ID";
            this.客户类型ID.Name = "客户类型ID";
            this.客户类型ID.ReadOnly = true;
            // 
            // 邮政编码
            // 
            this.邮政编码.HeaderText = "邮政编码";
            this.邮政编码.Name = "邮政编码";
            this.邮政编码.ReadOnly = true;
            // 
            // 地址
            // 
            this.地址.HeaderText = "地址";
            this.地址.Name = "地址";
            this.地址.ReadOnly = true;
            // 
            // 电话
            // 
            this.电话.HeaderText = "电话";
            this.电话.Name = "电话";
            this.电话.ReadOnly = true;
            // 
            // 传真
            // 
            this.传真.HeaderText = "传真";
            this.传真.Name = "传真";
            this.传真.ReadOnly = true;
            // 
            // 邮件
            // 
            this.邮件.HeaderText = "邮件";
            this.邮件.Name = "邮件";
            // 
            // 来源
            // 
            this.来源.HeaderText = "来源";
            this.来源.Name = "来源";
            this.来源.ReadOnly = true;
            // 
            // 删除标识
            // 
            this.删除标识.HeaderText = "删除标识";
            this.删除标识.Name = "删除标识";
            this.删除标识.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "客户名称:";
            // 
            // txtcusname
            // 
            this.txtcusname.Location = new System.Drawing.Point(63, 52);
            this.txtcusname.Name = "txtcusname";
            this.txtcusname.Size = new System.Drawing.Size(156, 21);
            this.txtcusname.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "客户来源:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(309, 53);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(41, 16);
            this.radioButton1.TabIndex = 25;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "CRM";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(356, 53);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 26;
            this.radioButton2.Text = "研究所";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 126);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtcusname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "客户列表";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnedit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnshuaxin;
        private System.Windows.Forms.ToolStripButton btnserach;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcusname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户MD5;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户类型ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 邮政编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 电话;
        private System.Windows.Forms.DataGridViewTextBoxColumn 传真;
        private System.Windows.Forms.DataGridViewTextBoxColumn 邮件;
        private System.Windows.Forms.DataGridViewTextBoxColumn 来源;
        private System.Windows.Forms.DataGridViewTextBoxColumn 删除标识;

    }
}

