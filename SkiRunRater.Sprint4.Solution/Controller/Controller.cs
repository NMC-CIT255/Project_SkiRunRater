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
        static ISkiRunRepository skiRunRepository;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            skiRunRepository = new SkiRunRepositoryXML();

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
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);
            List<SkiRun> skiRuns;

            using (skiRunBusiness)
            {
                skiRuns = skiRunBusiness.SelectAll();
                ConsoleView.DisplayAllSkiRuns(skiRuns);
                ConsoleView.DisplayContinuePrompt();
            }
        }

        private static void DisplaySkiRunDetail()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);

            List<SkiRun> skiRuns;
            SkiRun skiRun;
            int skiRunID;

            using (skiRunBusiness)
            {
                skiRuns = skiRunBusiness.SelectAll();
                skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                skiRun = skiRunBusiness.SelectById(skiRunID);
            }

            ConsoleView.DisplaySkiRun(skiRun);
            ConsoleView.DisplayContinuePrompt();
        }

        private static void AddSkiRun()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);

            SkiRun skiRun;

            skiRun = ConsoleView.AddSkiRun();
            using (skiRunBusiness)
            {
                skiRunBusiness.Insert(skiRun);
            }

            ConsoleView.DisplayContinuePrompt();
        }

        private static void UpdateSkiRun()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);

            List<SkiRun> skiRuns;
            SkiRun skiRun;
            int skiRunID;

            using (skiRunBusiness)
            {
                skiRuns = skiRunBusiness.SelectAll();
                skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                skiRun = skiRunBusiness.SelectById(skiRunID);
                skiRun = ConsoleView.UpdateSkiRun(skiRun);
                skiRunBusiness.Update(skiRun);
            }
        }

        private static void DeleteSkiRun()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);

            List<SkiRun> skiRuns;
            int skiRunID;
            string message;

            using (skiRunBusiness)
            {
                skiRuns = skiRunBusiness.SelectAll();
                skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                skiRunBusiness.Delete(skiRunID);
            }

            ConsoleView.DisplayReset();

            // TODO refactor to confirm
            message = String.Format("Ski Run ID: {0} had been deleted.", skiRunID);

            ConsoleView.DisplayMessage(message);
            ConsoleView.DisplayContinuePrompt();
        }

        private static void QuerySkiRunsByVertical()
        {
            SkiRunBusiness skiRunBusiness = new SkiRunBusiness(skiRunRepository);

            List<SkiRun> matchingSkiRuns;
            int minimumVertical;
            int maximumVertical;

            ConsoleView.GetVerticalQueryMinMaxValues(out minimumVertical, out maximumVertical);

            using (skiRunBusiness)
            {
                matchingSkiRuns = skiRunBusiness.QueryByVertical(minimumVertical, maximumVertical);
            }

            ConsoleView.DisplayQueryResults(matchingSkiRuns);
            ConsoleView.DisplayContinuePrompt();
        }

        #endregion

    }
}
