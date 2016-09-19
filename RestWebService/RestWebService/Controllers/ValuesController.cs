using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DomainModel;

namespace RestWebService.Controllers
{
    // [Authorize]
    public class ValuesController : ApiController
    {
        private MailRepository mailRepository;


        public ValuesController()
        {
            this.mailRepository = new MailRepository();
        }
        


        public IEnumerable<Mail> Get()
        {
            return mailRepository.Mails;
        }
        
               
        public Mail Get(int id)
        {
           
            return mailRepository.GetMail(id);
        }

        
        [HttpPost]
        public void CreateMail([FromBody]Mail mail)
        {
            mailRepository.CreateMail(mail);
            
        }

        [HttpPut]
        public void EditMail(int id, [FromBody]Mail mail)
        {
            mailRepository.EditMail(id, mail);
        }

        
        public void Delete(int id)
        {
            mailRepository.DeleteMail(id);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        mailRepository.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
