using System;
using NUnit.Framework;

namespace Acronymr.Tests
{
    public class AcronymrTests
    {
        [Test]
        public void ReturnsException_When_NullString()
        {
            Assert.Throws<ArgumentNullException>(() => Acronymr.GetAcronym(null));
        }

        [Test]
        public void ThrowsEmpty_When_EmptyString()
        {
            Assert.That(Acronymr.GetAcronym(""), Is.Empty);
        }

        [TestCase("Don't repeat yourself", ExpectedResult = "DRY")]
        [TestCase("Asynchronous Javascript and XML", ExpectedResult = "AJAX")]
        [TestCase("Complementary metal-oxide semiconductor", ExpectedResult = "CMOS")]
        public string ReturnsAcronym_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronym(text);
        }

        [TestCase("Rythm and blue", ExpectedResult = "R&B")]
        public string ReturnsAcronymWithSpecialLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronym(text, replaceSpecialWords: true);
        }

        [TestCase("The Institute of Electrical and Electronics Engineers", ExpectedResult = "IEEE")]
        public string ReturnsAcronymWithoutMinorLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronym(text, ignoreMinorWords: true);
        }
    }
}