using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }

        public OperationResult(T response)
        {
            this.Result = response;
        }
    }

    public class OperationResult
    {
        public List<string> Errors { get; }

        public OperationResult()
        {
            this.Errors = new List<string>();
        }

        public bool Succeeded
        {
            get { return this.Errors.Count == 0; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string err in this.Errors)
            {
                sb.AppendFormat("{0}; ", err);
            }

            return sb.ToString();
        }
    }
}

