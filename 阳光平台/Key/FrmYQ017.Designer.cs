namespace SHYSInterface
{
    partial class FrmYQ017
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
            this.btnexport = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCus = new System.Windows.Forms.Button();
            this.SFWJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JLS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSDBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSDMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSDZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LXRXM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LXDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YZBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BZXX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnexport
            // 
            this.btnexport.Location = new System.Drawing.Point(444, 3);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(89, 23);
            this.btnexport.TabIndex = 168;
            this.btnexport.Text = "输出";
            this.btnexport.UseVisualStyleBackColor = true;
            this.btnexport.Click += new System.EventHandler(this.btnexport_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(334, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(72, 23);
            this.button6.TabIndex = 167;
            this.button6.Text = "查询";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dgv2
            // 
            this.dgv2.AllowUserToAddRows = false;
            this.dgv2.AllowUserToDeleteRows = false;
            this.dgv2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SFWJ,
            this.JLS,
            this.PSDBM,
            this.PSDMC,
            this.PSDZ,
            this.LXRXM,
            this.LXDH,
            this.YZBM,
            this.BZXX});
            this.dgv2.Location = new System.Drawing.Point(1, 29);
            this.dgv2.Name = "dgv2";
            this.dgv2.ReadOnly = true;
            this.dgv2.RowHeadersWidth = 5;
            this.dgv2.RowTemplate.Height = 23;
            this.dgv2.Size = new System.Drawing.Size(939, 497);
            this.dgv2.TabIndex = 166;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 169;
            this.label1.Text = "医院编码";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(86, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(129, 21);
            this.textBox1.TabIndex = 170;
            // 
            // btnCus
            // 
            this.btnCus.Location = new System.Drawing.Point(221, 3);
            this.btnCus.Name = "btnCus";
            this.btnCus.Size = new System.Drawing.Size(37, 23);
            this.btnCus.TabIndex = 508;
            this.btnCus.Text = "...";
            this.btnCus.UseVisualStyleBackColor = true;
            this.btnCus.Click += new System.EventHandler(this.btnCus_Click);
            // 
            // SFWJ
            // 
            this.SFWJ.DataPropertyName = "SFWJ";
            this.SFWJ.HeaderText = "是否完结";
            this.SFWJ.Name = "SFWJ";
            this.SFWJ.ReadOnly = true;
            // 
            // JLS
            // 
            this.JLS.DataPropertyName = "JLS";
            this.JLS.HeaderText = "记录数";
            this.JLS.Name = "JLS";
            this.JLS.ReadOnly = true;
            this.JLS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PSDBM
            // 
            this.PSDBM.DataPropertyName = "PSDBM";
            this.PSDBM.HeaderText = "配送点编码";
            this.PSDBM.Name = "PSDBM";
            this.PSDBM.ReadOnly = true;
            // 
            // PSDMC
            // 
            this.PSDMC.DataPropertyName = "PSDMC";
            this.PSDMC.HeaderText = "配送点名称";
            this.PSDMC.Name = "PSDMC";
            this.PSDMC.ReadOnly = true;
            // 
            // PSDZ
            // 
            this.PSDZ.DataPropertyName = "PSDZ";
            this.PSDZ.HeaderText = "配送地址";
            this.PSDZ.Name = "PSDZ";
            this.PSDZ.ReadOnly = true;
            // 
            // LXRXM
            // 
            this.LXRXM.DataPropertyName = "LXRXM";
            this.LXRXM.HeaderText = "联系人姓名";
            this.LXRXM.Name = "LXRXM";
            this.LXRXM.ReadOnly = true;
            // 
            // LXDH
            // 
            this.LXDH.DataPropertyName = "LXDH";
            this.LXDH.HeaderText = "联系电话";
            this.LXDH.Name = "LXDH";
            this.LXDH.ReadOnly = true;
            // 
            // YZBM
            // 
            this.YZBM.DataPropertyName = "YZBM";
            this.YZBM.HeaderText = "邮政编码";
            this.YZBM.Name = "YZBM";
            this.YZBM.ReadOnly = true;
            // 
            // BZXX
            // 
            this.BZXX.DataPropertyName = "BZXX";
            this.BZXX.HeaderText = "备注说明";
            this.BZXX.Name = "BZXX";
            this.BZXX.ReadOnly = true;
            // 
            // FrmYQ017
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 529);
            this.Controls.Add(this.btnCus);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnexport);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dgv2);
            this.Name = "FrmYQ017";
            this.Text = "订单查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnCus;
        private System.Windows.Forms.DataGridViewTextBoxColumn SFWJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn JLS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn LXRXM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LXDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn YZBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BZXX;
    }
}