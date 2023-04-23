
using System.Data.SqlClient;
using System.Reflection;

public class Categories
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public byte[] Picture { get; set; }
    //from https://codverter.com/blog/articles/tutorials/20190715-create-class-from-database-table.html;

}
class Reflection
{
    static void Main(string[] args)
    {

        int categoryId;
        string categoryName, description;

        String connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true ";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();



        Categories categories = new Categories();
        PropertyInfo[] propertyInfos = categories.GetType().GetProperties();


        string fileText = string.Empty;
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if (propertyInfo.PropertyType == typeof(Int16))
            {
                fileText += $"{propertyInfo.Name} = dr[{propertyInfo.Name}] == DBNull.Value ? default : (short)dr[{propertyInfo.Name}],\n";      
            }
            if (propertyInfo.PropertyType == typeof(Int32))
            {
                fileText += $"{propertyInfo.Name} = dr[{propertyInfo.Name}] == DBNull.Value ? default : (int)dr[{propertyInfo.Name}],\n";
            }
            if (propertyInfo.PropertyType == typeof(String))
            {
                fileText += $"{propertyInfo.Name} = string.IsNullOrEmpty(dr[{propertyInfo.Name}].ToString()) ? string.Empty : dr[{propertyInfo.Name}].ToString(),\n";
            }
            if (propertyInfo.PropertyType == typeof(Double))
            {
                fileText += $"{propertyInfo.Name} = dr[{propertyInfo.Name}] == DBNull.Value ? default : (double)dr[{propertyInfo.Name}],\n";
            }
        }
        File.WriteAllText("../../../reflection.txt", fileText);  // There is any way to go where function started ?
        
        Console.WriteLine("Program Ended.");

        Console.Read();
    }
}

