namespace SHYSInterface
{
    partial class FrmYQ002
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
            this.resultXMl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cinvcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cInvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZXSPBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KCSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KCDW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnexport
            // 
            this.btnexport.Location = new System.Drawing.Point(506, 2);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(72, 23);
            this.btnexport.TabIndex = 168;
            this.btnexport.Text = "库存上传";
            this.btnexport.UseVisualStyleBackColor = true;
            this.btnexport.Click += new System.EventHandler(this.btnexport_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(188, 2);
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
            this.resultXMl,
            this.cinvcode,
            this.cInvName,
            this.SPLX,
            this.ZXSPBM,
            this.KCSL,
            this.KCDW});
            this.dgv2.Location = new System.Drawing.Point(1, 29);
            this.dgv2.Name = "dgv2";
            this.dgv2.ReadOnly = true;
            this.dgv2.RowHeadersWidth = 5;
            this.dgv2.RowTemplate.Height = 23;
            this.dgv2.Size = new System.Drawing.Size(939, 497);
            this.dgv2.TabIndex = 166;
            // 
            // resultXMl
            // 
            this.resultXMl.HeaderText = "resultXMl";
            this.resultXMl.Name = "resultXMl";
            this.resultXMl.ReadOnly = true;
            // 
            // cinvcode
            // 
            this.cinvcode.DataPropertyName = "cinvcode";
            this.cinvcode.HeaderText = "存货编码";
            this.cinvcode.Name = "cinvcode";
            this.cinvcode.ReadOnly = true;
            this.cinvcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cInvName
            // 
            this.cInvName.DataPropertyName = "cInvName";
            this.cInvName.HeaderText = "存货名称";
            this.cInvName.Name = "cInvName";
            this.cInvName.ReadOnly = true;
            // 
            // SPLX
            // 
            this.SPLX.DataPropertyName = "SPLX";
            this.SPLX.HeaderText = "商品类型";
            this.SPLX.Name = "SPLX";
            this.SPLX.ReadOnly = true;
            // 
            // ZXSPBM
            // 
            this.ZXSPBM.DataPropertyName = "ZXSPBM";
            this.ZXSPBM.HeaderText = "统编代码";
            this.ZXSPBM.Name = "ZXSPBM";
            this.ZXSPBM.ReadOnly = true;
            // 
            // KCSL
            // 
            this.KCSL.DataPropertyName = "KCSL";
            this.KCSL.HeaderText = "库存数量";
            this.KCSL.Name = "KCSL";
            this.KCSL.ReadOnly = true;
            // 
            // KCDW
            // 
            this.KCDW.DataPropertyName = "KCDW";
            this.KCDW.HeaderText = "库存单位";
            this.KCDW.Name = "KCDW";
            this.KCDW.ReadOnly = true;
            // 
            // FrmYQ002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 529);
            this.Controls.Add(this.btnexport);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dgv2);
            this.Name = "FrmYQ002";
            this.Text = "药品库存上传";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultXMl;
        private System.Windows.Forms.DataGridViewTextBoxColumn cinvcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPLX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZXSPBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn KCSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn KCDW;
    }
}