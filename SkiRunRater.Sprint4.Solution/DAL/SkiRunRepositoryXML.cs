using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkiRunRater
{
    /// <summary>
    /// ski run repository managing an XML data source
    /// </summary>
    public class SkiRunRepositoryXML : IDisposable, ISkiRunRepository
    {
        private List<SkiRun> _skiRuns;

        public SkiRunRepositoryXML()
        {
            _skiRuns = ReadSkiRunsData(DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to read all ski run information from the data file a
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of SkiRun objects</returns>
        public List<SkiRun> ReadSkiRunsData(string dataFilePath)
        {
            SkiRuns skiRunsFromFile = new SkiRuns();

            // initialize a FileStream object for reading
            StreamReader sReader = new StreamReader(DataSettings.dataFilePath);

            // initialize an XML seriailizer object
            XmlSerializer deserializer = new XmlSerializer(typeof(SkiRuns));

            using (sReader)
            {
                object xmlObject = deserializer.Deserialize(sReader);
                skiRunsFromFile = (SkiRuns)xmlObject;
            }

            return skiRunsFromFile.skiRuns;
        }

        /// <summary>
        /// method to write all of the list of ski runs to the text file
        /// </summary>
        public void Save()
        {
            // initialize a FileStream object for reading
            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePath, false);

            XmlSerializer serializer = new XmlSerializer(typeof(List<SkiRun>), new XmlRootAttribute("SkiRuns"));

            using (sWriter)
            {
                serializer.Serialize(sWriter, _skiRuns);
            }
        }

        /// <summary>
        /// method to add a new ski run
        /// </summary>
        /// <param name="skiRun"></param>
        public void Insert(SkiRun skiRun)
        {
            _skiRuns.Add(skiRun);

            Save();
        }

        /// <summary>
        /// method to delete a ski run by ski run ID
        /// </summary>
        /// <param name="ID"></param>
        public void Delete(int ID)
        {
            _skiRuns.Remove(_skiRuns.FirstOrDefault(sr => sr.ID == ID));

            Save();
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="skiRun">ski run object</param>
        public void Update(SkiRun skiRun)
        {
            Delete(skiRun.ID);
            Insert(skiRun);

            Save();
        }

        /// <summary>
        /// method to return a ski run object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun SelectById(int ID)
        {
            SkiRun skiRun = null;

            skiRun = _skiRuns.FirstOrDefault(sr => sr.ID == ID);

            return skiRun;
        }

        /// <summary>
        /// method to return a list of ski run objects
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> SelectAll()
        {
            return _skiRuns;
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _skiRuns = null;
        }
    }
}
