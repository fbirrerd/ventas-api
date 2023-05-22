using api_ventas.Models.Objects;
using NuGet.Protocol;

namespace api_ventas.Models.dto
{
    public class DataTranformationObjetct
    {
        public static List<iDocDetails> Convert(string obj) {

            var lista = new List<iDocDetails>();

            foreach (var o in obj) {

                var det = new iDocDetails();

                lista.Add(det);
            }


            return lista;


        }
    }
}
