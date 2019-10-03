using System;
using System.Collections.Generic;
using System.Linq;

namespace Find_A_Tutor.Frontend.Model
{
    public class Result<T> : Result//, IEquatable<Result<T>> //where T : class
    {
        private T _value;
        public T Value
        {
            get
            {
                if (IsSuccess)
                {
                    return _value;
                }

                throw new ApplicationException("Cannot download data from failure result.");
            }
            set
            {
                _value = value;
                IsSuccess = true;
            }
        }

        public static Result<T> Ok(T successfulResult)
        {
            return new Result<T>
            {
                _value = successfulResult,
                IsSuccess = true
            };
        }

        public new static Result<T> Error(params string[] errors)
        {
            return new Result<T>
            {
                Errors = errors?.ToList(),
                IsSuccess = false
            };
        }

        public bool Equals(Result<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }
    }

    public class Result
    {
        public List<string> Errors { get; set; }
        public bool IsSuccess { get; set; }

        public static Result Ok()
        {
            return new Result
            {
                IsSuccess = true
            };
        }

        public static Result Error(params string[] errors)
        {
            return new Result
            {
                Errors = errors?.ToList(),
                IsSuccess = false
            };
        }

        public static Result Error(Exception exception, string message)
        {
            return new Result
            {
                IsSuccess = true,
                Errors = new List<string>
                {
                    message,
                    exception.ToString()
                }
            };
        }
    }
}
