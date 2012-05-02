namespace Internals.Tests.Extensions
{
    using System;
    using NUnit.Framework;
    using Internals.Extensions;

    [TestFixture]
    public class TimeSpan_Specs
    {
        [Test]
        public void Should_property_format_a_timespan()
        {
            TimeSpan span = new TimeSpan(1, 2, 3, 4);

            string friendly = span.ToFriendlyString();

            Assert.AreEqual("1d2h3m4s", friendly);
        }
    }
}
