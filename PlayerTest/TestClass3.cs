using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerTest
{
    public class TestClass3:TestClass
    {
        public string Property2 { get; set; }
        public string Property3 { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as TestClass3;
            if (other == null)
            {
                return false;
            }
            if (!string.Equals(Property2, other.Property2))
            {
                return false;
            }
            if (!string.Equals(Property3,other.Property3))
            {
                
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override void Clone(object obj)
        {
            var other = obj as TestClass3;
            if (other == null) return;
            Property2 = other.Property2;
            Property3 = other.Property3;
            base.Clone(obj);
        }
    }
}
