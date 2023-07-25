using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Models.Tables;
using V01_SignIn.Data.Models;

namespace Queries.Accounts
{
    public class AccountQuery : PagedQueryBase
    {
        protected string filterUsername;
        protected string filterLanguageId;
        protected int filterUserRoleId;
        protected string filterStateId;
        public AccountQuery(AccountTableInputModel model ):base(11, model)
        {
            this.filterUsername = model.FilterUsername;
            this.filterLanguageId = model.FilterLanguageId;
            this.filterUserRoleId = model.FilterUserRoleId;
            this.filterStateId = model.FilterStateId;
        }

        protected string GetStatusName(string stateId)
        {
            switch(stateId)
            {
                case "A": return "Active";
                case "I": return "Inactive";
                default: return "???";
            }
        }

        protected override void SetResponseParams()
        {
            base.SetResponseParams();

            SelectColumnType language = new SelectColumnType("language");
            language.Items.Add(new SelectOptionItem("0", "-- select one --"));
            language.Items.Add(new SelectOptionItem("hr", "Croatian"));
            language.Items.Add(new SelectOptionItem("en", "English"));
            this.columnDefinitions[5] = language;


            SelectColumnType roleType = new SelectColumnType("role");
            roleType.Items.Add(new SelectOptionItem("1", "Administrator"));
            roleType.Items.Add(new SelectOptionItem("2", "User"));
            this.columnDefinitions[6] = roleType;

            SelectColumnType state = new SelectColumnType("state");
            state.Items.Add(new SelectOptionItem("A", "Active"));
            state.Items.Add(new SelectOptionItem("I", "Inactive"));

            this.columnDefinitions[7] = state;

            this.columnDefinitions[8] = new ButtonColumnType("Edit", "edit", "table-button", "table-button-cell");
            this.columnDefinitions[9] = new ButtonColumnType("Delete", "delete", "table-button", "table-button-cell");
            this.columnDefinitions[10] = new PrimaryKeyColumnType();
        }
        protected TRow GetRow(UserLogin item)
        {
            TRow row = new TRow(this.Result.Columns);
            int index = 0;
            row.Cells[index++] = item.Username;
            row.Cells[index++] = item.FirstName != null ? item.FirstName : string.Empty;
            row.Cells[index++] = this.GetText(item.LastName);
            row.Cells[index++] = item.Email;
            row.Cells[index++] = this.GetText(item.Phone);
            // ovdje je index = 5
            row.Cells[5] = ((SelectColumnType)this.columnDefinitions[5]).FindTextFromValue(item.LanguageId);
            row.Cells[6] = ((SelectColumnType)this.columnDefinitions[6]).FindTextFromValue(item.UserRoleId.ToString());
            row.Cells[7] = ((SelectColumnType)this.columnDefinitions[7]).FindTextFromValue(item.StateId);
            // ovdje je index = 8
            row.Cells[8] = item.UserLoginId.ToString(); // id za dugme edit
            row.Cells[9] = item.UserLoginId.ToString(); // id za dugme delete
            row.Cells[10] = item.UserLoginId.ToString(); // svaki redak u HTML tablici odgovara jednom retku tablice baze podataka UserLogin
            return row;
        }
        protected override bool Execute()
        {
            var accounts = this.db.UserLogins
                .Include(x => x.UserRole)
                .Include(x => x.Language)
                .OrderBy(x => x.Username);

            if(!string.IsNullOrEmpty(this.filterUsername))
            {
                accounts = (IOrderedQueryable<UserLogin>)accounts.Where(x => x.Username.Contains(this.filterUsername));
            }
            if (this.filterLanguageId != "0")
            {
                accounts = (IOrderedQueryable<UserLogin>)accounts.Where(x => x.LanguageId == this.filterLanguageId);
            }
            if(this.filterUserRoleId != 0)
            {
                accounts = (IOrderedQueryable<UserLogin>)accounts.Where(x => x.UserRoleId == this.filterUserRoleId);
            }
            if (this.filterStateId != "0")
            {
                accounts = (IOrderedQueryable<UserLogin>)accounts.Where(x => x.StateId == this.filterStateId);
            }

            int count = 0;
            foreach(var item in accounts)
            {
                count++;
                if(count >= this.fromRow && count <= this.toRow)
                {
                    TRow row = this.GetRow(item);
                    this.Result.Rows.Add(row);
                }
            }
            this.Result.TotalRows = count;
            this.Result.TotalPages = this.GetTotalPagesCount(count);
            return true;
        }
    }
}
