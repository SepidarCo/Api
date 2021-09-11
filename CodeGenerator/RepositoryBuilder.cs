using System;
using System.IO;
using System.Xml.Linq;
using Sepidar.Framework.Extensions;
using Sepidar.CodeGenerator;

namespace Sepidar.CodeGenerator
{
    public class RepositoryBuilder : RepositoryGenerator
    {
        public RepositoryBuilder()
            : base(GeneratorConfig.FreieBuehneStuttgartConnectionString)
        {
        }

        public void Save()
        {
            SaveRepositories();
        }

        private void SaveRepositories()
        {
            var repositoriesFolder = PrepareOutputFolder(@"%SepidarProjectsRoot%\Api\DataAccess\Repositories\");
            foreach (var model in Tables)
            {
                var schemaFolder = model.GetSchemaPath();
                var targetPath = Path.Combine(repositoriesFolder, schemaFolder, (model.IsView ? @"Views" : ""),
                    model.SingularName + "Repository.cs");
                var targetDirectory = Path.GetDirectoryName(targetPath);
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                File.WriteAllText(targetPath, model.GeneratedCode);
            }
        }

        public void SaveStartup(string path)
        {
            var targetPath = Environment.ExpandEnvironmentVariables(path);
            File.WriteAllText(targetPath, GeneratedInjectRepositoryServicesCode);
        }

        public override string Namespace
        {
            get
            {
                return @"namespace Sepidar.Api.DataAccess.Repositories";
            }
        }

        public override string GetNamespaceBySchema(string schema)
        {
            return @"namespace Sepidar.Api.{0}.Repositories".Fill(schema);
        }

        public override string UsingStatements
        {
            get
            {
                return @"using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;";
            }
        }



      
    }
}
