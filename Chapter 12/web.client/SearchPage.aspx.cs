using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebClient
{
	public class SearchPage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label idLabel;
		protected System.Web.UI.WebControls.Label titleLabel;
		protected System.Web.UI.WebControls.Label artistNameLabel;
		protected System.Web.UI.WebControls.Label averageRatingLabel;
		protected System.Web.UI.WebControls.Label labelNameLabel;
		protected System.Web.UI.WebControls.TextBox recordingId;
		protected System.Web.UI.WebControls.TextBox title;
		protected System.Web.UI.WebControls.TextBox artistName;
		protected System.Web.UI.WebControls.TextBox labelName;
		protected System.Web.UI.WebControls.RadioButtonList averageRating;
		protected System.Web.UI.WebControls.Button searchButton;
		protected System.Web.UI.WebControls.Button cancelButton;
		protected System.Web.UI.WebControls.Repeater searchResults;

		private CatalogServiceGateway gateway = 
			new CatalogServiceImplementation();
		private SearchPageHelper helper = new SearchPageHelper();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.searchButton.Click += new System.EventHandler(this.SearchButtonClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SearchButtonClick(object sender, System.EventArgs e)
		{
			SearchCriteria criteria = helper.Translate(
				recordingId.Text, title.Text, artistName.Text, 
				averageRating.SelectedValue, labelName.Text);

			searchResults.DataSource = gateway.Search(criteria);
			searchResults.DataBind();
		}
	}
}
