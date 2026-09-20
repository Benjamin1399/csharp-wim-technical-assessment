using FrequencyAllocationLibrary;
using FrequencyAllocationLibrary.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAllocationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // WelcomeUser
            ConsoleOperations.WelcomeUser();

            // App Info
            ConsoleOperations.DisplayAppInfo();

            // DisplayMessage for name of file
            ConsoleOperations.DisplayMessage("Enter filename to import cell site data: ");
            // GetStringInput for name of file
            string fileName = ConsoleOperations.GetStringInput();

            // LoadCsvData
            List<CellModel> cells = FrequencyAllocationOperations.LoadCsvData(fileName);
            ConsoleOperations.ShowUserImportedData(cells);

            // CalculateConflictDistance
            ConsoleOperations.DisplayMessage("Calculating Conflict distances of cells...");


            // CreateConflictGraph
            ConsoleOperations.DisplayMessage("Calculating Conflict graph of cells...");

            // FrequencyAllocation
            ConsoleOperations.DisplayMessage("Allocating frequencies to cells...");

            // DisplayAllocatedFrequencies
            ConsoleOperations.DisplayMessage("Frequency Allocation completed.");

            // Process another cell site?
        }
    }
}
