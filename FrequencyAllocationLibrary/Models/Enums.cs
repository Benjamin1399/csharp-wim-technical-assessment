using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAllocationLibrary.Models
{
    public class Enums
    {
        public enum ConflictThreshold
        {
            Threshold500m = 500
        }

        public enum Frequencies 
        {
            BaseFrequency = 110,
            MaxFrequency = 115
        }
    }
}
