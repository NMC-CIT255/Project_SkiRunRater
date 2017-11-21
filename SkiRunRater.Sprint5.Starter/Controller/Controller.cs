using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class Controller
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        bool active = true;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            AppEnum.ManagerAction userActionChoice;

            ConsoleView.DisplayWelcomeScreen();

            while (active)
            {
                userActionChoice = ConsoleView.GetUserActionChoice();

                switch (userActionChoice)
                {
                    case AppEnum.ManagerAction.None:
                        break;

                    case AppEnum.ManagerAction.ListAllSkiRuns:
                        ListAllSkiRuns();
                        break;

                    case AppEnum.ManagerAction.DisplaySkiRunDetail:
                        DisplaySkiRunDetail();
                        break;

                    case AppEnum.ManagerAction.AddSkiRun:
                        AddSkiRun();
                        break;

                    case AppEnum.ManagerAction.UpdateSkiRun:
                        UpdateSkiRun();
                        break;

                    case AppEnum.ManagerAction.DeleteSkiRun:
                        DeleteSkiRun();
                        break;

                    case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                        QuerySkiRunsByVertical();
                        break;

                    case AppEnum.ManagerAction.Quit:
                        active = false;
                        break;

                    default:
                        break;
                }

            }

            ConsoleView.DisplayExitPrompt();
        }

        private static void ListAllSkiRuns()
        {
            SkiRunRepositorySQL skiRunRepository = new SkiRunRepositorySQL();
            List<SkiRun> skiRuns;

            using (skiRunRepository)
            {
                skiRuns = skiRunRepository.SelectAll();
                ConsoleView.DisplayAllSkiRuns(skiRuns);
                ConsoleView.DisplayContinuePrompt();
            }
        }

        private static void DisplaySkiRunDetail()
        {
            SkiRunRepositorySQL skiRunRepository = new SkiRunRepositorySQL();
            List<SkiRun> skiRuns;
            SkiRun skiRun = new SkiRun();
            int skiRunID;

            using (skiRunRepository)
            {
                skiRuns = skiRunRepository.SelectAll();
                skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                skiRun = skiRunRepository.SelectById(skiRunID);
            }

            ConsoleView.DisplaySkiRun(skiRun);
            ConsoleView.DisplayContinuePrompt();
        }

        private static void AddSkiRun()
        {
            SkiRunRepositorySQL skiRunRepository = new SkiRunRepositorySQL();
            SkiRun skiRun = new SkiRun();

            skiRun = ConsoleView.AddSkiRun();
            using (skiRunRepository)
            {
                skiRunRepository.Insert(skiRun);
            }

            ConsoleView.DisplayContinuePrompt();
        }

        private static void UpdateSkiRun()
        {
            SkiRunRepositorySQL skiRunRepository = new SkiRunRepositorySQL();
            List<SkiRun> skiRuns = skiRunRepository.SelectAll();
            SkiRun skiRun = new SkiRun();
            int skiRunID;

            using (skiRunRepository)
            {
                skiRuns = skiRunRepository.SelectAll();
                skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                skiRun = skiRunRepository.SelectById(skiRunID);
                skiRun = ConsoleView.UpdateSkiRun(skiRun);
                skiRunRepository.Update(skiRun);
            }
        }

        private static void DeleteSkiRun()
        {
            SkiRunRepositorySQL skiRunRepository = new SkiRunRepositorySQL();
            List<SkiRun> skiRuns = skiRunRepository.SelectAll();
            SkiRun skiRun = new SkiRun();
            int skiRunID;
            string message;

            skiRunID = ConsoleView.GetSkiRunID(skiRuns);

            using (skiRunRepository)
            {
                skiRunRepository.Delete(skiRunID);
            }

            ConsoleView.DisplayReset();

            // TODO refactor
            message = String.Format("Ski Run ID: {0} had been deleted.", skiRunID);

            ConsoleView.DisplayMessage(message);
            ConsoleView.DisplayContinuePrompt();
        }

        private static void QuerySkiRunsByVertical()
        {
            SkiRunRepositorySQL skiRunRepository = new SkiRunRepositorySQL();
            IEnumerable<SkiRun> matchingSkiRuns = new List<SkiRun>();
            int minimumVertical;
            int maximumVertical;

            ConsoleView.GetVerticalQueryMinMaxValues(out minimumVertical, out maximumVertical);

            using (skiRunRepository)
            {
                matchingSkiRuns = skiRunRepository.QueryByVertical(minimumVertical, maximumVertical);
            }

            ConsoleView.DisplayQueryResults(matchingSkiRuns);
            ConsoleView.DisplayContinuePrompt();
        }

        #endregion

    }
}
