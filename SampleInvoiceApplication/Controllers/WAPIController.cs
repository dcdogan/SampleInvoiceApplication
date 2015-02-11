using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;


namespace LVInvoiceApplication.Controllers
{
    public class WAPIController : ApiController
    {


        public class UserAttributes
        {
            public string UserID { get; set; }
            public string UserFirstName { get; set; }
            public string UserLastName { get; set; }
            public string UserEmail { get; set; }
            public string UserRole { get; set; }
            public string UserDefaultCompany { get; set; }
            public string UserDefaultDept { get; set; }
            public string UserGLOwnership { get; set; }
            public string UserEmailPref1 { get; set; }
            public string UserEmailPref2 { get; set; }
            public string UserEmailPref3 { get; set; }
        }
       


        public HttpResponseMessage GetUserNames()
        {

            List<UserAttributes> usrList = new List<UserAttributes>{
                
                new UserAttributes{UserID = "aclerk",	UserFirstName = "Amy",	UserLastName = "Clerk", UserEmail = "aclerk@me.com", UserRole = "General Ledger", UserDefaultCompany = "Perfumes&Cosmetics", UserDefaultDept = "Cosmetics"},

                new UserAttributes{UserID = "jmattis",	UserFirstName = "Jane",	UserLastName = "Mattis", UserEmail = "jmattis@me.com", UserRole = "Account Manager", UserDefaultCompany = "Perfumes&Cosmetics",	UserDefaultDept = "Perfumes"},

                new UserAttributes{UserID = "jdoe",	UserFirstName = "John",	UserLastName = "Doe", UserEmail = "jdoe@me.com", UserRole = "Finance Manager", UserDefaultCompany = "Perfumes&Cosmetics", UserDefaultDept = "Cosmetics"},

                new UserAttributes{UserID = "sjohnson",	UserFirstName = "Scott",	UserLastName = "Johnson", UserEmail = "sjohnson@me.com", UserRole = "Accountant", UserDefaultCompany = "Perfumes&Cosmetics", UserDefaultDept = "Perfumes", }
                               
            };


            var jsonList = (from usr in usrList
                            where usr.UserRole != "Application Admin"
                            select new
                            {
                                usr.UserID,
                                FullName = usr.UserFirstName + " " + usr.UserLastName,
                                usr.UserFirstName,
                                usr.UserLastName,
                                usr.UserEmail,
                                usr.UserDefaultDept,
                                usr.UserDefaultCompany,
                                usr.UserRole
                            });

            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;

            json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            //json.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //json.Culture = new CultureInfo("it-IT");

            return Request.CreateResponse(HttpStatusCode.OK, jsonList, formatter);


        }
    }
}
