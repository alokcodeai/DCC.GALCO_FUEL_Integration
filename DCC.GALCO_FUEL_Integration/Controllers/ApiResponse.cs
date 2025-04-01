namespace DCC.GALCO_FUEL_Integration.Controllers
{
    public class ApiResponse<T>
    {
        public int status_code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public List<Dictionary<string, object>> dict_data { get; set; }

      //  public DataTable? tbldata { get; set; }
        public List<T>? data { get; set; }
        public string DocKey { get; set; }
    }
}
