using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.ProjectAndUnits.Units
{
    public class UpdateUnitDto
    {
        public Guid Id { get; set; }
        public string Descrption { get; set; }
        public string Location { get; set; }
        public int UnitArea { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
