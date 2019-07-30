namespace ReckNer_ConfigWebSite
{
    partial class frmConfig
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.txtWhiteList = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.opdDisclaimer = new System.Windows.Forms.OpenFileDialog();
            this.bkWorkerProcess = new System.ComponentModel.BackgroundWorker();
            this.lblMessage = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.pnlFields = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlFields.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(243, 60);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(146, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Apply configuration";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(121, 10);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(356, 20);
            this.txtUser.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(121, 37);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(356, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Admin User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Site Name";
            // 
            // txtSite
            // 
            this.txtSite.Location = new System.Drawing.Point(121, 64);
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(356, 20);
            this.txtSite.TabIndex = 6;
            // 
            // txtWhiteList
            // 
            this.txtWhiteList.Location = new System.Drawing.Point(121, 91);
            this.txtWhiteList.Name = "txtWhiteList";
            this.txtWhiteList.Size = new System.Drawing.Size(356, 20);
            this.txtWhiteList.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "WhiteList";
            // 
            // opdDisclaimer
            // 
            this.opdDisclaimer.Title = "Select your disclaimer file";
            // 
            // bkWorkerProcess
            // 
            this.bkWorkerProcess.WorkerReportsProgress = true;
            this.bkWorkerProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkWorkerProcess_DoWorkAsync);
            this.bkWorkerProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bkWorkerProcess_ProgressChanged);
            this.bkWorkerProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkWorkerProcess_RunWorkerCompleted);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(38, 3);
            this.lblMessage.MaximumSize = new System.Drawing.Size(450, 60);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 12;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(40, 33);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(437, 20);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 13;
            this.progressBar1.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(402, 60);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(314, 61);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 15;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Visible = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // pnlFields
            // 
            this.pnlFields.Controls.Add(this.label5);
            this.pnlFields.Controls.Add(this.txtWhiteList);
            this.pnlFields.Controls.Add(this.txtUser);
            this.pnlFields.Controls.Add(this.txtPassword);
            this.pnlFields.Controls.Add(this.label1);
            this.pnlFields.Controls.Add(this.label2);
            this.pnlFields.Controls.Add(this.label4);
            this.pnlFields.Controls.Add(this.label3);
            this.pnlFields.Controls.Add(this.txtSite);
            this.pnlFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFields.Location = new System.Drawing.Point(0, 0);
            this.pnlFields.Name = "pnlFields";
            this.pnlFields.Size = new System.Drawing.Size(540, 141);
            this.pnlFields.TabIndex = 16;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.progressBar1);
            this.pnlButtons.Controls.Add(this.btnReturn);
            this.pnlButtons.Controls.Add(this.btnConnect);
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Controls.Add(this.lblMessage);
            this.pnlButtons.Location = new System.Drawing.Point(0, 141);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(540, 94);
            this.pnlButtons.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(118, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(368, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "For multiple domains use comma \",\" as separator, for example: domain1.com,\r\ndomai" +
    "n2.com,domain3.com";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 234);
            this.ControlBox = false;
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlFields);
            this.Name = "frmConfig";
            this.Text = "Reckner - Config - v1.0.2";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.pnlFields.ResumeLayout(false);
            this.pnlFields.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.TextBox txtWhiteList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog opdDisclaimer;
        private System.ComponentModel.BackgroundWorker bkWorkerProcess;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Panel pnlFields;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label label5;
    }
}

