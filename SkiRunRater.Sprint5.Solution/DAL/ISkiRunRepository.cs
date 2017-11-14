using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public interface ISkiRunRepository : IDisposable
    {
        SkiRun SelectSkiRun(int userID);
        SkiRun SelectSkiRun(string skiRunName);
        List<SkiRun> SelectAllSkiRuns();
        List<SkiRun> SelectSkiRuns(string selectString);
        void InsertSkiRun(SkiRun skiRun);
        void UpdateSkiRun(SkiRun skiRun);
        void DeleteSkiRun(int ID);
    }
}
