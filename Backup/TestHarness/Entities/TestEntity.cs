using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using net.PaulChristensen.TestHarnessLib.Util;

namespace net.PaulChristensen.TestHarnessLib.Entities
{
    /// <summary>
    /// object that represents a "test" element in the configuration file.
    /// </summary>
    public class TestEntity
    {
        #region private constants
        #endregion private constants
        #region private fields
        #endregion private fields

        public TestEntity(XElement testElement, Dictionary<string, string> testProperties)
        {
            ProcessTestElement(testElement.Attributes(), testProperties);
            ProcessDependencyElements(testElement.Elements(Constants.ElementDependency), testProperties);
        }

        #region public properties
        /// <summary>
        /// The test name.
        /// </summary>
        public string TestName { get; private set; }

        /// <summary>
        /// description of the test.
        /// </summary>
        public string TestDescription { get; private set; }

        /// <summary>
        /// the path where the test can be found.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// the file name of the test.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// the fully qualified class name
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// the number of times to execute the test.
        /// </summary>
        public int RepeatCount { get; private set; }

        /// <summary>
        /// dictionary representing Test properties.
        /// </summary>
        public Dictionary<string, string> Properties { get; private set; }

        /// <summary>
        /// dictionary representing test library dependencies.
        /// </summary>
        public List<TestDependencyEntity> Dependencies { get; private set; }
        #endregion public properties

        #region private methods
        private void ProcessTestElement(IEnumerable<XAttribute> attributes, IDictionary<string, string> testProperties)
        {
            var newAttributeCollection = new Dictionary<string, string>();

            foreach (var attribute in attributes)
            {
                if (StringHelper.IsVariable(attribute.Value))
                    ProcessVariableAttribute(attribute, testProperties);
                else if (attribute.Name == Constants.TestNameAttribute)
                    TestName = attribute.Value;
                else if (attribute.Name == Constants.TestDescriptionAttribute)
                    TestDescription = attribute.Value;
                else if (attribute.Name == Constants.TestTypeNameAttribute)
                    TypeName = attribute.Value;
                else if (attribute.Name == Constants.TestRepeatCountAttribute)
                    RepeatCount = Convert.ToInt32(attribute.Value);
            }
        }

        private void ProcessDependencyElements(IEnumerable<XElement> elements, IDictionary<string, string> testProperties)
        {
            Dependencies = new List<TestDependencyEntity>();
            foreach (var element in elements)
            {
                Dependencies.Add(new TestDependencyEntity(element, testProperties));
            }
        }

        private void ProcessVariableAttribute(XAttribute attribute, IDictionary<string, string> testProperties)
        {
            string tempValue = attribute.Value;
            StringHelper.StripVariableDelimiters(ref tempValue);
            StringHelper.TrimLeadingSlashes(ref tempValue);
            if (attribute.Name.ToString() == Constants.TestPathAttribute)
            {
                if (tempValue.Length > 0)
                {
                    StringHelper.TrimTrailingSlashes(ref tempValue);
                }
            }
            Path = testProperties[Constants.TestPathAttribute] + tempValue;            
        }
        #endregion private methods
    }
}