using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Sepidar.Api.DataAccess.DbContexts;

namespace Sepidar.Api.DataAccess.Repositories.Module
{
    public partial class SliderRepository : Repository<Sepidar.Api.DataAccess.Models.Module.Slider>
    {
        public SliderRepository(AppDbContext context)
            : base(context)
        {
        }

        public override DataTable ConfigureDataTable()
        {
            var table = new DataTable();

			table.Columns.Add("Id", typeof(long));
			table.Columns.Add("LanguageId", typeof(long));
			table.Columns.Add("Title", typeof(string));
			table.Columns.Add("OrderNumber", typeof(long));
			table.Columns.Add("PictureToken", typeof(string));
			table.Columns.Add("Description", typeof(string));
			table.Columns.Add("IsActive", typeof(bool));
			table.Columns.Add("LinkUrl", typeof(string));

            return table;
        }

        public override void AddRecord(DataTable table, Sepidar.Api.DataAccess.Models.Module.Slider slider)
        {
            var row = table.NewRow();

			row["Id"] = (object)slider.Id ?? DBNull.Value;
			row["LanguageId"] = (object)slider.LanguageId ?? DBNull.Value;
			row["Title"] = (object)slider.Title ?? DBNull.Value;
			row["OrderNumber"] = (object)slider.OrderNumber ?? DBNull.Value;
			row["PictureToken"] = (object)slider.PictureToken ?? DBNull.Value;
			row["Description"] = (object)slider.Description ?? DBNull.Value;
			row["IsActive"] = (object)slider.IsActive ?? DBNull.Value;
			row["LinkUrl"] = (object)slider.LinkUrl ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override string TableName
        {
            get
            {
                return "[Module].[Sliders]";
            }
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
			bulkOperator.ColumnMappings.Add("LanguageId", "[LanguageId]");
			bulkOperator.ColumnMappings.Add("Title", "[Title]");
			bulkOperator.ColumnMappings.Add("OrderNumber", "[OrderNumber]");
			bulkOperator.ColumnMappings.Add("PictureToken", "[PictureToken]");
			bulkOperator.ColumnMappings.Add("Description", "[Description]");
			bulkOperator.ColumnMappings.Add("IsActive", "[IsActive]");
			bulkOperator.ColumnMappings.Add("LinkUrl", "[LinkUrl]");
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
                return "([LanguageId], [Title], [OrderNumber], [PictureToken], [Description], [IsActive], [LinkUrl]) values (s.[LanguageId], s.[Title], s.[OrderNumber], s.[PictureToken], s.[Description], s.[IsActive], s.[LinkUrl])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[LanguageId] = s.[LanguageId], t.[Title] = s.[Title], t.[OrderNumber] = s.[OrderNumber], t.[PictureToken] = s.[PictureToken], t.[Description] = s.[Description], t.[IsActive] = s.[IsActive], t.[LinkUrl] = s.[LinkUrl]";
            }
        }

        public override Expression<Func<Sepidar.Api.DataAccess.Models.Module.Slider, bool>> ExistenceFilter(Sepidar.Api.DataAccess.Models.Module.Slider t)
        {
            Expression<Func<Sepidar.Api.DataAccess.Models.Module.Slider, bool>> result = null;
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
						[Title] nvarchar(100) null,
						[OrderNumber] bigint not null,
						[PictureToken] nvarchar(500) not null,
						[Description] nvarchar(500) null,
						[IsActive] bit not null,
						[LinkUrl] nvarchar(500) null,    
                    )
                    ".Fill(tempTableName);
            return tempTableScript;
        }
    }
}
