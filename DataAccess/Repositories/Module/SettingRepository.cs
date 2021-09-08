using Sepidar.EntityFramework;
using Sepidar.Framework.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Sepidar.Api.DataAccess.DbContexts;

namespace Sepidar.Api.DataAccess.Repositories.Module
{
    public partial class SettingRepository : Repository<Sepidar.Api.DataAccess.Models.Module.Setting>
    {
        public SettingRepository(AppDbContext context)
            : base(context)
        {
        }

        public override DataTable ConfigureDataTable()
        {
            var table = new DataTable();

			table.Columns.Add("Id", typeof(long));
			table.Columns.Add("SiteName", typeof(string));
			table.Columns.Add("Keyword", typeof(string));
			table.Columns.Add("Description", typeof(string));
			table.Columns.Add("Copyright", typeof(string));
			table.Columns.Add("CompanyName", typeof(string));
			table.Columns.Add("Telephone", typeof(string));
			table.Columns.Add("Email", typeof(string));
			table.Columns.Add("Fax", typeof(string));
			table.Columns.Add("Address", typeof(string));
			table.Columns.Add("Map", typeof(string));
			table.Columns.Add("Instagram", typeof(string));
			table.Columns.Add("Facebook", typeof(string));
			table.Columns.Add("LinkedIn", typeof(string));
			table.Columns.Add("TikTok", typeof(string));
			table.Columns.Add("Youtube", typeof(string));
			table.Columns.Add("Whatsapp", typeof(string));
			table.Columns.Add("Twitter", typeof(string));
			table.Columns.Add("Pinterest", typeof(string));

            return table;
        }

        public override void AddRecord(DataTable table, Sepidar.Api.DataAccess.Models.Module.Setting setting)
        {
            var row = table.NewRow();

			row["Id"] = (object)setting.Id ?? DBNull.Value;
			row["SiteName"] = (object)setting.SiteName ?? DBNull.Value;
			row["Keyword"] = (object)setting.Keyword ?? DBNull.Value;
			row["Description"] = (object)setting.Description ?? DBNull.Value;
			row["Copyright"] = (object)setting.Copyright ?? DBNull.Value;
			row["CompanyName"] = (object)setting.CompanyName ?? DBNull.Value;
			row["Telephone"] = (object)setting.Telephone ?? DBNull.Value;
			row["Email"] = (object)setting.Email ?? DBNull.Value;
			row["Fax"] = (object)setting.Fax ?? DBNull.Value;
			row["Address"] = (object)setting.Address ?? DBNull.Value;
			row["Map"] = (object)setting.Map ?? DBNull.Value;
			row["Instagram"] = (object)setting.Instagram ?? DBNull.Value;
			row["Facebook"] = (object)setting.Facebook ?? DBNull.Value;
			row["LinkedIn"] = (object)setting.LinkedIn ?? DBNull.Value;
			row["TikTok"] = (object)setting.TikTok ?? DBNull.Value;
			row["Youtube"] = (object)setting.Youtube ?? DBNull.Value;
			row["Whatsapp"] = (object)setting.Whatsapp ?? DBNull.Value;
			row["Twitter"] = (object)setting.Twitter ?? DBNull.Value;
			row["Pinterest"] = (object)setting.Pinterest ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override string TableName
        {
            get
            {
                return "[Module].[Settings]";
            }
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
			bulkOperator.ColumnMappings.Add("SiteName", "[SiteName]");
			bulkOperator.ColumnMappings.Add("Keyword", "[Keyword]");
			bulkOperator.ColumnMappings.Add("Description", "[Description]");
			bulkOperator.ColumnMappings.Add("Copyright", "[Copyright]");
			bulkOperator.ColumnMappings.Add("CompanyName", "[CompanyName]");
			bulkOperator.ColumnMappings.Add("Telephone", "[Telephone]");
			bulkOperator.ColumnMappings.Add("Email", "[Email]");
			bulkOperator.ColumnMappings.Add("Fax", "[Fax]");
			bulkOperator.ColumnMappings.Add("Address", "[Address]");
			bulkOperator.ColumnMappings.Add("Map", "[Map]");
			bulkOperator.ColumnMappings.Add("Instagram", "[Instagram]");
			bulkOperator.ColumnMappings.Add("Facebook", "[Facebook]");
			bulkOperator.ColumnMappings.Add("LinkedIn", "[LinkedIn]");
			bulkOperator.ColumnMappings.Add("TikTok", "[TikTok]");
			bulkOperator.ColumnMappings.Add("Youtube", "[Youtube]");
			bulkOperator.ColumnMappings.Add("Whatsapp", "[Whatsapp]");
			bulkOperator.ColumnMappings.Add("Twitter", "[Twitter]");
			bulkOperator.ColumnMappings.Add("Pinterest", "[Pinterest]");
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
                return "([SiteName], [Keyword], [Description], [Copyright], [CompanyName], [Telephone], [Email], [Fax], [Address], [Map], [Instagram], [Facebook], [LinkedIn], [TikTok], [Youtube], [Whatsapp], [Twitter], [Pinterest]) values (s.[SiteName], s.[Keyword], s.[Description], s.[Copyright], s.[CompanyName], s.[Telephone], s.[Email], s.[Fax], s.[Address], s.[Map], s.[Instagram], s.[Facebook], s.[LinkedIn], s.[TikTok], s.[Youtube], s.[Whatsapp], s.[Twitter], s.[Pinterest])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[SiteName] = s.[SiteName], t.[Keyword] = s.[Keyword], t.[Description] = s.[Description], t.[Copyright] = s.[Copyright], t.[CompanyName] = s.[CompanyName], t.[Telephone] = s.[Telephone], t.[Email] = s.[Email], t.[Fax] = s.[Fax], t.[Address] = s.[Address], t.[Map] = s.[Map], t.[Instagram] = s.[Instagram], t.[Facebook] = s.[Facebook], t.[LinkedIn] = s.[LinkedIn], t.[TikTok] = s.[TikTok], t.[Youtube] = s.[Youtube], t.[Whatsapp] = s.[Whatsapp], t.[Twitter] = s.[Twitter], t.[Pinterest] = s.[Pinterest]";
            }
        }

        public override Expression<Func<Sepidar.Api.DataAccess.Models.Module.Setting, bool>> ExistenceFilter(Sepidar.Api.DataAccess.Models.Module.Setting t)
        {
            Expression<Func<Sepidar.Api.DataAccess.Models.Module.Setting, bool>> result = null;
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
						[SiteName] nvarchar(200) not null,
						[Keyword] nvarchar(300) not null,
						[Description] nvarchar(300) not null,
						[Copyright] nvarchar(500) not null,
						[CompanyName] nvarchar(200) null,
						[Telephone] nvarchar(200) not null,
						[Email] nvarchar(500) null,
						[Fax] nvarchar(200) null,
						[Address] nvarchar(200) null,
						[Map] nvarchar(4000) null,
						[Instagram] nvarchar(300) null,
						[Facebook] nvarchar(300) null,
						[LinkedIn] nvarchar(300) null,
						[TikTok] nvarchar(300) null,
						[Youtube] nvarchar(500) null,
						[Whatsapp] nvarchar(300) null,
						[Twitter] nvarchar(300) null,
						[Pinterest] nvarchar(300) null,    
                    )
                    ".Fill(tempTableName);
            return tempTableScript;
        }
    }
}
