using System.Collections.Generic;
using System.Linq;

namespace Find_A_Tutor.Core.Domain
{
    public class Result<T> where T : class
    {
        private T _value;
        public List<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                IsSuccess = true;
            }
        }


        public static Result<T> Ok(T successfulResult)
        {
            return new Result<T>(successfulResult);
        }

        private Result(T successfulResult)
        {
            _value = successfulResult;
            IsSuccess = true;
        }


        public static Result<T> Error(params string[] errors)
        {
            return new Result<T>(errors);
        }
        private Result(params string[] errors)
        {
            Errors = errors?.ToList();
            IsSuccess = false;
        }
    }
}
