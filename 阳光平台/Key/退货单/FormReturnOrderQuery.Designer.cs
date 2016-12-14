namespace SHYSInterface.退货单
{
    partial class FormReturnOrderQuery
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
            this.buttonReturnOrderQuery = new System.Windows.Forms.Button();
            this.dataGridViewReturnOrder = new System.Windows.Forms.DataGridView();
            this.SELECTED = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SFWJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THDBH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YQBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YYBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YYMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSDBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSDZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THDTJRQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THDTJFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THDCLZT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLCGBZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YPLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZXSPBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YPJX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGJLDW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YYDWMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BZNHSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCQYMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCPH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THDJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THZJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.THYY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonReturnOrderQuery
            // 
            this.buttonReturnOrderQuery.Location = new System.Drawing.Point(295, 10);
            this.buttonReturnOrderQuery.Name = "buttonReturnOrderQuery";
            this.buttonReturnOrderQuery.Size = new System.Drawing.Size(108, 31);
            this.buttonReturnOrderQuery.TabIndex = 0;
            this.buttonReturnOrderQuery.Text = "医院退货单查询";
            this.buttonReturnOrderQuery.UseVisualStyleBackColor = true;
            this.buttonReturnOrderQuery.Click += new System.EventHandler(this.buttonReturnOrderQuery_Click);
            // 
            // dataGridViewReturnOrder
            // 
            this.dataGridViewReturnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReturnOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReturnOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SELECTED,
            this.SFWJ,
            this.THDBH,
            this.YQBM,
            this.YYBM,
            this.YYMC,
            this.PSDBM,
            this.PSDZ,
            this.THDTJRQ,
            this.THDTJFS,
            this.THDCLZT,
            this.DLCGBZ,
            this.SPLX,
            this.YPLX,
            this.ZXSPBM,
            this.CPM,
            this.YPJX,
            this.GG,
            this.CGJLDW,
            this.YYDWMC,
            this.BZNHSL,
            this.SCQYMC,
            this.SCPH,
            this.THSL,
            this.THDJ,
            this.THZJ,
            this.THYY});
            this.dataGridViewReturnOrder.Location = new System.Drawing.Point(3, 47);
            this.dataGridViewReturnOrder.Name = "dataGridViewReturnOrder";
            this.dataGridViewReturnOrder.RowTemplate.Height = 23;
            this.dataGridViewReturnOrder.Size = new System.Drawing.Size(1025, 423);
            this.dataGridViewReturnOrder.TabIndex = 1;
            // 
            // SELECTED
            // 
            this.SELECTED.HeaderText = "选中";
            this.SELECTED.Name = "SELECTED";
            this.SELECTED.Width = 60;
            // 
            // SFWJ
            // 
            this.SFWJ.HeaderText = "是否完结";
            this.SFWJ.Name = "SFWJ";
            this.SFWJ.ReadOnly = true;
            this.SFWJ.Width = 60;
            // 
            // THDBH
            // 
            this.THDBH.HeaderText = "退货单编号";
            this.THDBH.Name = "THDBH";
            this.THDBH.ReadOnly = true;
            // 
            // YQBM
            // 
            this.YQBM.HeaderText = "药企编码";
            this.YQBM.Name = "YQBM";
            this.YQBM.ReadOnly = true;
            // 
            // YYBM
            // 
            this.YYBM.HeaderText = "医院编码";
            this.YYBM.Name = "YYBM";
            this.YYBM.ReadOnly = true;
            // 
            // YYMC
            // 
            this.YYMC.HeaderText = "医院名称";
            this.YYMC.Name = "YYMC";
            this.YYMC.ReadOnly = true;
            // 
            // PSDBM
            // 
            this.PSDBM.HeaderText = "配送点编码";
            this.PSDBM.Name = "PSDBM";
            this.PSDBM.ReadOnly = true;
            // 
            // PSDZ
            // 
            this.PSDZ.HeaderText = "配送地址";
            this.PSDZ.Name = "PSDZ";
            this.PSDZ.ReadOnly = true;
            // 
            // THDTJRQ
            // 
            this.THDTJRQ.HeaderText = "退货单提交日期";
            this.THDTJRQ.Name = "THDTJRQ";
            this.THDTJRQ.ReadOnly = true;
            // 
            // THDTJFS
            // 
            this.THDTJFS.HeaderText = "退货单提交方式";
            this.THDTJFS.Name = "THDTJFS";
            this.THDTJFS.ReadOnly = true;
            // 
            // THDCLZT
            // 
            this.THDCLZT.HeaderText = "退货单处理状态";
            this.THDCLZT.Name = "THDCLZT";
            this.THDCLZT.ReadOnly = true;
            // 
            // DLCGBZ
            // 
            this.DLCGBZ.HeaderText = "带量采购标志";
            this.DLCGBZ.Name = "DLCGBZ";
            this.DLCGBZ.ReadOnly = true;
            // 
            // SPLX
            // 
            this.SPLX.HeaderText = "商品类型";
            this.SPLX.Name = "SPLX";
            this.SPLX.ReadOnly = true;
            // 
            // YPLX
            // 
            this.YPLX.HeaderText = "药品类型";
            this.YPLX.Name = "YPLX";
            this.YPLX.ReadOnly = true;
            // 
            // ZXSPBM
            // 
            this.ZXSPBM.HeaderText = "统编代码";
            this.ZXSPBM.Name = "ZXSPBM";
            this.ZXSPBM.ReadOnly = true;
            // 
            // CPM
            // 
            this.CPM.HeaderText = "药品注册名称";
            this.CPM.Name = "CPM";
            this.CPM.ReadOnly = true;
            // 
            // YPJX
            // 
            this.YPJX.HeaderText = "药品剂型";
            this.YPJX.Name = "YPJX";
            this.YPJX.ReadOnly = true;
            // 
            // GG
            // 
            this.GG.HeaderText = "规格";
            this.GG.Name = "GG";
            this.GG.ReadOnly = true;
            // 
            // CGJLDW
            // 
            this.CGJLDW.HeaderText = "采购计量单位";
            this.CGJLDW.Name = "CGJLDW";
            this.CGJLDW.ReadOnly = true;
            // 
            // YYDWMC
            // 
            this.YYDWMC.HeaderText = "用药单位名称";
            this.YYDWMC.Name = "YYDWMC";
            this.YYDWMC.ReadOnly = true;
            // 
            // BZNHSL
            // 
            this.BZNHSL.HeaderText = "零售包装数量";
            this.BZNHSL.Name = "BZNHSL";
            this.BZNHSL.ReadOnly = true;
            // 
            // SCQYMC
            // 
            this.SCQYMC.HeaderText = "生产企业名称";
            this.SCQYMC.Name = "SCQYMC";
            this.SCQYMC.ReadOnly = true;
            // 
            // SCPH
            // 
            this.SCPH.HeaderText = "生产批号";
            this.SCPH.Name = "SCPH";
            this.SCPH.ReadOnly = true;
            // 
            // THSL
            // 
            this.THSL.HeaderText = "退货数量";
            this.THSL.Name = "THSL";
            this.THSL.ReadOnly = true;
            // 
            // THDJ
            // 
            this.THDJ.HeaderText = "退货单价";
            this.THDJ.Name = "THDJ";
            this.THDJ.ReadOnly = true;
            // 
            // THZJ
            // 
            this.THZJ.HeaderText = "退货总价";
            this.THZJ.Name = "THZJ";
            this.THZJ.ReadOnly = true;
            // 
            // THYY
            // 
            this.THYY.HeaderText = "退货原因";
            this.THYY.Name = "THYY";
            this.THYY.ReadOnly = true;
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(478, 10);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(114, 31);
            this.buttonSelectAll.TabIndex = 2;
            this.buttonSelectAll.Text = "全选";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(679, 10);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(114, 31);
            this.buttonImport.TabIndex = 3;
            this.buttonImport.Text = "导入到U8";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // FormReturnOrderQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 467);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonSelectAll);
            this.Controls.Add(this.dataGridViewReturnOrder);
            this.Controls.Add(this.buttonReturnOrderQuery);
            this.Name = "FormReturnOrderQuery";
            this.Text = "退货单查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonReturnOrderQuery;
        private System.Windows.Forms.DataGridView dataGridViewReturnOrder;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SELECTED;
        private System.Windows.Forms.DataGridViewTextBoxColumn SFWJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn THDBH;
        private System.Windows.Forms.DataGridViewTextBoxColumn YQBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn YYBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn YYMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSDZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn THDTJRQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn THDTJFS;
        private System.Windows.Forms.DataGridViewTextBoxColumn THDCLZT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLCGBZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn YPLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZXSPBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPM;
        private System.Windows.Forms.DataGridViewTextBoxColumn YPJX;
        private System.Windows.Forms.DataGridViewTextBoxColumn GG;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGJLDW;
        private System.Windows.Forms.DataGridViewTextBoxColumn YYDWMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BZNHSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCQYMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCPH;
        private System.Windows.Forms.DataGridViewTextBoxColumn THSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn THDJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn THZJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn THYY;
    }
}