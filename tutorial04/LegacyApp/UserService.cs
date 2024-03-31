using LegacyApp.Core.Interfaces;
using LegacyApp.Core.Managers;
using LegacyApp.Core.Models;
using LegacyApp.Core.Validators;
using LegacyApp.Core.Validators.Users;

namespace LegacyApp;

public class UserService
{
    private readonly IInputValidator _inputValidator = new InputValidator();
    private readonly ICreditLimitValidator _creditLimitValidator = new CreditLimitValidator();

    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (!_inputValidator.ValidateName(firstName, lastName))
        {
            return false;
        }

        if (!_inputValidator.ValidateEmail(email))
        {
            return false;
        }

        if (!_inputValidator.ValidateAge(dateOfBirth))
        {
            return false;
        }

        var clientRepository = new ClientRepository();
        var client = clientRepository.GetById(clientId);

        var user = new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };
        
        CreditLimitManager.SetCreditLimit(client, user);

        if (!_creditLimitValidator.IsCreditLimitValid(user))
        {
            return false;
        }

        UserDataAccess.AddUser(user);
        return true;
    }
}