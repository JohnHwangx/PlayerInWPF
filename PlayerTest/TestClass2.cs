namespace PlayerTest
{
    public class TestClass2:TestClass
    {
        public string Property2 { get; set; }

        /// <summary>确定指定的对象是否等于当前对象。</summary>
        /// <returns>如果指定的对象等于当前对象，则为 true；否则为 false。</returns>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            var other = obj as TestClass2;
            if (other==null)
            {
                return false;
            }
            if (!string.Equals(Property2,other.Property2))
            {
                return false;
            }
            return base.Equals(obj);
        }

        public bool Equals(TestClass3 obj)
        {
            TestClass other = obj;
            if (obj == null)
            {
                return false;
            }
            if (!string.Equals(Property2, obj.Property2))
            {
                return false;
            }
            return base.Equals(other);
        }

        //public override void Clone(object obj)
        //{
        //    var other = obj as TestClass2;
        //    if (other == null) return;
        //    Property2 = other.Property2;
        //    base.Clone(obj);
        //}

        public override int GetHashCode()
        {
            return 0;
        }
    }
}