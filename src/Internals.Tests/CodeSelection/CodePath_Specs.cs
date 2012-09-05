namespace Internals.Tests.CodeSelection
{
    using System;
    using System.Collections.Generic;
    using Conditional;
    using NUnit.Framework;

    [TestFixture]
    public class When_a_configurable_code_path_is_used
    {
        [Test]
        public void Should_default_to_the_default_code_path()
        {
            CodePath<DebugMode>.If(DefaultPath, PreferredPath);

            int rate = CodePath<DebugMode>.Iff(DefaultRate, PreferredRate, 10);

            // forcing it to enabled
            CodePath<DebugMode>.Enable();

            // forcing it to disabled
            CodePath<DebugMode>.Disable();

            CodePath<DebugMode>.Unless(() => { });

            // setting it used a custom type
//            CodePath<DebugMode>.Set(new EnvironmentMode("DebugMode"));


            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            string text;
            bool result = CodePath<DebugMode>.Iff(dictionary.TryGetValue, dictionary.TryGetValue, 27, out text);

        }


        void DefaultPath()
        {
        }

        void PreferredPath()
        {
        }

        int DefaultRate(int input)
        {
            return input*10;
        }

        int PreferredRate(int input)
        {
            return input*15;
        }
    }

    class DebugMode :
        Mode
    {
        public bool Enabled
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }

}