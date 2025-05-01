namespace FSTW_backend
{
    public class ResponseResult<T>
    {
        public bool Successed { get; }
        public T Value { get; }
        public List<Dictionary<string, string>> Errors { get; set; }

        public ResponseResult(bool successed, T value, List<Dictionary<string, string>> error)
        {
            Successed = successed;
            Value = value;
            Errors = error;
        }

        public static ResponseResult<T> Success(T value)
        {
            return new ResponseResult<T>(true, value, null);
        }

        public static ResponseResult<T> Failure(List<Dictionary<string, string>> error)
        {
            return new ResponseResult<T>(false, default, error);
        }
    }
}
