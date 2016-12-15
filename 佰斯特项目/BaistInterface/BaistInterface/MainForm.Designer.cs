namespace BaistInterface
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
            this.dataGridViewAccount = new System.Windows.Forms.DataGridView();
            this.VesselCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImportAccvouch = new System.Windows.Forms.Button();
            this.dataGridViewCode = new System.Windows.Forms.DataGridView();
            this.ERPCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8CodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlArchive = new System.Windows.Forms.TabControl();
            this.tabPageAcount = new System.Windows.Forms.TabPage();
            this.btnSaveAccount = new System.Windows.Forms.Button();
            this.tabPageCode = new System.Windows.Forms.TabPage();
            this.btnSaveCode = new System.Windows.Forms.Button();
            this.tabPageVendor = new System.Windows.Forms.TabPage();
            this.btnSaveVendor = new System.Windows.Forms.Button();
            this.dataGridViewVendor = new System.Windows.Forms.DataGridView();
            this.ERPVendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8Vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8VendorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageCustomer = new System.Windows.Forms.TabPage();
            this.btnImportCustom = new System.Windows.Forms.Button();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.dataGridViewCustomer = new System.Windows.Forms.DataGridView();
            this.ERPCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageDepartment = new System.Windows.Forms.TabPage();
            this.btnDepartment = new System.Windows.Forms.Button();
            this.dataGridViewDepartment = new System.Windows.Forms.DataGridView();
            this.ERPDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U8DepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCode)).BeginInit();
            this.tabControlArchive.SuspendLayout();
            this.tabPageAcount.SuspendLayout();
            this.tabPageCode.SuspendLayout();
            this.tabPageVendor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVendor)).BeginInit();
            this.tabPageCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomer)).BeginInit();
            this.tabPageDepartment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDepartment)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAccount
            // 
            this.dataGridViewAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VesselCode,
            this.U8Account,
            this.U8AccountName,
            this.Year});
            this.dataGridViewAccount.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewAccount.Name = "dataGridViewAccount";
            this.dataGridViewAccount.RowTemplate.Height = 23;
            this.dataGridViewAccount.Size = new System.Drawing.Size(551, 308);
            this.dataGridViewAccount.TabIndex = 0;
            this.dataGridViewAccount.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewAccount_CellValidating);
            // 
            // VesselCode
            // 
            this.VesselCode.HeaderText = "ERP帐套号";
            this.VesselCode.Name = "VesselCode";
            // 
            // U8Account
            // 
            this.U8Account.HeaderText = "U8帐套号";
            this.U8Account.Name = "U8Account";
            this.U8Account.ReadOnly = true;
            this.U8Account.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            // btnImportAccvouch
            // 
            this.btnImportAccvouch.Location = new System.Drawing.Point(486, 425);
            this.btnImportAccvouch.Name = "btnImportAccvouch";
            this.btnImportAccvouch.Size = new System.Drawing.Size(87, 29);
            this.btnImportAccvouch.TabIndex = 1;
            this.btnImportAccvouch.Text = "凭证导入";
            this.btnImportAccvouch.UseVisualStyleBackColor = true;
            this.btnImportAccvouch.Click += new System.EventHandler(this.btnImportAccvouch_Click);
            // 
            // dataGridViewCode
            // 
            this.dataGridViewCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ERPCode,
            this.U8Code,
            this.U8CodeName});
            this.dataGridViewCode.Location = new System.Drawing.Point(3, 6);
            this.dataGridViewCode.Name = "dataGridViewCode";
            this.dataGridViewCode.RowTemplate.Height = 23;
            this.dataGridViewCode.Size = new System.Drawing.Size(554, 311);
            this.dataGridViewCode.TabIndex = 0;
            this.dataGridViewCode.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewCode_CellValidating);
            // 
            // ERPCode
            // 
            this.ERPCode.HeaderText = "ERP会计科目编码";
            this.ERPCode.Name = "ERPCode";
            this.ERPCode.Width = 120;
            // 
            // U8Code
            // 
            this.U8Code.HeaderText = "U8会计科目编码";
            this.U8Code.Name = "U8Code";
            this.U8Code.ReadOnly = true;
            this.U8Code.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.U8Code.Width = 120;
            // 
            // U8CodeName
            // 
            this.U8CodeName.HeaderText = "U8会计科目名称";
            this.U8CodeName.Name = "U8CodeName";
            this.U8CodeName.ReadOnly = true;
            this.U8CodeName.Width = 120;
            // 
            // tabControlArchive
            // 
            this.tabControlArchive.Controls.Add(this.tabPageAcount);
            this.tabControlArchive.Controls.Add(this.tabPageCode);
            this.tabControlArchive.Controls.Add(this.tabPageVendor);
            this.tabControlArchive.Controls.Add(this.tabPageCustomer);
            this.tabControlArchive.Controls.Add(this.tabPageDepartment);
            this.tabControlArchive.Location = new System.Drawing.Point(12, 12);
            this.tabControlArchive.Name = "tabControlArchive";
            this.tabControlArchive.SelectedIndex = 0;
            this.tabControlArchive.Size = new System.Drawing.Size(571, 394);
            this.tabControlArchive.TabIndex = 3;
            // 
            // tabPageAcount
            // 
            this.tabPageAcount.Controls.Add(this.btnSaveAccount);
            this.tabPageAcount.Controls.Add(this.dataGridViewAccount);
            this.tabPageAcount.Location = new System.Drawing.Point(4, 22);
            this.tabPageAcount.Name = "tabPageAcount";
            this.tabPageAcount.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAcount.Size = new System.Drawing.Size(563, 368);
            this.tabPageAcount.TabIndex = 0;
            this.tabPageAcount.Text = "帐套档案";
            this.tabPageAcount.UseVisualStyleBackColor = true;
            // 
            // btnSaveAccount
            // 
            this.btnSaveAccount.Location = new System.Drawing.Point(480, 329);
            this.btnSaveAccount.Name = "btnSaveAccount";
            this.btnSaveAccount.Size = new System.Drawing.Size(77, 33);
            this.btnSaveAccount.TabIndex = 1;
            this.btnSaveAccount.Text = "保存";
            this.btnSaveAccount.UseVisualStyleBackColor = true;
            this.btnSaveAccount.Click += new System.EventHandler(this.btnSaveAccount_Click);
            // 
            // tabPageCode
            // 
            this.tabPageCode.Controls.Add(this.btnSaveCode);
            this.tabPageCode.Controls.Add(this.dataGridViewCode);
            this.tabPageCode.Location = new System.Drawing.Point(4, 22);
            this.tabPageCode.Name = "tabPageCode";
            this.tabPageCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCode.Size = new System.Drawing.Size(563, 368);
            this.tabPageCode.TabIndex = 1;
            this.tabPageCode.Text = "会计科目档案";
            this.tabPageCode.UseVisualStyleBackColor = true;
            // 
            // btnSaveCode
            // 
            this.btnSaveCode.Location = new System.Drawing.Point(480, 323);
            this.btnSaveCode.Name = "btnSaveCode";
            this.btnSaveCode.Size = new System.Drawing.Size(77, 39);
            this.btnSaveCode.TabIndex = 1;
            this.btnSaveCode.Text = "保存";
            this.btnSaveCode.UseVisualStyleBackColor = true;
            this.btnSaveCode.Click += new System.EventHandler(this.btnSaveCode_Click);
            // 
            // tabPageVendor
            // 
            this.tabPageVendor.Controls.Add(this.btnSaveVendor);
            this.tabPageVendor.Controls.Add(this.dataGridViewVendor);
            this.tabPageVendor.Location = new System.Drawing.Point(4, 22);
            this.tabPageVendor.Name = "tabPageVendor";
            this.tabPageVendor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVendor.Size = new System.Drawing.Size(563, 368);
            this.tabPageVendor.TabIndex = 2;
            this.tabPageVendor.Text = "供应商档案";
            this.tabPageVendor.UseVisualStyleBackColor = true;
            // 
            // btnSaveVendor
            // 
            this.btnSaveVendor.Location = new System.Drawing.Point(480, 331);
            this.btnSaveVendor.Name = "btnSaveVendor";
            this.btnSaveVendor.Size = new System.Drawing.Size(77, 31);
            this.btnSaveVendor.TabIndex = 1;
            this.btnSaveVendor.Text = "保存";
            this.btnSaveVendor.UseVisualStyleBackColor = true;
            this.btnSaveVendor.Click += new System.EventHandler(this.btnSaveVendor_Click);
            // 
            // dataGridViewVendor
            // 
            this.dataGridViewVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVendor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ERPVendor,
            this.U8Vendor,
            this.U8VendorName});
            this.dataGridViewVendor.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewVendor.Name = "dataGridViewVendor";
            this.dataGridViewVendor.RowTemplate.Height = 23;
            this.dataGridViewVendor.Size = new System.Drawing.Size(551, 314);
            this.dataGridViewVendor.TabIndex = 0;
            this.dataGridViewVendor.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewVendor_CellValidating);
            // 
            // ERPVendor
            // 
            this.ERPVendor.HeaderText = "ERP供应商编码";
            this.ERPVendor.Name = "ERPVendor";
            this.ERPVendor.Width = 120;
            // 
            // U8Vendor
            // 
            this.U8Vendor.HeaderText = "U8供应商编码";
            this.U8Vendor.Name = "U8Vendor";
            this.U8Vendor.ReadOnly = true;
            this.U8Vendor.Width = 120;
            // 
            // U8VendorName
            // 
            this.U8VendorName.HeaderText = "U8供应商名称";
            this.U8VendorName.Name = "U8VendorName";
            this.U8VendorName.ReadOnly = true;
            this.U8VendorName.Width = 200;
            // 
            // tabPageCustomer
            // 
            this.tabPageCustomer.Controls.Add(this.btnImportCustom);
            this.tabPageCustomer.Controls.Add(this.btnSaveCustomer);
            this.tabPageCustomer.Controls.Add(this.dataGridViewCustomer);
            this.tabPageCustomer.Location = new System.Drawing.Point(4, 22);
            this.tabPageCustomer.Name = "tabPageCustomer";
            this.tabPageCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCustomer.Size = new System.Drawing.Size(563, 368);
            this.tabPageCustomer.TabIndex = 3;
            this.tabPageCustomer.Text = "客户档案";
            this.tabPageCustomer.UseVisualStyleBackColor = true;
            // 
            // btnImportCustom
            // 
            this.btnImportCustom.Location = new System.Drawing.Point(365, 330);
            this.btnImportCustom.Name = "btnImportCustom";
            this.btnImportCustom.Size = new System.Drawing.Size(83, 30);
            this.btnImportCustom.TabIndex = 2;
            this.btnImportCustom.Text = "导入";
            this.btnImportCustom.UseVisualStyleBackColor = true;
            this.btnImportCustom.Click += new System.EventHandler(this.btnImportCustom_Click);
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.Location = new System.Drawing.Point(480, 330);
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.Size = new System.Drawing.Size(77, 32);
            this.btnSaveCustomer.TabIndex = 1;
            this.btnSaveCustomer.Text = "保存";
            this.btnSaveCustomer.UseVisualStyleBackColor = true;
            this.btnSaveCustomer.Click += new System.EventHandler(this.btnSaveCustomer_Click);
            // 
            // dataGridViewCustomer
            // 
            this.dataGridViewCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ERPCustomer,
            this.U8Customer,
            this.U8CustomerName});
            this.dataGridViewCustomer.Location = new System.Drawing.Point(7, 3);
            this.dataGridViewCustomer.Name = "dataGridViewCustomer";
            this.dataGridViewCustomer.RowTemplate.Height = 23;
            this.dataGridViewCustomer.Size = new System.Drawing.Size(550, 315);
            this.dataGridViewCustomer.TabIndex = 0;
            this.dataGridViewCustomer.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewCustomer_CellValidating);
            // 
            // ERPCustomer
            // 
            this.ERPCustomer.HeaderText = "ERP客户编码";
            this.ERPCustomer.Name = "ERPCustomer";
            this.ERPCustomer.Width = 200;
            // 
            // U8Customer
            // 
            this.U8Customer.HeaderText = "U8客户编码";
            this.U8Customer.Name = "U8Customer";
            this.U8Customer.ReadOnly = true;
            // 
            // U8CustomerName
            // 
            this.U8CustomerName.HeaderText = "U8客户名称";
            this.U8CustomerName.Name = "U8CustomerName";
            this.U8CustomerName.ReadOnly = true;
            this.U8CustomerName.Width = 200;
            // 
            // tabPageDepartment
            // 
            this.tabPageDepartment.Controls.Add(this.btnDepartment);
            this.tabPageDepartment.Controls.Add(this.dataGridViewDepartment);
            this.tabPageDepartment.Location = new System.Drawing.Point(4, 22);
            this.tabPageDepartment.Name = "tabPageDepartment";
            this.tabPageDepartment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDepartment.Size = new System.Drawing.Size(563, 368);
            this.tabPageDepartment.TabIndex = 4;
            this.tabPageDepartment.Text = "部门档案";
            this.tabPageDepartment.UseVisualStyleBackColor = true;
            // 
            // btnDepartment
            // 
            this.btnDepartment.Location = new System.Drawing.Point(479, 322);
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Size = new System.Drawing.Size(78, 40);
            this.btnDepartment.TabIndex = 1;
            this.btnDepartment.Text = "保存";
            this.btnDepartment.UseVisualStyleBackColor = true;
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // dataGridViewDepartment
            // 
            this.dataGridViewDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ERPDepartment,
            this.U8Department,
            this.U8DepartmentName});
            this.dataGridViewDepartment.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewDepartment.Name = "dataGridViewDepartment";
            this.dataGridViewDepartment.RowTemplate.Height = 23;
            this.dataGridViewDepartment.Size = new System.Drawing.Size(554, 313);
            this.dataGridViewDepartment.TabIndex = 0;
            this.dataGridViewDepartment.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewDepartment_CellValidating);
            // 
            // ERPDepartment
            // 
            this.ERPDepartment.HeaderText = "ERP部门编码";
            this.ERPDepartment.Name = "ERPDepartment";
            // 
            // U8Department
            // 
            this.U8Department.HeaderText = "U8部门编码";
            this.U8Department.Name = "U8Department";
            this.U8Department.ReadOnly = true;
            // 
            // U8DepartmentName
            // 
            this.U8DepartmentName.HeaderText = "U8部门名称";
            this.U8DepartmentName.Name = "U8DepartmentName";
            this.U8DepartmentName.ReadOnly = true;
            this.U8DepartmentName.Width = 200;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 466);
            this.Controls.Add(this.tabControlArchive);
            this.Controls.Add(this.btnImportAccvouch);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "佰斯特凭证接口";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCode)).EndInit();
            this.tabControlArchive.ResumeLayout(false);
            this.tabPageAcount.ResumeLayout(false);
            this.tabPageCode.ResumeLayout(false);
            this.tabPageVendor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVendor)).EndInit();
            this.tabPageCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomer)).EndInit();
            this.tabPageDepartment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDepartment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportAccvouch;
        private System.Windows.Forms.DataGridView dataGridViewCode;
        private System.Windows.Forms.DataGridView dataGridViewAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERPCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8CodeName;
        private System.Windows.Forms.TabControl tabControlArchive;
        private System.Windows.Forms.TabPage tabPageAcount;
        private System.Windows.Forms.Button btnSaveAccount;
        private System.Windows.Forms.TabPage tabPageCode;
        private System.Windows.Forms.Button btnSaveCode;
        private System.Windows.Forms.TabPage tabPageVendor;
        private System.Windows.Forms.DataGridView dataGridViewVendor;
        private System.Windows.Forms.TabPage tabPageCustomer;
        private System.Windows.Forms.TabPage tabPageDepartment;
        private System.Windows.Forms.DataGridView dataGridViewCustomer;
        private System.Windows.Forms.DataGridView dataGridViewDepartment;
        private System.Windows.Forms.Button btnSaveVendor;
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.Button btnDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERPVendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8Vendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8VendorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERPDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8DepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERPCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8CustomerName;
        private System.Windows.Forms.Button btnImportCustom;
        private System.Windows.Forms.DataGridViewTextBoxColumn VesselCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn U8AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;

    }
}

