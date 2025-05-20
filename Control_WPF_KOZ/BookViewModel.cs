using Control_WPF_KOZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_WPF_KOZ
{
    public class BookViewModel
    {
        public string Articul { get; set; }
        public string Name { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
        public string Date_publish { get; set; }
        public string StatusContent { get; set; }
        public int? SelectedUserId { get; set; }
        public string UserName { get; set; }
    }

}
