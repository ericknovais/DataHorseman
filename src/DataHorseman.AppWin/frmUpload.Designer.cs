namespace DataHorseman.AppWin;

partial class frmUpload
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
        txtArquivo = new TextBox();
        btnUpload = new Button();
        btnSalvar = new Button();
        SuspendLayout();
        // 
        // txtArquivo
        // 
        txtArquivo.Location = new Point(9, 19);
        txtArquivo.Margin = new Padding(3, 2, 3, 2);
        txtArquivo.Name = "txtArquivo";
        txtArquivo.ReadOnly = true;
        txtArquivo.Size = new Size(425, 23);
        txtArquivo.TabIndex = 0;
        // 
        // btnUpload
        // 
        btnUpload.Location = new Point(9, 44);
        btnUpload.Margin = new Padding(3, 2, 3, 2);
        btnUpload.Name = "btnUpload";
        btnUpload.Size = new Size(82, 22);
        btnUpload.TabIndex = 1;
        btnUpload.Text = "Upload";
        btnUpload.UseVisualStyleBackColor = true;
        btnUpload.Click += btnUpload_Click;
        // 
        // btnSalvar
        // 
        btnSalvar.Location = new Point(351, 46);
        btnSalvar.Margin = new Padding(3, 2, 3, 2);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(82, 22);
        btnSalvar.TabIndex = 2;
        btnSalvar.Text = "Salvar";
        btnSalvar.UseVisualStyleBackColor = true;
        btnSalvar.Click += btnSalvar_Click;
        // 
        // frmUpload
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(444, 76);
        Controls.Add(btnSalvar);
        Controls.Add(btnUpload);
        Controls.Add(txtArquivo);
        Margin = new Padding(3, 2, 3, 2);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmUpload";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Upload";
        Load += frmUpload_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox txtArquivo;
    private Button btnUpload;
    private Button btnSalvar;
}
