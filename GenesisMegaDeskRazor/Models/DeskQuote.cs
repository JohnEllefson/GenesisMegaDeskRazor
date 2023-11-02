using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Newtonsoft.Json;

namespace GenesisMegaDeskRazor.Models
{
    public class DeskQuote
    {

        public int Id { get; set; }
        [JsonProperty]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double BaseDeskPrice = 200;
        public double AdditionalSqInchCost { get; set; }
        public double DrawerCost { get; set; }
        public double SurfaceMaterialCost { get; set; }
        public double RushOrderCost { get; set; }
        [JsonProperty]
        public double TotalPrice { get; set; }
        public double[,]? rushOrderPrices;
        public int DeskId { get; set; }

        // Calculate the cost of adding more desk area
        private void CalcAdditionalSqInchCost(int width, int depth)
        {
            double pricePerExtraInch = 1;
            double squareInches = width * depth;

            if (squareInches > 1000)
            {
                AdditionalSqInchCost = pricePerExtraInch * (squareInches - 1000);
            }
        }

        // Calculate the added cost of drawers
        private void CalcDrawerCost(int drawers)
        {
            double costPerDrawer = 50;

            DrawerCost = costPerDrawer * drawers;
        }


        private void CalcRushOrderCost(int width, int depth, int rush)
        {
            double sqInch = width * depth;

            // If rush days are 14, then there is no rush or added cost.
            if (rush == 14)
            {
                RushOrderCost = 0;
                return;
            }

            // Call GetRushOrder to populate rushOrderPrices array
            GetRushOrder();

            // Headers help to easily find the coordinates of the priceChart
            int[] rushOrderHeader = { 3, 5, 7 };
            int[] deskSizeHeader = { 1000, 2000, 4610 }; //4608 is the max area given the maximum values of width and depth.

            // Find the index corresponding to sqInch is less than the value.
            int indexSize = Array.FindIndex(deskSizeHeader, header => sqInch < header);

            // Find the index that matches the value of rush days
            int indexRush = Array.IndexOf(rushOrderHeader, rush);

            // Check if rushOrderPrices is null, indicating an error in reading the file
            if (rushOrderPrices == null)
            {
                // Handle the error, for example, by returning a default value or showing an error message.
                Console.WriteLine("Error: Unable to retrieve rush order prices.");
                return; // Default value, or handle the error as needed.
            }

            // Calculate the rush order cost using rushOrderPrices array
            RushOrderCost = rushOrderPrices[indexRush, indexSize];

         
        }

        // New method to handle rush order prices
        private void GetRushOrder()
        {
            try
            {
                string filePath = "rushOrderPrices.txt";
                string[] lines = System.IO.File.ReadAllLines(filePath);

                // Initialize the existing class-level 2D array with three rows and three columns
                rushOrderPrices = new double[3, 3];

                int row = 0;
                int col = 0;

                foreach (string line in lines)
                {
                    // Parse the price from each line and add it to the array
                    if (row < 3 && col < 3)
                    {
                        if (double.TryParse(line, out double price))
                        {
                            rushOrderPrices[row, col] = price;
                            col++;
                        }
                    }

                    if (col >= 3)
                    {
                        col = 0;
                        row++;
                    }

                    if (row >= 3)
                    {
                        break; // We have filled the entire 3x3 array
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading rush order prices: {ex.Message}");
            }
        }


        public void CalcTotalPrice(Desk Desk)
        {
            CalcAdditionalSqInchCost(Desk.Width, Desk.Depth);
            CalcDrawerCost(Desk.NumberOfDrawers);
            SurfaceMaterialCost = (double) Desk.Material;
            CalcRushOrderCost(Desk.Width, Desk.Depth, Desk.RushOrderDays);
            TotalPrice = BaseDeskPrice + AdditionalSqInchCost + DrawerCost + SurfaceMaterialCost + RushOrderCost;
        }
    }
}
