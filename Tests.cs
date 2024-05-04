using NUnit.Framework;
using NUnit.Allure.Core;
using Allure.Net.Commons;
using System;
using NUnit.Allure.Attributes;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Threading;
using Bogus;
using TestProject.Helpers;
using TestTools;
using TestProject;

namespace NUnit
{
    [AllureNUnit]
    public class Tests
    {
        private const string ProjectId = "acd3e40c-b5ae-4b46-9240-9ce2536fc422";
        private const string ConfigurationId = "4dbab9b9-8436-4261-b36c-0fde22a7de5e";
        private const string ApiKey = "WFoyQVFrVExQZUQ5dWZNbTdh";
        [SetUp]
        public void SetUp()
        {
            AllureApi.SetTestName(TestContext.CurrentContext.Test.MethodName);
            AllureApi.Step("AllureSetup");
            AllureApi.Step($"StartingTest\nTestMethod: {TestContext.CurrentContext.Test.Name}.\nDate: {DateTime.Now}");
        }
        string SuiteName => Guid.NewGuid().ToString();
        [Test, TestCaseSource(nameof(ImageTestList))]
        public void ImageTest(ModelBase data)
        {
            AllureApi.Step($"Test with name {data}, {data.ManualTestId}\nTestMethod: {TestContext.CurrentContext.Test.Name} Passed!\n{DateTime.Now}\n");
            Thread.Sleep(TimeSpan.FromSeconds(FakerEx.RandomDouble(10)));
            AllureApi.Step($"Suite name: {SuiteName}");
            Assert.AreEqual(0, new Random().Next(2)%2, "Неверное исполнение теста");
        }
        [Test, TestCaseSource(nameof(TextTestList))]
        public void TextTest(string testParamName)
        {
            AllureApi.Step($"Test with name {testParamName}\nTestMethod: {TestContext.CurrentContext.Test.Name} Passed!\n{DateTime.Now}");
            AllureApi.Step($"Suite name: {SuiteName}");
            //AllureApi.AddAttachment(ResultDirectory);
            Assert.AreEqual(0, new Random().Next(2)%2, "Неверное исполнение теста");
        }
        private static FileInputParamsBase<ModelBase> NewTestSource => new FileInputParamsBase<ModelBase>("TestData\\" + "NotImplemendetBuildTest.json");
        [Test, TestCaseSource(nameof(NewTestSource))]
        public void NewTest(ModelBase data)
        {
            AllureApi.Step($"Test ID: {data.Id}");
            Thread.Sleep(TimeSpan.FromSeconds(FakerEx.RandomInt(max: 10)));
            Assert.AreEqual(0, FakerEx.RandomInt(), "Неверное выполнение теста");
        }
        private static FileInputParamsBase<ModelBase> ImageTestList => new FileInputParamsBase<ModelBase>("TestData\\" + "ImageTest.json");
        private static List<string> TextTestList => new() { "3", "4", "5"};
        private string ResultDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestDebugResults", "rus.jpeg");
    }
}