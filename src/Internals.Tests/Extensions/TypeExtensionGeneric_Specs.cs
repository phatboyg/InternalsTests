namespace Internals.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internals.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class When_getting_the_generic_types_from_an_interface
    {
        [Test]
        public void Should_return_the_appropriate_generic_type()
        {
            IEnumerable<Type> types = typeof(GenericClass).GetClosingArguments(typeof(IGeneric<>));

            Assert.AreEqual(1, types.Count());
            Assert.AreEqual(typeof(int), types.First());
        }

        [Test]
        public void Should_return_the_appropriate_generic_type_for_a_subclass_non_generic()
        {
            IEnumerable<Type> types = typeof(SubClass).GetClosingArguments(typeof(IGeneric<>));

            Assert.AreEqual(1, types.Count());
            Assert.AreEqual(typeof(int), types.First());
        }

        [Test]
        public void Should_return_the_appropriate_generic_type_with_a_generic_base_class()
        {
            IEnumerable<Type> types = typeof(NonGenericSubClass).GetClosingArguments(typeof(IGeneric<>));

            Assert.AreEqual(1, types.Count());
            Assert.AreEqual(typeof(int), types.First());
        }

        [Test]
        public void Should_return_the_generic_type_from_a_class()
        {
            IEnumerable<Type> types = typeof(NonGenericSubClass).GetClosingArguments(typeof(GenericBaseClass<>));

            Assert.AreEqual(1, types.Count());
            Assert.AreEqual(typeof(int), types.First());
        }

        interface IGeneric<T>
        {
        }

        class GenericClass :
            IGeneric<int>
        {
        }

        class SubClass :
            GenericClass
        {
        }

        class GenericBaseClass<T> :
            IGeneric<T>
        {
        }

        class NonGenericSubClass :
            GenericBaseClass<int>
        {
        }
    }
}