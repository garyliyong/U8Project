namespace SHYSInterface
{
    partial class FrmYQ010
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
            this.button4 = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SFWJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JLS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JHDH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDMXBH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YQBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YYBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YYMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSDBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSDZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YPLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZXSPBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YPJX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFGG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BZDWMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YYDWMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BZNHSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCQYMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGJLDW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGJG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DCPSBZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDTJFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDCLZT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDTJRQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BZSM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnexport
            // 
            this.btnexport.Location = new System.Drawing.Point(513, 2);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(89, 23);
            this.btnexport.TabIndex = 168;
            this.btnexport.Text = "导入U8";
            this.btnexport.UseVisualStyleBackColor = true;
            this.btnexport.Click += new System.EventHandler(this.btnexport_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(188, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(119, 23);
            this.button6.TabIndex = 167;
            this.button6.Text = "医院订单查询";
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
            this.check,
            this.SFWJ,
            this.JLS,
            this.JHDH,
            this.DDMXBH,
            this.YQBM,
            this.YYBM,
            this.YYMC,
            this.PSDBM,
            this.PSDZ,
            this.CGLX,
            this.DDLX,
            this.SPLX,
            this.YPLX,
            this.ZXSPBM,
            this.CPM,
            this.YPJX,
            this.CFGG,
            this.BZDWMC,
            this.YYDWMC,
            this.BZNHSL,
            this.SCQYMC,
            this.CGJLDW,
            this.CGJG,
            this.CGSL,
            this.DCPSBZ,
            this.DDTJFS,
            this.DDCLZT,
            this.DDTJRQ,
            this.BZSM});
            this.dgv2.Location = new System.Drawing.Point(1, 29);
            this.dgv2.Name = "dgv2";
            this.dgv2.RowHeadersWidth = 5;
            this.dgv2.RowTemplate.Height = 23;
            this.dgv2.Size = new System.Drawing.Size(939, 497);
            this.dgv2.TabIndex = 166;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(339, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 169;
            this.button4.Text = "全选";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // check
            // 
            this.check.HeaderText = "选中";
            this.check.Name = "check";
            this.check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.check.Width = 40;
            // 
            // SFWJ
            // 
            this.SFWJ.DataPropertyName = "SFWJ";
            this.SFWJ.HeaderText = "是否完结";
            this.SFWJ.Name = "SFWJ";
            this.SFWJ.ReadOnly = true;
            this.SFWJ.Width = 60;
            // 
            // JLS
            // 
            this.JLS.DataPropertyName = "JLS";
            this.JLS.HeaderText = "记录数";
            this.JLS.Name = "JLS";
            this.JLS.ReadOnly = true;
            this.JLS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.JLS.Width = 70;
            // 
            // JHDH
            // 
            this.JHDH.DataPropertyName = "JHDH";
            this.JHDH.HeaderText = "计划单号";
            this.JHDH.Name = "JHDH";
            this.JHDH.ReadOnly = true;
            // 
            // DDMXBH
            // 
            this.DDMXBH.DataPropertyName = "DDMXBH";
            this.DDMXBH.HeaderText = "订单明细编号";
            this.DDMXBH.Name = "DDMXBH";
            this.DDMXBH.ReadOnly = true;
            this.DDMXBH.Width = 110;
            // 
            // YQBM
            // 
            this.YQBM.DataPropertyName = "YQBM";
            this.YQBM.HeaderText = "药企编码";
            this.YQBM.Name = "YQBM";
            this.YQBM.ReadOnly = true;
            this.YQBM.Width = 80;
            // 
            // YYBM
            // 
            this.YYBM.DataPropertyName = "YYBM";
            this.YYBM.HeaderText = "医院编码";
            this.YYBM.Name = "YYBM";
            this.YYBM.ReadOnly = true;
            this.YYBM.Width = 80;
            // 
            // YYMC
            // 
            this.YYMC.DataPropertyName = "YYMC";
            this.YYMC.HeaderText = "医院名称";
            this.YYMC.Name = "YYMC";
            this.YYMC.Width = 120;
            // 
            // PSDBM
            // 
            this.PSDBM.DataPropertyName = "PSDBM";
            this.PSDBM.HeaderText = "配送点编码";
            this.PSDBM.Name = "PSDBM";
            this.PSDBM.ReadOnly = true;
            // 
            // PSDZ
            // 
            this.PSDZ.DataPropertyName = "PSDZ";
            this.PSDZ.HeaderText = "配送地址";
            this.PSDZ.Name = "PSDZ";
            this.PSDZ.ReadOnly = true;
            // 
            // CGLX
            // 
            this.CGLX.DataPropertyName = "CGLX";
            this.CGLX.HeaderText = "采购模式";
            this.CGLX.Name = "CGLX";
            this.CGLX.ReadOnly = true;
            this.CGLX.Width = 80;
            // 
            // DDLX
            // 
            this.DDLX.DataPropertyName = "DDLX";
            this.DDLX.HeaderText = "订单类型";
            this.DDLX.Name = "DDLX";
            this.DDLX.ReadOnly = true;
            // 
            // SPLX
            // 
            this.SPLX.DataPropertyName = "SPLX";
            this.SPLX.HeaderText = "商品类型";
            this.SPLX.Name = "SPLX";
            this.SPLX.ReadOnly = true;
            // 
            // YPLX
            // 
            this.YPLX.DataPropertyName = "YPLX";
            this.YPLX.HeaderText = "药品类型";
            this.YPLX.Name = "YPLX";
            this.YPLX.ReadOnly = true;
            // 
            // ZXSPBM
            // 
            this.ZXSPBM.DataPropertyName = "ZXSPBM";
            this.ZXSPBM.HeaderText = "统编代码";
            this.ZXSPBM.Name = "ZXSPBM";
            this.ZXSPBM.ReadOnly = true;
            // 
            // CPM
            // 
            this.CPM.DataPropertyName = "CPM";
            this.CPM.HeaderText = "药品注册名称";
            this.CPM.Name = "CPM";
            this.CPM.ReadOnly = true;
            // 
            // YPJX
            // 
            this.YPJX.DataPropertyName = "YPJX";
            this.YPJX.HeaderText = "药品剂型";
            this.YPJX.Name = "YPJX";
            this.YPJX.ReadOnly = true;
            // 
            // CFGG
            // 
            this.CFGG.DataPropertyName = "CFGG";
            this.CFGG.HeaderText = "成分规格";
            this.CFGG.Name = "CFGG";
            this.CFGG.ReadOnly = true;
            // 
            // BZDWMC
            // 
            this.BZDWMC.DataPropertyName = "BZDWMC";
            this.BZDWMC.HeaderText = "包装单位";
            this.BZDWMC.Name = "BZDWMC";
            this.BZDWMC.ReadOnly = true;
            // 
            // YYDWMC
            // 
            this.YYDWMC.DataPropertyName = "YYDWMC";
            this.YYDWMC.HeaderText = "最小使用单位";
            this.YYDWMC.Name = "YYDWMC";
            this.YYDWMC.ReadOnly = true;
            // 
            // BZNHSL
            // 
            this.BZNHSL.DataPropertyName = "BZNHSL";
            this.BZNHSL.HeaderText = "零售包装数量";
            this.BZNHSL.Name = "BZNHSL";
            this.BZNHSL.ReadOnly = true;
            // 
            // SCQYMC
            // 
            this.SCQYMC.DataPropertyName = "SCQYMC";
            this.SCQYMC.HeaderText = "生产企业名称";
            this.SCQYMC.Name = "SCQYMC";
            this.SCQYMC.ReadOnly = true;
            // 
            // CGJLDW
            // 
            this.CGJLDW.DataPropertyName = "CGJLDW";
            this.CGJLDW.HeaderText = "采购计量单位";
            this.CGJLDW.Name = "CGJLDW";
            this.CGJLDW.ReadOnly = true;
            // 
            // CGJG
            // 
            this.CGJG.DataPropertyName = "CGJG";
            this.CGJG.HeaderText = "采购价格";
            this.CGJG.Name = "CGJG";
            this.CGJG.ReadOnly = true;
            // 
            // CGSL
            // 
            this.CGSL.DataPropertyName = "CGSL";
            this.CGSL.HeaderText = "采购数量";
            this.CGSL.Name = "CGSL";
            this.CGSL.ReadOnly = true;
            // 
            // DCPSBZ
            // 
            this.DCPSBZ.DataPropertyName = "DCPSBZ";
            this.DCPSBZ.HeaderText = "多次配送标识";
            this.DCPSBZ.Name = "DCPSBZ";
            this.DCPSBZ.ReadOnly = true;
            // 
            // DDTJFS
            // 
            this.DDTJFS.DataPropertyName = "DDTJFS";
            this.DDTJFS.HeaderText = "订单提交方式";
            this.DDTJFS.Name = "DDTJFS";
            this.DDTJFS.ReadOnly = true;
            // 
            // DDCLZT
            // 
            this.DDCLZT.DataPropertyName = "DDCLZT";
            this.DDCLZT.HeaderText = "订单处理状态";
            this.DDCLZT.Name = "DDCLZT";
            this.DDCLZT.ReadOnly = true;
            // 
            // DDTJRQ
            // 
            this.DDTJRQ.DataPropertyName = "DDTJRQ";
            this.DDTJRQ.HeaderText = "订单提交日期";
            this.DDTJRQ.Name = "DDTJRQ";
            this.DDTJRQ.ReadOnly = true;
            // 
            // BZSM
            // 
            this.BZSM.DataPropertyName = "BZSM";
            this.BZSM.HeaderText = "备注说明";
            this.BZSM.Name = "BZSM";
            this.BZSM.ReadOnly = true;
            // 
            // FrmYQ010
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 529);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnexport);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dgv2);
            this.Name = "FrmYQ010";
            this.Text = "订单查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn SFWJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn JLS;
        private System.Windows.Forms.DataGridViewTextBoxColumn JHDH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDMXBH;
        private System.Windows.Forms.DataGridViewTextBoxColumn YQBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn YYBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn YYMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn YPLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZXSPBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn YPJX;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFGG;
        private System.Windows.Forms.DataGridViewTextBoxColumn BZDWMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn YYDWMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BZNHSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCQYMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGJLDW;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGJG;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn DCPSBZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDTJFS;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDCLZT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DDTJRQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn BZSM;
    }
}