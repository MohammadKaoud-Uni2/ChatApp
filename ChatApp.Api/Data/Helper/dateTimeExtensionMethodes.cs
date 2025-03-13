using System.Runtime.CompilerServices;

namespace ChatApp.Api.Data.Helper
{
    public  static class dateTimeExtensionMethodes
    {
        public static int CalculateAge(this DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;

        }
    }
}
