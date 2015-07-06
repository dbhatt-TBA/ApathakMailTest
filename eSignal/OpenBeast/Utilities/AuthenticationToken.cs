using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VCM.Common.Log;

namespace OpenBeast.Utilities
{
    public class dirAuthToken
    {

        private string _userID;
        public string UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;

            }
        }
        public List<UserDetail> UserDetails
        {
            get;
            set;
        }
    }


    public class UserDetail
    {
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;

            }
        }
        private string _GUid;

        public string GUid
        {
            get
            {
                return _GUid;
            }
            set
            {
                _GUid = value;

            }
        }
        private DateTime _Login;

        public DateTime Login
        {
            get
            {
                return _Login;
            }
            set
            {
                _Login = value;

            }
        }
        private DateTime _Logout;

        public DateTime Logout
        {
            get
            {
                return _Logout;
            }
            set
            {
                _Logout = value;

            }
        }
        public List<ConnectionDetail> ConnectionDetails
        {
            get;
            set;
        }

        private string _ClientType;
        public string ClientType
        {
            get
            {
                return _ClientType;
            }
            set
            {
                _ClientType = value;

            }
        }

        private DateTime _LastAuthTime;
        public DateTime LastAuthTime
        {
            get
            {
                return _LastAuthTime;
            }
            set
            {
                _LastAuthTime = value;

            }
        }

        private string _IPAddres;

        public string IPAddres
        {
            get
            {
                return _IPAddres;
            }
            set
            {
                _IPAddres = value;

            }
        }
        private string _City;

        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;

            }
        }
        private string _Country;

        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;

            }
        }
        private string _Org;

        public string Org
        {
            get
            {
                return _Org;
            }
            set
            {
                _Org = value;

            }
        }
    }
    public class ConnectionDetail
    {
        private string _ConnectionID;
        public string ConnectionID
        {
            get
            {
                return _ConnectionID;
            }
            set
            {
                _ConnectionID = value;

            }
        }

        public List<ImageDetail> ImageDetails
        {
            get;
            set;
        }

        private bool _connectionStatus;
        public bool ConnectionStatus
        {
            get
            {
                return _connectionStatus;
            }
            set
            {

                _connectionStatus = value;


            }
        }
        private string _UserAgent;
        public string UserAgent
        {
            get
            {
                return _UserAgent;
            }
            set
            {
                _UserAgent = value;

            }
        }
    }
    public class ImageDetail
    {
        private string _ImageName;
        public string ImageName
        {
            get
            {
                return _ImageName;
            }
            set
            {
                _ImageName = value;

            }
        }

        private string _ImageSIFID;
        public string ImageSIFID
        {
            get
            {
                return _ImageSIFID;
            }
            set
            {
                _ImageSIFID = value;

            }
        }
        private string _ImageValidity;
        public string ImageValidity
        {
            get
            {
                return _ImageValidity;
            }
            set
            {
                _ImageValidity = value;

            }
        }

        private string _InstanceID;
        public string InstanceID
        {
            get
            {
                return _InstanceID;
            }
            set
            {
                _InstanceID = value;

            }
        }

        private DateTime _ImageCreationTime;
        public DateTime ImageCreationTime
        {
            get
            {
                return _ImageCreationTime;
            }
            set
            {
                _ImageCreationTime = value;

            }
        }

        private DateTime _ImageCloseTime;
        public DateTime ImageCloseTime
        {
            get
            {
                return _ImageCloseTime;
            }
            set
            {
                _ImageCloseTime = value;

            }
        }
        private DateTime _LastActivityOn;
        public DateTime LastActivityOn
        {
            get
            {
                return _LastActivityOn;
            }
            set
            {
                _LastActivityOn = value;

            }
        }
    }

   
    public enum SysLogEnum
    {
        ADDAUTHENTICATIONTOKEN = 1,
        CREATECONNECTION = 2,
        CREATECALCULATOR = 3,
        CHANGECALCVALUE = 4,
        CLOSEDCALCULATOR = 5,
        SHARECALCULATOR = 6,
        OPENSHAREDCALCULATOR = 7,
        REMOVECONNECTION = 8,
        REMOVEAUTHENTICATIONTOKEN = 9,
        RESTORECALCFROMWORKSPACE = 10,
        SHAREDTOUSERAUTHENTICATED = 11

    }
}