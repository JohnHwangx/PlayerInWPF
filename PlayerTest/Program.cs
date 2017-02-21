using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerTest
{
    class Program
    {
        public TestClass TestClass;
        public List<TestClass> TestClasses;
        static void Main(string[] args)
        {;
            var prog = new Program
            {
                TestClasses = new List<TestClass>()
            };
            //prog.TestClass = new TestClass();
            //prog.ChangeClass(prog.TestClass);
            //prog.ChangeClass();

            //prog.ChangeClass(out prog._testClasses);
            //Console.WriteLine(string.Join("\n", prog._testClasses.Select(i => i.Name + ", " + i.Property).ToList()));
            //Console.WriteLine("1111");
            //Console.WriteLine(string.Join("\n", prog._testClasses.Select(i => i.Name + ", " + i.Property)));

            //Console.WriteLine(prog.IsClassEqual());

            var testClass3S=prog.GetTestClass3S();
            prog.ShowTestClass2S(prog.GetTestClass2S(testClass3S));
			Console.Write("Hello World");
        }

        public List<TestClass2> GetTestClass2S()
        {
            var testClass2S = new List<TestClass2>
            {
                new TestClass2 {Name = "name1", Property = "property1", Property2 = "property2"},
                new TestClass2 {Name = "name1", Property = "property1", Property2 = "perperty2.1"},
                new TestClass2 {Name = "name2", Property = "property1.2", Property2 = "property2.2"},
                new TestClass2 {Name = "name2", Property = "property1", Property2 = "property2"},
                new TestClass2 {Name = "name3", Property = "property1.2", Property2 = "property2.3"}
            };
            return testClass2S;
        }
        public List<TestClass3> GetTestClass3S()
        {
            var testClass3S = new List<TestClass3>
            {
                new TestClass3 {Name = "name1", Property = "property1", Property2 = "property2",Property3="property3"},
                new TestClass3 {Name = "name1", Property = "property1", Property2 = "property2",Property3="property3.2"},
                new TestClass3 {Name = "name2", Property = "property2", Property2 = "property3",Property3="property3.2"},
                new TestClass3 {Name = "name2", Property = "property2", Property2 = "property3",Property3="property3"},
                new TestClass3 {Name = "name2", Property = "property3", Property2 = "property2",Property3="property3.2"},
                new TestClass3 {Name = "name2", Property = "property4", Property2 = "property2",Property3="property3.5"},
                new TestClass3 {Name = "name3", Property = "property1", Property2 = "property1",Property3="property3.1"}
            };
            return testClass3S;
        }

        public List<TestClass> GetTestClasses(List<TestClass2> testClass2S)
        {
            var testClasses=new List<TestClass>();

            foreach (var testClass2 in testClass2S)
            {
                //if (testClasses.Contains(testClass2))
                //{
                //    continue;
                //}
                //testClasses.Add(testClass2);
                var isDifferent = true;
                foreach (var testClass in testClasses)
                {
                    if (testClass.Equals(testClass2))
                    {
                        isDifferent = false;
                        break;
                    }
                }
                if (isDifferent)
                {
                    testClasses.Add(testClass2);
                }
            }

            return testClasses;
        }

        public List<TestClass2> GetTestClass2S(List<TestClass3> testClass3S)
        {
            var testClass2S = new List<TestClass2>();

            foreach (var testClass3 in testClass3S)
            {
                var isDifferent = true;
                foreach (var testClass in testClass2S)
                {
                    if (testClass.Equals(testClass3))
                    {
                        isDifferent = false;
                        break;
                    }
                }
                if (isDifferent)
                {
                    TestClass testClass = testClass3;
                    TestClass2 testClass2 = new TestClass2();
                    testClass2.Clone(testClass);
                    testClass2.Property2 = testClass3.Property2;
                    testClass2S.Add(testClass2);
                }
            }

            return testClass2S;
        }

        public bool IsClassEqual()
        {
            TestClass testClass1 = new TestClass
            {
                Name="name",
                Property="property"
            };
            TestClass2 testClass2 = new TestClass2
            {
                Name="name",
                Property="property",
                Property2="property2"
            };

            return testClass1.Equals(testClass2);
        }

        public void ShowTestClasses(List<TestClass> testClasses)
        {
            Console.WriteLine(string.Join("\n", testClasses.Select(i => i.Name + "-->" + i.Property)));
        }

        public void ShowTestClass2S(List<TestClass2> testClass2S)
        {
            Console.WriteLine(string.Join("\n", testClass2S.Select(i => i.Name + "-->" + i.Property+"-->"+i.Property2)));
        }

        public void ChangeClass(/*List<TestClass> testClasses*/)
        {
            //if (testClasses == null) throw new ArgumentNullException(nameof(testClasses));
            var testClasses = new List<TestClass>
            {
                new TestClass {Name = "new name2", Property = "new property2"},
                new TestClass {Name="new name1",Property="new property1" },
                new TestClass {Name="new name4" ,Property="new property4"}
            };

            var testClasses2 = testClasses;
            testClasses2.Add(new TestClass {Name="new name3",Property="new property3"});
            Console.WriteLine(string.Join("\n",testClasses.Select(i=>i.Name+", "+i.Property).ToList()));
            Console.WriteLine("1111");
            Console.WriteLine(string.Join("\n",testClasses.Select(i=>i.Name+", "+i.Property)));
        }

        public void ChangeClass(out List<TestClass> testClasses)
        {
            //if (testClasses == null) throw new ArgumentNullException(nameof(testClasses));
            testClasses = new List<TestClass>
            {
                new TestClass {Name = "new name2", Property = "new property2"},
                new TestClass {Name="new name1",Property="new property1" },
                new TestClass {Name="new name4" ,Property="new property4"}
            };

            var testClasses2 = testClasses;
            testClasses2.Add(new TestClass { Name = "new name3", Property = "new property3" });
            //Console.WriteLine(string.Join("\n", testClasses.Select(i => i.Name + ", " + i.Property).ToList()));
            //Console.WriteLine("1111");
            //Console.WriteLine(string.Join("\n", testClasses.Select(i => i.Name + ", " + i.Property)));
        }
        public void ChangeClass(TestClass testClasses)
        {
            if (testClasses == null) throw new ArgumentNullException(nameof(testClasses));
            testClasses = new TestClass { Name = "new name", Property = "new property" };
        }
        
    }
}
