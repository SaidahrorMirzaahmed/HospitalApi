using HospitalApi.Domain.Enums;
using System.Text.RegularExpressions;

namespace HospitalApi.Service.Helpers;

public static class ValidationHelper
{

    public static bool IsPhoneValid(string phone)
    {
        string pattern = @"^\+998\d{9}$";

        return Regex.IsMatch(phone, pattern);
    }

    public static bool IsValidSpecialist(UserSpecialists specialist)
    {
        var numberOfElements = Enum.GetValues(typeof(UserSpecialists)).Length;

        return Convert.ToInt32(specialist) < numberOfElements;
    }

}