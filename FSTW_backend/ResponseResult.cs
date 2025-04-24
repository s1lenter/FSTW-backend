namespace FSTW_backend
{
    public class ResponseResult<T>
    {
        public bool Successed { get; }
        public T Value { get; }
        public string? Error { get; set; }

        public ResponseResult(bool successed, T value, string error)
        {
            Successed = successed;
            Value = value;
            Error = error;
        }

        public static ResponseResult<T> Success(T value)
        {
            return new ResponseResult<T>(true, value, null);
        }

        public static ResponseResult<T> Failure(string error)
        {
            return new ResponseResult<T>(false, default, error);
        }
    }
}
