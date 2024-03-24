using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOS.ReportDTO
{
    public class SelectFilterDTO
    {
        public List<LazerMaster> LazerMasters { get; set; }

        public List<LazerCategory> LazerCategories { get; set;}

        public List<Stock> Stock { get; set; }

        public List<Income> Incomes { get; set; }

        public decimal ArzumMiniBudget {  get; set; }

        public decimal ArzumBeautyBudget { get; set; }

        public decimal ArzumEsteticBudget {  get; set; }


        public List<BodyShapingPacketCategory> BodyShapingPacketCategories { get; set; }

        public List<BodyShapingMaster> BodyShapingMasters { get; set; }

        public List<Filial> Filials { get; set;}

        public List<SolariumCategories> SolariumCategories { get; set;}

        public List<CosmetologyCategory> CosmetologyCategories { get; set; }

        public List<Cosmetologs> Cosmetologs { get; set; }

        public List<SpendCategory> SpendCategory { get; set; }


        public List<LipuckaCategories> LipuckaCategories { get; set; }

        public List<PirsinqCategory> PirsinqCategory { get; set; }

        public List<BodyShapingMaster> BodyShapingMaster { get; set; }

        public List<KassaActionList> KassaActionLists { get; set; }


    }
}
