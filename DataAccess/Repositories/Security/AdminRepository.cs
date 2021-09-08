using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Sepidar.Api.DataAccess.DbContexts;

namespace Sepidar.Api.DataAccess.Repositories.Security
{
    public partial class AdminRepository : Repository<Sepidar.Api.DataAccess.Models.Security.Admin>
    {
        public AdminRepository(AppDbContext context)
            : base(context)
        {
        }

        public override DataTable ConfigureDataTable()
        {
            var table = new DataTable();

			table.Columns.Add("Id", typeof(long));

            return table;
        }

        public override void AddRecord(DataTable table, Sepidar.Api.DataAccess.Models.Security.Admin admin)
        {
            var row = table.NewRow();

			row["Id"] = (object)admin.Id ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override string TableName
        {
            get
            {
                return "[Security].[Admins]";
            }
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
        }

        public override string BulkUpdateComparisonKey
        {
            get
            {
                return "(t.[Id] = s.[Id])";;
            }
        }

        public override string BulkUpdateInsertClause
        {
            get
            {
                return "([Id]) values (s.[Id])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[Id] = s.[Id]";
            }
        }

        public override Expression<Func<Sepidar.Api.DataAccess.Models.Security.Admin, bool>> ExistenceFilter(Sepidar.Api.DataAccess.Models.Security.Admin t)
        {
            Expression<Func<Sepidar.Api.DataAccess.Models.Security.Admin, bool>> result = null;
            if (t.Id > 0)
            {
                result = i => i.Id == t.Id;
            }
            else
            {
                result = i => i.Id == t.Id;
            }
            return result;
        }

        public override string TempTableCreationScript(string tempTableName)
        {
            var tempTableScript =  @"
                    create table {0}
                    (
						[Id] bigint not null,    
                    )
                    ".Fill(tempTableName);
            return tempTableScript;
        }
    }
}
