using System.IO;
using Sepidar.Framework.Extensions;
using Sepidar.CodeGenerator;

namespace Sepidar.CodeGenerator
{
    public class DbContextBuilder : DbContextGenerator
    {
        public DbContextBuilder()
            : base(GeneratorConfig.FreieBuehneStuttgartConnectionString)
        {
        }

        public void Save()
        {
            var dbContextsFolder = PrepareOutputFolder(@"%SepidarProjectsRoot%\Api\DataAccess\DbContexts\");
            if (CodeGeneratorConfig.CreateDbContextPerModel)
            {
                foreach (var table in Tables)
                {
                    var schemaFolder = table.GetSchemaPath();
                    var targetPath = Path.Combine(dbContextsFolder, schemaFolder, (table.IsView ? @"Views" : ""), table.SingularName + "DbContext.cs");
                    var targetDirectory = Path.GetDirectoryName(targetPath);
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }
                    File.WriteAllText(targetPath, table.GeneratedCode);
                }
            }
            else
            {
                var targetPath = Path.Combine(dbContextsFolder, "AppDbContext.cs");

                var dbContextClass = GenerateDbContextClass(Tables);
                var targetDirectory = Path.GetDirectoryName(targetPath);
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }
                File.WriteAllText(targetPath, dbContextClass);

            }

        }

        public override string GetNamespaceBySchema(string schema)
        {
            return @"namespace Sepidar.Api.{0}.DbContexts".Fill(schema);
        }

        public override string Namespace
        {
            get
            {
                return @"namespace Sepidar.Api.DataAccess.DbContexts";
            }
        }

        public override string UsingStatements
        {
            get
            {
                return @"using Microsoft.EntityFrameworkCore;
";
            }
        }

        public override string ConnectionStringName
        {
            get
            {
                return "Api";
            }
        }
    }
}
