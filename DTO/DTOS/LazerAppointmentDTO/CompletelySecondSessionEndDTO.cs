using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.LazerAppointmentDTO
{
    public class CompletelySecondSessionEndDTO
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? InCompleteEnd { get; set; }

        public bool Succesfully { get; set; }

        public string CustomerFullName { get; set; }

        public string Lazeroloq { get; set; }

        public int ImpulsCount { get; set; }
    }
}
