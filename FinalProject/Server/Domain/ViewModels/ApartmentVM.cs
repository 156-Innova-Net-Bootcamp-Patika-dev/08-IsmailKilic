using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.ViewModels
{
    public class ApartmentVM
    {
        public int Id { get; set; }
        public char Block { get; set; }
        public bool IsFree { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int No { get; set; }
        public OwnerType OwnerType { get; set; }
    }
}
