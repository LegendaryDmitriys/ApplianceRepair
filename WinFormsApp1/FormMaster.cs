namespace WinFormsApp1;
using Npgsql;

public partial class FormMaster : Form
{
    private string connectionString = "Server=localhost;Database=Accounting;User Id=postgres;Password=admin;";
    private int selectedRequestId = -1;
    public FormMaster()
    {
        InitializeComponent();
        
        listView1.View = View.Details;
        listView1.FullRowSelect = true;
        listView1.GridLines = true;

        listView1.Columns.Add("ID", 50);
        listView1.Columns.Add("Вид", 150);
        listView1.Columns.Add("Модель", 150);
        listView1.Columns.Add("Проблема пользователя", 150);
        listView1.Columns.Add("Заказанные запчасти и материалы", 150);
        listView1.Columns.Add("Статус", 150);
        listView1.Columns.Add("Клиент", 150);
        this.Controls.Add(listView1);
        
        button1.Visible = false;
        button2.Visible = false;
        
        richTextBox1.Visible = false;
        comboBox1.Visible = false;
        
        string[] statusRequest = new string[] { "Готова к выдаче", "В процессе ремонта", "Новая заявка" };
        
        comboBox1.Items.AddRange(statusRequest);
        comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        this.Controls.Add(this.comboBox1);
        
        LoadRequests();
    }
    
     private void LoadRequests() {
        listView1.Items.Clear();

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
                        rr.problemdescription, 
                        rs.statusname, 
                        rr.repairparts, 
                        u.fio AS client_name
                    FROM 
                        repair_requests rr
                    INNER JOIN 
                        request_statuses rs 
                    ON 
                        rr.requeststatusid = rs.statusid
                    INNER JOIN 
                        users u 
                    ON 
                        rr.clientid = u.userid";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] row = new string[]
                            {
                                reader.GetInt32(0).ToString(),
                                reader.GetString(1),          
                                reader.GetString(2),          
                                reader.GetString(3),          
                                reader.IsDBNull(5) ? "Нет данных" : reader.GetString(5), 
                                reader.GetString(4),         
                                reader.IsDBNull(6) ? "Нет данных" : reader.GetString(6) 
                            };

                            ListViewItem item = new ListViewItem(row);
                            listView1.Items.Add(item);
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
     
    private string GetRepairParts(int requestId)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT repairparts FROM repair_requests WHERE requestid = @RequestId";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@RequestId", requestId);
                return cmd.ExecuteScalar()?.ToString() ?? "";
            }
        }
    }
    
    private string GetStatusRequest(int requestId)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT 
                rs.statusname 
            FROM 
                repair_requests rr
            INNER JOIN 
                request_statuses rs 
            ON 
                rr.requeststatusid = rs.statusid
            WHERE 
                rr.requestid = @RequestId";

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
            richTextBox1.Text = GetRepairParts(selectedRequestId);
            comboBox1.Text = GetStatusRequest(selectedRequestId);
            button1.Visible = true;
            button2.Visible = true;
            richTextBox1.Visible = true;
            comboBox1.Visible = true;
        }
    }
    
    private void button2_Click(object sender, EventArgs e)
    {
        if (selectedRequestId == -1)
        {
            MessageBox.Show("Выберите заявку для добавления комментария.");
            return;
        }

        string repairParts = richTextBox1.Text;

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "UPDATE repair_requests SET repairparts = @RepairParts WHERE requestid = @RequestId";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RepairParts", repairParts);
                    cmd.Parameters.AddWithValue("@RequestId", selectedRequestId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Комментарий добавлен!");
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
        LoadRequests();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (selectedRequestId == -1)
        {
            MessageBox.Show("Выберите заявку для изменения статуса.");
            return;
        }

        string requestStatus = comboBox1.Text;
        int statusId;

        switch (requestStatus)
        {
            case "Готова к выдаче":
                statusId = 3;
                break;
            case "В процессе ремонта":
                statusId = 1;
                break;
            case "Новая заявка":
                statusId = 2;
                break;
            default:
                MessageBox.Show("Неизвестный статус!");
                return;
        }

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();

                string query = requestStatus == "Готова к выдаче"
                    ? "UPDATE repair_requests SET requeststatusid = @StatusId, completiondate = @CompletionDate WHERE requestid = @RequestId"
                    : "UPDATE repair_requests SET requeststatusid = @StatusId WHERE requestid = @RequestId";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StatusId", statusId);
                    cmd.Parameters.AddWithValue("@RequestId", selectedRequestId);

                    if (requestStatus == "Готова к выдаче")
                    {
                        cmd.Parameters.AddWithValue("@CompletionDate", DateTime.Now);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Статус изменен!");
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

        LoadRequests();
    }
}