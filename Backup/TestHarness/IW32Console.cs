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
namespace net.PaulChristensen.TestHarnessLib
{
    public interface IW32Console
    {
        #region properties
        string CurrentTestId { get; set; }
        string CurrentTestName { get; set; }
        string CurrentStatusMessage { get; set; }
        string CurrentTestResults { get; set; }
        /// <summary>
        /// provides the total number of available tests
        /// </summary>
        string TestCountString { get; set; }
        /// <summary>
        /// gives the count of the current executing test
        /// </summary>
        string CurrentTestCountString { get; set; }
        string TestTitle { get; set; }
        #endregion properties

        #region methods
        void BeginNewTest();
        void AddAvailableTest(ITest iTest);
        void SetLogFilePath(string logFilePath);
        #endregion methods
    }
}