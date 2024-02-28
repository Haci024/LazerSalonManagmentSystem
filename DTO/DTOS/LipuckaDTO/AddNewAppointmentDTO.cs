using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LipuckaDTO
{
    public class AddNewAppointmentDTO
    {
        public string Customer { get; set; }

        public string LipuckaMaster { get; set; }

        public int LipuckaMasterId { get; set; }

        public List<LipuckaCategories> LipuckaCategories { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public int[] LipuckaCategoryIds { get; set; }

       public string Description { get; set; }

        public decimal Price { get; set; }

        

    }
}
