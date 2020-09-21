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
using System.Linq;


namespace AzureStorage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string JSONString = string.Empty;
            using (EMPLOYEEDBEntities ede = new EMPLOYEEDBEntities())
            {
                var empList = ede.Employees.Where((f) => f.ID > 0).ToList();
             
                /*converting empl list to JSON format*/
                JSONString = JsonConvert.SerializeObject(empList);
                /*writing the json format o/p to the page*/
                /*Response.Write(JSONString);*/
            }

            /*ConnectionState string for local azure storage instance */
            string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, "testblobcontainer");

            /*converting json formst data to memorystream*/
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(JSONString));

            BlobClient blob = blobContainerClient.GetBlobClient("empJsonData");
            blob.Upload(ms);

        }


    }
}