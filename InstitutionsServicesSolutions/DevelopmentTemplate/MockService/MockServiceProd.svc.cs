namespace MockService
{
    public class MockServiceProd : IMockServiceProd
    {
        public string GetEnvName_ProdMethod()
        {
            return "Production";
        }
    }
}
