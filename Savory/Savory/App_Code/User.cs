using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASE
{
    public class User
    {
        #region User creation
        //USER ID > Nullable - to use in case of new user creation.
        private long? _userID;
        public long? UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        //EMAIL ID
        private string _loginID;
        public string LoginID
        {
            get { return _loginID; }
            set { _loginID = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #endregion

        #region Configuration/Permissions

        private int _role;
        public int Role
        {
            get { return _role; }
            set { _role = value; }
        }

        private bool _trusted;  //Default=0
        public bool IsTrusted
        {
            get { return _trusted; }
            set { _trusted = value; }
        }

        private bool _isEnabled;  //Default=1
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        private int _retryCount;
        public int RetryCount
        {
            get { return _retryCount; }
            set { _retryCount = value; }
        }

        private int _planId;    //Default=1
        public int PlanID
        {
            get { return _planId; }
            set { _planId = value; }
        }

        private int _imageListPermissions;    //Default=2
        public int ImageListPermissions
        {
            get { return _imageListPermissions; }
            set { _imageListPermissions = value; }
        }

        private bool _mustChangePwd;  //Default=0
        public bool MustChangePassword
        {
            get { return _mustChangePwd; }
            set { _mustChangePwd = value; }
        }

        private string _allowdLoginTimes; //Default=''
        public string AllowedLoginTimes
        {
            get { return _allowdLoginTimes; }
            set { _allowdLoginTimes = value; }
        }

        private int _maxSimultaneousLogin;    //Default=1 or 3
        public int MaxSimultaneousLogin
        {
            get { return _maxSimultaneousLogin; }
            set { _maxSimultaneousLogin = value; }
        }

        private string _expiratinoDate; //Default=NULL
        public string ExpirationDate
        {
            get { return _expiratinoDate; }
            set { _expiratinoDate = value; }
        }

        private int _createdBy;
        public int CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        #endregion

        #region Basic/Personal Info

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _primaryEmail;
        public string PrimaryEmailID
        {
            get { return _primaryEmail; }
            set { _primaryEmail = value; }
        }

        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        private string _state;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _zipcode;
        public string ZipCode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }
        private string _secQuestion;
        public string SecurityQuestion
        {
            get { return _secQuestion; }
            set { _secQuestion = value; }
        }
        private string _secAnswer;
        public string SecurityAnswer
        {
            get { return _secAnswer; }
            set { _secAnswer = value; }
        }

        private string _billingPhone;
        public string BillingPhone
        {
            get { return _billingPhone; }
            set { _billingPhone = value; }
        }

        private string _billingAddress1;
        public string BillingAddress1
        {
            get { return _billingAddress1; }
            set { _billingAddress1 = value; }
        }

        private string _billingAddress2;
        public string BillingAddress2
        {
            get { return _billingAddress2; }
            set { _billingAddress2 = value; }
        }

        private string _billingCity;
        public string BillingCity
        {
            get { return _billingCity; }
            set { _billingCity = value; }
        }

        private string _billingCountry;
        public string BillingCountry
        {
            get { return _billingCountry; }
            set { _billingCountry = value; }
        }

        private string _billingZip;
        public string BillingZip
        {
            get { return _billingZip; }
            set { _billingZip = value; }
        }

        private string _billingSecurityQuestion;
        public string BillingSecurityQuestion
        {
            get { return _billingSecurityQuestion; }
            set { _billingSecurityQuestion = value; }
        }

        private string _billingSecurityAnswer;
        public string BillingSecurityAnswer
        {
            get { return _billingSecurityAnswer; }
            set { _billingSecurityAnswer = value; }
        }

        private string _billingLastAccessCode;
        public string BillingLastAccessCode
        {
            get { return _billingLastAccessCode; }
            set { _billingLastAccessCode = value; }
        }

        private Int16 _billingSubscriptionGapMonth;
        public Int16 BillingSubscriptionGapMonth
        {
            get { return _billingSubscriptionGapMonth; }
            set { _billingSubscriptionGapMonth = value; }
        }

        private string _lastActivityDate;
        public string LastActivityDate
        {
            get { return _lastActivityDate; }
            set { _lastActivityDate = value; }
        }

        #endregion
    }
}
