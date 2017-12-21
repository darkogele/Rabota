
namespace Contracts.Models.DataForExecutedImport
{
    public class ExecutedImportOutput
    {
        public string EDB { get; set; }
        public double ImportAmount { get; set; }
        public double ImportTaxAmount { get; set; }
        public int ImportMonth { get; set; }
        public int ImportYear { get; set; }
    }
}
