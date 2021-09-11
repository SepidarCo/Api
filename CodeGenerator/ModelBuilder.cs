using System.IO;
using Sepidar.Framework.Extensions;
using Sepidar.CodeGenerator;

namespace Sepidar.CodeGenerator
{

    public class ModelBuilder : ModelGenerator
    {
        public ModelBuilder()
            : base(GeneratorConfig.FreieBuehneStuttgartConnectionString)
        {
        }

        public void Save()
        {
            var modelsFolder = PrepareOutputFolder(@"%SepidarProjectsRoot%\Api\DataAccess\Models\");
            foreach (var model in Tables)
            {
                var schemaFolder = model.GetSchemaPath();
                var targetPath = Path.Combine(modelsFolder, schemaFolder, (model.IsView ? @"Views" : ""), model.SingularName + ".cs");
                var targetDirectory = Path.GetDirectoryName(targetPath);
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }
                File.WriteAllText(targetPath, model.GeneratedCode);
            }
        }

        public override string Namespace
        {
            get
            {
                return @"namespace Sepidar.Api.DataAccess.Models";
            }
        }

        public override string GetNamespaceBySchema(string schema)
        {
            return @"namespace Sepidar.Api.{0}.Models".Fill(schema);
        }

        public override string UsingStatements
        {
            get
            {
                return @"using System;";
            }
        }
    }
}
