using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Client
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }

        public IEnumerable<Mail> Mails { get; set; }
    }
}
