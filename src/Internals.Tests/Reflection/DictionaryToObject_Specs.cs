namespace Internals.Tests.Reflection
{
    using System.Collections.Generic;
    using Internals.Reflection;
    using Mapping;
    using NUnit.Framework;


    [TestFixture]
    public class Converting_a_dictionary_to_an_object
    {
        [Test]
        public void Should_include_a_string()
        {
            Assert.IsNotNullOrEmpty(_values.StringValue);
            Assert.AreEqual("Hello", _values.StringValue);
        }

        [Test]
        public void Should_include_nullable_long()
        {
            Assert.IsTrue(_values.LongValue.HasValue);
            Assert.AreEqual(123, _values.LongValue.Value);
        }

        [Test]
        public void Should_include_the_enum()
        {
            Assert.AreEqual(ValueType.Integer, _values.ValueType);
        }

        [Test]
        public void Should_include_the_enum_as_a_string()
        {
            Assert.AreEqual(ValueType.String, _values.ValueTypeAsString);
        }

        [Test]
        public void Should_include_the_enum_as_an_integer()
        {
            Assert.AreEqual(ValueType.Integer, _values.ValueTypeAsInt);
        }

        [Test]
        public void Should_include_the_integer()
        {
            Assert.AreEqual(27, _values.IntValue);
        }

        [Test]
        public void Should_include_the_sub_value()
        {
            Assert.IsNotNull(_values.SubValue);

            Assert.AreEqual("A", _values.SubValue.A);
            Assert.AreEqual("B", _values.SubValue.B);
        }

        [Test]
        public void Should_include_the_sub_values()
        {
            Assert.IsNotNull(_values.SubValues);
            Assert.AreEqual(2, _values.SubValues.Length);

            Assert.AreEqual("A", _values.SubValues[0].A);
            Assert.AreEqual("B", _values.SubValues[0].B);

            Assert.AreEqual("1", _values.SubValues[1].A);
            Assert.AreEqual("2", _values.SubValues[1].B);
        }

        IDictionary<string, object> _dictionary;
        Values _values;

        [TestFixtureSetUp]
        public void Setup()
        {
            _dictionary = new Dictionary<string, object>
                {
                    {"IntValue", 27},
                    {"StringValue", "Hello"},
                    {"LongValue", (long?)123},
                    {"ValueType", ValueType.Integer},
                    {"ValueTypeAsInt", 2},
                    {"ValueTypeAsString", "String"},
                    {
                        "SubValue", new Dictionary<string, object>
                            {
                                {"A", "A"},
                                {"B", "B"}
                            }
                    },
                    {"StringValues", new[] {"A", "B", "C"}},
                    {
                        "SubValues", new object[]
                            {
                                new Dictionary<string, object>
                                    {
                                        {"A", "A"},
                                        {"B", "B"}
                                    },
                                new Dictionary<string, object>
                                    {
                                        {"A", "1"},
                                        {"B", "2"}
                                    }
                            }
                    },
                };


            ObjectConverterCache converterCache =
                new DynamicObjectConverterCache(new DynamicImplementationBuilder());

            _values = (Values)converterCache.GetConverter(typeof(Values)).GetObject(_dictionary);
        }


        public interface Values
        {
            int IntValue { get; }
            string StringValue { get; }
            long? LongValue { get; }
            ValueType ValueType { get; }
            ValueType ValueTypeAsInt { get; }
            ValueType ValueTypeAsString { get; }
            SubValue SubValue { get; }
            string[] StringValues { get; }
            SubValue[] SubValues { get; }
        }


        public interface SubValue
        {
            string A { get; }
            string B { get; }
        }


        public enum ValueType
        {
            Default,
            String,
            Integer
        }
    }
}