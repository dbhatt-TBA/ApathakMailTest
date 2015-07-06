using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASE
{
    public static class Enums
    {
        public enum Roles
        {
            Guest = 0,
            SuperUser = 1,
            ComopanyAdmin = 2,
            NormalUser = 3
        }

        public enum ValidationFlag
        {
            Success = 0,
            UserNotFound = -2,
            UserLockedOutByHelpDesk = -8,
            PasswordWrongUserLockedOut = -5,
            PasswordWrongLastTime = -4,
            PasswordWrong = -3,
            PasswordMustChange = -6,
            MaxLoginExceeded = -7
        }
    }
}
