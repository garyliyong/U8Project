namespace SHYSInterface.退货单
{
    partial class FormReturnOrderFilter
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
            this.comboBoxYYBM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxYPLX = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxSPLX = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxTJFS = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxTHDCLZT = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxTHDBH = new System.Windows.Forms.TextBox();
            this.checkBoxSFBHZGS = new System.Windows.Forms.CheckBox();
            this.checkBoxDLCGBZ = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxYYBM
            // 
            this.comboBoxYYBM.FormattingEnabled = true;
            this.comboBoxYYBM.Location = new System.Drawing.Point(130, 40);
            this.comboBoxYYBM.Name = "comboBoxYYBM";
            this.comboBoxYYBM.Size = new System.Drawing.Size(303, 20);
            this.comboBoxYYBM.Sorted = true;
            this.comboBoxYYBM.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "医院编码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "日期";
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(130, 77);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(134, 21);
            this.dateTimePickerStart.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "到";
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(315, 77);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(118, 21);
            this.dateTimePickerEnd.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "药品类型";
            // 
            // comboBoxYPLX
            // 
            this.comboBoxYPLX.FormattingEnabled = true;
            this.comboBoxYPLX.Location = new System.Drawing.Point(130, 125);
            this.comboBoxYPLX.Name = "comboBoxYPLX";
            this.comboBoxYPLX.Size = new System.Drawing.Size(303, 20);
            this.comboBoxYPLX.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "商品类型";
            // 
            // comboBoxSPLX
            // 
            this.comboBoxSPLX.FormattingEnabled = true;
            this.comboBoxSPLX.Items.AddRange(new object[] {
            "药品类",
            "医用耗材器械类",
            "其他"});
            this.comboBoxSPLX.Location = new System.Drawing.Point(130, 165);
            this.comboBoxSPLX.Name = "comboBoxSPLX";
            this.comboBoxSPLX.Size = new System.Drawing.Size(303, 20);
            this.comboBoxSPLX.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "退货单提交方式";
            // 
            // comboBoxTJFS
            // 
            this.comboBoxTJFS.FormattingEnabled = true;
            this.comboBoxTJFS.Items.AddRange(new object[] {
            "医院填报",
            "药企代填"});
            this.comboBoxTJFS.Location = new System.Drawing.Point(130, 203);
            this.comboBoxTJFS.Name = "comboBoxTJFS";
            this.comboBoxTJFS.Size = new System.Drawing.Size(303, 20);
            this.comboBoxTJFS.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "退货单处理状态";
            // 
            // comboBoxTHDCLZT
            // 
            this.comboBoxTHDCLZT.FormattingEnabled = true;
            this.comboBoxTHDCLZT.Location = new System.Drawing.Point(130, 241);
            this.comboBoxTHDCLZT.Name = "comboBoxTHDCLZT";
            this.comboBoxTHDCLZT.Size = new System.Drawing.Size(303, 20);
            this.comboBoxTHDCLZT.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "退货单编号";
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(30, 325);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(104, 44);
            this.buttonFilter.TabIndex = 12;
            this.buttonFilter.Text = "过滤";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(171, 325);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(104, 44);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "清空";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(315, 325);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(104, 44);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxTHDBH
            // 
            this.textBoxTHDBH.Location = new System.Drawing.Point(129, 280);
            this.textBoxTHDBH.Name = "textBoxTHDBH";
            this.textBoxTHDBH.Size = new System.Drawing.Size(303, 21);
            this.textBoxTHDBH.TabIndex = 13;
            // 
            // checkBoxSFBHZGS
            // 
            this.checkBoxSFBHZGS.AutoSize = true;
            this.checkBoxSFBHZGS.Location = new System.Drawing.Point(26, 10);
            this.checkBoxSFBHZGS.Name = "checkBoxSFBHZGS";
            this.checkBoxSFBHZGS.Size = new System.Drawing.Size(108, 16);
            this.checkBoxSFBHZGS.TabIndex = 14;
            this.checkBoxSFBHZGS.Text = "是否包含子公司";
            this.checkBoxSFBHZGS.UseVisualStyleBackColor = true;
            // 
            // checkBoxDLSGBZ
            // 
            this.checkBoxDLCGBZ.AutoSize = true;
            this.checkBoxDLCGBZ.Location = new System.Drawing.Point(171, 10);
            this.checkBoxDLCGBZ.Name = "checkBoxDLSGBZ";
            this.checkBoxDLCGBZ.Size = new System.Drawing.Size(96, 16);
            this.checkBoxDLCGBZ.TabIndex = 14;
            this.checkBoxDLCGBZ.Text = "是否带量采购";
            this.checkBoxDLCGBZ.UseVisualStyleBackColor = true;
            // 
            // FormReturnOrderFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 395);
            this.Controls.Add(this.checkBoxDLCGBZ);
            this.Controls.Add(this.checkBoxSFBHZGS);
            this.Controls.Add(this.textBoxTHDBH);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.comboBoxTHDCLZT);
            this.Controls.Add(this.comboBoxTJFS);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxSPLX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxYPLX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxYYBM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormReturnOrderFilter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "退货单查询过滤";
            this.Load += new System.EventHandler(this.FormReturnOrderFilter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxYYBM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxYPLX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSPLX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxTJFS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxTHDCLZT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxTHDBH;
        private System.Windows.Forms.CheckBox checkBoxSFBHZGS;
        private System.Windows.Forms.CheckBox checkBoxDLCGBZ;
    }
}