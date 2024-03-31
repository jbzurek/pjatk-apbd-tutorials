using LegacyApp.Core.Models;

namespace LegacyApp.Core.Interfaces;

public interface ICreditLimitValidator
{
    public bool IsCreditLimitValid(User user);
}