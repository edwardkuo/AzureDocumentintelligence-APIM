namespace DocumentIntelligenceApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.TextBox txtOutput;
    private System.Windows.Forms.Button btnUpload;

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
        this.components = new System.ComponentModel.Container();
        this.txtOutput = new System.Windows.Forms.TextBox();
        this.btnUpload = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // txtOutput
        // 
        this.txtOutput.Location = new System.Drawing.Point(12, 12);
        this.txtOutput.Multiline = true;
        this.txtOutput.Name = "txtOutput";
        this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtOutput.Size = new System.Drawing.Size(776, 350);
        this.txtOutput.TabIndex = 0;
        // 
        // btnUpload
        // 
        this.btnUpload.Location = new System.Drawing.Point(12, 380);
        this.btnUpload.Name = "btnUpload";
        this.btnUpload.Size = new System.Drawing.Size(100, 30);
        this.btnUpload.TabIndex = 1;
        this.btnUpload.Text = "Upload";
        this.btnUpload.UseVisualStyleBackColor = true;
        this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.btnUpload);
        this.Controls.Add(this.txtOutput);
        this.Name = "Form1";
        this.Text = "Document Intelligence App";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}
