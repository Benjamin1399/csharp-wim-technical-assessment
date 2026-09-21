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
            Threshold200m = 200,
            Threshold500m = 500,
            Threshold1k = 1000,
            Threshold350m = 350
        }

        public enum Frequencies 
        {
            BaseFrequency = 110,
            MaxFrequency = 115
        }
    }
}
