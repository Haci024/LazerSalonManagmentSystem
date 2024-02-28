using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.CosmetologyDTO
{
    public class CompleteSessionDTO
    {
        public string Description {  get; set; }    

        public decimal Price {  get; set; }

        public string Customer {  get; set; }

        public string Master {  get; set; }
       
        public DateTime? OutDate { get; set; }

        public int CosmetologId { get; set; }    


    }
}
