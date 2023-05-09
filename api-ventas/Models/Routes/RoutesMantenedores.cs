using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api_ventas.Models.Routes
{
    public class RoutesMantenedores
    {
        public static void ActiveRoutes(WebApplication app) { 
            ActiveRoutesConsorcio(app);
        }

        private static void ActiveRoutesConsorcio(WebApplication app)
        {
            app.MapGet("/consorcio", (VentasDB db) =>
            {
                return db.Consorcio.ToList()
                is List<CONSORCIO> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/consorcio/{id:long}", (long? id, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    if (id == null || db.Consorcio == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = db.Consorcio.FirstOrDefault(e => e.consorcio_id == id);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/consorcio", async (CONSORCIO obj, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    Consorcio c = new(db);
                    if (c.existCorsorcioXNombre(obj.nombre))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                                    "Consorcio", obj.nombre));

                    }

                    obj.fecha_creacion = DateTime.Now;
                    db.Consorcio.Add(obj);
                    await db.SaveChangesAsync();

                    r.respuesta = db.Consorcio
                        .OrderByDescending(e => e.consorcio_id)
                        .First();

                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPut("/consorcio", (CONSORCIO obj, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    Consorcio c = new(db);
                    if (c.existCorsorcioXId(obj.consorcio_id))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "Consorcio", obj.nombre));
                    }

                    var nObj = (from e in db.Consorcio
                                where e.consorcio_id == obj.consorcio_id
                                select e).FirstOrDefault();

                    if (nObj == null)
                    {
                        throw new Exception("No se encuentra el dato consultado en la base datos");
                    }

                    nObj.estado = obj.estado;
                    nObj.nombre = obj.nombre;
                    db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/consorcio", (int id, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in db.Consorcio
                                  where e.consorcio_id == id
                                  select e;

                    db.Consorcio.RemoveRange(cSelect);
                    db.SaveChangesAsync();
                    r.respuesta = "OK";
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
        }
        private static void activeRoutesEmpresa(WebApplication app)
        {
            app.MapGet("/empresa", (VentasDB db) =>
            {
                return db.Empresa.ToList()
                is List<EMPRESA> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/empresa/{id:long}", (long? id, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    if (id == null || db.Empresa == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = db.Empresa.FirstOrDefault(e => e.empresa_id == id);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/empresa", async (EMPRESA obj, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    Consorcio c = new(db);
                    if (!c.existCorsorcioXId(obj.consorcio_id))
                    {
                        throw new Exception(String.Format("No existe un {0} con id '{1}' en la base de datos",
                            "consorcio", obj.consorcio_id));
                    }
                    Empresa e = new(db);
                    if (e.existEmpresaXNombre(obj.nombre))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamada '{1}' en la base de datos",
                            "empresa", obj.nombre));
                    }

                    obj.fecha_creacion = DateTime.Now.ToUniversalTime();
                    db.Empresa.Add(obj);
                    await db.SaveChangesAsync();

                    r.respuesta = db.Empresa
                        .OrderByDescending(e => e.empresa_id)
                        .First();

                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPut("/empresa", (EMPRESA obj, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    int contar = (from e in db.Empresa
                                  where e.nombre == obj.nombre && e.empresa_id != obj.empresa_id
                                  select e).Count();

                    if (contar > 0)
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "Empresa", obj.nombre));
                    }

                    var nObj = (from e in db.Empresa
                                where e.consorcio_id == obj.consorcio_id
                                select e).FirstOrDefault();

                    if (nObj == null)
                    {
                        throw new Exception("No se encuentra el dato consultado en la base datos");
                    }

                    nObj.estado = obj.estado;
                    nObj.nombre = obj.nombre;
                    db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/empresa", (int id, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in db.Empresa
                                  where e.empresa_id == id
                                  select e;

                    db.Empresa.RemoveRange(cSelect);
                    db.SaveChangesAsync();
                    r.respuesta = "OK";
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
        }
        private static void ActivarRoutesPerfil(WebApplication app)
        {

        }

    }
}
