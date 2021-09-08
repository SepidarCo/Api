using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Sepidar.Api.DataAccess.DbContexts;

namespace Sepidar.Api.DataAccess.Repositories.Module
{
    public partial class GalleryRepository : Repository<Sepidar.Api.DataAccess.Models.Module.Gallery>
    {
        public GalleryRepository(AppDbContext context)
            : base(context)
        {
        }

        public override DataTable ConfigureDataTable()
        {
            var table = new DataTable();

			table.Columns.Add("Id", typeof(long));
			table.Columns.Add("LanguageId", typeof(long));
			table.Columns.Add("OrderNumber", typeof(long));
			table.Columns.Add("PictureToken", typeof(string));
			table.Columns.Add("Title", typeof(string));
			table.Columns.Add("Alt", typeof(string));
			table.Columns.Add("IsActive", typeof(bool));

            return table;
        }

        public override void AddRecord(DataTable table, Sepidar.Api.DataAccess.Models.Module.Gallery gallery)
        {
            var row = table.NewRow();

			row["Id"] = (object)gallery.Id ?? DBNull.Value;
			row["LanguageId"] = (object)gallery.LanguageId ?? DBNull.Value;
			row["OrderNumber"] = (object)gallery.OrderNumber ?? DBNull.Value;
			row["PictureToken"] = (object)gallery.PictureToken ?? DBNull.Value;
			row["Title"] = (object)gallery.Title ?? DBNull.Value;
			row["Alt"] = (object)gallery.Alt ?? DBNull.Value;
			row["IsActive"] = (object)gallery.IsActive ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override string TableName
        {
            get
            {
                return "[Module].[Galleries]";
            }
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
			bulkOperator.ColumnMappings.Add("LanguageId", "[LanguageId]");
			bulkOperator.ColumnMappings.Add("OrderNumber", "[OrderNumber]");
			bulkOperator.ColumnMappings.Add("PictureToken", "[PictureToken]");
			bulkOperator.ColumnMappings.Add("Title", "[Title]");
			bulkOperator.ColumnMappings.Add("Alt", "[Alt]");
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
                return "([LanguageId], [OrderNumber], [PictureToken], [Title], [Alt], [IsActive]) values (s.[LanguageId], s.[OrderNumber], s.[PictureToken], s.[Title], s.[Alt], s.[IsActive])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[LanguageId] = s.[LanguageId], t.[OrderNumber] = s.[OrderNumber], t.[PictureToken] = s.[PictureToken], t.[Title] = s.[Title], t.[Alt] = s.[Alt], t.[IsActive] = s.[IsActive]";
            }
        }

        public override Expression<Func<Sepidar.Api.DataAccess.Models.Module.Gallery, bool>> ExistenceFilter(Sepidar.Api.DataAccess.Models.Module.Gallery t)
        {
            Expression<Func<Sepidar.Api.DataAccess.Models.Module.Gallery, bool>> result = null;
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
						[LanguageId] bigint null,
						[OrderNumber] bigint not null,
						[PictureToken] nvarchar(500) not null,
						[Title] nvarchar(200) null,
						[Alt] nvarchar(200) null,
						[IsActive] bit not null,    
                    )
                    ".Fill(tempTableName);
            return tempTableScript;
        }
    }
}
