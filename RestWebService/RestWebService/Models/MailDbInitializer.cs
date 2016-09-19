using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestWebService.Models
{
    public class MailDbInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext db)
        {
            //db.Mails.Add(new Mail {  ID = 8, ClientId = 7, MessageHeader = "dfssdf", Sender = "see", Destination = "asdfsdf", Content = "sdfsd" });
            

            base.Seed(db);
        }
    }
}