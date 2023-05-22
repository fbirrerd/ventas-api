using api_ventas.Models.Data;
using api_ventas.Models.dto;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using Microsoft.CodeAnalysis.VisualBasic;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Linq;

namespace api_ventas.Models.Business
{
    public class Venta
    {

        public static RespuestaVenta realizarVenta(iDoc doc, VentasDB Db)
        {
            try
            {
                //validar empresa - unidad de negocio
                if (!Unegocio.existUNegocioEmpresa(doc.empresa_id, doc.unegocio_id, Db)) {
                    throw new Exception("La unidad de negocio enviada no pertenece a la empresa enviada");
                }

                //validar el tipo de documento enviado
                if (!TipoVenta.existeTipoVenta(doc.TipoVenta, Db)) {
                    throw new Exception("No existe el tipo de venta indicado");
                }

                //validar los stocks
                doc.Details = normalizarStock(doc.Details);
                var stockOk = validarStock(doc, Db);
                
                if (stockOk)
                {
                    //agregar folio
                    var nFolio = Folio.getNewFolio(doc.empresa_id, doc.unegocio_id,doc.TipoVenta, Db);

                    //descontar la venta del stock
                    foreach (var d in doc.Details) {

                        Stock.consumirStock(
                            doc.empresa_id, 
                            doc.unegocio_id,
                            doc.usuario,
                            d.producto_id,
                            d.cantidad,   
                            Db);
                    }
                    //se inserta la venta en la base de datos





                    //agregar al documento de venta
                    RespuestaVenta? respuesta = new RespuestaVenta();
                    respuesta.documento = nFolio.ToString().PadLeft(10,'0');
                    respuesta.detalle = new List<VentaDetalle>();

                    foreach (var dd in doc.Details) {
                        var prod = Stock.getProductoXId(dd.producto_id, Db);
                        respuesta.detalle.Add(new VentaDetalle(prod.codigo, prod.nombre1, dd.cantidad, dd.monto));
                    }

                    decimal sumatoriaMonto = 0;
                    respuesta.detalle.ForEach(item => sumatoriaMonto += item.monto);
                    respuesta.total = new VentaTotal(sumatoriaMonto,19);

                    return respuesta;
                }
                throw new Exception("Existe un error al intentar generar la boleta");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static List<iDocDetails> normalizarStock(List<iDocDetails> detalles) {
            string? resultado = detalles
                .GroupBy(objeto => objeto.producto_id)
                .Select(grupo => new
                {
                    producto_id = grupo.Key,
                    cantidad = grupo.Sum(objeto => objeto.cantidad),
                    monto = grupo.Average(objeto => objeto.monto)
                }).ToJson();


            List<iDocDetails>? respuesta = JsonConvert.DeserializeObject<List<iDocDetails>>(resultado);
            return respuesta;
        }


        public static bool validarStock(iDoc doc, VentasDB Db)
        {
            List<String> resultado  = new List<String>();
            doc.Details.ForEach(i =>
            {
                try {
                    ProductoStock ps = Stock.getProductoStock(
                        doc.empresa_id,
                        i.producto_id,
                        Db
                       );
                    if (ps.cantidad < i.cantidad) {
                        resultado.Add(String.Format("No existe stock en el producto {0} (ID:{1}), faltan  {2}  productos ", ps.codigo, ps.producto_id, i.cantidad- ps.cantidad));
                    }

                }
                catch (Exception ex) {
                    resultado.Add(String.Format("No existe producto {0}  en la empresa {1}", i.producto_id, doc.empresa_id));
                }

            });
            if (resultado.Count > 0) {
                throw new Exception(string.Join(',',resultado));
            }
            return true;

        }
        public static RespuestaVenta getDOcumentoVenta() 
        {
            return new RespuestaVenta();
        }

        public static TVenta insertarVenta(iDoc doc, long nroFolio, decimal impuestoVenta, VentasDB Db)
        {
            TVenta venta = new TVenta
            {
                empresa_id = doc.empresa_id,
                tipo_venta_sigla = doc.TipoVenta,
                fecha_venta = DateTime.Now,
                documento = nroFolio,
                unegocio_id = doc.unegocio_id,
                usuario_creador = doc.usuario
            };
            var resultado = Db.Venta.Add(venta);
            foreach (var d in doc.Details) { 
                TVentaDetalle det = new TVentaDetalle();
                det.producto_id = d.producto_id;
                det.cantidad = d.cantidad;
                det.valor = d.monto;
                //det.venta_id = resultado.CurrentValues("venta_id");
                det.impuesto = impuestoVenta;
                Db.VentaDetalle.Add(det);
            }







            return objVenta;
        }


    }
}
