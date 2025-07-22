using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
        public static Result Success() => new Result(true, string.Empty);
        public static Result Failure(string error) => new Result(false, error);
    }


    public class Result<T>:Result
    {
        protected Result(bool isSuccess, string error, T data) : base(isSuccess, error)
        {
            Data = data;
        }

        private T Data { get; set; }
    }
}
