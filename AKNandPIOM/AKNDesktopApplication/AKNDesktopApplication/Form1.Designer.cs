namespace AKNDesktopApplication
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.embgTxt = new System.Windows.Forms.TextBox();
            this.callBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.statusTxt = new System.Windows.Forms.TextBox();
            this.amountTxt = new System.Windows.Forms.TextBox();
            this.numberTxt = new System.Windows.Forms.TextBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.errorTxt = new System.Windows.Forms.TextBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ЕМБГ:";
            // 
            // embgTxt
            // 
            this.embgTxt.Location = new System.Drawing.Point(56, 12);
            this.embgTxt.Name = "embgTxt";
            this.embgTxt.Size = new System.Drawing.Size(203, 20);
            this.embgTxt.TabIndex = 1;
            // 
            // callBtn
            // 
            this.callBtn.Location = new System.Drawing.Point(184, 56);
            this.callBtn.Name = "callBtn";
            this.callBtn.Size = new System.Drawing.Size(75, 23);
            this.callBtn.TabIndex = 2;
            this.callBtn.Text = "Повикај";
            this.callBtn.UseVisualStyleBackColor = true;
            this.callBtn.Click += new System.EventHandler(this.callBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Име и презиме:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Пензиски број:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Пензија:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Статус на пензија:";
            // 
            // nameTxt
            // 
            this.nameTxt.Location = new System.Drawing.Point(125, 33);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.Size = new System.Drawing.Size(193, 20);
            this.nameTxt.TabIndex = 7;
            // 
            // statusTxt
            // 
            this.statusTxt.Location = new System.Drawing.Point(125, 126);
            this.statusTxt.Name = "statusTxt";
            this.statusTxt.Size = new System.Drawing.Size(193, 20);
            this.statusTxt.TabIndex = 8;
            // 
            // amountTxt
            // 
            this.amountTxt.Location = new System.Drawing.Point(125, 94);
            this.amountTxt.Name = "amountTxt";
            this.amountTxt.Size = new System.Drawing.Size(193, 20);
            this.amountTxt.TabIndex = 9;
            // 
            // numberTxt
            // 
            this.numberTxt.Location = new System.Drawing.Point(125, 61);
            this.numberTxt.Name = "numberTxt";
            this.numberTxt.Size = new System.Drawing.Size(193, 20);
            this.numberTxt.TabIndex = 10;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.errorTxt);
            this.groupBox.Controls.Add(this.label6);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.numberTxt);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.amountTxt);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.statusTxt);
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.nameTxt);
            this.groupBox.Location = new System.Drawing.Point(346, 15);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(418, 283);
            this.groupBox.TabIndex = 11;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Резултат";
            this.groupBox.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Грешка:";
            // 
            // errorTxt
            // 
            this.errorTxt.Location = new System.Drawing.Point(63, 244);
            this.errorTxt.Name = "errorTxt";
            this.errorTxt.Size = new System.Drawing.Size(328, 20);
            this.errorTxt.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 676);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.callBtn);
            this.Controls.Add(this.embgTxt);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "AКН го повикува ПИОМ";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox embgTxt;
        private System.Windows.Forms.Button callBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox statusTxt;
        private System.Windows.Forms.TextBox amountTxt;
        private System.Windows.Forms.TextBox numberTxt;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TextBox errorTxt;
        private System.Windows.Forms.Label label6;
    }
}

