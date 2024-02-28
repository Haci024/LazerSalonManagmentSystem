using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class AddLazerCategoryDTO
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<Filial> Filial {get;set;}

        public List<LazerCategory> MainCategory { get;set;}
        
        public int FilialId { get; set; }

        public int MainCategoryId { get; set; }
    }
}
