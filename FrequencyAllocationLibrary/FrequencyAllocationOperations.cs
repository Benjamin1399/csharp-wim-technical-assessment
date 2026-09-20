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
