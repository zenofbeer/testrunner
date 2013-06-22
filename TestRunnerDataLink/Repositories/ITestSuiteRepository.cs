namespace net.PaulChristensen.TestRunnerDataLink.Repositories
{
    public interface ITestSuiteRepository
    {
        /// <summary>
        /// get the number of tests in the suite
        /// </summary>
        /// <returns></returns>
        int GetTestCount();
    }
}