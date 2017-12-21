namespace CallingHandler.Models
{
    public class ServiceResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
    }
}
