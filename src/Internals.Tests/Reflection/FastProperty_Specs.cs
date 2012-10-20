namespace Internals.Tests.Reflection
{
    using System.Linq;
    using System.Reflection;
    using Internals.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class FastProperty_Specs
    {
        [Test]
        public void Should_be_able_to_access_a_private_setter()
        {
            var instance = new PrivateSetter();
#if !NETFX_CORE
            PropertyInfo property = instance.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.Name == "Name")
                .First();
#else
            PropertyInfo property = instance.GetType()
                .GetTypeInfo()
                .GetDeclaredProperty("Name");
#endif

            var fastProperty = new ReadWriteProperty<PrivateSetter>(property, true);

            const string expectedValue = "Chris";
            fastProperty.Set(instance, expectedValue);

            Assert.AreEqual(expectedValue, fastProperty.Get(instance));
        }

        [Test]
        public void Should_cache_properties_nicely()
        {
            var cache = new ReadWritePropertyCache<PrivateSetter>(true);

            var instance = new PrivateSetter();

            const string expectedValue = "Chris";
            cache["Name"].Set(instance, expectedValue);

            Assert.AreEqual(expectedValue, instance.Name);
        }


        class PrivateSetter
        {
            public string Name { get; private set; }
        }
    }
}