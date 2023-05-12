﻿using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace api_ventas.Models.Business
{
    public class Stock
    {

        private static TStock getStock(
            long producto_id,
            long empresa_id,
            long unegocio_id,
            VentasDB Db) {
            TStock? t = Db.Stock.Where(
                e => e.producto_id == producto_id &&
                e.empresa_id ==  empresa_id &&
                e.unegocio_id == unegocio_id).FirstOrDefault();
            return t;        
        }
        private static TTipoMedida getUnidadMedida(
            long unidad_medida_id,
            VentasDB Db)
        {
            TTipoMedida? t = Db.TipoMedida.Where(
                e => e.tipo_medida_id == unidad_medida_id).FirstOrDefault();
            return t;
        }
        public static bool GenerarMovimientoBodega(iMovimientoStock oMov, VentasDB Db)
        {
            try {
                if (oMov == null)
                {
                    throw new Exception("Datos enviados incompletos");
                }
                var t = getStock(
                    oMov.producto_id,
                    oMov.empresa_id,
                    oMov.unegocio_id, 
                    Db);
                if (t == null)
                {
                    t = new TStock
                    {
                        stock_id = 0,
                        empresa_id = oMov.empresa_id,
                        producto_id = oMov.producto_id,
                        unegocio_id = oMov.unegocio_id,
                        cant_disponible = oMov.cantidad,
                        cant_reserva = 0,
                        cant_merma = 0,
                        cant_historico = oMov.cantidad
                        
                    };
                    Db.Stock.Add(t);
                }
                else 
                {
                    if (oMov.tipo_movimiento.Equals("S"))
                    {
                        if (oMov.cantidad > t.cant_disponible) {
                            throw new Exception("No existe stock disponible");
                        }
                        t.cant_disponible -= oMov.cantidad;
                    }
                    if (oMov.tipo_movimiento.Equals("I")) {
                        t.cant_disponible += oMov.cantidad;
                        t.cant_historico +=  oMov.cantidad;
                    }
                }
                TStockMovimiento sm = new TStockMovimiento();
                sm.cantidad = oMov.cantidad;
                sm.producto_id = oMov.producto_id;
                sm.usuario = oMov.usuario;
                sm.unegocio_id = oMov.unegocio_id;
                sm.creation_date = DateTime.Now.ToUniversalTime();
                sm.empresa_id = oMov.empresa_id;
                sm.tipo_movimiento = oMov.tipo_movimiento;

                Db.StockMovimiento.Add(sm);
                var r = Db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
                            
            }
        }

        public static oProducto getDatosProducto(
            iProducto prod,
            VentasDB Db)
        {

            oProducto oProducto = new oProducto();

            TProducto oTProd = getProductoActual(
                prod.codigoProducto, 
                prod.empresa_id, 
                Db);

            if (oTProd != null) {
                oProducto = new oProducto(oTProd.producto_id, oTProd.codigo, oTProd.nombre1);
                //se agrega el stock
                var t = getStock(
                    oTProd.producto_id,
                    empresa_id: prod.empresa_id,
                    prod.unegocio_id, 
                    Db);
                if (t != null) {
                    oProducto.cantidad = t.cant_disponible;
                }
                //se agrega la unidad de medida
                Medida med = new Medida();

                med.valor = oTProd.valor_medida;
                oProducto.medida = med;
            }
            if (oProducto == null) {
                throw new Exception("No se ha podido traer la informacion del producto solicitado");
            }
            else {
                return oProducto;
            }
        }

        private static TProducto getProductoActual(
            string codigoProducto, 
            long empresa_id, 
            VentasDB Db) {
            var lista = Db.Producto.Where(l => l.codigo.Equals(codigoProducto)   
            && (l.empresa_id== null || l.empresa_id == empresa_id)).OrderBy(p=> p.empresa_id).ToList();
            if (lista.Count() == 0)
            {
                throw new Exception("Producto no existe");
            }
            else 
            {
                return lista[0];
            }
        }


        public static bool existStockDisponible(
            long empresa_id, 
            string codigoProducto, 
            decimal CantidadConsultada, 
            VentasDB Db) {

            var item = Db.Producto.Where(s => s.codigo.Equals(codigoProducto)).FirstOrDefault();
            if (item != null)
            {
                return existStockDisponible(empresa_id, item.producto_id,CantidadConsultada, Db);
            }
            return false;
        }
        public static bool existStockDisponible(
            long empresa_id, 
            long producto_id,
            decimal CantidadConsultada, 
            VentasDB Db)
        {
            var item = Db.Stock.Where(s => s.empresa_id == empresa_id && s.producto_id == producto_id).FirstOrDefault();
            if (item != null) {
                if (item.cant_disponible > CantidadConsultada)
                {
                    return true;        
                }
            }
            return false;
        }

    }
}
