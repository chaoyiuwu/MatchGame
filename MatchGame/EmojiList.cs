using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchGame {

    public static class EmojiList {
        readonly static Dictionary<string, string> FoodEmoji = new Dictionary<string, string> {
            {"Burger", "🍔" },
            {"Pizza", "🍕"},
            {"Fries", "🍟" },
            {"Bagel", "🥯"},
            {"Waffle", "🧇"},
            {"Pancake", "🥞"},
            {"Pie","🥧" },
            {"Tea","🍵" },
            {"Sandwich","🥪" },
            {"Sushi","🍣" },
            {"Chocolate","🍫" },
            {"Ramen","🍜" },
            {"Donut","🍩" },
            {"Rice Ball", "🍙" },
            {"Cupcake","🧁" },
            {"Cookie", "🍪" },
            {"Taco","🌮" },
            {"Salads","🥗" }
        };

        /// <summary>
        /// From stackoverflow post https://stackoverflow.com/questions/1028136/random-entry-from-dictionary
        /// </summary>
        private static IEnumerable<TValue> UniqueRandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            Random rand = new Random();
            Dictionary<TKey, TValue> values = new Dictionary<TKey, TValue>(dict);
            while (values.Count > 0)
            {
                TKey randomKey = values.Keys.ElementAt(rand.Next(0, values.Count));  // hat tip @yshuditelu 
                TValue randomValue = values[randomKey];
                values.Remove(randomKey);
                yield return randomValue;
            }
        }
        public static List<string> GetEmojiList(int size) {
            var list = new List<string>();
            foreach (object value in UniqueRandomValues(FoodEmoji).Take(size))
            {
                list.Add(value.ToString());
                list.Add(value.ToString());
            }

            return list;
        }
    }
}
