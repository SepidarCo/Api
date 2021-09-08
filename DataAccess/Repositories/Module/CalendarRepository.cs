using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Sepidar.Api.DataAccess.DbContexts;

namespace Sepidar.Api.DataAccess.Repositories.Module
{
    public partial class CalendarRepository : Repository<Sepidar.Api.DataAccess.Models.Module.Calendar>
    {
        public CalendarRepository(AppDbContext context)
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
			table.Columns.Add("ShortDate", typeof(string));
			table.Columns.Add("Date", typeof(string));
			table.Columns.Add("Address", typeof(string));
			table.Columns.Add("IsActive", typeof(bool));
			table.Columns.Add("LinkUrl", typeof(string));

            return table;
        }

        public override void AddRecord(DataTable table, Sepidar.Api.DataAccess.Models.Module.Calendar calendar)
        {
            var row = table.NewRow();

			row["Id"] = (object)calendar.Id ?? DBNull.Value;
			row["LanguageId"] = (object)calendar.LanguageId ?? DBNull.Value;
			row["OrderNumber"] = (object)calendar.OrderNumber ?? DBNull.Value;
			row["Title"] = (object)calendar.Title ?? DBNull.Value;
			row["ShortDate"] = (object)calendar.ShortDate ?? DBNull.Value;
			row["Date"] = (object)calendar.Date ?? DBNull.Value;
			row["Address"] = (object)calendar.Address ?? DBNull.Value;
			row["IsActive"] = (object)calendar.IsActive ?? DBNull.Value;
			row["LinkUrl"] = (object)calendar.LinkUrl ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override string TableName
        {
            get
            {
                return "[Module].[Calendars]";
            }
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
			bulkOperator.ColumnMappings.Add("LanguageId", "[LanguageId]");
			bulkOperator.ColumnMappings.Add("OrderNumber", "[OrderNumber]");
			bulkOperator.ColumnMappings.Add("Title", "[Title]");
			bulkOperator.ColumnMappings.Add("ShortDate", "[ShortDate]");
			bulkOperator.ColumnMappings.Add("Date", "[Date]");
			bulkOperator.ColumnMappings.Add("Address", "[Address]");
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
                return "([LanguageId], [OrderNumber], [Title], [ShortDate], [Date], [Address], [IsActive], [LinkUrl]) values (s.[LanguageId], s.[OrderNumber], s.[Title], s.[ShortDate], s.[Date], s.[Address], s.[IsActive], s.[LinkUrl])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[LanguageId] = s.[LanguageId], t.[OrderNumber] = s.[OrderNumber], t.[Title] = s.[Title], t.[ShortDate] = s.[ShortDate], t.[Date] = s.[Date], t.[Address] = s.[Address], t.[IsActive] = s.[IsActive], t.[LinkUrl] = s.[LinkUrl]";
            }
        }

        public override Expression<Func<Sepidar.Api.DataAccess.Models.Module.Calendar, bool>> ExistenceFilter(Sepidar.Api.DataAccess.Models.Module.Calendar t)
        {
            Expression<Func<Sepidar.Api.DataAccess.Models.Module.Calendar, bool>> result = null;
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
						[Title] nvarchar(100) not null,
						[ShortDate] nchar(200) null,
						[Date] nchar(200) not null,
						[Address] nvarchar(1000) null,
						[IsActive] bit not null,
						[LinkUrl] nvarchar(500) null,    
                    )
                    ".Fill(tempTableName);
            return tempTableScript;
        }
    }
}
