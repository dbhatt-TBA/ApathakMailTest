using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Cadmin
{
    #region Constructor
    public Cadmin()
    {

        this._webSiteContentId = -1;
    }
    #endregion

    #region Private Variables
    private System.Int32 _letterID;
    private System.Int32 _webSiteContentId;
    private System.String _topicName;
    private System.String _topicContent;
    private System.Int64 _userId;

    #endregion

    #region Public Properties

    public System.Int32 LetterID
    {
        get { return _letterID; }
        set { _letterID = value; }
    }

    public System.Int32 WebSiteContentId
    {
        get { return _webSiteContentId; }
        set { _webSiteContentId = value; }
    }

    public System.String TopicName
    {
        get { return _topicName; }
        set { _topicName = value; }
    }

    public System.String TopicContent
    {
        get { return _topicContent; }
        set { _topicContent = value; }
    }

    public System.Int64 UserID
    {
        get { return _userId; }
        set { _userId = value; }
    }

    #endregion

    #region Research And News Letter Properties

    private System.String _newsLettersHeading;
    private byte[] _newsLettersContent;
    private System.String _newsLettersSynopsis;
    private System.String _newsLettersFileName;
    private System.String _newsLettersFilePath;
    private System.String _newsLettersActualFileName;
    private System.Char _newsLettersRecordAction;

    public System.String NewsLettersHeading
    {
        get { return _newsLettersHeading; }
        set { _newsLettersHeading = value; }
    }
    public byte[] NewsLettersContent
    {
        get { return _newsLettersContent; }
        set { _newsLettersContent = value; }
    }
    public System.String NewsLettersFileName
    {
        get { return _newsLettersFileName; }
        set { _newsLettersFileName = value; }
    }
    public System.String NewsLettersFilePath
    {
        get { return _newsLettersFilePath; }
        set { _newsLettersFilePath = value; }
    }
    public System.String NewsLettersActualFileName
    {
        get { return _newsLettersActualFileName; }
        set { _newsLettersActualFileName = value; }
    }
    public System.String NewsLettersSynopsis
    {
        get { return _newsLettersSynopsis; }
        set { _newsLettersSynopsis = value; }
    }
    public System.Char NewsLettersRecordAction
    {
        get { return _newsLettersRecordAction; }
        set { _newsLettersRecordAction = value; }
    }

    private System.Int16 _rnParentCategoryID;
    private System.Int16 _rnCategoryID;
    private System.String _rnMainCategory;

    public System.Int16 RN_ParentCategoryID
    {
        get { return _rnParentCategoryID; }
        set { _rnParentCategoryID = value; }
    }

    public System.Int16 RN_CategoryID
    {
        get { return _rnCategoryID; }
        set { _rnCategoryID = value; }
    }

    public System.String RN_MainCategory
    {
        get { return _rnMainCategory; }
        set { _rnMainCategory = value; }
    }

    #endregion

    #region News Letter Subscription Properties

    private System.Int32 _subscriptionID;
    private System.String _subscriptorEmailID;
    private System.String _subscriptorIPAddress;
    private System.Boolean _subscriptionIsActivate;
    private System.String _subscribed_RN_LetterID;

    public System.Int32 SubscriptionID
    {
        get { return _subscriptionID; }
        set { _subscriptionID = value; }
    }

    public System.String SubscriptorEmailID
    {
        get { return _subscriptorEmailID; }
        set { _subscriptorEmailID = value; }
    }

    public System.String SubscriptorIPAddress
    {
        get { return _subscriptorIPAddress; }
        set { _subscriptorIPAddress = value; }
    }

    public System.Boolean SubscriptionIsActivate
    {
        get { return _subscriptionIsActivate; }
        set { _subscriptionIsActivate = value; }
    }

    public System.String Subscribed_RN_LetterID
    {
        get { return _subscribed_RN_LetterID; }
        set { _subscribed_RN_LetterID = value; }
    }


    #endregion

    #region Site User Registration and Registration Request

    private System.String _firstName, _lastName, _bankName, _address1, _address2, _phone, _city, _zip, _country, _emailId, _emailId_Secondary,
                        _ipAddress, _locationCode, _selectedApplicationList, _guid, _forApplication, _InstitutionType, _securityQuestion, _securityAnswer,
                        _billingPhone, _billingAddress1, _billingAddress2, _billingCity, _billingZip, _billingCountry, _billingSecurityQuestion, _billingSecurityAnswer, _billingLastAccessCode;

    private System.Int16 _billingSubscriptionGapMonth;

    public System.String FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public System.String LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public System.String BankName
    {
        get { return _bankName; }
        set { _bankName = value; }
    }

    public System.String Address1
    {
        get { return _address1; }
        set { _address1 = value; }
    }

    public System.String Address2
    {
        get { return _address2; }
        set { _address2 = value; }
    }

    public System.String Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    public System.String City
    {
        get { return _city; }
        set { _city = value; }
    }

    public System.String Zip
    {
        get { return _zip; }
        set { _zip = value; }
    }

    public System.String Country
    {
        get { return _country; }
        set { _country = value; }
    }

    public System.String EmailId
    {
        get { return _emailId; }
        set { _emailId = value; }
    }

    public System.String EmailId_Secondary
    {
        get { return _emailId_Secondary; }
        set { _emailId_Secondary = value; }
    }

    public System.String IpAddress
    {
        get { return _ipAddress; }
        set { _ipAddress = value; }
    }

    public System.String LocationCode
    {
        get { return _locationCode; }
        set { _locationCode = value; }
    }

    public System.String SelectedApplicationList
    {
        get { return _selectedApplicationList; }
        set { _selectedApplicationList = value; }
    }

    public System.String GUID
    {
        get { return _guid; }
        set { _guid = value; }
    }

    public System.String ForApplication
    {
        get { return _forApplication; }
        set { _forApplication = value; }
    }

    public System.String InstitutionType
    {
        get { return _InstitutionType; }
        set { _InstitutionType = value; }
    }

    public System.String SecurityQuestion
    {
        get { return _securityQuestion; }
        set { _securityQuestion = value; }
    }

    public System.String SecurityAnswer
    {
        get { return _securityAnswer; }
        set { _securityAnswer = value; }
    }

    public System.String BillingPhone
    {
        get { return _billingPhone; }
        set { _billingPhone = value; }
    }

    public System.String BillingAddress1
    {
        get { return _billingAddress1; }
        set { _billingAddress1 = value; }
    }

    public System.String BillingAddress2
    {
        get { return _billingAddress2; }
        set { _billingAddress2 = value; }
    }

    public System.String BillingCity
    {
        get { return _billingCity; }
        set { _billingCity = value; }
    }

    public System.String BillingCountry
    {
        get { return _billingCountry; }
        set { _billingCountry = value; }
    }

    public System.String BillingZip
    {
        get { return _billingZip; }
        set { _billingZip = value; }
    }

    public System.String BillingSecurityQuestion
    {
        get { return _billingSecurityQuestion; }
        set { _billingSecurityQuestion = value; }
    }

    public System.String BillingSecurityAnswer
    {
        get { return _billingSecurityAnswer; }
        set { _billingSecurityAnswer = value; }
    }

    public System.String BillingLastAccessCode
    {
        get { return _billingLastAccessCode; }
        set { _billingLastAccessCode = value; }
    }

    public System.Int16 BillingSubscriptionGapMonth
    {
        get { return _billingSubscriptionGapMonth; }
        set { _billingSubscriptionGapMonth = value; }
    }

    #endregion

    #region Create User

    private System.String _CreateUserLocationCode, _CreateUserLoginID, _CreateUserNewUserName, _CreateUserApplicationList, _CreateUserPassword, _CreateUserCustID, _CreateUserPhoneNo, _CreateUserCustMnemonic, _CreateSecondaryEmailID;
    private System.Int64 _CreateUserID, _CreateUserByUserID;
    private System.Int16 _CreateUserLoginTypeID;

    public System.Int64 CreateUserID
    {
        get { return _CreateUserID; }
        set { _CreateUserID = value; }
    }

    public System.Int16 CreateUserLoginTypeID
    {
        get { return _CreateUserLoginTypeID; }
        set { _CreateUserLoginTypeID = value; }
    }

    public System.String CreateUserLoginID
    {
        get { return _CreateUserLoginID; }
        set { _CreateUserLoginID = value; }
    }

    public System.String CreateUserCustID
    {
        get { return _CreateUserCustID; }
        set { _CreateUserCustID = value; }
    }
    public System.String CreateUserLocationCode
    {
        get { return _CreateUserLocationCode; }
        set { _CreateUserLocationCode = value; }
    }

    public System.Int64 CreateUserByUserID
    {
        get { return _CreateUserByUserID; }
        set { _CreateUserByUserID = value; }
    }

    public System.String CreateUserNewUserName
    {
        get { return _CreateUserNewUserName; }
        set { _CreateUserNewUserName = value; }
    }

    public System.String CreateUserApplicationList
    {
        get { return _CreateUserApplicationList; }
        set { _CreateUserApplicationList = value; }
    }

    public System.String CreateUserPassword
    {
        get { return _CreateUserPassword; }
        set { _CreateUserPassword = value; }
    }

    public System.String CreateUserPhoneNo
    {
        get { return _CreateUserPhoneNo; }
        set { _CreateUserPhoneNo = value; }
    }

    public System.String CreateUserCustMnemonic
    {
        get { return _CreateUserCustMnemonic; }
        set { _CreateUserCustMnemonic = value; }
    }
    public System.String CreateSecondaryEmailID
    {
        get { return _CreateSecondaryEmailID; }
        set { _CreateSecondaryEmailID = value; }
    }
    #endregion

    #region Beast Auto Url

    private System.String _PageName, _URLEncrypted, _URLEncryptedMsg, _AutoUrlRefNo;
    private Int64 _TraderId;
    private System.DateTime _ValidDateUrlDate;

    /*public System.String PageName
    {
        get { return _PageName; }

        set { _PageName = value; }
    }*/

    public System.Int64 TraderId
    {
        get { return _TraderId; }

        set { _TraderId = value; }
    }

    public System.String URLEncrypted
    {
        get { return _URLEncrypted; }

        set { _URLEncrypted = value; }
    }

    public System.String URLEncryptedMsg
    {
        get { return _URLEncryptedMsg; }

        set { _URLEncryptedMsg = value; }
    }

    public System.DateTime ValidDateUrlDate
    {

        get { return _ValidDateUrlDate; }

        set { _ValidDateUrlDate = value; }
    }

    public System.String AutoUrlRefNo
    {
        get { return _AutoUrlRefNo; }

        set { _AutoUrlRefNo = value; }
    }

    #endregion

    #region Weather Contact
    private System.String _custId, _alias, _trader, _traderNo, _ITSupport;
    private System.String _ITSupportNo, _ITSupportMail, _ClearingFcm, _ClearingMail, _ConnectionType, _ITContact, _TraderEmail;

    public System.String ConnectionType
    {
        get { return _ConnectionType; }
        set { _ConnectionType = value; }
    }
    public System.String ClearingMail
    {
        get { return _ClearingMail; }
        set { _ClearingMail = value; }
    }
    public System.String ClearingFcm
    {
        get { return _ClearingFcm; }
        set { _ClearingFcm = value; }
    }
    public System.String ITSupportMail
    {
        get { return _ITSupportMail; }
        set { _ITSupportMail = value; }
    }
    public System.String ITSupportNo
    {
        get { return _ITSupportNo; }
        set { _ITSupportNo = value; }
    }
    public System.String ITSupport
    {
        get { return _ITSupport; }
        set { _ITSupport = value; }
    }
    public System.String TraderNumber
    {
        get { return _traderNo; }
        set { _traderNo = value; }
    }
    public System.String Trader
    {
        get { return _trader; }
        set { _trader = value; }
    }
    public System.String CustID
    {
        get { return _custId; }
        set { _custId = value; }
    }
    public System.String Alias
    {
        get { return _alias; }
        set { _alias = value; }
    }

    public System.String ITContact
    {
        get { return _ITContact; }
        set { _ITContact = value; }
    }
    public System.String TraderEmail
    {
        get { return _TraderEmail; }
        set { _TraderEmail = value; }
    }

    #endregion

    #region AUTO URL

    public System.String AutoUrlUserID
    {
        get;
        set;
    }

    public System.String AutoUrlSessionID
    {
        get;
        set;
    }

    public System.String AutoUrlApplicationCode
    {
        get;
        set;
    }

    public System.String AutoUrlStartDate
    {
        get;
        set;
    }

    public System.String AutoUrlEndDate
    {
        get;
        set;
    }

    public System.String AutoUrlURLEncrypted
    {
        get;
        set;
    }

    public System.String AutoUrlMessageEncrypted
    {
        get;
        set;
    }

    public System.String AutoUrlMovetoPage
    {
        get;
        set;
    }

    public System.String AutoUrlMailSubject
    {
        get;
        set;
    }

    public System.String AutoUrlMailBody
    {
        get;
        set;
    }
    public System.String AutoUrlMnemonic
    {
        get;
        set;
    }
    public System.String AutoUrlCustomerID
    {
        get;
        set;
    }

    public System.String AutoUrlCompanyLegalEntity
    {
        get;
        set;
    }
    public System.String AutoUrlIpAddress
    {
        get;
        set;
    }
    public System.String AutoUrlRecoredCreateBy
    {
        get;
        set;
    }
    public System.String AutoUrlMinuteInterval
    {
        get;
        set;
    }

    #endregion
}