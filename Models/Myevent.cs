using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace LogLife11.Models
{
    public class Myevent
    {

        public int id { get; set; }
        public string NameEvent { get; set; }
        public string NameOrganizer { get; set; }
     

        public Myevent()
        {
        }
    }
}
