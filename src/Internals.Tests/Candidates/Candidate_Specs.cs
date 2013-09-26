namespace Internals.Tests.Candidates
{
    using System.Collections.Generic;
    using System.Linq;
    using Internals.Candidates;
    using NUnit.Framework;


    [TestFixture]
    public class Using_the_candidate_syntax
    {
        [Test]
        public void Should_call_else_if_no_match()
        {
            int value = 27;

            bool called = false;
            value.Case(x => x == 49, x => { })
                 .Else(x => called = true);

            Assert.IsTrue(called);
        }

        [Test]
        public void Should_call_for_each_enumerable()
        {
            IEnumerable<int> values = Enumerable.Range(1, 100);

            IEnumerable<string> results = values.Case(x => x % 3 == 0 && x % 5 == 0, x => "FizzBuzz")
                                                .Case(x => x % 3 == 0, x => "Fizz")
                                                .Case(x => x % 5 == 0, x => "Buzz")
                                                .Else(x => x.ToString());

            string result = string.Join(",", results.ToArray());

            Assert.AreEqual(ExpectedFizzBuzz, result);
        }

        [Test]
        public void Should_call_for_each_enumerable_with_match()
        {
            IEnumerable<int> values = Enumerable.Range(1, 100);

            IEnumerable<string> results = values.Case(x => x % 3 == 0 && x % 5 == 0, x => "FizzBuzz")
                                                .Case(x => x % 3 == 0, x => "Fizz")
                                                .Case(x => x % 5 == 0, x => "Buzz")
                                                .Else(x => x.ToString());

            string result = string.Join(",", results.ToArray());

            Assert.AreEqual(ExpectedFizzBuzz, result);
        }

        [Test]
        public void Should_match_the_first_condition()
        {
            int value = 27;

            bool called = false;
            value.Case(x => x == 27, x => called = true);

            Assert.IsTrue(called);
        }

        [Test]
        public void Should_match_the_first_match()
        {
            int value = 27;

            bool called = false;
            value.Case(x => x == 27, x => called = true);

            Assert.IsTrue(called);
        }

        [Test]
        public void Should_only_call_the_correct_if()
        {
            int value = 27;

            bool called = false;
            value.Case(x => x == 49, x => { })
                 .Case(x => x == 27, x => called = true);

            Assert.IsTrue(called);
        }

        [Test]
        public void Should_only_call_the_correct_match()
        {
            int value = 27;

            bool called = false;
            value.Case(x => x == 49, x => { })
                 .Case(x => x == 27, x => called = true);

            Assert.IsTrue(called);
        }

        [Test]
        public void Should_only_call_the_first_if()
        {
            int value = 27;

            bool called = false;
            value.Case(x => x == 27, x => { })
                 .Case(x => x == 27, x => called = true);

            Assert.IsFalse(called);
        }

        [Test]
        public void Should_only_call_the_first_match()
        {
            int value = 27;

            bool called = false;
            value.Case(x => x == 27, x => { })
                 .Case(x => x == 27, x => called = true);

            Assert.IsFalse(called);
        }

        const string ExpectedFizzBuzz =
            "1,2,Fizz,4,Buzz,Fizz,7,8,Fizz,Buzz,11,Fizz,13,14,FizzBuzz,16,17,Fizz,19,Buzz,Fizz,22,23,Fizz,Buzz,26,Fizz,28,29,FizzBuzz,31,32,Fizz,34,Buzz,Fizz,37,38,Fizz,Buzz,41,Fizz,43,44,FizzBuzz,46,47,Fizz,49,Buzz,Fizz,52,53,Fizz,Buzz,56,Fizz,58,59,FizzBuzz,61,62,Fizz,64,Buzz,Fizz,67,68,Fizz,Buzz,71,Fizz,73,74,FizzBuzz,76,77,Fizz,79,Buzz,Fizz,82,83,Fizz,Buzz,86,Fizz,88,89,FizzBuzz,91,92,Fizz,94,Buzz,Fizz,97,98,Fizz,Buzz";
    }
}