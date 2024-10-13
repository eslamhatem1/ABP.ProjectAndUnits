using System;
using System.ComponentModel.DataAnnotations;

namespace ABP.ProjectAndUnits.Units
{
    public class CreateUnitDto
    {
        public string Descrption { get; set; }
        public string Location { get; set; }
        public int UnitArea { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
