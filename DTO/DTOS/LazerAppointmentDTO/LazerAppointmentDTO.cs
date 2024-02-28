using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class LazerAppointmentDTO
    {
        public string LazerMasterName { get; set; }

        public int LazerMasterId { get; set; }

        public string CustomerName { get; set; }

        public bool IsStart { get;set; }
        public List<LazerCategory> MainCategory { get; set; }

        public List<LazerCategory> ChildCategory { get; set; }

        public decimal Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ReservationDate { get; set; }
      
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime KorreksionDate { get; set; }

        public int ImpulsCount { get; set; }

        public int[] lazerCategoriesId { get; set; }

     

    
    }
}
