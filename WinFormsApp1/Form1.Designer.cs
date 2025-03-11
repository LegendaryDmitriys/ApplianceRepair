namespace WinFormsApp1;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Segoe UI", 24F);
        label1.Location = new System.Drawing.Point(260, 89);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(229, 40);
        label1.TabIndex = 0;
        label1.Text = "Авторизация";
        // 
        // textBox1
        // 
        textBox1.Font = new System.Drawing.Font("Segoe UI", 12F);
        textBox1.Location = new System.Drawing.Point(260, 151);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.PlaceholderText = "Логин";
        textBox1.Size = new System.Drawing.Size(204, 32);
        textBox1.TabIndex = 1;
        // 
        // textBox2
        // 
        textBox2.Font = new System.Drawing.Font("Segoe UI", 12F);
        textBox2.Location = new System.Drawing.Point(260, 197);
        textBox2.Multiline = true;
        textBox2.Name = "textBox2";
        textBox2.PasswordChar = '*';
        textBox2.PlaceholderText = "Пароль";
        textBox2.Size = new System.Drawing.Size(204, 32);
        textBox2.TabIndex = 2;
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(284, 249);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(149, 33);
        button1.TabIndex = 3;
        button1.Text = "Войти";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(button1);
        Controls.Add(textBox2);
        Controls.Add(textBox1);
        Controls.Add(label1);
        Text = "Система учета заявок на ремонт бытовой техники";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button button1;

    #endregion
}