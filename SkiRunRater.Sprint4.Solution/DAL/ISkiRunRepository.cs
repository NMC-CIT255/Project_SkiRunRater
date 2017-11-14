using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public interface ISkiRunRepository
    {
            List<SkiRun> SelectAll();
            SkiRun SelectByID(string id);
            void Insert(SkiRun obj);
            void Update(SkiRun obj);
            void Delete(string id);
            void Save();
    }
}
