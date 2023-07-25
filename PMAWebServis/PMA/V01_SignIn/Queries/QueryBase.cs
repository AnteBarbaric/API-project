using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using V01_SignIn.Data.Models;

namespace Queries
{
    public class QueryBase
    {
        protected TESTContext db;
        protected string sql;
        public bool Success { get; protected set; }
        public string Error { get; protected set; }
        public double ElapsedMs { get; protected set; }
        
        protected Stopwatch sw;
        public QueryBase()
        {
            this.db = new TESTContext();
        }

        protected virtual bool Execute()
        {
            return false;
        }
        public virtual void FinalizeQuery()
        {

        }
        public bool RunQuery()
        {
            try
            {
                this.sw = new Stopwatch();
                this.sw.Start();
                this.Success = this.Execute();
                this.FinalizeQuery();
            }
            catch(Exception e)
            {
                this.Error = string.Format("{0}\n\rSTACK TRACE:{1}", e.Message, e.StackTrace);
            }
            finally
            {
                this.sw.Stop();
                this.ElapsedMs = this.sw.Elapsed.TotalMilliseconds;
            }
            return this.Success;
        }
    }
}
