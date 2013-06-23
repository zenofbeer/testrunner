using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using net.PaulChristensen.TestHarnessLib;
using net.PaulChristensen.TestRunnerDataLink.Managers;
using net.PaulChristensen.TestRunnerDataLink.Repositories;

namespace net.PaulChristensen.TestRunner
{
    class Program
    {

        private bool _currentTestPassed;
        //private TestBatch _testBatch;


        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<XmlTestSuiteRepositoryRepository>().As<ITestSuiteRepository>();
            builder.RegisterType<TestBuilderManager>().As<ITestBuilderManager>();
            
            using (var container = builder.Build())
            {
                var manager = container.Resolve<ITestBuilderManager>();
                IHarness harness = new Harness(manager);
            }            
        }
    }
}