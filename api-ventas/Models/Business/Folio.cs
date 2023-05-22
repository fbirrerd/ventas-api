using api_ventas.Models.Data;
using api_ventas.Models.Tables;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace api_ventas.Models.Business
{
    public class Folio
    {
        public static long getNewFolio(long empresa_id, long? unegocio_id, string tipo_venta_sigla, VentasDB Db) {
            //ir a la empresa y  preguntar si la empresa tiene folio por emnpresa o unidad de negocio
            bool bFolioXUnidadNegocio = Empresa.empresaConDocsXUnidadDeNegocio(empresa_id, Db);
            if (!bFolioXUnidadNegocio)
            {
                unegocio_id = null;

            }
            long nFolio = 1;

            var lista = Db.Folio.Where(f =>
                f.empresa_id == empresa_id &&
                f.unegocio_id == unegocio_id
                && f.tipo_venta_sigla.Equals(tipo_venta_sigla)).ToList();

            if (lista == null || lista.Count==0) {
                TFolio t = new TFolio();
                t.folio = 2;
                t.empresa_id= empresa_id;
                t.tipo_venta_sigla = tipo_venta_sigla;
                t.unegocio_id = unegocio_id;

                Db.Folio.Add(t);
                Db.SaveChanges();
                nFolio = 1;
            }
            else
            {
                TFolio t = lista[0];
                nFolio = t.folio;
                t.folio = nFolio + 1;
                Db.SaveChanges();
            }
            return nFolio;
        }
    }
}
