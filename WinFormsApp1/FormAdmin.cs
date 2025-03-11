using System.Data;

namespace WinFormsApp1;
using Npgsql;

public partial class FormAdmin : Form
{
    public string connectionString = "Server=localhost;Database=Accounting;User Id=postgres;Password=admin;";
    private int selectedRequestId = -1;
    public FormAdmin()
    {
        InitializeComponent();
        
        listView1.View = View.Details;
        listView1.FullRowSelect = true;
        listView1.GridLines = true;

        listView1.Columns.Add("ID", 50);
        listView1.Columns.Add("Дата начала", 150);
        listView1.Columns.Add("Вид", 150);
        listView1.Columns.Add("Модель", 150);
        listView1.Columns.Add("Проблема пользователя", 150);
        listView1.Columns.Add("Статус", 150);
        listView1.Columns.Add("Дата завершения", 150);
        listView1.Columns.Add("Комментарии мастера", 150);
        listView1.Columns.Add("Мастер", 150);
        listView1.Columns.Add("Клиент", 150);
        this.Controls.Add(listView1);
        
        string[] statusRequest = new string[] { "Готова к выдаче", "В процессе ремонта", "Новая заявка" };
        
        comboBox1.Items.AddRange(statusRequest);
        comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        this.Controls.Add(this.comboBox1);

        LoadUsers();
        LoadMasters();
        LoadRequests();
    }
    
    public void LoadRequests() {
        listView1.Items.Clear();

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = @"
                    SELECT 
                        rr.requestid, 
                        rr.startdate, 
                        rr.hometechtype, 
                        rr.hometechmodel, 
                        rr.problemdescription, 
                        rs.statusname, 
                        rr.completiondate, 
                        rr.repairparts, 
                        m.fio AS master_name, 
                        c.fio AS client_name
                    FROM 
                        repair_requests rr
                    INNER JOIN 
                        request_statuses rs 
                    ON 
                        rr.requeststatusid = rs.statusid
                    LEFT JOIN 
                        users m 
                    ON 
                        rr.masterid = m.userid
                    LEFT JOIN 
                        users c 
                    ON 
                        rr.clientid = c.userid";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] row = new string[]
                            {
                                reader.GetInt32(0).ToString(), 
                                reader.GetDateTime(1).ToString("yyyy-MM-dd"), 
                                reader.GetString(2), 
                                reader.GetString(3),
                                reader.GetString(4), 
                                reader.IsDBNull(5) ? "Нет данных" : reader.GetString(5), 
                                reader.IsDBNull(6) ? "Нет данных" : reader.GetDateTime(6).ToString("yyyy-MM-dd"),
                                reader.IsDBNull(7) ? "Нет данных" : reader.GetString(7), 
                                reader.IsDBNull(8) ? "Нет данных" : reader.GetString(8), 
                                reader.IsDBNull(9) ? "Нет данных" : reader.GetString(9) 
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
    
    
    private void LoadRequestDetails(int requestId)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT 
                rr.startdate, 
                rr.hometechtype, 
                rr.hometechmodel, 
                rr.problemdescription, 
                rs.statusname, 
                rr.completiondate, 
                rr.repairparts, 
                rr.masterid, 
                rr.clientid
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
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox1.Text = reader.GetString(1); 
                        textBox2.Text = reader.GetString(2); 
                        richTextBox1.Text = reader.GetString(3); 
                        comboBox1.Text = reader.GetString(4); 
                        richTextBox2.Text = reader.IsDBNull(6) ? "Нет данных" : reader.GetString(6); 
                        comboBox2.Text = reader.IsDBNull(7) ? "Нет данных" : reader.GetInt32(7).ToString(); 
                        comboBox3.Text = reader.IsDBNull(8) ? "Нет данных" : reader.GetInt32(8).ToString(); 
                    }
                }
            }
        }
    }
    
    private void LoadMasters()
    {
        comboBox2.Items.Clear();
    
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT userid, fio FROM users where \"type\" = 'Мастер'";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(new ComboBoxItem(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
        }
    }
    
    private void LoadUsers()
    {
        comboBox3.Items.Clear();
    
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT userid, fio FROM users where \"type\" = 'Заказчик'";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(new ComboBoxItem(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
        }
    }
    

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count > 0)
        {
            ListViewItem selectedItem = listView1.SelectedItems[0];
            selectedRequestId = int.Parse(selectedItem.SubItems[0].Text); 
            LoadRequestDetails(selectedRequestId);
            button1.Visible = true;
            button2.Visible = true;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (selectedRequestId == -1)
        {
            MessageBox.Show("Выберите заявку для редактирования!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        UpdateRequest(selectedRequestId);
    }

    private void button3_Click(object sender, EventArgs e)
    {
        if (selectedRequestId == -1)
        {
            MessageBox.Show("Выберите заявку для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult result = MessageBox.Show(
            "Вы уверены, что хотите удалить заявку?", "Подтверждение", 
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            DeleteRequest(selectedRequestId);
        }
    }

    private void DeleteRequest(int requestId)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM repair_requests WHERE requestid = @RequestId";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заявка удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRequests(); 
                    }
                    else
                    {
                        MessageBox.Show("Заявка не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении заявки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
    private void UpdateRequest(int requestId) {
        
        int statusId = GetStatusId(comboBox1.SelectedItem.ToString());
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = @"
                    UPDATE repair_requests 
                    SET 
                        hometechtype = @Type, 
                        hometechmodel = @Model, 
                        problemdescription = @Problem, 
                        requeststatusid = @StatusId, 
                        repairparts = @Parts, 
                        masterid = @Master, 
                        clientid = @Client
                    WHERE 
                        requestid = @RequestId";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RequestId", requestId);
                    cmd.Parameters.AddWithValue("@Type", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Model", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Problem", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@StatusId", statusId);
                    cmd.Parameters.AddWithValue("@Parts", richTextBox2.Text);
                    int masterId = comboBox2.SelectedItem is ComboBoxItem masterItem ? masterItem.Id : -1;
                    int clientId = comboBox3.SelectedItem is ComboBoxItem clientItem ? clientItem.Id : -1;
                    cmd.Parameters.AddWithValue("@Master", masterId == -1 ? DBNull.Value : (object)masterId);
                    cmd.Parameters.AddWithValue("@Client", clientId == -1 ? DBNull.Value : (object)clientId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заявка обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRequests();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка обновления заявки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при редактировании заявки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


    private void button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || 
            string.IsNullOrWhiteSpace(richTextBox1.Text) || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
        {
            MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        string type = textBox1.Text;
        string model = textBox2.Text;
        string problemDescription = richTextBox1.Text;
        string status = comboBox1.SelectedItem.ToString();
    

        int masterId = comboBox2.SelectedItem is ComboBoxItem masterItem ? masterItem.Id : -1;
        int clientId = comboBox3.SelectedItem is ComboBoxItem clientItem ? clientItem.Id : -1;


        CreateRequest(type, model, problemDescription, status, masterId, clientId);
    }
    
    private void CreateRequest(string type, string model, string problemDescription, string status, int masterId, int clientId) {
        
        int statusId = GetStatusId(status);

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = @"
                    INSERT INTO repair_requests (
                        startdate, 
                        hometechtype, 
                        hometechmodel, 
                        problemdescription, 
                        requeststatusid, 
                        masterid, 
                        clientid
                    ) 
                    VALUES (
                        @StartDate, 
                        @Type, 
                        @Model, 
                        @Problem, 
                        @StatusId, 
                        @Master, 
                        @Client
                    )";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Model", model);
                    cmd.Parameters.AddWithValue("@Problem", problemDescription);
                    cmd.Parameters.AddWithValue("@StatusId", statusId);
                    cmd.Parameters.AddWithValue("@Master", masterId == -1 ? DBNull.Value : (object)masterId);
                    cmd.Parameters.AddWithValue("@Client", clientId == -1 ? DBNull.Value : (object)clientId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заявка успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRequests();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении заявки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении заявки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private int GetStatusId(string statusName)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT statusid FROM request_statuses WHERE statusname = @StatusName";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StatusName", statusName);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }
    }

    private void ShowStatistics()
    {
        int completedRequestsCount = GetCompletedRequestsCount();
        TimeSpan averageCompletionTime = GetAverageCompletionTime();
        Dictionary<string, int> faultTypeStatistics = GetFaultTypeStatistics();
        
        FormStatistics statisticsForm = new FormStatistics(completedRequestsCount, averageCompletionTime, faultTypeStatistics);
        statisticsForm.ShowDialog();
    }
    
    public int GetCompletedRequestsCount()
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT COUNT(*) 
            FROM repair_requests 
            WHERE requeststatusid = 3 AND completiondate IS NOT NULL";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
                
            }
        }
    }

    public TimeSpan GetAverageCompletionTime()
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT AVG(
                (EXTRACT(EPOCH FROM completiondate) - EXTRACT(EPOCH FROM startdate))
            ) 
            FROM repair_requests
            WHERE requeststatusid = 3 
                AND completiondate IS NOT NULL 
                AND startdate IS NOT NULL";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    double seconds = Convert.ToDouble(result);
                    return TimeSpan.FromSeconds(seconds);
                }
                return TimeSpan.Zero;
            }
        }
    }



    public Dictionary<string, int> GetFaultTypeStatistics()
    {
        var statistics = new Dictionary<string, int>();

        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT problemdescription, COUNT(*) FROM repair_requests GROUP BY problemdescription";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string problemDescription = reader.GetString(0);
                        int count = reader.GetInt32(1);
                        statistics[problemDescription] = count;
                    }
                }
            }
        }

        return statistics;
    }
    private void button4_Click(object sender, EventArgs e)
    {
        ShowStatistics();
    }

    private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }
}

