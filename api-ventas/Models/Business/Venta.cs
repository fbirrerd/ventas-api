using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace api_ventas.Models.Business
{
    public class Venta
    {

        public static DocumentoVenta realizarVenta(iDoc doc, VentasDB Db)
        {
            decimal dPorcentajeImpuesto = 19;
            try
            {
                //validar empresa - unidad de negocio
                if (!Unegocio.existUNegocioEmpresa(doc.empresa_id, doc.unegocio_id, Db))
                {
                    throw new Exception(string.Format("La unidad de negocio enviada no pertenece a la empresa {0}", doc.empresa_id));
                }

                //validar el tipo de documento enviado
                if (!TipoVenta.existeTipoVenta(doc.TipoVenta, Db))
                {
                    throw new Exception("No existe el tipo de venta indicado");
                }

                //validar los stocks
                doc.Details = normalizarStock(doc.Details);
                var stockOk = validarStock(doc, Db);

                if (stockOk)
                {
                    //agregar folio
                    var nFolio = Folio.getNewFolio(doc.empresa_id, doc.unegocio_id, doc.TipoVenta, Db);
                    //descontar la venta del stock
                    foreach (var d in doc.Details)
                    {
                        Stock.consumirStock(
                            doc.empresa_id,
                            doc.unegocio_id,
                            doc.usuario,
                            d.producto_id,
                            d.cantidad * -1,
                            Db);
                    }
                    //se inserta la venta en la base de datos
                    var oVenta = insertarVenta(doc, nFolio, dPorcentajeImpuesto, Db);
                    //agregar al documento de venta
                    var respuesta = getDOcumentoVenta(oVenta, Db);
                    return respuesta;

                }
                throw new Exception("Existe un error al intentar generar la boleta");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static List<iDocDetails> normalizarStock(List<iDocDetails> detalles)
        {
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

        private static DocumentoVenta getDOcumentoVenta(TVenta venta, VentasDB Db)
        {
            DocumentoVenta? respuesta = new DocumentoVenta();
            respuesta.documento = venta.documento.ToString().PadLeft(7, '0');
            respuesta.detalle = new List<VentaDetalle>();

            foreach (var dd in venta.detalle)
            {
                var prod = Stock.getProductoXId(dd.producto_id, Db);
                respuesta.detalle.Add(new VentaDetalle(prod.codigo, prod.nombre1, dd.cantidad, dd.valor));
            }

            respuesta.total = new VentaTotal(venta.neto, venta.impuesto, venta.total);
            return respuesta;
        }


        public static bool validarStock(iDoc doc, VentasDB Db)
        {
            List<String> resultado = new List<String>();
            doc.Details.ForEach(i =>
            {
                try
                {
                    ProductoStock ps = Stock.getProductoStock(
                        doc.empresa_id,
                        i.producto_id,
                        Db
                       );
                    if (ps.cantidad < i.cantidad)
                    {
                        resultado.Add(String.Format("No existe stock en el producto {0} (ID:{1}), faltan  {2}  productos ", ps.codigo, ps.producto_id, i.cantidad - ps.cantidad));
                    }

                }
                catch (Exception ex)
                {
                    resultado.Add(String.Format("No existe producto {0}  en la empresa {1}", i.producto_id, doc.empresa_id));
                }

            });
            if (resultado.Count > 0)
            {
                throw new Exception(string.Join(',', resultado));
            }
            return true;

        }
        public static DocumentoVenta getDOcumentoVenta()
        {
            return new DocumentoVenta();
        }

        public static TVenta insertarVenta(iDoc doc, long nroFolio, decimal impuestoVenta, VentasDB Db)
        {
            try
            {
                var tot = extraerTotal(doc, impuestoVenta);
                TVenta venta = new TVenta
                {
                    empresa_id = doc.empresa_id,
                    tipo_venta_sigla = doc.TipoVenta,
                    fecha_venta = DateTime.Now.ToUniversalTime(),
                    documento = nroFolio,
                    unegocio_id = doc.unegocio_id,
                    usuario_creador = doc.usuario,
                    neto = tot.neto,
                    impuesto = tot.impuesto,
                    total = tot.total
                };
                Db.Venta.Add(venta);
                Db.SaveChanges();
                List<TVentaDetalle> lista = new List<TVentaDetalle>();
                foreach (var d in doc.Details)
                {
                    TVentaDetalle det = new TVentaDetalle();
                    det.producto_id = d.producto_id;
                    det.cantidad = d.cantidad;
                    det.valor = d.monto;
                    det.venta_id = venta.venta_id;
                    Db.VentaDetalle.Add(det);
                    lista.Add(det);
                }
                Db.SaveChanges();
                venta.detalle = lista;
                return venta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static VentaTotal extraerTotal(iDoc doc, decimal porcentajeImpuestoVenta)
        {
            //calcular el total
            decimal total = 0;
            foreach (var det in doc.Details)
            {
                total = total + (det.cantidad * det.monto);
            }

            return new VentaTotal(total, porcentajeImpuestoVenta);




        }






    }
}
