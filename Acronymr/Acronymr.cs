using System;
using System.Collections.Generic;
using System.Text;

namespace Acronymr
{
    public static class Acronymr
    {
        private static readonly char[] _wordSeparators = {' ', '-'};

        private static readonly Dictionary<string, char> _replaceLetterTable = new Dictionary<string, char>
        {
            { "and", '&' }
        };

        private static readonly HashSet<string> _ignoredWords = new HashSet<string>
        {
            "the",
            "of",
            "and"
        };

        public static string GetAcronym(string text, bool replaceSpecialWords = false, bool ignoreMinorWords = false)
        {
            return GetAcronymWithTables(text,
                                        replaceSpecialWords ? _replaceLetterTable : null,
                                        ignoreMinorWords ? _ignoredWords : null);
        }

        public static string GetAcronymWithTables(string text, Dictionary<string, char> replaceLetterTable = null, HashSet<string> ignoredWords = null)
        {
            char? LetterFunc(string word)
            {
                if (replaceLetterTable != null && replaceLetterTable.TryGetValue(word, out char letter))
                {
                    return letter;
                }
                return null;
            }

            bool IgnoreWordPredicate(string word)
            {
                return ignoredWords != null && ignoredWords.Contains(word);
            }

            return GetAcronymWithPredicates(text, LetterFunc, IgnoreWordPredicate);
        }

        public static string GetAcronymWithPredicates(string text, Func<string, char?> letterFunc = null, Func<string, bool> ignoreWordPredicate = null)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (letterFunc == null)
            {
                letterFunc = word => null;
            }
            if (ignoreWordPredicate == null)
            {
                ignoreWordPredicate = word => false;
            }

            var words = text.Split(_wordSeparators, StringSplitOptions.RemoveEmptyEntries);
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i].ToLower();

                if (ignoreWordPredicate(word))
                    continue;

                char? letter = letterFunc(word) ?? char.ToUpper(word[0]);

                stringBuilder.Append(letter.Value);
            }

            return stringBuilder.ToString();
        }
    }
}
