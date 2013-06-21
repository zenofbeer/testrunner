/*
 * TestRunner - TestHarnessLib, a test harness tool.
 * Copyright (C) 2010 - Paul Christensen
 * http://www.PaulChristensen.net
 * Paul@PaulChristensen.net
 * 
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;

namespace net.PaulChristensen.TestHarnessLib
{
    public interface IHarness
    {
        #region methods
        void SetStatusMessage(string statusMessage);
        void SetTestResults(TestResult status);
        void SetConsoleTitle(string consoleTitle);        
        void AddTestToQue(ITest iTest);
        bool GetAssemblyDependency(Dictionary<string, string> dependencyDictionary, string dependencyKey, out string dependencyPath, out string dependencyType);

        [Obsolete("Use GetDependencyValue<T> instead", true)]
        string GetDependencyValue(Dictionary<string, string> dependencyDictionary, string dependencyKey);
        T GetDependencyValue<T>(Dictionary<string, string> dependencyDictionary, string dependencyKey);
        void LogDebug(string message);
        void LogError(string message);
        void LogInfo(string message);
        #endregion methods

        #region events
        void ExecuteButton_Click(object sender, EventArgs e);
        void ClearQue_Click(object sender, EventArgs e);
        #endregion events

        #region properties
        string CurrentTestStatus { get; set; }
        string CurrentTestResult { get; set; }
        object[] ConstructorParameters { set; }
        #endregion properties
    }
}