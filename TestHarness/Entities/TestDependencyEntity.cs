using System.Collections.Generic;
using System.Xml.Linq;
using net.PaulChristensen.TestHarnessLib.Util;

namespace net.PaulChristensen.TestHarnessLib.Entities
{
    public class TestDependencyEntity
    {
        public TestDependencyEntity(XElement element, IDictionary<string, string> testProperties)
        {
            TypeNames = new List<string>();
            ProcessDependencyElement(element, testProperties);
        }

        /// <summary>
        /// the path to the dependency library
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// the name of the dependency (the full assembly name minus the extension)
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// the file extension of the dependency
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// a list of class names of one or more classes in an assembly.
        /// </summary>
        public List<string> TypeNames { get; private set; }

        #region private fields
        private void ProcessDependencyElement(XElement element, IDictionary<string, string> testProperties)
        {
            var attributes = element.Attributes();
            foreach (var attribute in attributes)
            {
                string tempValue = attribute.Value;
                if (StringHelper.IsVariable(tempValue))
                {
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
                else if (attribute.Name == Constants.TestNameAttribute)
                    Name = tempValue;
                else if (attribute.Name == Constants.DependencyExtensionAttribute)
                    Extension = tempValue;
                else if(attribute.Name == Constants.ElementTypeName)
                {
                    TypeNames.Add(tempValue);
                }
            }
            ProcessTypeNameElements(element);
        }

        private void ProcessTypeNameElements(XElement element)
        {
            IEnumerable<XElement> typeNames = element.Elements(Constants.ElementTypeName);
            foreach (var name in typeNames)
            {
                TypeNames.Add(name.Value);
            }
        }
        #endregion private fields
    }
}