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
    public enum TestResult
    {
        StatusSuccess = 0,
        StatusFailed = 1,
        StatusUnknown = 3,
        StatusInProgress = 4,
        StatusPreparingTest = 5
    }

    public enum TestRunStatus
    {
        StatusTestPreparing = 0,
        StatusTestInprocess = 1,
        StatusTestComplete = 2
    }

    public enum TestApplicationKind
    {
        Dll = 0
    }
}