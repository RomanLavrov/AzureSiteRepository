using AzureSite.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzureSite.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authorization(string Login, string Password)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("romanlavrov_AzureStorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("TestTable");
            
            TableOperation retrieveOperation = TableOperation.Retrieve<RegData>(Login, Password);
            TableResult result = table.Execute(retrieveOperation);

            if (result.HttpStatusCode == 200)
                return View(result);
            else
                return View("Exception2");
        }
    }
}