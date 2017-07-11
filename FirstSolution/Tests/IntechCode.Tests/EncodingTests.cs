using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntechCode.Tests
{
    [TestFixture]
    public class EncodingTests
    {

        [Test]
        public void encoding_is_all_about_text_to_byte_and_byte_to_test()
        {
            string s = "émile";
            string sA = "ممدون";
            var bytesUTF16 = Encoding.Unicode.GetBytes(s);
            var bytesUTF8 = Encoding.UTF8.GetBytes(s);
            var bytesUTF7 = Encoding.UTF7.GetBytes(s);
            var AbytesUTF16 = Encoding.Unicode.GetBytes(sA);
            var AbytesUTF8 = Encoding.UTF8.GetBytes(sA);
            var AbytesUTF7 = Encoding.UTF7.GetBytes(sA);

            var emotic = "🚣";
            var emoticInMemory = Encoding.Unicode.GetBytes(emotic);
        }

        [Test]
        public void funny_chars()
        {
            string s1 = "éè";
            string s2 = "éè".Normalize(NormalizationForm.FormKD);
            if (s1 == s2) throw new Exception("POUF");
        }


        [TestCase("éàô", "eao")]
        [TestCase("Nöel", "Noel")]
        [TestCase("Nöel est là!", "Noel est la!")]
        public void removing_diacritics(string s, string expected)
        {
            RemoveDiacritics(s).Should().Be(expected);
        }

        private static string RemoveDiacritics(string s)
        {
            StringBuilder b = new StringBuilder();
            foreach (var ch in s.Normalize(NormalizationForm.FormKD))
            {
                var cat = char.GetUnicodeCategory(ch);
                if (cat != System.Globalization.UnicodeCategory.ModifierLetter
                    && cat != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    b.Append(ch);
                }
            }
            return b.ToString();
        }
    }
    }
