using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Acronymr.Tests
{
    public class AcronymrTests
    {
        [Test]
        public void GetAcronym_ReturnsException_When_NullString()
        {
            Assert.Throws<ArgumentNullException>(() => Acronymr.GetAcronym(null));
        }

        [Test]
        public void GetAcronym_ThrowsEmpty_When_EmptyString()
        {
            Assert.That(Acronymr.GetAcronym(""), Is.Empty);
        }

        [TestCase("Don't repeat yourself", ExpectedResult = "DRY")]
        [TestCase("Asynchronous Javascript and XML", ExpectedResult = "AJAX")]
        [TestCase("Complementary metal-oxide semiconductor", ExpectedResult = "CMOS")]
        public string GetAcronym_ReturnsAcronym_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronym(text);
        }

        [TestCase("Don't repeat yourself", ExpectedResult = "DRY")]
        [TestCase("Asynchronous Javascript and XML", ExpectedResult = "AJAX")]
        [TestCase("Complementary metal-oxide semiconductor", ExpectedResult = "CMOS")]
        public string GetAcronymWithPredicates_ReturnsAcronym_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronymWithPredicates(text);
        }

        [TestCase("Rythm and blue", ExpectedResult = "R&B")]
        public string GetAcronym_ReturnsAcronymWithSpecialLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronym(text, replaceSpecialWords: true);
        }

        [TestCase("Rythm and blue", ExpectedResult = "R&B")]
        public string GetAcronymWithTables_ReturnsAcronymWithSpecialLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronymWithTables(text, replaceLetterTable: new Dictionary<string, char> { {"and", '&'} });
        }

        [TestCase("Rythm and blue", ExpectedResult = "R&B")]
        public string GetAcronymWithPredicates_ReturnsAcronymWithSpecialLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronymWithPredicates(text, getLetterFunc: word =>
            {
                if (word == "and") return '&';
                return null;
            });
        }

        [TestCase("The Institute of Electrical and Electronics Engineers", ExpectedResult = "IEEE")]
        public string GetAcronym_ReturnsAcronymWithoutMinorLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronym(text, ignoreMinorWords: true);
        }

        [TestCase("The Institute of Electrical and Electronics Engineers", ExpectedResult = "IEEE")]
        public string GetAcronymWithTables_ReturnsAcronymWithoutMinorLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronymWithTables(text, ignoredWords: new [] { "the", "of", "and"});
        }

        [TestCase("The Institute of Electrical and Electronics Engineers", ExpectedResult = "IEEE")]
        public string GetAcronymWithPredicates_ReturnsAcronymWithoutMinorLetters_When_NonEmptyString(string text)
        {
            return Acronymr.GetAcronymWithPredicates(text, ignoreWordPredicate: word =>
            {
                switch (word)
                {
                    case "the":
                    case "of":
                    case "and":
                        return true;

                    default:
                        return false;
                }
            });
        }
    }
}