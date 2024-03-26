using LegacyApp;

namespace LegacyAppTest;

public class UserServiceTests 
{
    [Fact]
    public void AddUser_Should_Return_False_When_Missing_FirstName()
    {
        // Arrange
        var service = new UserService();
        const string firstName = "";
        const string lastName = "Doe";
        const string email = "johndoe@gmail.com";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var result = service.AddUser(firstName, lastName, email, dateOfBirth, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Missing_LastName()
    {
        // Arrange
        var service = new UserService();
        const string firstName = "John";
        const string lastName = "";
        const string email = "johndoe@gmail.com";
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var result = service.AddUser(firstName, lastName, email, dateOfBirth, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Invalid()
    {
        // Arrange
        var service = new UserService();
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoegmailcom";
        var dateOfBirth = new DateTime(1990, 1, 1);
        const int clientId = 1;

        // Act
        var result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Throw_Exception_When_No_Credit_Exists_For_User()
    {
        // Arrange
        var service = new UserService();

        // Act
        Assert.Throws<ArgumentException>(() =>
        {
            _ = service.AddUser("John", "Andrzejewicz", "andrzejewicz@wp.pl", new DateTime(1980, 1, 1), 6);
        });
    }

    [Fact]
    public void AddUser_Should_Return_False_When_User_Is_Under_21()
    {
        // Arrange
        var service = new UserService();
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "johndoe@gmail.com";
        var dateOfBirth = new DateTime(2010, 1, 1);

        // Act
        var result = service.AddUser(firstName, lastName, email, dateOfBirth, 1);

        // Assert
        Assert.False(result);
    }
}