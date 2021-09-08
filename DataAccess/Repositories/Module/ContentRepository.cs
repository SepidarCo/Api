using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Sepidar.Api.DataAccess.DbContexts;

namespace Sepidar.Api.DataAccess.Repositories.Module
{
    public partial class ContentRepository : Repository<Sepidar.Api.DataAccess.Models.Module.Content>
    {
        public ContentRepository(AppDbContext context)
            : base(context)
        {
        }

        public override DataTable ConfigureDataTable()
        {
            var table = new DataTable();

			table.Columns.Add("Id", typeof(long));
			table.Columns.Add("LanguageId", typeof(long));
			table.Columns.Add("OrderNumber", typeof(long));
			table.Columns.Add("Title", typeof(string));
			table.Columns.Add("PictureToken", typeof(string));
			table.Columns.Add("Description", typeof(string));
			table.Columns.Add("IsActive", typeof(bool));

            return table;
        }

        public override void AddRecord(DataTable table, Sepidar.Api.DataAccess.Models.Module.Content content)
        {
            var row = table.NewRow();

			row["Id"] = (object)content.Id ?? DBNull.Value;
			row["LanguageId"] = (object)content.LanguageId ?? DBNull.Value;
			row["OrderNumber"] = (object)content.OrderNumber ?? DBNull.Value;
			row["Title"] = (object)content.Title ?? DBNull.Value;
			row["PictureToken"] = (object)content.PictureToken ?? DBNull.Value;
			row["Description"] = (object)content.Description ?? DBNull.Value;
			row["IsActive"] = (object)content.IsActive ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override string TableName
        {
            get
            {
                return "[Module].[Contents]";
            }
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
			bulkOperator.ColumnMappings.Add("LanguageId", "[LanguageId]");
			bulkOperator.ColumnMappings.Add("OrderNumber", "[OrderNumber]");
			bulkOperator.ColumnMappings.Add("Title", "[Title]");
			bulkOperator.ColumnMappings.Add("PictureToken", "[PictureToken]");
			bulkOperator.ColumnMappings.Add("Description", "[Description]");
			bulkOperator.ColumnMappings.Add("IsActive", "[IsActive]");
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
                return "([LanguageId], [OrderNumber], [Title], [PictureToken], [Description], [IsActive]) values (s.[LanguageId], s.[OrderNumber], s.[Title], s.[PictureToken], s.[Description], s.[IsActive])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[LanguageId] = s.[LanguageId], t.[OrderNumber] = s.[OrderNumber], t.[Title] = s.[Title], t.[PictureToken] = s.[PictureToken], t.[Description] = s.[Description], t.[IsActive] = s.[IsActive]";
            }
        }

        public override Expression<Func<Sepidar.Api.DataAccess.Models.Module.Content, bool>> ExistenceFilter(Sepidar.Api.DataAccess.Models.Module.Content t)
        {
            Expression<Func<Sepidar.Api.DataAccess.Models.Module.Content, bool>> result = null;
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
						[LanguageId] bigint not null,
						[OrderNumber] bigint not null,
						[Title] nvarchar(100) not null,
						[PictureToken] nvarchar(500) null,
						[Description] nvarchar(MAX) not null,
						[IsActive] bit not null,    
                    )
                    ".Fill(tempTableName);
            return tempTableScript;
        }
    }
}
