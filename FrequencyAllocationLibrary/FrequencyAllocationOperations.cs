using FrequencyAllocationLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAllocationLibrary
{
    public class FrequencyAllocationOperations
    {
        public static void CalculateConflictDistance(List<CellModel> cells)
        {
            // Calculate conflict distances using Pythagorean formula
            // For cell A, calcuate Cell B, C , .. till S
            // For Cell B, calculatr cell C, D, .. till S

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = i + 1; j < cells.Count; j++)
                {
                    double conflictDistance = PythagoreanDistance(cells[i], cells[j]);

                    cells[i].ConflictDistances.Add(cells[j].ID, conflictDistance);
                }
            }
        }

        private static double PythagoreanDistance(CellModel firstCell, CellModel secondCell)
        {
            double eastingDiff = secondCell.Easting - firstCell.Easting;
            double northingDiff = secondCell.Northing - firstCell.Northing;
            
            double eastingDiffSquared = Math.Pow(eastingDiff, 2);
            double northingDiffSquared = Math.Pow(northingDiff, 2);
            
            double sum = eastingDiffSquared + northingDiffSquared;

            double distance = Math.Sqrt(sum);

            return distance;
        }

        public static void CreateConflictGraph(List<CellModel> cells)
        {
            // we have the conflict distance, now lets compare threshold and add cell to graph

            foreach (CellModel cell in cells)
            {
                foreach (KeyValuePair<string, double> conflictCell in cell.ConflictDistances)
                {
                    if (conflictCell.Value <= (double)Enums.ConflictThreshold.Threshold500m)
                    {
                        // found conflict so assign cell to conflict graph
                        cell.ConflictGraph.Add(conflictCell.Key);

                        // now need to assign the this cell to Key,
                        // e.g if we are on cell A and cell B is conflict then cell A -> B
                        // then we must add A to B's conflict graph as well
                        foreach (CellModel secondCell in cells)
                        {
                            if (secondCell.ID == conflictCell.Key)
                            {
                                secondCell.ConflictGraph.Add(cell.ID);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void FrequencyAllocation(List<CellModel> cells)
        {
            foreach (CellModel cell in cells)
            {
                foreach (string conflictCell in cell.ConflictGraph)
                {
                    if (cell.AllocatedFrequency == null)
                    {
                        cell.AllocatedFrequency = (int)Enums.Frequencies.BaseFrequency;
                    }

                    // find conflict cell
                    foreach (CellModel findCell in cells)
                    {
                        if (findCell.ID == conflictCell)
                        {
                            // Found conflict cell so lets check the freq
                            if (cell.AllocatedFrequency == findCell.AllocatedFrequency)
                            {
                                // Cell has same frequency as conflict cell so lets increase Cell Freq
                                cell.AllocatedFrequency++;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static List<CellModel> LoadCsvData(string fileName)
        {
            List<CellModel> cells = new List<CellModel>();

            using (var reader = new StreamReader(fileName))
            {
                string header = reader.ReadLine();

                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    CellModel cell = new CellModel();

                    string[] cellLine = line.Split(',');

                    //Cell ID
                    cell.ID = cellLine[0];

                    // Easting
                    cell.Easting = Convert.ToDouble(cellLine[1]);

                    // Northing
                    cell.Northing = Convert.ToDouble(cellLine[2]);

                    // Add cell to list of cells
                    cells.Add(cell);
                }
            }

            return cells;
        }
    }
}
