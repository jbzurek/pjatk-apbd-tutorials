using LegacyApp.Core.Interfaces;

namespace LegacyApp.Core.Validators.Users;

public class InputValidator : IInputValidator
{
    public bool ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }

        return true;
    }

    public bool ValidateEmail(string email)
    {
        if (!email.Contains('@') && !email.Contains('.'))
        {
            return false;
        }

        return true;
    }

    public bool ValidateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < 21)
        {
            return false;
        }

        return true;
    }
}