namespace SHYSInterface
{
    partial class FrmYQ001
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
            this.cCurrencyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEnglishName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cInvStd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cidefine1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEnterprise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cidefine8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cidefine2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cidefine3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cComUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cidefine4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnexport
            // 
            this.btnexport.Location = new System.Drawing.Point(506, 2);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(72, 23);
            this.btnexport.TabIndex = 168;
            this.btnexport.Text = "上传";
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
            this.cCurrencyName,
            this.cEnglishName,
            this.cInvStd,
            this.cidefine1,
            this.cEnterprise,
            this.cidefine8,
            this.cidefine2,
            this.cFile,
            this.cidefine3,
            this.cComUnitName,
            this.cidefine4});
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
            // cCurrencyName
            // 
            this.cCurrencyName.DataPropertyName = "cCurrencyName";
            this.cCurrencyName.HeaderText = "通用名称";
            this.cCurrencyName.Name = "cCurrencyName";
            this.cCurrencyName.ReadOnly = true;
            // 
            // cEnglishName
            // 
            this.cEnglishName.DataPropertyName = "cEnglishName";
            this.cEnglishName.HeaderText = "英文名";
            this.cEnglishName.Name = "cEnglishName";
            this.cEnglishName.ReadOnly = true;
            // 
            // cInvStd
            // 
            this.cInvStd.DataPropertyName = "cInvStd";
            this.cInvStd.HeaderText = "规格型号";
            this.cInvStd.Name = "cInvStd";
            this.cInvStd.ReadOnly = true;
            // 
            // cidefine1
            // 
            this.cidefine1.DataPropertyName = "cidefine1";
            this.cidefine1.HeaderText = "统编代码";
            this.cidefine1.Name = "cidefine1";
            this.cidefine1.ReadOnly = true;
            // 
            // cEnterprise
            // 
            this.cEnterprise.DataPropertyName = "cEnterprise";
            this.cEnterprise.HeaderText = "生产企业";
            this.cEnterprise.Name = "cEnterprise";
            this.cEnterprise.ReadOnly = true;
            // 
            // cidefine8
            // 
            this.cidefine8.DataPropertyName = "cidefine8";
            this.cidefine8.HeaderText = "药品商品条形码";
            this.cidefine8.Name = "cidefine8";
            this.cidefine8.ReadOnly = true;
            this.cidefine8.Width = 120;
            // 
            // cidefine2
            // 
            this.cidefine2.DataPropertyName = "cidefine2";
            this.cidefine2.HeaderText = "药品本位码";
            this.cidefine2.Name = "cidefine2";
            this.cidefine2.ReadOnly = true;
            // 
            // cFile
            // 
            this.cFile.DataPropertyName = "cFile";
            this.cFile.HeaderText = "批准文号";
            this.cFile.Name = "cFile";
            this.cFile.ReadOnly = true;
            // 
            // cidefine3
            // 
            this.cidefine3.DataPropertyName = "cidefine3";
            this.cidefine3.HeaderText = "包装材质";
            this.cidefine3.Name = "cidefine3";
            this.cidefine3.ReadOnly = true;
            // 
            // cComUnitName
            // 
            this.cComUnitName.DataPropertyName = "cComUnitName";
            this.cComUnitName.HeaderText = "包装单位";
            this.cComUnitName.Name = "cComUnitName";
            this.cComUnitName.ReadOnly = true;
            // 
            // cidefine4
            // 
            this.cidefine4.DataPropertyName = "cidefine4";
            this.cidefine4.HeaderText = "包装方式";
            this.cidefine4.Name = "cidefine4";
            this.cidefine4.ReadOnly = true;
            // 
            // FrmYQ001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 529);
            this.Controls.Add(this.btnexport);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dgv2);
            this.Name = "FrmYQ001";
            this.Text = "药品上传";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn cCurrencyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEnglishName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInvStd;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidefine1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEnterprise;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidefine8;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidefine2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidefine3;
        private System.Windows.Forms.DataGridViewTextBoxColumn cComUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidefine4;
    }
}