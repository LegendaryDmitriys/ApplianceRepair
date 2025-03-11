using Npgsql;
using WinFormsApp1;

namespace UnitTest;
using Moq;


public class UnitTest1
{
    private string testConnectionString = "Server=localhost;Database=Accounting;User Id=postgres;Password=admin;";
        
    [Fact]
    public void LoadRequestsDbTest()
    {
        var formAdmin = new FormAdmin();
        formAdmin.connectionString = testConnectionString;


        formAdmin.LoadRequests();


        Assert.NotEmpty(formAdmin.listView1.Items);
    }
    
    [Fact]
    public void GetAverageCompletionTime()
    {

        var formAdmin = new FormAdmin();
        formAdmin.connectionString = testConnectionString;
        
        TimeSpan result = formAdmin.GetAverageCompletionTime();


        Assert.Equal(67, result.Days); 
    }
    
    [Fact]
    public void GetFaultTypeStatistics()
    {
        var formAdmin = new FormAdmin();
        formAdmin.connectionString = testConnectionString;


        var result = formAdmin.GetFaultTypeStatistics();
        
        Assert.Equal(6, result.Count);
        Assert.Equal(2, result["Не работает"]);
        Assert.Equal(3, result["Перестал работать"]);
    }
    
    [Fact]
    public void GetCompletedRequestsCount()
    {
        var formAdmin = new FormAdmin();
        formAdmin.connectionString = testConnectionString;
        
        int result = formAdmin.GetCompletedRequestsCount();
        
        Assert.Equal(3, result); 
    }
    
    [Fact]
    public void GetProblemDescriptionIdValid()
    {

        var mainForm = new MainForm("1", "Заказчик");
        mainForm.connectionString = testConnectionString;
        
        string description = mainForm.GetProblemDescription(1); 
        
        Assert.Equal("Перестал работать", description);
    }
    
    [Fact]
    public void GetProblemDescriptionIdInvalid()
    {
        var mainForm = new MainForm("test", "test");
        mainForm.connectionString = testConnectionString;
        
        string description = mainForm.GetProblemDescription(999); 
        
        Assert.Equal("", description);
    }
}