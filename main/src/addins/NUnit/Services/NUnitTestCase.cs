//
// NUnitTestCase.cs
//
// Author:
//   Lluis Sanchez Gual
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//


using System;
using System.Collections;

using MonoDevelop.Core;
using NUnit.Core;
using MonoDevelop.NUnit.External;

namespace MonoDevelop.NUnit
{
	class NUnitTestCase: UnitTest
	{
		NUnitAssemblyTestSuite rootSuite;
		string className;
		string pathName;
		
		public NUnitTestCase (NUnitAssemblyTestSuite rootSuite, NunitTestInfo tinfo, string className) : base (tinfo.Name)
		{
			this.className = className;
			this.pathName = tinfo.PathName;
			this.rootSuite = rootSuite;
			this.TestId = tinfo.TestId;
		}
		
		public string ClassName {
			get { return className; }
		}
		
		protected override UnitTestResult OnRun (TestContext testContext)
		{
			return rootSuite.RunUnitTest (this, className, pathName, Name, testContext);
		}
		
		protected override bool OnCanRun (MonoDevelop.Core.Execution.IExecutionHandler executionContext)
		{
			return rootSuite.CanRun (executionContext);
		}

		
		public override SourceCodeLocation SourceCodeLocation {
			get {
				UnitTest p = Parent;
				while (p != null) {
					NUnitAssemblyTestSuite root = p as NUnitAssemblyTestSuite;
					if (root != null)
						return root.GetSourceCodeLocation (this);
					p = p.Parent;
				}
				return null; 
			}
		}
	}
}

