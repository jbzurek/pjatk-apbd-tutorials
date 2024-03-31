using LegacyApp.Core.Models;

namespace LegacyApp.Core.Managers;

public class CreditLimitManager
{
    public static void SetCreditLimit(Client client, User user)
    {
        switch (client.Type)
        {
            case "VeryImportantClient":
                user.HasCreditLimit = false;
                break;
            case "ImportantClient":
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit *= 2;
                    user.CreditLimit = creditLimit;
                }

                break;
            }
            default:
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }

                break;
            }
        }
    }
}