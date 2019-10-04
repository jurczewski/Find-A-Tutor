using System.Collections.Generic;

namespace Find_A_Tutor.Frontend.Model
{
    public class ResultSimple<T>
    {
        public T Value { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
    }
}
