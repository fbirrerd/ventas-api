using api_ventas.Models.Data;

namespace api_ventas.Models.Business
{
    public class Venta
    {

        public static bool realizarVenta()
        {
            try {
                //validar los stocks
                //calcular los montos y los impuestos
                //armar el objeto del documento de respuesta
                //proceder a eliminar los stocks

                //responser
                return true;
            
            } catch(Exception ex) {
                throw new Exception(ex.Message, ex);
            }
        
        
        
        }



    }
}
