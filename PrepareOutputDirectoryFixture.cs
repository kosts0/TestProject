using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using Allure.Net.Commons;
using NUnit.Framework;

/// <summary>
/// https://github.com/orgs/allure-framework/discussions/2154
/// </summary>
[SetUpFixture]
class PrepareOutputDirectoryFixture
{
    string? path;

    [OneTimeSetUp]
    public void SetOutputDirectory()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        this.path = Path.GetTempFileName();
        var directory = Directory.CreateDirectory(
            Path.Combine(
                GetCurrentAllureDirectory(),
                CreateSubfolderName()
            )
        );
        File.WriteAllText(
            this.path,
            JsonSerializer.Serialize(
                new { allure = new { directory = directory.FullName } }
            ),
            Encoding.UTF8
        );
        Environment.SetEnvironmentVariable(AllureConstants.ALLURE_CONFIG_ENV_VARIABLE, this.path);
    }

    [OneTimeTearDown]
    public void DeleteTempConfigFile()
    {
        Environment.SetEnvironmentVariable(AllureConstants.ALLURE_CONFIG_ENV_VARIABLE, null);
        if (this.path is not null && File.Exists(this.path))
        {
            File.Delete(this.path);
        }
        Trace.Flush();
    }

    static string GetCurrentAllureDirectory() => "allure-results";

    static string CreateSubfolderName() => string.Concat(TestContext.Parameters.Get("TestRunId") ?? "DebugTestRun");
}