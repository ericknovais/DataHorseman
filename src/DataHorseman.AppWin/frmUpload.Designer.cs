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
        txtArquivo.Location = new Point(10, 25);
        txtArquivo.Name = "txtArquivo";
        txtArquivo.ReadOnly = true;
        txtArquivo.Size = new Size(485, 27);
        txtArquivo.TabIndex = 0;
        // 
        // btnUpload
        // 
        btnUpload.Location = new Point(10, 58);
        btnUpload.Name = "btnUpload";
        btnUpload.Size = new Size(94, 29);
        btnUpload.TabIndex = 1;
        btnUpload.Text = "Upload";
        btnUpload.UseVisualStyleBackColor = true;
        btnUpload.Click += btnUpload_Click;
        // 
        // btnSalvar
        // 
        btnSalvar.Location = new Point(401, 61);
        btnSalvar.Name = "btnSalvar";
        btnSalvar.Size = new Size(94, 29);
        btnSalvar.TabIndex = 2;
        btnSalvar.Text = "Salvar";
        btnSalvar.UseVisualStyleBackColor = true;
        btnSalvar.Click += btnSalvar_Click;
        // 
        // frmUpload
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(507, 102);
        Controls.Add(btnSalvar);
        Controls.Add(btnUpload);
        Controls.Add(txtArquivo);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmUpload";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Upload";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox txtArquivo;
    private Button btnUpload;
    private Button btnSalvar;
}
