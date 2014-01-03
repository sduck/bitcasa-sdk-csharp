namespace ExampleApp
{
    partial class MainForm
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
            this.txtAuthUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.btnClientInfo = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAuthUrl
            // 
            this.txtAuthUrl.Location = new System.Drawing.Point(12, 25);
            this.txtAuthUrl.Name = "txtAuthUrl";
            this.txtAuthUrl.ReadOnly = true;
            this.txtAuthUrl.Size = new System.Drawing.Size(588, 20);
            this.txtAuthUrl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Authentication URL:";
            // 
            // txtAuthCode
            // 
            this.txtAuthCode.Location = new System.Drawing.Point(12, 75);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(199, 20);
            this.txtAuthCode.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Authorization code:";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(217, 73);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.Location = new System.Drawing.Point(12, 112);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(588, 225);
            this.lstResult.TabIndex = 5;
            this.lstResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstResult_MouseDoubleClick);
            // 
            // btnClientInfo
            // 
            this.btnClientInfo.Location = new System.Drawing.Point(481, 51);
            this.btnClientInfo.Name = "btnClientInfo";
            this.btnClientInfo.Size = new System.Drawing.Size(119, 23);
            this.btnClientInfo.TabIndex = 6;
            this.btnClientInfo.Text = "Update client info";
            this.btnClientInfo.UseVisualStyleBackColor = true;
            this.btnClientInfo.Click += new System.EventHandler(this.btnClientInfo_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 349);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(612, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 371);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnClientInfo);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAuthCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAuthUrl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitcasa C# SDK - Example";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAuthUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAuthCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button btnClientInfo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

