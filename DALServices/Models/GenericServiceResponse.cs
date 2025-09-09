namespace Services.Models
{
    public class GenericServiceResponse<T>
    {
        public bool Status { get; set; }
        public string message { get; set; }
        public T Data { get; set; }
    }
}
