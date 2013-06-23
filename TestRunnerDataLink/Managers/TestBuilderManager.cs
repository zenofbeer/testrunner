using System;
using System.Collections.Generic;
using net.PaulChristensen.TestRunnerDataLink.Entities;
using net.PaulChristensen.TestRunnerDataLink.Repositories;

namespace net.PaulChristensen.TestRunnerDataLink.Managers
{
    public class TestBuilderManager : ITestBuilderManager
    {
        private readonly ITestSuiteRepository _testSuiteRepository;

        public TestBuilderManager(ITestSuiteRepository testSuiteRepository)
        {
            _testSuiteRepository = testSuiteRepository;
        }

        public int GetTestCount()
        {
            return _testSuiteRepository.GetTestCount();
        }

        [Obsolete("Remove this method. it is not a valid way of populating the object")]
        public Dictionary<string, string> GetTestSuiteDefinition()
        {
            return _testSuiteRepository.GetTestSuiteDefinition();
        }

        public TestSuite GetTestSuite(int testSuiteId)
        {
            return _testSuiteRepository.GetTestSuite(testSuiteId);
        }

        public List<TestEntity> GetTests(int testSuiteId)
        {
            return _testSuiteRepository.GetTests(testSuiteId);
        }
    }
}