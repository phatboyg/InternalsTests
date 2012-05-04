namespace Internals.Tests.Primitives
{
    using Internals.Primitives;
    using NUnit.Framework;

    [TestFixture]
    public class Enumeration_Specs
    {
        [Test]
        public void Should_be_comparable()
        {
            Assert.Greater(MyEnumeration.Second, MyEnumeration.First);
        }

        [Test]
        public void Should_be_equal()
        {
            MyEnumeration left = MyEnumeration.First;
            MyEnumeration right = MyEnumeration.First;

            Assert.AreEqual(left, right);
        }

        [Test]
        public void Should_be_parsable()
        {
            var value = Enumeration.Parse<MyEnumeration>(MyEnumeration.First.Name);

            Assert.AreEqual(MyEnumeration.First, value);
        }


        [Test]
        public void Should_be_parsed_from_an_index()
        {
            var value = Enumeration.FromIndex<MyEnumeration>(1);

            Assert.AreEqual(MyEnumeration.First, value);
        }

        class MyEnumeration :
            Enumeration
        {
            public static readonly MyEnumeration First;
            public static readonly MyEnumeration Second;

            static MyEnumeration()
            {
                First = Init(() => First, 1);
                Second = new MyEnumeration(2, "Second");
            }

            public MyEnumeration()
            {
            }

            public MyEnumeration(int index, string name)
                : base(index, name)
            {
            }
        }
    }
}