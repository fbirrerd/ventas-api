namespace api_ventas.Models.Objects
{
    public class iDoc
    {
        public long numero { get; set; } 
        public long empresa_id { get; set; }
        public long unegocio_id { get; set; }
        public string usuario { get; set; }
        public DateTime creation_date { get; set; }
        public iDocDetails Details { get; set; }
        public long tipoventa_id { get; set; }
        public string observaciones { get; set; }

        public iDoc()
        {
            this.Details = new iDocDetails();

        }


    }
}
