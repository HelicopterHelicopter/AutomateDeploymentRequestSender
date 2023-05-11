namespace AutomateDeploymentRequest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.APISelector = new System.Windows.Forms.ComboBox();
            this.APISelectLabel = new System.Windows.Forms.Label();
            this.changeSummary = new System.Windows.Forms.Label();
            this.changeSummaryText = new System.Windows.Forms.RichTextBox();
            this.SendDeploymentRequest = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.AdditionalComments = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // APISelector
            // 
            this.APISelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.APISelector.FormattingEnabled = true;
            this.APISelector.Items.AddRange(new object[] {
            "ProductInsuranceAPI",
            "OrderInsuranceAPI",
            "PaymentInsuranceAPI"});
            this.APISelector.Location = new System.Drawing.Point(33, 48);
            this.APISelector.Name = "APISelector";
            this.APISelector.Size = new System.Drawing.Size(385, 33);
            this.APISelector.TabIndex = 0;
            this.APISelector.SelectedIndexChanged += new System.EventHandler(this.APISelector_SelectedIndexChanged);
            // 
            // APISelectLabel
            // 
            this.APISelectLabel.AutoSize = true;
            this.APISelectLabel.Location = new System.Drawing.Point(32, 16);
            this.APISelectLabel.Name = "APISelectLabel";
            this.APISelectLabel.Size = new System.Drawing.Size(176, 25);
            this.APISelectLabel.TabIndex = 1;
            this.APISelectLabel.Text = "Select API to deploy:";
            // 
            // changeSummary
            // 
            this.changeSummary.AutoSize = true;
            this.changeSummary.Location = new System.Drawing.Point(32, 101);
            this.changeSummary.Name = "changeSummary";
            this.changeSummary.Size = new System.Drawing.Size(184, 25);
            this.changeSummary.TabIndex = 2;
            this.changeSummary.Text = "Summary of changes:";
            // 
            // changeSummaryText
            // 
            this.changeSummaryText.Location = new System.Drawing.Point(39, 142);
            this.changeSummaryText.Name = "changeSummaryText";
            this.changeSummaryText.Size = new System.Drawing.Size(379, 100);
            this.changeSummaryText.TabIndex = 3;
            this.changeSummaryText.Text = "";
            // 
            // SendDeploymentRequest
            // 
            this.SendDeploymentRequest.Location = new System.Drawing.Point(284, 397);
            this.SendDeploymentRequest.Name = "SendDeploymentRequest";
            this.SendDeploymentRequest.Size = new System.Drawing.Size(268, 34);
            this.SendDeploymentRequest.TabIndex = 4;
            this.SendDeploymentRequest.Text = "Send for Deployment";
            this.SendDeploymentRequest.UseVisualStyleBackColor = true;
            this.SendDeploymentRequest.Click += new System.EventHandler(this.SendDeploymentRequest_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 450);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 32);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(60, 25);
            this.Status.Text = "Status";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(39, 293);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(379, 79);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.Text = "";
            // 
            // AdditionalComments
            // 
            this.AdditionalComments.AutoSize = true;
            this.AdditionalComments.Location = new System.Drawing.Point(39, 259);
            this.AdditionalComments.Name = "AdditionalComments";
            this.AdditionalComments.Size = new System.Drawing.Size(250, 25);
            this.AdditionalComments.TabIndex = 7;
            this.AdditionalComments.Text = "Additional comments (if any) :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.AdditionalComments);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.SendDeploymentRequest);
            this.Controls.Add(this.changeSummaryText);
            this.Controls.Add(this.changeSummary);
            this.Controls.Add(this.APISelectLabel);
            this.Controls.Add(this.APISelector);
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox APISelector;
        private System.Windows.Forms.Label APISelectLabel;
        private System.Windows.Forms.Label changeSummary;
        private System.Windows.Forms.RichTextBox changeSummaryText;
        private System.Windows.Forms.Button SendDeploymentRequest;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label AdditionalComments;
        private System.Windows.Forms.ToolStripStatusLabel Status;
    }
}
