using LegacyApp.Core.Models;

namespace LegacyApp;

public static class UserDataAccess
{
    /// <summary>
    /// This service is simulating saving user to remote database
    /// </summary>
    public static void AddUser(User user)
    {
        int randomWaitTime = new Random().Next(1000);
        Thread.Sleep(randomWaitTime);
        Console.WriteLine($"Added the user {user} successfully");
    }
}