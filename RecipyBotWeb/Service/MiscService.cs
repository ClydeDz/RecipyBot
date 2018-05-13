using System;
using System.Collections.Generic;
using System.Linq;
using RecipyBotWeb.Constants;

namespace RecipyBotWeb.Service
{
    public static class MiscService
    {
        /// <summary>
        /// Returns x numbers from the first y numbers.
        /// For instance, if x is 2 and y is 5, this method returns
        /// 2 numbers that are between 0 and 4.
        /// If y is less than equal to 0, this method returns null.
        /// If x is less than equal to y, this method returns x.
        /// If x is greater than y, this method checks if -
        /// Default Top N constant is less than equal to u, returns the constant
        /// else returns y numbers.
        /// </summary>
        public static IEnumerable<int> GiveXFromYNumbers(int x, int y)
        {
            if (y <= 0)
            {
                return null;
            }
            int theseNumberOfItems = x <= y ? 
                x :
                BotConstants.OtherConstants.DefaultTopN <= y ?
                    BotConstants.OtherConstants.DefaultTopN :
                    y;
            List<int> rangeSet = new List<int>();
            for (int i=0; i<y; i++)
            {
                rangeSet.Add(i);
            }
            return rangeSet.Shuffle().Take(theseNumberOfItems); 
        }

        /// <summary>
        /// Returns true if the supplied username isn't empty, null or
        /// has the default value 'user' (case ignored).
        /// </summary>
        public static bool IsUserNameDefaultOrBlank(string username)
        {
            return string.IsNullOrEmpty(username) || username.Trim().Equals("user", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns true if both string match each other.
        /// Case is ignored. String are trimmed for whitespaces from both ends.
        /// </summary>
        public static bool CompareTwoStrings(string a, string b)
        {
            return string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b) ?
                false :
                a.Trim().Equals(b.Trim(), StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Randomly returns a tagline from the pre-defined list of taglines.
        /// </summary>
        public static string GetRandomTagline()
        {
            return BotConstants.OtherConstants.Taglines.Shuffle().Take(1).FirstOrDefault().ToString();
        }

        /// <summary>
        /// Randomly returns one ingredient prefix from the list of
        /// pre-defined prefixes.
        /// </summary>
        public static string GetRandomIngredientsPrefix()
        {
            return string.Format("{0} ", BotConstants.OtherConstants.Prefixes.Shuffle().Take(1).FirstOrDefault().ToString());
        }

        /// <summary>
        /// Returns the GIF video URL trimming the letter 'v' at the end.
        /// </summary>
        public static string MakeGif(string gifUrl)
        {
            return string.IsNullOrEmpty(gifUrl) ? "https://i.imgur.com/DpUg0ai.gif" : gifUrl.Substring(0, gifUrl.Length - 1);
        }

        /// <summary>
        /// Gets the numeric value from the supplied dictionary
        /// where the key is 'number'.
        /// Returns the default Top N number if no key is found or if its null.
        /// </summary>
        public static int GetNumericEntity(Dictionary<string, object> paramters)
        {
            var theNumber = paramters?.Where(p => p.Key == BotConstants.ApiAiParametersConstants.Number)
                .Select(k => k.Value)
                .FirstOrDefault();
            return (theNumber == null) ? BotConstants.OtherConstants.DefaultTopN : Convert.ToInt32(theNumber);           
        }

        public static string GetFoodEntities(Dictionary<string, object> paramters)
        {
            string foodItem = string.Empty;
            foreach (var j in paramters)
            {
                if (j.Key == BotConstants.ApiAiParametersConstants.FoodItem)
                {
                    foodItem = string.IsNullOrEmpty(j.Value.ToString()) ? BotConstants.OtherConstants.DefaultIngredientsSerialized : j.Value.ToString();
                }
                if (j.Key == BotConstants.ApiAiParametersConstants.Recipe)
                {
                    foodItem = string.IsNullOrEmpty(j.Value.ToString()) ? BotConstants.OtherConstants.DefaultRecipeDish : j.Value.ToString();
                }
            }
            return foodItem;
        }
    }

    #region EXTENSIONS
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (rng == null) throw new ArgumentNullException("rng");

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
        {
            List<T> buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
    #endregion
}