using System.Collections.Generic;
using net.PaulChristensen.TestRunnerDataLink.Entities;

namespace net.PaulChristensen.TestRunnerDataLink.Managers
{
    public interface ITestBuilderManager
    {
        int GetTestCount();
        TestSuite GetTestSuite(int testSuiteId);
        List<TestEntity> GetTests(int testSuiteId);
    }
}