using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }

        public int Role_ID { get; set; }
        public bool isActive{ get; set; }
        public DateTime Created_Date { get; set; }

        public DateTime Updated_Date { get; set; }
    }
}
