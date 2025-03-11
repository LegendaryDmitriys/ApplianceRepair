
using System.Diagnostics;

namespace WinFormsApp1;
using Npgsql;

public partial class MainForm : Form
{
    public string connectionString = "Server=localhost;Database=Accounting;User Id=postgres;Password=admin;";
    private int selectedRequestId = -1;
    private (int userId, string fio, string phoneNumber) GetUserInfo(string login)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT userid, fio, phone FROM users WHERE login = @Login";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            string fio = reader.GetString(1);
                            string phoneNumber = reader.GetString(2);
                            return (userId, fio, phoneNumber);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении данных пользователя: " + ex.Message);
            }
        }

        return (-1, "", "");
    }

    
    private void LoadUserRequests()
    {
        listView1.Items.Clear();

        var (userId, _, _) = GetUserInfo(label1.Text);
        if (userId == -1)
        {
            MessageBox.Show("Ошибка: Пользователь не найден!");
            return;
        }

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = @"
                SELECT 
                    rr.requestid, 
                    rr.hometechtype, 
                    rr.hometechmodel, 
                    rs.statusname 
                FROM 
                    repair_requests rr
                INNER JOIN 
                    request_statuses rs 
                ON 
                    rr.requeststatusid = rs.statusid
                WHERE 
                    rr.clientid = @UserId";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] row = new string[]
                            {
                                reader.GetInt32(0).ToString(), 
                                reader.GetString(1),          
                                reader.GetString(2), 
                                reader.GetString(3)          
                            };

                            ListViewItem item = new ListViewItem(row);
                            listView1.Items.Add(item);
                            Debug.WriteLine($"Загружена заявка ID: {row[0]}, Статус: {row[3]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заявок: " + ex.Message);
            }
        }
    }


    public MainForm(string login, string role)
    {
        InitializeComponent();
        
        label1.Text = login;
        
        label3.Visible = false;
        label4.Visible = false;
        label5.Text = role;
        label6.Visible = false;
        
        textBox1.Visible = false;
        textBox6.Visible = false;
        
        richTextBox1.Visible = false;
        
        button2.Visible = false;
        button3.Visible = false;
        
        listView1.View = View.Details;
        listView1.FullRowSelect = true;
        listView1.GridLines = true;

        listView1.Columns.Add("ID", 50);
        listView1.Columns.Add("Тип устройства", 150);
        listView1.Columns.Add("Модель", 150);
        listView1.Columns.Add("Статус", 150);

        this.Controls.Add(listView1);
        
        LoadUserRequests();
    }
    

    private void button1_Click(object sender, EventArgs e)
    {
        bool isVisible = !label3.Visible;
        
        foreach (Control control in this.Controls)
        {
            if (control == label1 || control == label2 || control == label8 || control == label5 || control == textBox2 || control == button1)
                continue;
            
            if (control is Label || control is TextBox || control is RichTextBox || control is Button)
            {
                control.Visible = isVisible;
            }
        }
    }

    public void button2_Click(object sender, EventArgs e) {
        
        var (userId, clientFullName, phoneNumber) = GetUserInfo(label1.Text);

        if (userId == -1)
        {
            MessageBox.Show("Ошибка: Пользователь не найден!");
            return;
        }

        string applianceType = textBox1.Text;
        string applianceModel = textBox6.Text;
        string problemDescription = richTextBox1.Text;
        int statusId = 2; 
        DateTime startDate = DateTime.Now;
        
        Debug.WriteLine($"Добавление новой заявки: Тип={applianceType}, Модель={applianceModel}, Проблема={problemDescription}");

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = @"
                    INSERT INTO repair_requests (
                        clientid, 
                        hometechtype, 
                        hometechmodel, 
                        problemdescription, 
                        requeststatusid, 
                        startdate
                    ) 
                    VALUES (
                        @UserId, 
                        @ApplianceType, 
                        @ApplianceModel, 
                        @ProblemDescription, 
                        @StatusId, 
                        @StartDate
                    )";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ApplianceType", applianceType);
                    cmd.Parameters.AddWithValue("@ApplianceModel", applianceModel);
                    cmd.Parameters.AddWithValue("@ProblemDescription", problemDescription);
                    cmd.Parameters.AddWithValue("@StatusId", statusId);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine($"Заявка успешно добавлена для пользователя ID: {userId}");
                        MessageBox.Show("Заявка успешно добавлена!");
                        LoadUserRequests();
                    }
                    else
                    {
                        Debug.WriteLine($"Ошибка при добавлении заявки для пользователя ID: {userId}");
                        MessageBox.Show("Ошибка при добавлении заявки.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при добавлении заявки: {ex.Message}");
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }


    private void textBox2_TextChanged(object sender, EventArgs e)
    {
        string query = textBox2.Text.ToLower();

        foreach (ListViewItem item in listView1.Items)
        {
            item.BackColor = Color.White;

            foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
            {
                if (subItem.Text.ToLower().Contains(query))
                {
                    item.BackColor = Color.GreenYellow; 
                    break;
                }
            }
        }
    }

    public string GetProblemDescription(int requestId)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT problemdescription FROM repair_requests WHERE requestid = @RequestId";
        
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                return cmd.ExecuteScalar()?.ToString() ?? "";
            }
        }
    }
    
    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 0)
        {
            ListViewItem selectedItem = listView1.SelectedItems[0];
            selectedRequestId = int.Parse(selectedItem.SubItems[0].Text); 
            richTextBox1.Text = GetProblemDescription(selectedRequestId);
            button3.Visible = true; 
        }
    }

    
    private void button3_Click(object sender, EventArgs e)
    {
        if (selectedRequestId == -1)
        {
            MessageBox.Show("Выберите заявку для редактирования.");
            return;
        }

        string newDescription = richTextBox1.Text;

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "UPDATE repair_requests SET problemdescription = @NewDescription WHERE requestid = @RequestId";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewDescription", newDescription);
                    cmd.Parameters.AddWithValue("@RequestId", selectedRequestId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Описание обновлено!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: заявка с таким ID не найдена!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении: " + ex.Message);
            }
        }

        LoadUserRequests();
    }
}