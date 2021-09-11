using System.IO;
using Microsoft.Extensions.Configuration;
using Sepidar.Framework;
using Sepidar.Framework.Extensions;

namespace Sepidar.CodeGenerator
{
    public class GeneratorConfig : Config
    {
        public static string FreieBuehneStuttgartConnectionString
        {
            get
            {
                var connectionString = GetConnectionString("Api");
                return connectionString;
            }
        }
    }
}
