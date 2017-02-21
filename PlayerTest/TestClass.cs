using System;

namespace PlayerTest
{
    public class TestClass:IComparable<TestClass>
    {
        public string Name { get; set; }
        public string Property { get; set; }
        public int CompareTo(TestClass other)
        {
            if (string.Compare(other.Name, Name, StringComparison.Ordinal)>0)
            {
                return 1;
            }
            else if(string.Compare(other.Name, Name, StringComparison.Ordinal) < 0)
            {
                return -1;
            }
            return 0;
        }

        public override bool Equals(object obj)
        {
            var other = obj as TestClass;
            if (other==null)
            {
                return false;
            }
            if (!string.Equals(Name,other.Name))
            {
                return false;
            }
            if (!string.Equals(Property,other.Property))
            {
                return false;
            }
            return true;
        }

        public virtual void Clone(object obj)
        {
            var other = obj as TestClass;
            if (other == null) return;
            Name = other.Name;
            Property = other.Property;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}