using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Mail
    {
        public int ID { get; set; }
        public string MessageHeader { get; set; }
        public DateTime DataT { get; set; }
        public string Destination { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }

        public int? ClientId { get; set; }

        //public Client Client { get; set; }
    }
}
