using FrequencyAllocationLibrary;
using FrequencyAllocationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAllocationApp
{
    public class ConsoleOperations
    {
        public static void WelcomeUser()
        {
            Console.WriteLine("Welcome to the Frequency Allocation Utility");
            Console.WriteLine("*******************************************");
            Console.WriteLine();
        }

        public static void DisplayAppInfo()
        {
            Console.WriteLine("This app servers as a utility to allocate frquencies to cell sites:");
            Console.WriteLine("1) Upload a csv file with Cell ID, Easting and Northing values into the bin directory");
            Console.WriteLine("Example:");
            Console.WriteLine("Cell ID, Easting, Northing");
            Console.WriteLine("A,536660,183800");
            Console.WriteLine("2) Utility will process data and assign frequencies (110, 111, 112, 113, 114, 115)");
            Console.WriteLine("3) Cell sites and their allocated frequency will be displayed");
            Console.WriteLine();
        }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine($"{message}");
        }

        public static string GetStringInput()
        {
            string fileText = Console.ReadLine();

            return fileText;
        }

        public static void ShowUserImportedData(List<CellModel> cells)
        {
            Console.WriteLine("The following data has been imported:");
            foreach (CellModel cell in cells)
            {
                Console.WriteLine($"Cell {cell.ID}: {cell.Easting},{cell.Northing}");
            }

            Console.WriteLine();
        }

        public static void DisplayAllocatedFrequencies(List<CellModel> cells)
        {
            foreach (CellModel cell in cells)
            {
                Console.WriteLine($"Cell {cell.ID} = {cell.AllocatedFrequency}");
                Console.WriteLine($"Cell {cell.ID} conflicts with:");
                foreach (string conflictCell in cell.ConflictGraph)
                {
                    foreach (CellModel findCell in cells)
                    {
                        if (findCell.ID == conflictCell)
                        {
                            Console.Write($"{conflictCell}={findCell.AllocatedFrequency}|");
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void DisplayDistances(List<CellModel> cells)
        {
            foreach (CellModel cell in cells)
            {
                Console.WriteLine($"Conflict distance for cell {cell.ID}:");
                foreach (KeyValuePair<string, double> distance in cell.ConflictDistances)
                {
                    Console.Write($"{distance.Key}: {distance.Value}| ");
                }
                Console.WriteLine();
            }
        }

        public static void DisplayConflicts(List<CellModel> cells)
        {
            foreach (CellModel cell in cells)
            {
                Console.WriteLine($"Cell {cell.ID} has conflicts with: ");
                foreach (string conflictCell in cell.ConflictGraph)
                {
                    Console.Write($"{conflictCell}| ");
                }
                Console.WriteLine();
            }
        }

        public static void StartApplication()
        {
            string startAgain = "";
            do
            {
                Console.Clear();

                // WelcomeUser
                WelcomeUser();

                // App Info
                DisplayAppInfo();

                // DisplayMessage for name of file
                DisplayMessage("Enter filename to import cell site data: ");
                // GetStringInput for name of file
                string fileName = GetStringInput();

                // Start timer
                Stopwatch sw = Stopwatch.StartNew();

                // LoadCsvData
                List<CellModel> cells = FrequencyAllocationOperations.LoadCsvData(fileName);
                ShowUserImportedData(cells);

                // CalculateConflictDistance
                DisplayMessage("Calculating Conflict distances of cells...");
                FrequencyAllocationOperations.CalculateConflictDistance(cells);
                DisplayDistances(cells);
                Console.WriteLine();


                // CreateConflictGraph
                DisplayMessage("Calculating Conflict graph of cells...");
                FrequencyAllocationOperations.CreateConflictGraph(cells);
                DisplayConflicts(cells);
                Console.WriteLine();


                // FrequencyAllocation
                DisplayMessage("Allocating frequencies to cells...");
                FrequencyAllocationOperations.FrequencyAllocation(cells);
                Console.WriteLine();


                // DisplayAllocatedFrequencies
                DisplayMessage("Frequency Allocation completed.");
                DisplayAllocatedFrequencies(cells);
                Console.WriteLine();

                // Stop timer and display time
                sw.Stop();
                TimeSpan elapsed = sw.Elapsed;
                Console.WriteLine($"Processing took {elapsed} for {cells.Count} cells");
                Console.WriteLine();

                // Process another cell site?
                DisplayMessage("Process another site? (yes/no):");
                startAgain = GetStringInput();

            } while (startAgain.ToLower() == "yes");
        }
    }
}
