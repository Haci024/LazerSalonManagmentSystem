using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.PirsinqDTO
{
    public class AddPirsinqAppointmentDTO
    {
        public string Customer { get; set; }

        public string PirsinqMaster { get; set; }

        public string Description { get; set; }

        public int PirsinqMasterId { get; set; }

        public List<PirsinqCategory> PirsinqCategories { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public int[] PirsinqCategoryIds { get; set; }

        public decimal Price { get; set; }
    }
}
