using System;
using System.IO;
using System.Linq;
using System.Text;
using Sepidar.Framework.Extensions;

namespace Sepidar.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Models
            var modelBuilder = new ModelBuilder();
            modelBuilder.Generate();
            modelBuilder.Save();

            // Contexts
            var contextBuilder = new DbContextBuilder();
            contextBuilder.Generate();
            contextBuilder.Save();

            // Repositories
            var repositoryBuilder = new RepositoryBuilder();
            repositoryBuilder.Generate();
            repositoryBuilder.Save();

            var startupFilePath = Environment.ExpandEnvironmentVariables(@"%SepidarProjectsRoot%\Api\CodeGenerator\StartupFileTemplate.txt");
            repositoryBuilder.GenerateRepositoryServicesForStartup(startupFilePath);
            repositoryBuilder.SaveStartup(@"%SepidarProjectsRoot%\Api\Service\BaseStartup.cs");
            GenerateBusinessServicesForStartup(@"%SepidarProjectsRoot%\Api\Service\BaseStartup.cs");

            //var siteStartupFilePath = Environment.ExpandEnvironmentVariables(@"%SepidarProjectsRoot%\Api\CodeGenerator\SiteStartupFileTemplate.txt");
            //repositoryBuilder.GenerateRepositoryServicesForStartup(siteStartupFilePath);
            //repositoryBuilder.SaveStartup(@"%SepidarProjectsRoot%\Api\Site\Startup.cs");
            //GenerateBusinessServicesForStartup(@"%SepidarProjectsRoot%\Api\Site\Startup.cs");

            // Repository Factory
            // var repositoryFactoryBuilder = new RepositoryFactoryBuilder();
            // repositoryFactoryBuilder.Generate(repositoryBuilder.Tables);
            // repositoryFactoryBuilder.Save();
        }

        private static void GenerateBusinessServicesForStartup(string path)
        {
            var targetPath = Environment.ExpandEnvironmentVariables(@"%SepidarProjectsRoot%\Api\Business");
            var files = Directory.GetFiles(targetPath, "*Business.cs");

            targetPath = Environment.ExpandEnvironmentVariables(path);
            var text = File.ReadAllText(targetPath);
            var businessServicesStringBuilder = new StringBuilder();
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                businessServicesStringBuilder.Append(@"
            services.AddScoped<{0}, {1}>();".Fill("I" + fileName, fileName));
                businessServicesStringBuilder.AppendLine();
            }

            text = text.Replace("// InjectBusinesses", businessServicesStringBuilder.ToString());

            File.WriteAllText(targetPath, text);
        }
    }
}
