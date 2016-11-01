using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class InitializeDataFile
    {

        public static void AddTestData()
        {
            List<SkiRun> skiRuns = new List<SkiRun>();

            // initialize the IList of high scores - note: no instantiation for an interface
            skiRuns.Add(new SkiRun() { ID = 1, Name = "Buck", Vertical = 200 });
            skiRuns.Add(new SkiRun() { ID = 2, Name = "Buckaroo", Vertical = 325 });
            skiRuns.Add(new SkiRun() { ID = 3, Name = "Hoot Owl", Vertical = 655 });
            skiRuns.Add(new SkiRun() { ID = 4, Name = "Shelburg's Chute", Vertical = 1023 });

            WriteAllSkiRuns(skiRuns, DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to write all ski run info to the data file
        /// </summary>
        /// <param name="skiRuns">list of ski run info</param>
        /// <param name="dataFilePath">path to the data file</param>
        public static void WriteAllSkiRuns(List<SkiRun> skiRuns, string dataFilePath)
        {
            string csvText;
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            // build out the StringBuilder object from the list of ski runs in CSV format
            foreach (var skiRun in skiRuns)
            {
                sb.AppendLine(skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical);
            }

            // convert the StringBuilder object to a string
            csvText = sb.ToString();

            // write the CSV formated string to a file
            using (StreamWriter streamWriter = new StreamWriter(dataFilePath))
            {
                streamWriter.Write(csvText);
            }
        }
    }
}
