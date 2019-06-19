using System;

namespace Inexa.Atlantis.Re.Commons.Infras.Domains
{
    public class EntityBase
    {
        //[Key]
        //public int Id { get; set; }
    }

    public class DtoBase
    {
        public int RowsCount { get; set; }
        public int Offset { get; set; }
        public int Rows { get; set; }
        public DateTime CrDateDebut { get; set; }
        public DateTime CrDateFin { get; set; } 
        public string CodeOperation { get; set; }
        public string Action { get; set; }

        public long Return { get; set; }
    }
}
