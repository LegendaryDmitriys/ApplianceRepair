namespace WinFormsApp1;
using Npgsql;

public partial class Form1 : Form
{
    private string connectionString = "Server=localhost;Database=Accounting;User Id=postgres;Password=admin;"; 
    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string login = textBox1.Text;
        string password = textBox2.Text;
        
        if (string.IsNullOrWhiteSpace(login) && string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Введите логин и пароль");
            return;
        }
        
        var (IsAuthenticate, role) = this.IsAuthenticate(login, password);
        
        if (IsAuthenticate)
        {
            if (role == "Заказчик")
            {
                MainForm newForm = new MainForm(login, role);
                newForm.Show();
            }
            else if (role == "Мастер")
            {
                FormMaster newFormMaster = new FormMaster();
                newFormMaster.Show();
            }
            else
            {
                FormAdmin newFormAdmin = new FormAdmin();
                newFormAdmin.Show();
            }
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль");
        }
        
        
    }

    private (bool IsAuthenticate, string role) IsAuthenticate(string login, string password)
    {
        bool isAuthenticated = false;
        string role = string.Empty;
        
        
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                
                string query = "SELECT type FROM users WHERE login = @Login AND password = @Password";
                
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isAuthenticated = true;
                            role = reader.GetString(0); 
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        return (isAuthenticated, role);
    }
    
}
