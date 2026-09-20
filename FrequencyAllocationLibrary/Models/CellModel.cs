using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAllocationLibrary.Models
{
    public class CellModel
    {
        public string ID { get; set; }

        public double Easting { get; set; }

        public double Northing { get; set; }

        public int? AllocatedFrequency { get; set; } = null;

        public List<double> ConflictDistances { get; set; } = new List<double>();

        public List<string> ConflictGraph { get; set; }
    }
}
