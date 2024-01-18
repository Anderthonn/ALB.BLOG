namespace ALB.BLOG.BLO.ViewModels
{
    public class ErrorVM
    {
        public string? RequestId { get; private set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorVM(string? requestId)
        {
            RequestId = requestId;
        }
    }
}