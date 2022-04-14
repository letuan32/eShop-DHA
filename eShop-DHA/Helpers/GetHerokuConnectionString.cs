namespace eShop_DHA.Helpers;

public static class HerokuHelper
{
    public static string GetConnectionString()
    {
        var dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        var databaseUri = new Uri(dbUrl);
        string dbName = databaseUri.LocalPath.TrimStart('/');
        string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
        var connectionString =
            $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={dbName};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        return connectionString;
    }
   
}