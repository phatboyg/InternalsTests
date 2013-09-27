namespace Internals.Tests.Reflection
{
    using System;
    using System.Collections.Generic;
    using Mapping;
    using NUnit.Framework;


    [TestFixture]
    public class Creating_a_dictionary_from_an_interface
    {
        [Test]
        public void Should_handle_a_present_nullable_value()
        {
            Assert.IsTrue(_dictionary.ContainsKey("NullableDecimalValue"));
        }

        [Test]
        public void Should_handle_a_string()
        {
            Assert.IsTrue(_dictionary.ContainsKey("StringValue"));
        }

        [Test]
        public void Should_handle_an_array_of_objects()
        {
            Assert.IsTrue(_dictionary.ContainsKey("SubValues"));

            object value = _dictionary["SubValues"];

            Assert.IsInstanceOf<IDictionary<string, object>[]>(value);
        }

        [Test]
        public void Should_handle_an_array_of_strings()
        {
            Assert.IsTrue(_dictionary.ContainsKey("Names"));

            object value = _dictionary["Names"];

            Assert.IsInstanceOf<string[]>(value);
        }

        [Test]
        public void Should_handle_an_empty_nullable_value()
        {
            Assert.IsFalse(_dictionary.ContainsKey("NullableValue"));
        }

        [Test]
        public void Should_handle_an_int()
        {
            Assert.IsTrue(_dictionary.ContainsKey("IntValue"));
        }

        [Test]
        public void Should_handle_datetime()
        {
            Assert.IsTrue(_dictionary.ContainsKey("DateTimeValue"));
        }

        IDictionary<string, object> _dictionary;

        [TestFixtureSetUp]
        public void Setup()
        {
            var factory = new DictionaryConverterCache();

            DictionaryConverter converter = factory.GetConverter(typeof(ValuesImpl));

            _dictionary =
                converter.GetDictionary(new ValuesImpl("Hello", 27, new DateTime(2012, 10, 1), null, 123.45m));
        }


        interface Values
        {
            string StringValue { get; }
            int IntValue { get; }
            DateTime DateTimeValue { get; }
            long? NullableValue { get; }
            decimal? NullableDecimalValue { get; }
            string[] Names { get; }

            SubValue[] SubValues { get; }
        }


        interface SubValue
        {
            string Text { get; }
        }


        class ValuesImpl :
            Values
        {
            readonly DateTime _dateTimeValue;
            readonly int _intValue;
            readonly decimal? _nullableDecimalValue;
            readonly long? _nullableValue;
            readonly string _stringValue;
            string[] _names;
            SubValue[] _subValues;

            public ValuesImpl(string stringValue, int intValue, DateTime dateTimeValue, long? nullableValue,
                decimal? nullableDecimalValue)
            {
                _stringValue = stringValue;
                _intValue = intValue;
                _dateTimeValue = dateTimeValue;
                _nullableValue = nullableValue;
                _nullableDecimalValue = nullableDecimalValue;
                _names = new[] {"A", "B", "C"};
                _subValues = new SubValue[]
                    {
                        new SubValueImpl("A")
                    };
            }


            public string StringValue
            {
                get { return _stringValue; }
            }

            public int IntValue
            {
                get { return _intValue; }
            }

            public DateTime DateTimeValue
            {
                get { return _dateTimeValue; }
            }

            public long? NullableValue
            {
                get { return _nullableValue; }
            }

            public decimal? NullableDecimalValue
            {
                get { return _nullableDecimalValue; }
            }

            public string[] Names
            {
                get { return _names; }
            }

            public SubValue[] SubValues
            {
                get { return _subValues; }
            }


            class SubValueImpl : SubValue
            {
                readonly string _text;

                public SubValueImpl(string text)
                {
                    _text = text;
                }

                public string Text
                {
                    get { return _text; }
                }
            }
        }
    }
}