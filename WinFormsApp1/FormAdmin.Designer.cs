using System.ComponentModel;

namespace WinFormsApp1;

partial class FormAdmin
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        listView1 = new System.Windows.Forms.ListView();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        richTextBox1 = new System.Windows.Forms.RichTextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        comboBox1 = new System.Windows.Forms.ComboBox();
        richTextBox2 = new System.Windows.Forms.RichTextBox();
        comboBox2 = new System.Windows.Forms.ComboBox();
        comboBox3 = new System.Windows.Forms.ComboBox();
        button4 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // listView1
        // 
        listView1.Location = new System.Drawing.Point(381, 67);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(358, 316);
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(592, 403);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(147, 35);
        button1.TabIndex = 1;
        button1.Text = "Добавить";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(32, 403);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(147, 35);
        button2.TabIndex = 2;
        button2.Text = "Редактировать";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // button3
        // 
        button3.Location = new System.Drawing.Point(206, 403);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(147, 35);
        button3.TabIndex = 3;
        button3.Text = "Удалить";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // textBox1
        // 
        textBox1.Location = new System.Drawing.Point(23, 67);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(156, 23);
        textBox1.TabIndex = 4;
        // 
        // richTextBox1
        // 
        richTextBox1.Location = new System.Drawing.Point(23, 107);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new System.Drawing.Size(330, 96);
        richTextBox1.TabIndex = 6;
        richTextBox1.Text = "";
        // 
        // textBox2
        // 
        textBox2.Location = new System.Drawing.Point(197, 67);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(156, 23);
        textBox2.TabIndex = 7;
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(23, 219);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(330, 23);
        comboBox1.TabIndex = 8;
        // 
        // richTextBox2
        // 
        richTextBox2.Location = new System.Drawing.Point(23, 257);
        richTextBox2.Name = "richTextBox2";
        richTextBox2.Size = new System.Drawing.Size(330, 58);
        richTextBox2.TabIndex = 9;
        richTextBox2.Text = "";
        // 
        // comboBox2
        // 
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new System.Drawing.Point(23, 331);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(156, 23);
        comboBox2.TabIndex = 10;
        // 
        // comboBox3
        // 
        comboBox3.FormattingEnabled = true;
        comboBox3.Location = new System.Drawing.Point(197, 331);
        comboBox3.Name = "comboBox3";
        comboBox3.Size = new System.Drawing.Size(156, 23);
        comboBox3.TabIndex = 11;
        // 
        // button4
        // 
        button4.Location = new System.Drawing.Point(381, 403);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(147, 35);
        button4.TabIndex = 12;
        button4.Text = "Cтатистика";
        button4.UseVisualStyleBackColor = true;
        button4.Click += button4_Click;
        // 
        // FormAdmin
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(button4);
        Controls.Add(comboBox3);
        Controls.Add(comboBox2);
        Controls.Add(richTextBox2);
        Controls.Add(comboBox1);
        Controls.Add(textBox2);
        Controls.Add(richTextBox1);
        Controls.Add(textBox1);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(listView1);
        Text = "Система учета заявок на ремонт бытовой техники";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button button4;

    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.ComboBox comboBox3;

    private System.Windows.Forms.RichTextBox richTextBox2;

    private System.Windows.Forms.ComboBox comboBox1;

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.RichTextBox richTextBox1;

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;

    public System.Windows.Forms.ListView listView1;

    #endregion
}