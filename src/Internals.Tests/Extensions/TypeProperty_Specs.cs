namespace Internals.Tests.Extensions
{
    using System.Linq;
    using NUnit.Framework;
    using Internals.Extensions;

    [TestFixture]
    public class Working_with_the_properties_of_a_type
    {
        [Test]
        public void Should_include_the_properties_on_the_class()
        {
            var properties = typeof(A).GetAllProperties();

            Assert.AreEqual(2, properties.Count());
        }

        [Test]
        public void Should_include_properties_from_the_base_class()
        {
            var properties = typeof(B).GetAllProperties();

            Assert.AreEqual(3, properties.Count());
        }

        [Test]
        public void Should_include_properties_from_the_interface()
        {
            var properties = typeof(C).GetAllProperties();

            Assert.AreEqual(2, properties.Count());
        }

        [Test]
        public void Should_include_properties_from_the_interface_and_base_interfaces()
        {
            var properties = typeof(D).GetAllProperties();

            Assert.AreEqual(4, properties.Count());
        }

        [Test]
        public void Should_include_properties_from_the_interface_and_such()
        {
            var properties = typeof(ID).GetAllProperties();

            Assert.AreEqual(4, properties.Count());
        }

        [Test]
        public void Should_include_static_properties()
        {
            var properties = typeof(A).GetAllStaticProperties();

            Assert.AreEqual(1, properties.Count());
        }

        [Test]
        public void Should_include_static_properties_of_base_classes()
        {
            var properties = typeof(B).GetAllStaticProperties();

            Assert.AreEqual(1, properties.Count());
        }


        class A
        {
            public string One { get; set; }
            public string Two { get; set; }

            public static string Alpha { get; set; }
        }

        class B :
            A
        {
            public string Three { get; set; }
        }

        interface IC
        {
             string Four { get; set; }
             string Five { get; set; }
        }

        class C :
            IC
        {
            public string Four { get; set; }
            public string Five { get; set; }
        }

        interface ID :
            IC
        {
            string Six { get; set; }
            string Seven { get; set; }
        }

        class D :
            ID
        {
            public string Four { get; set; }
            public string Five { get; set; }
            public string Six { get; set; }
            public string Seven { get; set; }
        }


    }
}