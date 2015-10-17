using System;

namespace SampleCustomSessionIDManager
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Session["testvar1"] = "my test variable";

			Response.Write("SessionID:"+Session.SessionID);
		}
	}
}