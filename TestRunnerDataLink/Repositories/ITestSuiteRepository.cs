using System.Collections.Generic;

namespace net.PaulChristensen.TestRunnerDataLink.Repositories
{
    public interface ITestSuiteRepository
    {
        /// <summary>
        /// get the number of tests in the suite
        /// </summary>
        /// <returns></returns>
        int GetTestCount();
        /// <summary>
        /// get the test definition containing test suite global properties
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetTestSuiteDefinition();
    }
}