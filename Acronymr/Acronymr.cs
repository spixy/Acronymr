using System;
using System.Collections.Generic;
using System.Text;

namespace Acronymr
{
    public static class Acronymr
    {
        private static readonly char[] _wordSeparators = {' ', '-'};

        private static readonly IDictionary<string, char> defaultReplaceLetterTable = new Dictionary<string, char>
        {
            { "and", '&' }
        };

        private static readonly ICollection<string> defaultIgnoredWords = new HashSet<string>
        {
            "the",
            "of",
            "and"
        };

        public static string GetAcronym(string text, bool replaceSpecialWords = false, bool ignoreMinorWords = false)
        {
            return GetAcronymWithTables(text,
                                        replaceSpecialWords ? defaultReplaceLetterTable : null,
                                        ignoreMinorWords ? defaultIgnoredWords : null);
        }

        public static string GetAcronymWithTables(string text, IDictionary<string, char> replaceLetterTable = null, ICollection<string> ignoredWords = null)
        {
            char? GetLetterFunc(string word)
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

            return GetAcronymWithPredicates(text, GetLetterFunc, IgnoreWordPredicate);
        }

        public static string GetAcronymWithPredicates(string text, Func<string, char?> getLetterFunc = null, Func<string, bool> ignoreWordPredicate = null)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            var words = text.Split(_wordSeparators, StringSplitOptions.RemoveEmptyEntries);
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i].ToLower();

                if (ignoreWordPredicate != null && ignoreWordPredicate(word))
                    continue;

                char? letter = getLetterFunc?.Invoke(word) ?? char.ToUpper(word[0]);

                stringBuilder.Append(letter.Value);
            }

            return stringBuilder.ToString();
        }

        // overengineered :/
    }
}
