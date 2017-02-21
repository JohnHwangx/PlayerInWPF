using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerTest
{
    public class StringTest
    {
        public void JoinTest()
        {
            var list=new List<string>
            {
                "aaaaaaa ",
                "bbbbbbb ",
                "ccccccc"
            };
            Console.WriteLine(string.Join("=1 and",list.Select(i=>"["+i+"]")));
        }

        public void ConactTest()
        {
            var list = new List<string>
            {
                "aaaaaaa ",
                "bbbbbbb ",
                "ccccccc"
            };
        }

        public void ChangeClass(List<TestClass> testClasses)
        {
            if (testClasses == null) throw new ArgumentNullException(nameof(testClasses));
            testClasses = new List<TestClass>
            {
                new TestClass {Name = "new name1", Property = "new property1"},
                new TestClass {Name="new name2",Property="new property2" },
                new TestClass {Name="new name3" ,Property="new property3"}
            };
        }
    }
}