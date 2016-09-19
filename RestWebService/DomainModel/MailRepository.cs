using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
   public class MailRepository 
    {
        private EFDbContext _context;
        public MailRepository()
        {
            _context = new EFDbContext();

        }

        public IEnumerable<Mail> Mails
        {
            get { return _context.Mails; }
        }

        
        public void CreateMail(Mail mail)
        {
            _context.Mails.Add(mail);
            _context.SaveChanges();
        }
        
        public void EditMail(int id, Mail mail)
        {
            if (id == mail.ID)
            {
                _context.Entry(mail).State = EntityState.Modified;

                _context.SaveChanges();
            }
        }
        public void DeleteMail(int id)
        {
            Mail mail = _context.Mails.Find(id);
            if (mail != null)
            {
                _context.Mails.Remove(mail);
                _context.SaveChanges();
            }
        }
        public Mail GetMail(int id)
        {
            Mail mail = _context.Mails.Find(id);
            return mail;
        }
        //public void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _context.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
