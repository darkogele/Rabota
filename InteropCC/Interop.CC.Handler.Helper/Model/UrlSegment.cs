namespace Interop.CC.Handler.Helper.Model
{
    public class UrlSegment
    {
        public UrlSegment()
        {
            IsUrlCorrrect = false;
            Async = false;
        }
        public string Consumer { get; set; }
        public string RoutingToken { get; set; }
        public string Service { get; set; }
        public string Method { get; set; }
        public bool Async { get; set; }
        public bool IsUrlCorrrect { get; set; }
        public bool IsInteropTestCommunicationCall { get; set; }
    }
}