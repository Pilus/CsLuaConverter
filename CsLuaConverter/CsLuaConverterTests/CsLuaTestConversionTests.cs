namespace CsLuaConverterTests
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CsLuaConverter;

    [TestClass]
    public class CsLuaTestConversionTests
    {
        private readonly Regex resultRegex = new Regex(@"(\d+)\ttests run\.\t(\d+)\tfailed\.\t(\d+)\tsucceded\.");
        private readonly string testAddOnLocationPath = Path.GetTempPath() + "CsLua";
        private readonly string testOutputFolder = Directory.GetCurrentDirectory();

        [TestMethod]
        public async Task CsLuaTestShouldConvertAndRunWithoutErrors()
        {
            this.CopyCsLuaFileFromOutputToTestsOutput();
            await this.ConvertCsLuaTestAsync();
            this.OverrideContinueOnErrorFlag();
            this.CopyWinLuaRunFile();
            await this.RunCsLuaTestsAsync();
        }

        private void CopyCsLuaFileFromOutputToTestsOutput()
        {
            var conveterOutFolder = new FileInfo(this.testOutputFolder + "\\..\\..\\..\\..\\CsLuaProjects\\CsLuaConverter").FullName;
            File.Copy(conveterOutFolder + "\\CsLua.lua", this.testOutputFolder + "\\CsLua.lua", true);
        }

        private async Task ConvertCsLuaTestAsync()
        {
            var converter = new Converter();

            var csLuaTestProjectPath = this.testOutputFolder + "\\..\\..\\..\\..\\CsLuaProjects\\CsLuaTest.sln";
            var fileInfo = new FileInfo(csLuaTestProjectPath);

            await converter.ConvertAsync(fileInfo.FullName, this.testAddOnLocationPath);
        }

        private void CopyWinLuaRunFile()
        {
            File.Copy(this.testOutputFolder + "\\Run.lua", this.testAddOnLocationPath + "\\CsLuaTest\\Run.lua", true);
        }

        private void OverrideContinueOnErrorFlag()
        {
            var csLuaTestFile = this.testAddOnLocationPath + "\\CsLuaTest\\CsLuaTest.lua";
            string text = File.ReadAllText(csLuaTestFile);
            text = text.Replace("ContinueOnError = false,", "ContinueOnError = true,");
            File.WriteAllText(csLuaTestFile, text);
        }

        private async Task RunCsLuaTestsAsync()
        {
            var result = await this.RunCmdAsync($"{this.testAddOnLocationPath}\\CsLuaTest", "lua .\\Run.lua");
            var match = this.resultRegex.Match(result);
            if (!match.Success)
            {
                throw new Exception("Could not find test result in output.\n" + result);
            }

            var total = int.Parse(match.Groups[1].Value);
            var failed = int.Parse(match.Groups[2].Value);
            var success = int.Parse(match.Groups[3].Value);

            Console.WriteLine(result);

            if (failed > 0)
            {
                throw new Exception($"{failed} out of {total} failed.");
            }
        }

        private async Task<string> RunCmdAsync(string workingDirectory, string arguments)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = workingDirectory;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + arguments;
            process.StartInfo = startInfo;
            process.Start();

            return await Task<string>.Factory.StartNew(() =>
            {
                var output = Read(process);
                
                var error = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception($"Cmd.exe threw error: {error}");
                }

                return output;
            });
        }

        private static string Read(Process process)
        {
            var stringWriter = new StringWriter();

            char[] buffer = new char[5000];
            int read = 1;
            while (read > 0)
            {
                read = process.StandardOutput.Read(buffer, 0, buffer.Length);
                string data = read > 0 ? new string(buffer, 0, read) : null;
                if (data != null)
                {
                    stringWriter.Write(data);
                }
            }

            return stringWriter.ToString();
        }
    }
}
