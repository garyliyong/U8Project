namespace SHYSInterface
{
    partial class FrmYQ013
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
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cinvcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cinvname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDefine29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDLsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cdefine25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSBVCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bReturnFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDefine32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDefine33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cdefine14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDefine34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cdefine11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkbox1 = new System.Windows.Forms.CheckBox();
            this.dt2 = new System.Windows.Forms.DateTimePicker();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.result,
            this.cinvcode,
            this.cinvname,
            this.cDefine29,
            this.iDLsID,
            this.cMemo,
            this.cdefine25,
            this.dDate,
            this.cSBVCode,
            this.bReturnFlag,
            this.cCusName,
            this.cDefine32,
            this.cDefine33,
            this.cdefine14,
            this.cDefine34,
            this.cMaker,
            this.cdefine11});
            this.dgv1.Location = new System.Drawing.Point(2, 83);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersWidth = 11;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.Size = new System.Drawing.Size(929, 420);
            this.dgv1.TabIndex = 5;
            this.dgv1.DoubleClick += new System.EventHandler(this.dgv1_DoubleClick);
            this.dgv1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv1_DataError);
            // 
            // check
            // 
            this.check.DataPropertyName = "check";
            this.check.HeaderText = "选中";
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.check.Width = 40;
            // 
            // result
            // 
            this.result.DataPropertyName = "result";
            this.result.HeaderText = "处理结果";
            this.result.Name = "result";
            this.result.Width = 80;
            // 
            // cinvcode
            // 
            this.cinvcode.DataPropertyName = "cinvcode";
            this.cinvcode.HeaderText = "药品编码";
            this.cinvcode.Name = "cinvcode";
            this.cinvcode.Width = 80;
            // 
            // cinvname
            // 
            this.cinvname.DataPropertyName = "cinvname";
            this.cinvname.HeaderText = "药品名称";
            this.cinvname.Name = "cinvname";
            // 
            // cDefine29
            // 
            this.cDefine29.DataPropertyName = "cDefine29";
            this.cDefine29.HeaderText = "订单明细编号";
            this.cDefine29.Name = "cDefine29";
            this.cDefine29.Width = 110;
            // 
            // iDLsID
            // 
            this.iDLsID.DataPropertyName = "iDLsID";
            this.iDLsID.HeaderText = "配送单条码";
            this.iDLsID.Name = "iDLsID";
            this.iDLsID.ReadOnly = true;
            // 
            // cMemo
            // 
            this.cMemo.DataPropertyName = "cMemo";
            this.cMemo.HeaderText = "终止原因";
            this.cMemo.Name = "cMemo";
            this.cMemo.Width = 120;
            // 
            // cdefine25
            // 
            this.cdefine25.DataPropertyName = "cdefine25";
            this.cdefine25.HeaderText = "是否配送";
            this.cdefine25.Name = "cdefine25";
            this.cdefine25.ReadOnly = true;
            this.cdefine25.Width = 80;
            // 
            // dDate
            // 
            this.dDate.DataPropertyName = "dDate";
            this.dDate.HeaderText = "发票日期";
            this.dDate.Name = "dDate";
            this.dDate.ReadOnly = true;
            this.dDate.Width = 80;
            // 
            // cSBVCode
            // 
            this.cSBVCode.DataPropertyName = "cSBVCode";
            this.cSBVCode.HeaderText = "发票号";
            this.cSBVCode.Name = "cSBVCode";
            this.cSBVCode.ReadOnly = true;
            // 
            // bReturnFlag
            // 
            this.bReturnFlag.DataPropertyName = "bReturnFlag";
            this.bReturnFlag.HeaderText = "退货标识";
            this.bReturnFlag.Name = "bReturnFlag";
            this.bReturnFlag.ReadOnly = true;
            this.bReturnFlag.Width = 80;
            // 
            // cCusName
            // 
            this.cCusName.DataPropertyName = "cCusName";
            this.cCusName.HeaderText = "客户名称";
            this.cCusName.Name = "cCusName";
            this.cCusName.ReadOnly = true;
            // 
            // cDefine32
            // 
            this.cDefine32.DataPropertyName = "cDefine32";
            this.cDefine32.HeaderText = "医院配送点编码";
            this.cDefine32.Name = "cDefine32";
            this.cDefine32.ReadOnly = true;
            this.cDefine32.Width = 80;
            // 
            // cDefine33
            // 
            this.cDefine33.DataPropertyName = "cDefine33";
            this.cDefine33.HeaderText = "配送地址";
            this.cDefine33.Name = "cDefine33";
            this.cDefine33.ReadOnly = true;
            // 
            // cdefine14
            // 
            this.cdefine14.DataPropertyName = "cdefine14";
            this.cdefine14.HeaderText = "计划单号";
            this.cdefine14.Name = "cdefine14";
            this.cdefine14.ReadOnly = true;
            // 
            // cDefine34
            // 
            this.cDefine34.DataPropertyName = "cDefine34";
            this.cDefine34.HeaderText = "配送箱数";
            this.cDefine34.Name = "cDefine34";
            this.cDefine34.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // cMaker
            // 
            this.cMaker.DataPropertyName = "cMaker";
            this.cMaker.HeaderText = "制单人";
            this.cMaker.Name = "cMaker";
            this.cMaker.ReadOnly = true;
            // 
            // cdefine11
            // 
            this.cdefine11.DataPropertyName = "cdefine11";
            this.cdefine11.HeaderText = "cdefine11";
            this.cdefine11.Name = "cdefine11";
            this.cdefine11.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(641, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 75);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "执行";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消发送";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "终止配送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.checkbox1);
            this.groupBox1.Controls.Add(this.dt2);
            this.groupBox1.Controls.Add(this.dt1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 75);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(437, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "全选";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(358, 46);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "是否配送";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(541, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "查询";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkbox1
            // 
            this.checkbox1.AutoSize = true;
            this.checkbox1.Location = new System.Drawing.Point(358, 19);
            this.checkbox1.Name = "checkbox1";
            this.checkbox1.Size = new System.Drawing.Size(72, 16);
            this.checkbox1.TabIndex = 8;
            this.checkbox1.Text = "是否退货";
            this.checkbox1.UseVisualStyleBackColor = true;
            // 
            // dt2
            // 
            this.dt2.Location = new System.Drawing.Point(228, 43);
            this.dt2.Name = "dt2";
            this.dt2.Size = new System.Drawing.Size(111, 21);
            this.dt2.TabIndex = 7;
            // 
            // dt1
            // 
            this.dt1.Location = new System.Drawing.Point(85, 46);
            this.dt1.Name = "dt1";
            this.dt1.Size = new System.Drawing.Size(114, 21);
            this.dt1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(228, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(111, 21);
            this.textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(114, 21);
            this.textBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "发票日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "发票号";
            // 
            // FrmYQ013
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 529);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmYQ013";
            this.Text = "订单终止配送";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkbox1;
        private System.Windows.Forms.DateTimePicker dt2;
        private System.Windows.Forms.DateTimePicker dt1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.DataGridViewTextBoxColumn cinvcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cinvname;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDefine29;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDLsID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cdefine25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSBVCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn bReturnFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDefine32;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDefine33;
        private System.Windows.Forms.DataGridViewTextBoxColumn cdefine14;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDefine34;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaker;
        private System.Windows.Forms.DataGridViewTextBoxColumn cdefine11;

    }
}