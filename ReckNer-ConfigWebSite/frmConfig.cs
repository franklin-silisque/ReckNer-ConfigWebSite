using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.SharePoint.Client;
using System.Net;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using OfficeDevPnP.Core.Pages;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.Online.SharePoint.TenantManagement;
using System.Configuration;
using Microsoft.Graph;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IO;
using OfficeDevPnP.Core.Sites;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ReckNer_ConfigWebSite
{
    public partial class frmConfig : System.Windows.Forms.Form
    {
        private string siteAdminUrl = "";
        private string fileUrl = "";
        private string library = "";
        private string fileName = "";
        private bool finishprocess = false;
        public frmConfig()
        {
            InitializeComponent();
        }
        private bool ValidateInput() {
            bool returnvalue = true;
            if (string.IsNullOrEmpty(txtSite.Text) || string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtWhiteList.Text) || string.IsNullOrEmpty(txtPassword.Text)) {



                // MessageBox.Show("Please insert all configuration fields");
                lblMessage.Text = "Please insert all configuration fields!!";
                    return false;
            }
            //if (Regex.IsMatch(txtSite.Text, @"&|_|,|.|;|:|/|'|!|@|$|%|^|+|=|\|||<|>|{|}|#|~|*|?")) {
            //    lblMessage.Text = "The Site Name cann't contains this invalid characters: '&','_',',','.',';',':','/','\"','\"','!','@','$','%','^','+','=','\','|','<','>','{','}','#','~','*','?'";
            //}

            return returnvalue;
        }
        public void SaveFileToSharePoint(ClientContext clientContext)
        {

            Microsoft.SharePoint.Client.Folder folderSHPO = clientContext.Web.GetFolderByServerRelativeUrl(library);
            
            FileCreationInformation newFileAtributes = new FileCreationInformation();
            newFileAtributes.ContentStream = opdDisclaimer.OpenFile(); ;
            newFileAtributes.Url = fileName;
            newFileAtributes.Overwrite = true;

            Microsoft.SharePoint.Client.File newfile = folderSHPO.Files.Add(newFileAtributes);
            clientContext.Load(newfile);
            //Update the metadata for a field having name "DocType"
            
            clientContext.ExecuteQuery();
        }
        private void ApplyConfigurations(string site, string user, string pass, string whitelist) {
            
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            
                lblMessage.Text = "";
            finishprocess = false;

            if (!ValidateInput())
            {

                return;
            }

            pnlFields.Visible = false;
            pnlFields.Height = 30;
            pnlButtons.Location = new Point(0, 70);
            progressBar1.Visible = true;
            btnConnect.Visible = false;
            btnReturn.Visible = true;
            btnReturn.Enabled = false;
            btnClose.Enabled = false;
            EnableInterface(false);
            //
            string siteUrl = txtSite.Text;
            string userName = txtUser.Text;
            string password = txtPassword.Text;
            string whiteList = txtWhiteList.Text;

            bkWorkerProcess.RunWorkerAsync();

            //MessageBox.Show("All configuration was applied correctly!!!");

        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            //Load values from appSettings
            txtUser.Text = ConfigurationManager.AppSettings["UserAdmin"];
            txtWhiteList.Text = ConfigurationManager.AppSettings["WhiteList"];
            siteAdminUrl = ConfigurationManager.AppSettings["URLSiteAdmin"];
            fileUrl = ConfigurationManager.AppSettings["FileDisclaimerURL"];
            library = ConfigurationManager.AppSettings["Library"];
            fileName = ConfigurationManager.AppSettings["FileName"];


        }
        private void EnableInterface(Boolean show) {
            txtPassword.Enabled = show;
            txtUser.Enabled = show;
            txtWhiteList.Enabled = show;
            txtSite.Enabled = show;
            btnConnect.Enabled = show;
        }
        private async Task<ClientContext> createSite(TeamSiteCollectionCreationInformation properties, ClientContext ctx)
        {
            return await ctx.CreateSiteAsync(properties);
        }

        private HttpResponseMessage callResponseFlow(string urlSite, string creatorName, string email) {
            var postURL = "https://prod-26.westus.logic.azure.com:443/workflows/071baec44afe460197dc6c24e98c7b7a/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=o635AZ0fVpCIVxi8Y_JMSj776NAFxuuXrbeQ4crIcX8";



            var parameters = new BEPostData();
            parameters.type = "object";
            parameters.properties = new BEProperties();
            parameters.properties.webUrl = urlSite;
            parameters.properties.creatorName = creatorName;
            parameters.properties.creatorEmail = email;
            parameters.properties.createdTimeUTC = DateTime.Now.ToString();
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(postURL),
                    Method = HttpMethod.Post,
                };

                var json = JsonConvert.SerializeObject(parameters.properties);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                request.Content.Headers.ContentType =
                  new MediaTypeHeaderValue("application/json");

                result = client.SendAsync(request).Result;
                result.EnsureSuccessStatusCode();

            }
            return result;
        }

        private void bkWorkerProcess_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            try
            {

                //
                string siteUrl = txtSite.Text;
                string userName = txtUser.Text;
                string password = txtPassword.Text;
                string whiteList = txtWhiteList.Text;




                (sender as BackgroundWorker).ReportProgress(5, "The configuration process was started");

                //if (siteUrl.Substring(siteUrl.Length - 1) == "/")
                //{
                //    siteUrl = siteUrl.Substring(0, siteUrl.Length - 1);
                //}


                OfficeDevPnP.Core.AuthenticationManager authMgr = new OfficeDevPnP.Core.AuthenticationManager();



                using (var clientContext = authMgr.GetSharePointOnlineAuthenticatedContextTenant(siteAdminUrl, userName, password))
                {

                    (sender as BackgroundWorker).ReportProgress(10, "Getting access to admin site");

                    Tenant adminSite = new Tenant(clientContext);

                    var siteName = txtSite.Text;


                    

                        Regex pattern = new Regex("[&_,.;:/\"!@$%^+=\\|<>{}#~*? ]");
                   siteName = pattern.Replace(siteName, string.Empty);

                    Microsoft.SharePoint.Client.Site site = null;

                    if (adminSite.CheckIfSiteExists("https://jreckner.sharepoint.com/sites/"+siteName, "Active")) {
                        throw new Exception("The site: " + txtSite.Text + ", already exists. Please choose another name."); 
                    }

                    var siteCreationProperties = new SiteCreationProperties();

                    var sitedesign = new OfficeDevPnP.Core.Entities.SiteEntity();

                    string[] owners = { txtUser.Text };
                    (sender as BackgroundWorker).ReportProgress(15, "Creating site collection");
                    //CommunicationSiteCollectionCreationInformation modernteamSiteInfo = new CommunicationSiteCollectionCreationInformation
                    //{
                    //    Description = "",
                    //    Owner = txtUser.Text,
                    //    SiteDesignId = new Guid("6a3f7a23-031b-4072-bf24-4193c14af65f"),
                    //    Title = txtSite.Text,
                    //    Lcid = 1033,
                    //    AllowFileSharingForGuestUsers = false,
                    //    Url = "https://jreckner.sharepoint.com/sites/" + siteName

                    //};

                    TeamSiteCollectionCreationInformation modernteamSiteInfo = new TeamSiteCollectionCreationInformation() {
                        DisplayName = txtSite.Text,
                        //Owners = owners,
                        //Alias = "https://jreckner.sharepoint.com/sites/" + siteName,
                        Alias = siteName,
                        Lcid = 1033,
                        IsPublic = true,
                        Description = ""
                    };


                    var result = Task.Run(async () => { return await clientContext.CreateSiteAsync(modernteamSiteInfo); }).Result;


                    siteUrl = result.Url;

                    var properties = adminSite.GetSitePropertiesByUrl(siteUrl, true);

                    clientContext.Load(properties);
                    site = adminSite.GetSiteByUrl(siteUrl);
                    Web web = site.RootWeb;
                    clientContext.Load(web);
                    ListCreationInformation doclist = new ListCreationInformation()
                    {
                        Title = "Document Library",
                        TemplateType = 101,

                    };
                    web.Lists.Add(doclist);
                    (sender as BackgroundWorker).ReportProgress(20, "Creating Document Library");
                    clientContext.ExecuteQuery();



                    (sender as BackgroundWorker).ReportProgress(30, "Configuring WhiteList");

                    properties.SharingDomainRestrictionMode = SharingDomainRestrictionModes.AllowList;
                    properties.SharingAllowedDomainList = whiteList;

                    properties.SharingCapability = SharingCapabilities.ExternalUserSharingOnly;
                    properties.Update();
                    clientContext.ExecuteQuery();
                }



                using (var ctx = authMgr.GetSharePointOnlineAuthenticatedContextTenant(siteUrl, userName, password))
                {
                    (sender as BackgroundWorker).ReportProgress(40, "Copy disclaimer file");
                    callResponseFlow(siteUrl, userName, userName);

                    (sender as BackgroundWorker).ReportProgress(50, "Getting access to site collection");



                    Web web = ctx.Web;
                    ctx.Load(web);

                    ctx.ExecuteQueryRetry();

                    var page2 = ctx.Web.AddClientSidePage("HomePageDisclaimer.aspx", true);
                    page2.AddSection(CanvasSectionTemplate.OneColumn, 5);

                    CanvasSection section = new CanvasSection(page2, CanvasSectionTemplate.OneColumn, page2.Sections.Count + 1);
                    page2.Sections.Add(section);
                    page2.PageTitle = "Disclaimer";
                    page2.LayoutType = ClientSidePageLayoutType.Home;
                    page2.DisableComments();
                    page2.PromoteAsHomePage();
                    page2.PageHeader.LayoutType = ClientSidePageHeaderLayoutType.NoImage;
                    page2.Save();
                    (sender as BackgroundWorker).ReportProgress(60, "Generating disclaimer Page");

                    // Check if file exists  
                    //Microsoft.SharePoint.Client.Folder folder = ctx.Web.GetFolderByServerRelativeUrl(ctx.Web.ServerRelativeUrl + "/" + library);
                    var documentFile = ctx.Web.GetFileByServerRelativeUrl(ctx.Web.ServerRelativeUrl + "/" + library + "/" + fileName);
                    web.Context.Load(documentFile, f => f.Exists); // Only load the Exists property
                    web.Context.ExecuteQuery();
                    if (documentFile.Exists)
                    {
                        Console.WriteLine("File exists!!!!");

                        //}

                        ctx.Load(documentFile);
                        ctx.Load(documentFile, w => w.SiteId);
                        ctx.Load(documentFile, w => w.WebId);
                        ctx.Load(documentFile, w => w.ListId);
                        ctx.Load(documentFile, w => w.UniqueId);
                        ctx.ExecuteQuery();

                        //if (documentFile != null)
                        //{ //si el documento existe
                        var pdfWebPart = page2.InstantiateDefaultWebPart(DefaultClientSideWebParts.DocumentEmbed);
                        var url = new Uri(ctx.Web.Url + "/" + library + "/" + fileName);

                        pdfWebPart.Properties["siteId"] = documentFile.SiteId;
                        pdfWebPart.Properties["webId"] = documentFile.WebId;
                        pdfWebPart.Properties["listId"] = documentFile.ListId;
                        pdfWebPart.Properties["uniqueId"] = documentFile.UniqueId;
                        pdfWebPart.Properties["file"] = url.AbsoluteUri;
                        pdfWebPart.Properties["serverRelativeUrl"] = documentFile.ServerRelativeUrl;
                        pdfWebPart.Properties["wopiurl"] = String.Format("{0}/_layouts/15/{1}?sourcedoc=%7b{2}%7d&action=interactivepreview", web.Url, "WopiFrame.aspx", documentFile.UniqueId.ToString("D"));
                        pdfWebPart.Properties["startPage"] = 1;
                        page2.AddControl(pdfWebPart, section.Columns[0]);
                        (sender as BackgroundWorker).ReportProgress(80, "Configuring webpart");
                        page2.Save();
                        page2.Publish();

                    }
                    else
                    {
                        throw (new Exception("We can't not find the disclaimer file, please upload it to the site at Document library as 'Disclaimer.pdf' and apply the configuration again."));
                    }




                }
                
                (sender as BackgroundWorker).ReportProgress(100, "All configuration was applied correctly!!!");
                //MessageBox.Show("All configuration was applied correctly!!!");
            }
            catch (Exception ex)
            {
                string errormessage = "";
                errormessage = ex.Message;
                if (ex.InnerException != null)
                    errormessage = ex.InnerException.Message;
                
                (sender as BackgroundWorker).ReportProgress(0, "Error: " + ex.Message);
            }
            finally {
                finishprocess = true;
            }
        }

        private void bkWorkerProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblMessage.Text = e.UserState.ToString();
            progressBar1.Value = e.ProgressPercentage;

          
        }

        private void bkWorkerProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableInterface(true);
            btnReturn.Enabled = true;
            btnClose.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            pnlFields.Visible = true;
            pnlFields.Height = 141;
            pnlButtons.Location = new Point(0, 141);
            progressBar1.Visible = false;
            lblMessage.Text = "";

            btnConnect.Visible = true;
            btnReturn.Visible = false;
            

            if (finishprocess) {
                txtWhiteList.Text = "";
                txtSite.Text = "";
            }
            
            
        }
    }
}