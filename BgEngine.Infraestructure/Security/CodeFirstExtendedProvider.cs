//==============================================================================
// This file is part of BgEngine.
//
// BgEngine is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BgEngine is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BgEngine. If not, see <http://www.gnu.org/licenses/>.
//==============================================================================
// Copyright (c) 2011 Yago Pérez Vázquez
// Version: 1.0
//==============================================================================

using System;
using System.Web.Security;

namespace BgEngine.Infraestructure.Security
{
    public abstract class CodeFirstExtendedProvider : MembershipProvider
    {

        private const int OneDayInMinutes = 24 * 60;
        public virtual string CreateAccount(string userName, string password, string email)
        {
            return CreateAccount(userName, password, email, requireConfirmationToken: false);
        }

        public abstract string CreateAccount(string userName, string password, string email, bool requireConfirmationToken);

        public abstract string CreateAccount(string userName, string password, string email, string firstname, string lastname, string timezone, string culture, bool requireConfirmationToken);

        public abstract string ExtendedValidateUser(string userNameOrEmail, string password);

        public abstract bool ConfirmAccount(string accountConfirmationToken);

        public abstract bool DeleteAccount(string userName);

        public virtual string GeneratePasswordResetToken(string userName)
        {
            return GeneratePasswordResetToken(userName, tokenExpirationInMinutesFromNow: OneDayInMinutes);
        }

        public abstract string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow);

        public abstract Guid GetUserIdFromPasswordResetToken(string token);
        public abstract bool IsConfirmed(string userName);
        public abstract bool ResetPasswordWithToken(string token, string newPassword);
        public abstract int GetPasswordFailuresSinceLastSuccess(string userName);
        public abstract DateTime GetCreateDate(string userName);
        public abstract DateTime GetPasswordChangedDate(string userName);
        public abstract DateTime GetLastPasswordFailureDate(string userName);

    }
}