using System;
using System.IO;
using Sepidar.Framework.Extensions;
using Sepidar.CodeGenerator;

namespace Sepidar.CodeGenerator
{
    public class StartupBuilder 
    {
        public void Generate()
        {
            var startupFilePath = @"StartupFileTemplate.txt";
            var text = File.ReadAllText(startupFilePath);

//            string repos = @"
//
//            text = text.Replace("// InjectRepositories", repos);

            var targetPath = Environment.ExpandEnvironmentVariables(@"%SepidarProjectsRoot%\Api\Service\BaseStartup.cs");
            File.WriteAllText(targetPath, text);


        }
    }
}
