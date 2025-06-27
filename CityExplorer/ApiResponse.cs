namespace CityExplorer
{
    public class ApiResponse<T>
    {

        public bool Issuccess { get; set; }

        public string? Message { get; set; }

        public string? Errors { get; set; }

        public T? Data { get; set; }
    }
}
