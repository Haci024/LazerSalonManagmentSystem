using Business.IServices;
using Data.DAL;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager
{
    public class BodyShapingPacketsReportsManager : IBodyShapingPacketsReportsService
    {
        private readonly IBodyShapingAppointmentReportsDAL _bodyShapingAppointmentReportsDAL;
        public BodyShapingPacketsReportsManager(IBodyShapingAppointmentReportsDAL bodyShapingAppointmentReportsDAL)
        {
            _bodyShapingAppointmentReportsDAL = bodyShapingAppointmentReportsDAL;
        }
        public void Create(BodyShapingPacketsReports t)
        {
            _bodyShapingAppointmentReportsDAL.Create(t);
        }

        public void Delete(BodyShapingPacketsReports t)
        {
            _bodyShapingAppointmentReportsDAL.Delete(t);
        }

        public BodyShapingPacketsReports GetById(int id)
        {
            return _bodyShapingAppointmentReportsDAL.GetById(id);
        }

        public List<BodyShapingPacketsReports> GetList()
        {
           return _bodyShapingAppointmentReportsDAL.GetList();
        }

        public void Update(BodyShapingPacketsReports t)
        {
           _bodyShapingAppointmentReportsDAL.Update(t);
        }
    }
}
