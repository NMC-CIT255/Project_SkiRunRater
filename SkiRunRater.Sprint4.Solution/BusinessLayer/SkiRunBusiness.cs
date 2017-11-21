using System;
using System.Collections.Generic;
using System.Linq;

namespace SkiRunRater
{
    public class SkiRunBusiness : IDisposable, ISkiRunRepository
    {
        ISkiRunRepository _skiRunRepository;

        public SkiRunBusiness(ISkiRunRepository repository)
        {
            _skiRunRepository = repository;
        }

        public void Insert(SkiRun skiRun)
        {
            _skiRunRepository.Insert(skiRun);
        }

        public void Delete(int ID)
        {
            _skiRunRepository.Delete(ID);
        }

        public void Update(SkiRun skiRun)
        {
            _skiRunRepository.Update(skiRun);
        }

        public SkiRun SelectById(int id)
        {
            return _skiRunRepository.SelectById(id);
        }

        public List<SkiRun> SelectAll()
        {
            return _skiRunRepository.SelectAll();
        }

        /// <summary>
        /// method to query the data by the vertical of each ski run in feet
        /// </summary>
        /// <param name="minimumVertical">int minimum vertical</param>
        /// <param name="maximumVertical">int maximum vertical</param>
        /// <returns></returns>
        public List<SkiRun> QueryByVertical(int minimumVertical, int maximumVertical)
        {
            List<SkiRun> skiRuns = _skiRunRepository.SelectAll();
                
            return skiRuns.Where(sr => sr.Vertical >= minimumVertical && sr.Vertical <= maximumVertical).ToList();
        }

        public void Save()
        {
            _skiRunRepository.Save();
        }

        public void Dispose()
        {
            _skiRunRepository = null;
        }
    }
}
