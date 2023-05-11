using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Configuration;
using System.IO.Compression;

namespace AutomateDeploymentRequest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UserCredential credential;
        string[] scopes = { GmailService.Scope.GmailSend, DriveService.Scope.Drive };
        string ApplicationName = "AutoDeployRequestSender";

        public async Task CreateZipAsync(int apiIndex)
        {
            Task task = Task.Run(()=>CreateZip(apiIndex));
            await task;
        }
        public KeyValuePair<bool,string> CreateZip(int apiIndex)
        {
            try
            {
                string apiFolderPath = @"D:\Deployment\Insurance";
                string zipFilePath = @"D:\Deployment\Insurance";

                switch (apiIndex)
                {
                    case 0:
                        apiFolderPath += @"\ProductInsuranceService";
                        zipFilePath += @"\ProductInsuranceService.zip";
                        break;
                    case 1:
                        apiFolderPath += @"\OrderInsuranceService";
                        zipFilePath += @"\OrderInsuranceService.zip";
                        break;
                    case 2:
                        apiFolderPath += @"\PaymentServiceAPI";
                        zipFilePath += @"\PaymentServiceAPI.zip";
                        break;
                }
                if (System.IO.File.Exists(zipFilePath))
                {
                    System.IO.File.Delete(zipFilePath);
                }
                ZipFile.CreateFromDirectory(apiFolderPath, zipFilePath);
                return new KeyValuePair<bool, string>(true,zipFilePath);
            }catch(Exception ex)
            {
                return new KeyValuePair<bool, string>(true,"");
            }
            
        }

        public async Task<KeyValuePair<bool,string>> UploadToDrive(string zipFilePath)
        {
            try
            {
                string shareLink = "";
                var driveService = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                var uploadFile = new Google.Apis.Drive.v2.Data.File();
                uploadFile.Title = APISelector.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss").Replace("-", "");
                uploadFile.WritersCanShare = true;
                //uploadFile.UserPermission = new Permission()
                //{
                //    //Kind= "drive#permission",
                //    Type ="anyone",
                //    Role="reader"
                //};
                uploadFile.Capabilities = new Google.Apis.Drive.v2.Data.File.CapabilitiesData()
                {
                    CanShare = true,
                    CanModifyContent = true,
                    CanEdit = true
                    
                };

                //uploadFile.Permissions = new List<Permission>();
                //uploadFile.Permissions.Add(new Permission {Kind= "drive#permission", Type = "anyone", Role = "commenter" });

                using (var stream = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read))
                {
                    await driveService.Files.Insert(uploadFile, stream, "application/zip").UploadAsync();
                }

                FilesResource.ListRequest request = driveService.Files.List();
                request.Q = "mimeType='application/zip'";
                request.Spaces = "drive";
                request.Fields = "nextPageToken, items(id, title,alternateLink)";
                request.PageToken = null;
                FileList fileList = request.Execute();

                foreach (Google.Apis.Drive.v2.Data.File file in fileList.Items)
                {
                    if (file.Title == uploadFile.Title)
                    {
                        Permission harhsalPermission = new Permission();
                        harhsalPermission.Value = "harshal.shah@iifl.com";
                        harhsalPermission.Type = "user";
                        harhsalPermission.Role = "writer";
                        harhsalPermission.EmailAddress = "harshal.shah@iifl.com";
                        driveService.Permissions.Insert(harhsalPermission, file.Id).Execute();

                        Permission manishPermission = new Permission();
                        manishPermission.Value = "manish.singh2@iifl.com";
                        manishPermission.Type = "user";
                        manishPermission.Role = "writer";
                        manishPermission.EmailAddress = "manish.singh2@iifl.com";
                        //sharePermission.WithLink = true;
                        driveService.Permissions.Insert(manishPermission, file.Id).Execute();

                        Permission amalPermission = new Permission();
                        amalPermission.Value = "amal.murali@iifl.com";
                        amalPermission.Type = "user";
                        amalPermission.Role = "writer";
                        amalPermission.EmailAddress = "amal.murali@iifl.com";
                        driveService.Permissions.Insert(amalPermission, file.Id).Execute();

                        shareLink = file.AlternateLink;


                    }
                }

                if (shareLink != null && shareLink != "")
                {
                    return new KeyValuePair<bool, string>(true, shareLink);
                }

                return new KeyValuePair<bool, string>(false, shareLink);
            }
            catch(Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "");
            }

        }

        public string Base64UrlEncode(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=","");
        }

        public string GenerateEmailBody(string shareLink,int apiIndex)
        {
            string apiEndpoint = "";
            string apiName = "";
            switch (apiIndex)
            {
                case 0:
                    apiEndpoint = ConfigurationManager.AppSettings["ProductAPIBaseUrl"].ToString();
                    apiName = "ProductInsurance API";
                    break;
                case 1:
                    apiEndpoint = ConfigurationManager.AppSettings["OrderAPIBaseUrl"];
                    apiName = "OrderInsurance API";
                    break;
                case 2:
                    apiName = "PaymentInsurance API";
                    apiEndpoint = ConfigurationManager.AppSettings["PaymentAPIBaseUrl"];
                    break;
            }

            //mailto: harshal.shah @iifl.com add this in @harshal href 
            string emailBody = $"Hello Manish<br>Mention new requirement or redeployment : UAT deployment<br>Server IP/URL :   <a href=\"{apiEndpoint}\">{apiEndpoint}</a><br>Build Project Name :  {apiName}<br>Source Path / Zoho URL : <a href=\"{shareLink}\">{shareLink}</a><br>Destination Physical Path : <br>Short summary for the changes being made: {changeSummaryText.Text}<br>List of DB Changes: None<br>Deployment Type : <br><br><a href=\"mailto: harshal.shah @iifl.com\">@Harshal Shah</a> Please Approve.";

            return emailBody;
        }

        private void DeleteExtraFilesIfExists(int apiIndex)
        {
            try
            {
                string apiFolderPath = @"D:\Deployment\Insurance";
                switch (apiIndex)
                {
                    case 0:
                        apiFolderPath += @"\ProductInsuranceService";
                        
                        break;
                    case 1:
                        apiFolderPath += @"\OrderInsuranceService";
                        
                        break;
                    case 2:
                        apiFolderPath += @"\PaymentServiceAPI";
                        
                        break;
                }

                if (System.IO.File.Exists(apiFolderPath+@"\appsettings.json"))
                {
                    System.IO.File.Delete(apiFolderPath + @"\appsettings.json");
                }

                if (System.IO.File.Exists(apiFolderPath + @"\web.config"))
                {
                    System.IO.File.Delete(apiFolderPath + @"\web.config");
                }

                if (System.IO.File.Exists(apiFolderPath + @"\appsettings.Development.json"))
                {
                    System.IO.File.Delete(apiFolderPath + @"\appsettings.Development.json");
                }

            }
            catch(Exception ex)
            {

            }
        }

        private async void SendDeploymentRequest_Click(object sender, EventArgs e)
        {
            try
            {
                Status.Text = "Initializing Process...";
                if (APISelector.SelectedIndex != -1)
                {
                    using (var stream = new FileStream(@"D:\Projects\testing\AutomateDeploymentRequest\AutomateDeploymentRequest\credential.json", FileMode.Open, FileAccess.Read))
                    {
                        string credPath = "token.json";
                        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, scopes, "user", System.Threading.CancellationToken.None, new FileDataStore(credPath, true));
                    }

                    DeleteExtraFilesIfExists(APISelector.SelectedIndex);

                    KeyValuePair<bool,string> isZipCreated = CreateZip(APISelector.SelectedIndex);

                    if (isZipCreated.Key)
                    {
                        KeyValuePair<bool, string> driveUpload = await UploadToDrive(isZipCreated.Value);
                        if (driveUpload.Key)
                        {
                            var service = new GmailService(new BaseClientService.Initializer
                            {
                                HttpClientInitializer = credential,
                                ApplicationName = ApplicationName
                            });
                            
                            string emailBody = GenerateEmailBody(driveUpload.Value, APISelector.SelectedIndex);
                            //add later 
                            string message = $"To: manish.singh2@iifl.com\r\nCc :harshal.shah@iifl.com,amal.murali@iifl.com,suman.c@livlong.com,anjani.gente@livlong.com,devops@livlong.com\r\nSubject: {APISelector.Text} UAT deployment\r\nContent-Type: text/html;charset=utf-8\r\n\r\n{emailBody}";

                            var msg = new Google.Apis.Gmail.v1.Data.Message();
                            msg.Raw = Base64UrlEncode(message.ToString());
                            service.Users.Messages.Send(msg,"me").Execute();

                            Status.Text = "Deployment email sent. Probably...";
                        }
                        else
                        {
                            Status.Text = "Error in uploading file to drive";
                        }
                    }
                    else
                    {
                        Status.Text = "Error in creating zip";
                    }
                    
                }
                else
                {

                }
                
                



            }catch(Exception ex)
            {
                Status.Text = "Error in sending deployment request";
            }
            

        }

        private void APISelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
