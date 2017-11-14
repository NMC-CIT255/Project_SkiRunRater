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
            SkiRunRepositoryXML_DS skiRunRepository = new SkiRunRepositoryXML_DS();

            ConsoleView.DisplayWelcomeScreen();

            using (skiRunRepository)
            {
                List<SkiRun> skiRuns = skiRunRepository.SelectAllRuns();

                int skiRunID;
                SkiRun skiRun;
                string message;

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;

                        case AppEnum.ManagerAction.ListAllSkiRuns:
                            skiRuns = skiRunRepository.SelectAllRuns();
                            ConsoleView.DisplayAllSkiRuns(skiRuns);

                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                            skiRun = skiRunRepository.SelectByID(skiRunID);

                            ConsoleView.DisplaySkiRun(skiRun);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.AddSkiRun:
                            skiRun = ConsoleView.AddSkiRun();
                            skiRunRepository.InsertSkiRun(skiRun);

                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.UpdateSkiRun:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                            skiRun = skiRunRepository.SelectByID(skiRunID);

                            skiRun = ConsoleView.UpdateSkiRun(skiRun);

                            skiRunRepository.UpdateSkiRun(skiRun);
                            break;

                        case AppEnum.ManagerAction.DeleteSkiRun:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                            skiRunRepository.DeleteSkiRun(skiRunID);

                            ConsoleView.DisplayReset();
                            message = String.Format("Ski Run ID: {0} had been deleted.", skiRunID);
                            ConsoleView.DisplayMessage(message);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            List<SkiRun> matchingSkiRuns = new List<SkiRun>();

                            int minimumVertical;
                            int maximumVertical;
                            ConsoleView.GetVerticalQueryMinMaxValues(out minimumVertical, out maximumVertical);

                            matchingSkiRuns = skiRunRepository.QueryByVertical(minimumVertical, maximumVertical);

                            ConsoleView.DisplayQueryResults(matchingSkiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;

                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        #endregion

    }
}
