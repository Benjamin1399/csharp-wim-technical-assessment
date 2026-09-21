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
            FrequencyAllocationOperations.CalculateConflictDistance(cells);
            ConsoleOperations.DisplayDistances(cells);
            Console.WriteLine();


            // CreateConflictGraph
            ConsoleOperations.DisplayMessage("Calculating Conflict graph of cells...");
            FrequencyAllocationOperations.CreateConflictGraph(cells);
            ConsoleOperations.DisplayConflicts(cells);
            Console.WriteLine();


            // FrequencyAllocation
            ConsoleOperations.DisplayMessage("Allocating frequencies to cells...");
            FrequencyAllocationOperations.FrequencyAllocation(cells);


            // DisplayAllocatedFrequencies
            ConsoleOperations.DisplayMessage("Frequency Allocation completed.");
            ConsoleOperations.DisplayAllocatedFrequencies(cells);

            // Process another cell site?
        }
    }
}
