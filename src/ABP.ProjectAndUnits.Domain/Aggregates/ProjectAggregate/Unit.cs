using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ABP.ProjectAndUnits.Aggregates.ProjectAggregate
{
    public class Unit : Entity<Guid>
    {
        public string Descrption { get; private set; }
        public string Location { get; private set; }
        public int UnitArea { get; private set; }
        public int NumberOfRooms { get; private set; }
        public Guid ProjectId { get; private set; }

        public Unit()
        {

        }
        internal Unit(Guid Id, string descrption, string location, int unitarea, int numberofrooms, Guid projectId) : base(Id)
        {
            Descrption = descrption;
            Location = location;
            UnitArea = unitarea;
            NumberOfRooms = numberofrooms;
            ProjectId = projectId;

        }

        public static Unit Init(Guid Id, string descrption, string location, int unitarea, int numberofrooms, Guid projectId)
            => new Unit(Id,descrption, location, unitarea, numberofrooms, projectId);

        public void Update(string descrption, string location, int unitarea, int numberofrooms)
        {
            Descrption = descrption;
            Location = location;
            UnitArea = unitarea;
            NumberOfRooms = numberofrooms;
        }
    }
}