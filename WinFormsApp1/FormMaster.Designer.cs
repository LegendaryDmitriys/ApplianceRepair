using System.ComponentModel;

namespace WinFormsApp1;

partial class FormMaster
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
        richTextBox1 = new System.Windows.Forms.RichTextBox();
        comboBox1 = new System.Windows.Forms.ComboBox();
        SuspendLayout();
        // 
        // listView1
        // 
        listView1.Location = new System.Drawing.Point(240, 84);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(525, 340);
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(36, 316);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(185, 36);
        button1.TabIndex = 1;
        button1.Text = "Изменить";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(36, 200);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(185, 36);
        button2.TabIndex = 2;
        button2.Text = "Добавить комментарий";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // richTextBox1
        // 
        richTextBox1.Location = new System.Drawing.Point(36, 84);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new System.Drawing.Size(185, 110);
        richTextBox1.TabIndex = 3;
        richTextBox1.Text = "";
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(36, 273);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(185, 23);
        comboBox1.TabIndex = 4;
        comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        // 
        // FormMaster
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(comboBox1);
        Controls.Add(richTextBox1);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(listView1);
        Text = "Система учета заявок на ремонт бытовой техники";
        ResumeLayout(false);
    }

    private System.Windows.Forms.ComboBox comboBox1;

    private System.Windows.Forms.RichTextBox richTextBox1;

    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;

    #endregion
}