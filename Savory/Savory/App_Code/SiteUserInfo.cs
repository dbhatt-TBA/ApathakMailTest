using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for SiteUserInfo
/// </summary>
public class SiteUserInfo
{
    public SiteUserInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Properties declaration and Properties Assignment

    //USER ID
    private long _userID;
    public long UserID
    {
        get { return _userID; }
        set { _userID = value; }
    }

    //CUST ID
    private long _custID;
    public long CustID
    {
        get { return _custID; }
        set { _custID = value; }
    }

    //USER NAME
    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }

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

    private string _bankName;
    public string BankName
    {
        get { return _bankName; }
        set { _bankName = value; }
    }

    private string _institutionName;
    public string InstituteType
    {
        get { return _institutionName; }
        set { _institutionName = value; }
    }

    //EMAIL ID
    private string _emailID;
    public string EmailID
    {
        get { return _emailID; }
        set { _emailID = value; }
    }

    //IS USER IS LOGIN OR USER IS SITE USER   0 ==> NONE OF THIS ; 1 ==> USER IS LOGIN ; 2 ==> USER IS SITE USER
    private int _UserIsLoginOrIsSiteUser;
    public int UserIsLoginOrIsSiteUser
    {
        get { return _UserIsLoginOrIsSiteUser; }
        set { _UserIsLoginOrIsSiteUser = value; }
    }

    private int _loginTypeID;
    public int LoginTypeID
    {
        get { return _loginTypeID; }
        set { _loginTypeID = value; }
    }

    private string _lastActivityDate;
    public string LastActivityDate
    {
        get { return _lastActivityDate; }
        set { _lastActivityDate = value; }
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

    public SiteUserInfo(long lUserID, string strUserName, string strEmailID, int intUserLoginOrSiteUser, int intLoginTypeId, string strLastActivityDate, long strCustomerID)
    {
        this.UserID = lUserID;
        this.UserName = strUserName;
        this.EmailID = strEmailID;
        this.UserIsLoginOrIsSiteUser = intUserLoginOrSiteUser;
        this.LoginTypeID = intLoginTypeId;
        this.LastActivityDate = strLastActivityDate;
        this.CustID = strCustomerID;
    }

    public SiteUserInfo(int intUserLoginOrSiteUser)
    {
        this.UserIsLoginOrIsSiteUser = intUserLoginOrSiteUser;
    }

    public SiteUserInfo(string pFirstName, string pLastName, string pCompanyName,
                        string pInstitutionType, string pEmail, string pPhone,
                        string pAddress, string pCity, string pCountry,
                        string pState, string pZip, string pSecurityQuestion, string pSecurityAnswer)
    {
        this.FirstName = pFirstName;
        this.LastName = pLastName;
        this.BankName = pCompanyName;
        this.InstituteType = pInstitutionType;
        this.EmailID = pEmail;
        this.Phone = pPhone;
        this.Address = pAddress;
        this.City = pCity;
        this.Country = pCountry;
        this.State = pState;
        this.ZipCode = pZip;
        this.SecurityQuestion = pSecurityQuestion;
        this.SecurityAnswer = pSecurityAnswer;
    }

    //Billing constructor
    public SiteUserInfo(string pEmail, string pBillingPhone,
                        string pBillingAddress, string pBillingCity, string pBillingCountry,
                        string pBillingZip, string pBillingSecurityQuestion, string pBillingSecurityAnswer,
                        string pLastActioncode, Int16 pSubscriptionMonth)
    {
        this.EmailID = pEmail;
        this.BillingPhone = pBillingPhone;
        this.BillingAddress1 = pBillingAddress;
        this.BillingCity = pBillingCity;
        this.BillingCountry = pBillingCountry;
        this.BillingZip = pBillingZip;
        this.BillingSecurityQuestion = pBillingSecurityQuestion;
        this.BillingSecurityAnswer = pBillingSecurityAnswer;
        this.BillingLastAccessCode = "";
        this.BillingSubscriptionGapMonth = 0;        
    }

    #endregion


}
