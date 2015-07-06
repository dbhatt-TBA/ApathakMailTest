using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenBeast
{
    public partial class TestApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidRequest();
            }
        }

        private void ValidRequest()
        {
            if (Request.QueryString["id"] != null)
            {
              string GUID=  Request.QueryString["id"].Trim();

              RetriveDataUsingGUID(GUID);

            }
        }

        private  void RetriveDataUsingGUID(string guid)
        {
        
        }

    }
}