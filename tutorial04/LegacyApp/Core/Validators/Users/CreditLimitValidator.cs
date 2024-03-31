using LegacyApp.Core.Interfaces;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Validators.Users;

public class CreditLimitValidator : ICreditLimitValidator
{
    public bool IsCreditLimitValid(User user)
    {
        if (user.HasCreditLimit && user.CreditLimit < 500)
        {
            return false;
        }

        return true;
    }
}