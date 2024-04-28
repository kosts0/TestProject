using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TestTools
{
    /// <summary>
    /// https://gist.github.com/srini85/7419f13475814a6250744e857a1e3bf8
    /// </summary>
    /// <typeparam name="TTestCaseParams"></typeparam>
    public class FileInputParamsBase<TTestCaseParams> : IEnumerable
    {
        public FileInputParamsBase(string fileName)
        {
            FileName = fileName;
        }
        protected virtual string DataFilesSubFolder => Directory.GetCurrentDirectory();

        protected virtual string FileName { get; set; }

        private string GetFilePath()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (directoryName == null)
                throw new Exception("Couldn't get assembly directory");

            return Path.Combine(directoryName, DataFilesSubFolder, $"{FileName}");
        }

        public IEnumerator GetEnumerator()
        {
            var testFixtureParams = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(GetFilePath()));
            var genericItems = testFixtureParams[$"{typeof(TTestCaseParams).Name}"].ToObject<IEnumerable<TTestCaseParams>>();

            foreach (var item in genericItems)
            {
                yield return new object[] { item };
            }
        }
    }
}