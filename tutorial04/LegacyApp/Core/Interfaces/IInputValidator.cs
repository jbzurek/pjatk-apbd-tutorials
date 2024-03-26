namespace LegacyApp.Core.Interfaces;

public interface IInputValidator
{
    public bool ValidateName(string firstName, string lastName);
    public bool ValidateEmail(string email);
    public bool ValidateAge(DateTime dateOfBirth);
}