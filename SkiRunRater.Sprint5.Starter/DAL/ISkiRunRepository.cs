using System;
using System.Collections.Generic;

namespace SkiRunRater
{
    public interface ISkiRunRepository : IDisposable
    {
        SkiRun SelectById(int userID);
        List<SkiRun> SelectAll();
        void Insert(SkiRun skiRun);
        void Update(SkiRun skiRun);
        void Delete(int ID);
    }
}
