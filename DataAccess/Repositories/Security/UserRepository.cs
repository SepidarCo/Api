using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Sepidar.Api.DataAccess.DbContexts;

namespace Sepidar.Api.DataAccess.Repositories.Security
{
    public partial class UserRepository : Repository<Sepidar.Api.DataAccess.Models.Security.User>
    {
        public UserRepository(AppDbContext context)
            : base(context)
        {
        }

        public override DataTable ConfigureDataTable()
        {
            var table = new DataTable();

			table.Columns.Add("Id", typeof(long));
			table.Columns.Add("FullName", typeof(string));
			table.Columns.Add("Username", typeof(string));
			table.Columns.Add("Password", typeof(string));
			table.Columns.Add("PictureToken", typeof(string));
			table.Columns.Add("MobileNumber", typeof(string));
			table.Columns.Add("Email", typeof(string));
			table.Columns.Add("CreationDate", typeof(DateTime));

            return table;
        }

        public override void AddRecord(DataTable table, Sepidar.Api.DataAccess.Models.Security.User user)
        {
            var row = table.NewRow();

			row["Id"] = (object)user.Id ?? DBNull.Value;
			row["FullName"] = (object)user.FullName ?? DBNull.Value;
			row["Username"] = (object)user.Username ?? DBNull.Value;
			row["Password"] = (object)user.Password ?? DBNull.Value;
			row["PictureToken"] = (object)user.PictureToken ?? DBNull.Value;
			row["MobileNumber"] = (object)user.MobileNumber ?? DBNull.Value;
			row["Email"] = (object)user.Email ?? DBNull.Value;
			row["CreationDate"] = (object)user.CreationDate ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override string TableName
        {
            get
            {
                return "[Security].[Users]";
            }
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
			bulkOperator.ColumnMappings.Add("FullName", "[FullName]");
			bulkOperator.ColumnMappings.Add("Username", "[Username]");
			bulkOperator.ColumnMappings.Add("Password", "[Password]");
			bulkOperator.ColumnMappings.Add("PictureToken", "[PictureToken]");
			bulkOperator.ColumnMappings.Add("MobileNumber", "[MobileNumber]");
			bulkOperator.ColumnMappings.Add("Email", "[Email]");
			bulkOperator.ColumnMappings.Add("CreationDate", "[CreationDate]");
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
                return "([FullName], [Username], [Password], [PictureToken], [MobileNumber], [Email], [CreationDate]) values (s.[FullName], s.[Username], s.[Password], s.[PictureToken], s.[MobileNumber], s.[Email], s.[CreationDate])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[FullName] = s.[FullName], t.[Username] = s.[Username], t.[Password] = s.[Password], t.[PictureToken] = s.[PictureToken], t.[MobileNumber] = s.[MobileNumber], t.[Email] = s.[Email], t.[CreationDate] = s.[CreationDate]";
            }
        }

        public override Expression<Func<Sepidar.Api.DataAccess.Models.Security.User, bool>> ExistenceFilter(Sepidar.Api.DataAccess.Models.Security.User t)
        {
            Expression<Func<Sepidar.Api.DataAccess.Models.Security.User, bool>> result = null;
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
						[FullName] varchar(100) null,
						[Username] varchar(50) null,
						[Password] varchar(50) not null,
						[PictureToken] nvarchar(500) null,
						[MobileNumber] varchar(50) null,
						[Email] nvarchar(1000) null,
						[CreationDate] datetime not null,    
                    )
                    ".Fill(tempTableName);
            return tempTableScript;
        }
    }
}
