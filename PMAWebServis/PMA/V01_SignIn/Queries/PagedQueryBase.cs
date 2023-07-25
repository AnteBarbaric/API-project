using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Models.Tables;

namespace Queries
{
    public class PagedQueryBase : QueryBase
    {
        protected int fromRow;
        protected int toRow;
        protected ColumnTypeBase[] columnDefinitions;
        
        protected TableInputModel model;
        public TableResponseModel Result;
        public PagedQueryBase(int columns, TableInputModel model)
        {
            this.model = model;
            this.fromRow = (model.Page - 1) * model.RowsPerPage + 1;
            this.toRow = this.fromRow + model.RowsPerPage - 1;

            this.Result = new TableResponseModel(columns);
            this.Result.TableId = model.TableId;
            this.Result.Trigger = model.Trigger;
            this.Result.CurrentPage = model.Page;
            this.Result.RowsPerPage = model.RowsPerPage;
            this.columnDefinitions = new ColumnTypeBase[columns];

            this.SetResponseParams();
        }

        protected virtual void SetResponseParams()
        {

        }
        protected int GetTotalPagesCount(int totalRows)
        {
            int count = totalRows / this.model.RowsPerPage;
            if (totalRows % this.model.RowsPerPage != 0)
            {
                count++;
            }
            return count;
        }
        protected string GetText(string text)
        {
            return !string.IsNullOrEmpty(text) ? text : string.Empty;
        }
        public override void FinalizeQuery()
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            this.Result.ColumnDefinitions =  Newtonsoft.Json.JsonConvert.SerializeObject(this.columnDefinitions, options);
        }
    }
}
