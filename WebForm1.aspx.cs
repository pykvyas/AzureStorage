using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text;

namespace AzureStorage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Read data from sql db*/
            SqlConnection con = new SqlConnection("Data Source=vyas;Initial Catalog=EMPLOYEEDB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select ID,Name from Employee", con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            con.Close();


            /*convert retrieved dataset to json format*/
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(ds);
            /*writing the json format o/p to the page*/
            Response.Write(JSONString);

            /*ConnectionState string for local azure storage instance */
            string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

            /*CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting(connectionString));*/

            BlobContainerClient container = new BlobContainerClient(connectionString,"testblobcontainer");

            /*converting json formst data to memorystream*/
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(JSONString));

            BlobClient blob = container.GetBlobClient("sampleBlob");
            blob.Upload(ms);















        }

        
    }
}