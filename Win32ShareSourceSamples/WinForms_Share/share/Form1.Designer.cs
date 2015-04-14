namespace share
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.ShareTextBox = new System.Windows.Forms.TextBox();
			this.ShareWeblinkBox = new System.Windows.Forms.TextBox();
			this.SHareDataBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(329, 136);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(154, 28);
			this.button1.TabIndex = 0;
			this.button1.Text = "Share Text";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(329, 201);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(154, 28);
			this.button2.TabIndex = 1;
			this.button2.Text = "Share WebLink";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(329, 330);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(154, 28);
			this.button3.TabIndex = 2;
			this.button3.Text = "Share Custom Data";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(12, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(310, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Let\'s Share From our Desktop Friends !!";
			// 
			// ShareTextBox
			// 
			this.ShareTextBox.Location = new System.Drawing.Point(13, 107);
			this.ShareTextBox.Multiline = true;
			this.ShareTextBox.Name = "ShareTextBox";
			this.ShareTextBox.Size = new System.Drawing.Size(289, 57);
			this.ShareTextBox.TabIndex = 5;
			this.ShareTextBox.Text = "Sharing from a WinForms.NET Desktop app. Hello Windows 10!";
			// 
			// ShareWeblinkBox
			// 
			this.ShareWeblinkBox.Location = new System.Drawing.Point(15, 206);
			this.ShareWeblinkBox.Name = "ShareWeblinkBox";
			this.ShareWeblinkBox.Size = new System.Drawing.Size(287, 20);
			this.ShareWeblinkBox.TabIndex = 6;
			this.ShareWeblinkBox.Text = "http://www.bing.com";
			// 
			// SHareDataBox
			// 
			this.SHareDataBox.Location = new System.Drawing.Point(13, 281);
			this.SHareDataBox.Multiline = true;
			this.SHareDataBox.Name = "SHareDataBox";
			this.SHareDataBox.Size = new System.Drawing.Size(289, 77);
			this.SHareDataBox.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Enter text to share";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 182);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(119, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Enter WebLink to share";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 250);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Enter custom data to share";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(529, 370);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.SHareDataBox);
			this.Controls.Add(this.ShareWeblinkBox);
			this.Controls.Add(this.ShareTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Share From Desktop App";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ShareTextBox;
        private System.Windows.Forms.TextBox ShareWeblinkBox;
        private System.Windows.Forms.TextBox SHareDataBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

