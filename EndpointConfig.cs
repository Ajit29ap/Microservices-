namespace MicroService
{
    public class EndpointConfig
    {
        public string Path { get; set; }
        public string[] Verbs { get; set; }
        public string Tag { get; set; }
        public string OperationId { get; set; }
        public bool RequireAuth { get; set; } = false;
        public int[] Produces { get; set; } = new[] { StatusCodes.Status200OK };
    }
}
