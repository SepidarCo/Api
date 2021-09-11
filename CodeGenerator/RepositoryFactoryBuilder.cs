using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sepidar.Framework.Extensions;
using Sepidar.CodeGenerator;

namespace Sepidar.CodeGenerator
{
    public class RepositoryFactoryBuilder
    {
        StringBuilder repositoryFactory = new StringBuilder();

        public void Generate(List<Table> repositories)
        {
            repositories = repositories.OrderBy(i => i.Schema).ThenBy(i => i.SingularName).ToList();
            AppendClassStart();
            foreach (var repository in repositories)
            {
                var returnTypeFqn = GetReturnTypeFqn(repository);
                repositoryFactory.AppendLine();
                AppendProperty(repository, returnTypeFqn);
            }
            AppendClassEnd();
        }

        private void AppendProperty(Table repository, string returnTypeFqn)
        {
            repositoryFactory.AppendLine(@"        public static {0} {1}{2}Repository
        {{
            get
            {{
                return new {0}();
            }}
        }}".Fill(returnTypeFqn, repository.Schema, repository.SingularName));
        }

        private void AppendClassEnd()
        {
            repositoryFactory.AppendLine(@"    }
}");
        }

        private void AppendClassStart()
        {
            repositoryFactory.Append(@"namespace Sepidar.Api.DataAccess
{
    public class RepositoryFactory
    {");
        }

        private static string GetReturnTypeFqn(Table repository)
        {
            var schemaPart = repository.Schema.IsSomething() ? "." + repository.Schema : "";
            var viewPart = repository.IsView ? ".Views" : "";
            var classPart = "." + repository.SingularName + "Repository";
            var returnTypeFqn = "Repositories" + schemaPart + viewPart + classPart;
            return returnTypeFqn;
        }

        public void Save()
        {
            var rootPath = Environment.ExpandEnvironmentVariables(@"%SepidarProjectsRoot%\Api\DataAccess\");
            var targetPath = Path.Combine(rootPath, "RepositoryFactory.cs");
            File.WriteAllText(targetPath, repositoryFactory.ToString());
        }
    }
}
