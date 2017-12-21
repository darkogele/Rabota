namespace Interop.CS.MetaService.Helpers
{
    public class CSRepoFactory : ICSRepoFactory
    {
        public IMetaServiceHelper GetMetaServiceHelper()
        {
            return new MetaServiceHelper();
        }
    }
}
