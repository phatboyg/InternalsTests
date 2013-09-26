namespace Internals.Tests.Primitives
{
    using System.Collections.Generic;
    using System.Linq;
    using Internals.Primitives;
    using NUnit.Framework;
    using Trie.Tests;


    [TestFixture]
    public class Loading_a_trie_full_of_data
    {
        [Test]
        public void Should_find_a_range_of_results()
        {
            TrieWalker<string> walker;
            Assert.IsTrue(_trie.TryFind("Mcc", out walker));

            IList<string> matches = walker.GetMatches();

            Assert.AreEqual(191, matches.Count);
        }

        [Test]
        public void Should_find_all_the_results()
        {
            TrieWalker<string> walker = _trie.Root();

            IList<string> matches = walker.GetMatches();

            Assert.AreEqual(_distinctCount, matches.Count);
        }

        [Test]
        public void Should_find_all_the_results_enumerable()
        {
            TrieWalker<string> walker = _trie.Root();

            Assert.AreEqual(_distinctCount, walker.Count());
        }

        Trie<string> _trie;
        string[] _lastNames;
        int _distinctCount;

        [TestFixtureSetUp]
        public void Setup()
        {
            _lastNames = SampleData.LastNames;
            _distinctCount = _lastNames.Distinct().Count();

            _trie = new Trie<string>();
            foreach (string lastName in _lastNames)
                _trie.Add(lastName, lastName);
        }
    }
}