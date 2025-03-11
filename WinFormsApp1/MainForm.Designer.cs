using System.ComponentModel;

namespace WinFormsApp1;

partial class MainForm
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
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        textBox6 = new System.Windows.Forms.TextBox();
        label6 = new System.Windows.Forms.Label();
        richTextBox1 = new System.Windows.Forms.RichTextBox();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        listView1 = new System.Windows.Forms.ListView();
        label8 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        textBox2 = new System.Windows.Forms.TextBox();
        button3 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(679, 7);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(109, 23);
        label1.TabIndex = 0;
        label1.Text = "label1";
        // 
        // label2
        // 
        label2.Location = new System.Drawing.Point(585, 7);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(88, 23);
        label2.TabIndex = 1;
        label2.Text = "Пользователь";
        // 
        // textBox1
        // 
        textBox1.Location = new System.Drawing.Point(31, 138);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(122, 23);
        textBox1.TabIndex = 3;
        // 
        // label3
        // 
        label3.Location = new System.Drawing.Point(31, 112);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(136, 23);
        label3.TabIndex = 8;
        label3.Text = "Вид бытовой техники";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label4
        // 
        label4.Location = new System.Drawing.Point(173, 112);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(154, 23);
        label4.TabIndex = 10;
        label4.Text = "Модель бытовой техники";
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // textBox6
        // 
        textBox6.Location = new System.Drawing.Point(173, 138);
        textBox6.Name = "textBox6";
        textBox6.Size = new System.Drawing.Size(122, 23);
        textBox6.TabIndex = 9;
        // 
        // label6
        // 
        label6.Location = new System.Drawing.Point(31, 189);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(136, 23);
        label6.TabIndex = 12;
        label6.Text = "Описание проблемы";
        label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // richTextBox1
        // 
        richTextBox1.Location = new System.Drawing.Point(31, 215);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new System.Drawing.Size(264, 111);
        richTextBox1.TabIndex = 17;
        richTextBox1.Text = "";
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(31, 86);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(264, 23);
        button1.TabIndex = 18;
        button1.Text = "Открыть форму для заявки";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(31, 344);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(264, 35);
        button2.TabIndex = 19;
        button2.Text = "Отправить";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // listView1
        // 
        listView1.Location = new System.Drawing.Point(333, 88);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(441, 335);
        listView1.TabIndex = 20;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
        // 
        // label8
        // 
        label8.Location = new System.Drawing.Point(333, 62);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(189, 17);
        label8.TabIndex = 21;
        label8.Text = "Список отправленных заявок";
        // 
        // label5
        // 
        label5.Location = new System.Drawing.Point(585, 30);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(189, 17);
        label5.TabIndex = 22;
        // 
        // textBox2
        // 
        textBox2.Location = new System.Drawing.Point(513, 59);
        textBox2.Name = "textBox2";
        textBox2.PlaceholderText = "Поиск";
        textBox2.Size = new System.Drawing.Size(261, 23);
        textBox2.TabIndex = 23;
        textBox2.TextChanged += textBox2_TextChanged;
        // 
        // button3
        // 
        button3.Location = new System.Drawing.Point(31, 388);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(264, 35);
        button3.TabIndex = 24;
        button3.Text = "Редактировать";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(button3);
        Controls.Add(textBox2);
        Controls.Add(label5);
        Controls.Add(label8);
        Controls.Add(listView1);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(richTextBox1);
        Controls.Add(label6);
        Controls.Add(label4);
        Controls.Add(textBox6);
        Controls.Add(label3);
        Controls.Add(textBox1);
        Controls.Add(label2);
        Controls.Add(label1);
        Text = "Система учета заявок на ремонт бытовой техники";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button button3;

    private System.Windows.Forms.TextBox textBox2;

    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Label label8;

    private System.Windows.Forms.ListView listView1;

    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBox6;
    private System.Windows.Forms.Label label6;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBox1;

    private System.Windows.Forms.Label label1;

    #endregion
}