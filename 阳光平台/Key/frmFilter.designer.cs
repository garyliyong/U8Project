using System;
using System.Windows.Forms;

namespace SHYSInterface
{
    partial class frmFilter
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
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpDJRQ2 = new System.Windows.Forms.DateTimePicker();
            this.dtpDJRQ1 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "订单提交方式";
            this.label4.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.comboBox7);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.comboBox6);
            this.panel1.Controls.Add(this.comboBox5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comboBox4);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.dtpDJRQ2);
            this.panel1.Controls.Add(this.dtpDJRQ1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(12, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 330);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 286);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(318, 21);
            this.textBox1.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 41;
            this.label8.Text = "订单明细编号";
            // 
            // comboBox7
            // 
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(119, 256);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(319, 20);
            this.comboBox7.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 264);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "订单处理状态";
            this.label7.Visible = false;
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "医院填报",
            "药企代填"});
            this.comboBox6.Location = new System.Drawing.Point(119, 225);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(319, 20);
            this.comboBox6.TabIndex = 38;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "药品类",
            "医用耗材器械类",
            "其他"});
            this.comboBox5.Location = new System.Drawing.Point(103, 197);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(335, 20);
            this.comboBox5.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "订单类型";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "医院自行订单",
            "托管药库订单"});
            this.comboBox4.Location = new System.Drawing.Point(103, 170);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(335, 20);
            this.comboBox4.TabIndex = 35;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(103, 142);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(336, 20);
            this.comboBox3.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "采购模式";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(103, 111);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(336, 20);
            this.comboBox2.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "药品类型";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(103, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(335, 20);
            this.comboBox1.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "医院编码";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(38, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "是否包含子公司";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 200);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "商品类型";
            // 
            // dtpDJRQ2
            // 
            this.dtpDJRQ2.Location = new System.Drawing.Point(290, 82);
            this.dtpDJRQ2.Name = "dtpDJRQ2";
            this.dtpDJRQ2.Size = new System.Drawing.Size(149, 21);
            this.dtpDJRQ2.TabIndex = 19;
            // 
            // dtpDJRQ1
            // 
            this.dtpDJRQ1.Location = new System.Drawing.Point(103, 82);
            this.dtpDJRQ1.Name = "dtpDJRQ1";
            this.dtpDJRQ1.Size = new System.Drawing.Size(149, 21);
            this.dtpDJRQ1.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(263, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "到";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "日期";
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.LightBlue;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilter.Location = new System.Drawing.Point(279, 345);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(66, 27);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "过滤";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Silver;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(351, 345);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(66, 27);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(423, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmFilter
            // 
            this.AcceptButton = this.btnFilter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(520, 384);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "frmFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "过滤";
            this.Load += new System.EventHandler(this.frmFilter2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dtpDJRQ2;
        private System.Windows.Forms.DateTimePicker dtpDJRQ1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
        //    if (keyData != Keys.F2)
        //        return base.ProcessCmdKey(ref msg, keyData);

        //    if (msg.HWnd == this.txtKeHu1.Handle)
        //    {
        //        frmQueryCustomer.QueryCondition = this.txtKeHu1.Text;
        //        frmQueryCustomer Customer = new frmQueryCustomer();

        //        if (DialogResult.OK == Customer.ShowDialog())
        //        {
        //            this.txtKeHu1.Text = frmQueryCustomer.CustomerID;
        //        }
        //    }
        //    else if (msg.HWnd == this.txtKeHu2.Handle)
        //    {
        //        frmQueryCustomer.QueryCondition = this.txtKeHu2.Text;
        //        frmQueryCustomer Customer = new frmQueryCustomer();

        //        if (DialogResult.OK == Customer.ShowDialog())
        //        {
        //            this.txtKeHu2.Text = frmQueryCustomer.CustomerID;
        //        }
        //    }
        //    else if (msg.HWnd == this.txtYYS1.Handle)
        //    {
        //        frmQueryDepartment.QueryCondition = this.txtYYS1.Text;
        //        frmQueryDepartment Department = new frmQueryDepartment();

        //        if (DialogResult.OK == Department.ShowDialog())
        //        {
        //            this.txtYYS1.Text = frmQueryDepartment.DepartmentID;
        //        }
        //    }
        //    else if (msg.HWnd == this.txtYYS2.Handle)
        //    {
        //        frmQueryDepartment.QueryCondition = this.txtYYS2.Text;
        //        frmQueryDepartment Department = new frmQueryDepartment();

        //        if (DialogResult.OK == Department.ShowDialog())
        //        {
        //            this.txtYYS2.Text = frmQueryDepartment.DepartmentID;
        //        }
        //    }
        //    else if (msg.HWnd == this.txtChanPin1.Handle)
        //    {
        //        frmQueryInventory.QueryCondition = this.txtChanPin1.Text;
        //        frmQueryInventory Inventory = new frmQueryInventory();

        //        if (DialogResult.OK == Inventory.ShowDialog())
        //        {
        //            this.txtChanPin1.Text = frmQueryInventory.InvCode;
        //        }
        //    }
        //    else if (msg.HWnd == this.txtChanPin2.Handle)
        //    {
        //        frmQueryInventory.QueryCondition = this.txtChanPin2.Text;
        //        frmQueryInventory Inventory = new frmQueryInventory();

        //        if (DialogResult.OK == Inventory.ShowDialog())
        //        {
        //            this.txtChanPin2.Text = frmQueryInventory.InvCode;
        //        }
        //    }

         return base.ProcessCmdKey(ref msg, keyData);
        }
        private Label label11;
        private ComboBox comboBox2;
        private Label label2;
        private ComboBox comboBox1;
        private Label label1;
        private CheckBox checkBox1;
        private ComboBox comboBox3;
        private Label label3;
        private ComboBox comboBox7;
        private Label label7;
        private ComboBox comboBox6;
        private ComboBox comboBox5;
        private Label label6;
        private ComboBox comboBox4;
        private TextBox textBox1;
        private Label label8;
    }
}