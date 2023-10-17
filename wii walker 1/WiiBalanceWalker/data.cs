using System;
using System.IO;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        string csvFilePath = @"C:\Users\LENOVO\Desktop\wii walker 1\WiiBalanceWalker\bin\x64\Debug\data.csv";
        string newDataFilePath = @"C:\Users\LENOVO\Desktop\wii walker 1\WiiBalanceWalker\bin\x64\Debug\latest_data.csv";

        // Create a timer to periodically update the data
        Timer timer = new Timer(UpdateData, new { CsvFilePath = csvFilePath, NewDataFilePath = newDataFilePath }, 0, 1000); // Update every second (1000 ms)

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

        timer.Dispose();
    }

    static void UpdateData(object state)
    {
        var args = (dynamic)state;
        string csvFilePath = args.CsvFilePath;
        string newDataFilePath = args.NewDataFilePath;

        try
        {
            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvFilePath);

            // Ensure there's data to process
            if (lines.Length > 0)
            {
                // Get the latest line (assumes new data is added at the end)
                string latestData = lines.Last();

                // Write the latest data to the new data file
                File.WriteAllText(newDataFilePath, latestData);

                Console.WriteLine($"Updated data: {latestData}");
            }
            else
            {
                Console.WriteLine("No data found in the CSV file.");
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
