using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace ABP.ProjectAndUnits.Aggregates.ProjectAggregate
{
    public class Project : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string ProjectCode { get; private set; }
        public string Descrption { get; private set; }
        public string ProjectLocation { get; private set; }
        public int NumberOfUnits { get; private set; }

        private readonly List<Unit> _units = new();
        public ICollection<Unit> Units { get; private set; } = new List<Unit>();
        internal Project()
        {

        }
        internal Project(Guid id, string name, string projectcode, string descrption, string projectlocation, int numberofunits) : base(id)
        {
            Name = name;
            ProjectCode = projectcode;
            Descrption = descrption;
            ProjectLocation = projectlocation;
            NumberOfUnits = numberofunits;

        }

        public Project Update(string name, string projectcode, string descrption, string projectlocation, int numberofunits)
        {
            Name = name;
            ProjectCode = projectcode;
            Descrption = descrption;
            ProjectLocation = projectlocation;
            NumberOfUnits = numberofunits;
            return this;

        }

        public void AddUnits(Guid id, string descrption, string location, int unitarea, int numberofrooms, Guid projectId)
        {
            Units.Add(Unit.Init(id, descrption, location, unitarea, numberofrooms, projectId));
        }

        public void UpdateUnits(List<Unit> updatedUnits)
        {
            var unitsToRemove = _units.Where(u => !updatedUnits.Any(updatedUnit => updatedUnit.Id == u.Id)).ToList();
            foreach (var unit in unitsToRemove)
            {
                RemoveUnit(unit);
            }

            var unitsToAdd = updatedUnits.Where(updatedUnit => !_units.Any(u => u.Id == updatedUnit.Id)).ToList();
            foreach (var unit in unitsToAdd)
            {
                AddUnit(unit);
            }

            foreach (var existingUnit in _units)
            {
                var updatedUnit = updatedUnits.SingleOrDefault(u => u.Id == existingUnit.Id);
                if (updatedUnit != null)
                {
                    existingUnit.Update(updatedUnit.Descrption, updatedUnit.Location, updatedUnit.UnitArea, updatedUnit.NumberOfRooms);
                }
            }
        }
        public void RemoveUnit(Unit unit)
        {
            _units.Remove(unit);
        }

        public void AddUnit(Unit unit)
        {
            _units.Add(unit);
        }
    }
}
