using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyMovieTheater.API.Tests.FeatureTests.Infrastructure
{
    public class JsonGetter
    {
        private const string ArrayPattern = "^(.*)\\[([0-9]*)]$"; //matches: "myArray[0]" with groups [1]=myArray and [2]=0
        private const string ArrayFirstPattern = "^\\[([0-9]*)](.*)$"; //matches: "myArray[0]" with groups [1]=myArray and [2]=0
        private const string ArrayObjectMatchPattern = "^(.*)\\[(.*)]$"; //matches: "myArray[0]" with groups [1]=myArray and [2]=0
        private const string ArrayObjectMatchFirstPattern = "^\\[(.*)](.*)$"; //matches: "myArray[0]" with groups [1]=myArray and [2]=0
        private readonly IDictionary<string, string> _sortOrderBySelector;

        public JsonGetter()
        {
            _sortOrderBySelector = new Dictionary<string, string>();
        }

        public JsonGetter(IDictionary<string, string> sortOrderBySelector)
        {
            _sortOrderBySelector = sortOrderBySelector;
        }

        public object GetPropertyOrDefault(string json, string selector)
        {
            try
            {
                return GetProperty(json, selector);
            }
            catch (KeyNotFoundException e) { }
            catch (NullReferenceException e) { }
            return null;
        }

        public object GetProperty(string json, string selector)
        {
            var realSelector = MatchSearchArrayFirst(selector).Success ? MatchSearchArrayFirst(selector).Groups[2].Value.Substring(1) : selector;

            return GetProperty(GetFirstArray(json, selector) ?? JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter()), realSelector);
        }

        public object GetFirstArray(string json, string selector)
        {
            var matchArray = MatchArrayFirst(selector);
            var searchArrayMatch = MatchSearchArrayFirst(selector);
            var enumerable = (matchArray.Success || searchArrayMatch.Success) ? JsonConvert.DeserializeObject<IEnumerable<ExpandoObject>>(json) : new List<ExpandoObject>();

            return (matchArray.Success)
                ? enumerable.ToList()[int.Parse(matchArray.Groups[1].Value)]
                : (searchArrayMatch.Success)
                    ? FindMatchInArray(enumerable, "[]", searchArrayMatch.Groups[1].Value)
                    : null;
        }

        public object GetObjectFromList(string json, string jsonSelector, string expected)
        {
            var list = JsonConvert.DeserializeObject<List<ExpandoObject>>(json, new ExpandoObjectConverter());
            return list
                .First(obj =>
                {
                    var value = GetProperty(obj, jsonSelector);
                    return (value != null) && value.Equals(expected);
                });
        }

        public bool ListContains(string json, string jsonSelector, string expected)
        {
            var list = JsonConvert.DeserializeObject<List<ExpandoObject>>(json, new ExpandoObjectConverter());
            return ListContains(list, jsonSelector, expected);
        }

        public bool ListAtContains(string json, string arraySelector, string objectSelector, string expected)
        {
            var array = GetProperty(json, arraySelector) as List<object>;
            AssertArray(arraySelector, array);
            return ListContains(array, objectSelector, expected);
        }

        public IEnumerable<object> GetAllObjectsInArray(string json, string arraySelector, string objectSelector, string expected)
        {
            var array = GetProperty(json, arraySelector) as List<object>;
            AssertArray(arraySelector, array);
            return array.Where(x => expected.Equals(GetProperty(x, objectSelector).ToString()));
        }

        public object GetProperty(object root, string selector)
        {
            return selector.Split('.')
                .Aggregate(root, (current, s) => Find(current as IDictionary<string, object>, s));
        }

        private object Find(IDictionary<string, object> obj, string selector)
        {
            var matchArray = MatchArray(selector);
            var matchSearchArray = MatchSearchArray(selector);
            string propertyName = matchSearchArray.Groups[1].Value;
            string propertyName1 = matchArray.Groups[1].Value;
            return matchArray.Success
                ? FindInArray((IEnumerable<object>)obj[propertyName1], propertyName1, int.Parse(matchArray.Groups[2].Value))
                : (matchSearchArray.Success) ? FindMatchInArray((IEnumerable<object>)obj[propertyName], propertyName, matchSearchArray.Groups[2].Value) : obj[selector];
        }

        private Match MatchArray(string selector)
        {
            var r = new Regex(ArrayPattern, RegexOptions.IgnoreCase);
            var match = r.Match(selector);
            return match;
        }

        private Match MatchArrayFirst(string selector)
        {
            var r = new Regex(ArrayFirstPattern, RegexOptions.IgnoreCase);
            var match = r.Match(selector);
            return match;
        }

        private Match MatchSearchArrayFirst(string selector)
        {
            var r = new Regex(ArrayObjectMatchFirstPattern, RegexOptions.IgnoreCase);
            var match = r.Match(selector);
            return match;
        }

        private Match MatchSearchArray(string selector)
        {
            var r = new Regex(ArrayObjectMatchPattern, RegexOptions.IgnoreCase);
            var match = r.Match(selector);
            return match;
        }

        private object FindInArray(IEnumerable<object> findInObject, string propertyName, int propertyIndex)
        {
            return findInObject.OrderBy(i => GetOrderForSelector(propertyName, i)).ToList()[propertyIndex];
        }

        private object FindMatchInArray(IEnumerable<object> findInObject, string propertyName, string matchSelector)
        {
            var matchOn = matchSelector.Split('=');
            var findMatchInArray = FindInArray(findInObject, matchOn, propertyName);
            return findMatchInArray;
        }

        private static object FindInArray(IEnumerable<object> enumerable, string[] matchOn, string propertyName)
        {
            try
            {
                return enumerable.First(i =>
                {
                    IDictionary<string, object> list = (IDictionary<string, object>)i;
                    return list[matchOn[0]].ToString() == matchOn[1].ToString();
                });
            }
            catch (InvalidOperationException e)
            {
                throw new KeyNotFoundException("Cannot find the key value of " + matchOn[1] + " in property " + matchOn[0] + " for array " + propertyName);
            }
        }

        private bool ListContains(IEnumerable<object> list, string selector, string expected)
        {
            return list
                .Select(obj => GetProperty(obj, selector))
                .Contains(expected);
        }

        private object GetOrderForSelector(string selector, object i)
        {
            if (!_sortOrderBySelector.ContainsKey(selector)) throw new Exception("List order was not specified for \"" + selector + "\"");
            return GetProperty(i, _sortOrderBySelector[selector]);
        }

        private static void AssertArray(string arraySelector, List<object> array)
        {
            if (array == null)
            {
                throw new ObjectMayNotBeArrayException("Array is null. Ensure the selector <" + arraySelector + "> is actually an array.");
            }
        }
    }

    public class ObjectMayNotBeArrayException : Exception
    {
        public ObjectMayNotBeArrayException()
        {
        }

        public ObjectMayNotBeArrayException(string message)
            : base(message)
        {
        }

        public ObjectMayNotBeArrayException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
