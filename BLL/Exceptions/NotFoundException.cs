namespace BLL.Exceptions
{
    public class NotFoundException : RssManagerException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
