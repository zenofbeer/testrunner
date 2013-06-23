using System.Collections.Generic;
using net.PaulChristensen.TestRunnerDataLink.Entities;

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
        /// <summary>
        /// get a test suite
        /// </summary>
        /// <param name="testSuiteId"></param>
        /// <returns></returns>
        TestSuite GetTestSuite(int testSuiteId);
        /// <summary>
        /// get the list of tests based on the testSuiteId
        /// </summary>
        /// <param name="testSuiteId"></param>
        /// <returns></returns>
        List<TestEntity> GetTests(int testSuiteId);
    }
}