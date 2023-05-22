using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;

namespace api_ventas.Models.Routes
{
    public class RoutesMantenedores
    {
        public static void ActiveRoutes(RouteGroupBuilder app)
        {
            ActiveRoutesConsorcio(app);
            activeRoutesEmpresa(app);
            ActiveRouteUNegocio(app);
            ActivarRoutesPerfil(app);
        }

        private static void ActiveRoutesConsorcio(RouteGroupBuilder app)
        {
            app.MapGet("/consorcio", (VentasDB Db) =>
            {
                return Db.Consorcio.ToList()
                is List<TConsorcio> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/consorcio/{id:long}", (long? id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    if (id == null || Db.Consorcio == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = Db.Consorcio.FirstOrDefault(e => e.consorcio_id == id);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/consorcio", async (TConsorcio obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    Consorcio c = new(Db);
                    if (c.existCorsorcioXNombre(obj.nombre))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                                    "Consorcio", obj.nombre));

                    }

                    obj.fecha_creacion = DateTime.Now;
                    Db.Consorcio.Add(obj);
                    await Db.SaveChangesAsync();

                    r.respuesta = Db.Consorcio
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
            app.MapPut("/consorcio", (TConsorcio obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    Consorcio c = new(Db);
                    if (c.existCorsorcioXId(obj.consorcio_id))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "Consorcio", obj.nombre));
                    }

                    var nObj = (from e in Db.Consorcio
                                where e.consorcio_id == obj.consorcio_id
                                select e).FirstOrDefault();

                    if (nObj == null)
                    {
                        throw new Exception("No se encuentra el dato consultado en la base datos");
                    }

                    nObj.estado = obj.estado;
                    nObj.nombre = obj.nombre;
                    Db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/consorcio", (int id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in Db.Consorcio
                                  where e.consorcio_id == id
                                  select e;

                    Db.Consorcio.RemoveRange(cSelect);
                    Db.SaveChangesAsync();
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
        private static void activeRoutesEmpresa(RouteGroupBuilder app)
        {
            app.MapGet("/empresa", (VentasDB Db) =>
            {
                return Db.Empresa.ToList()
                is List<TEmpresa> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/empresa/{id:long}", (long? id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    if (id == null || Db.Empresa == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = Db.Empresa.FirstOrDefault(e => e.empresa_id == id);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/empresa", async (TEmpresa obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    Consorcio c = new(Db);
                    if (!c.existCorsorcioXId(obj.consorcio_id))
                    {
                        throw new Exception(String.Format("No existe un {0} con id '{1}' en la base de datos",
                            "consorcio", obj.consorcio_id));
                    }
                    Empresa e = new(Db);
                    if (e.existEmpresaXNombre(obj.nombre))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamada '{1}' en la base de datos",
                            "empresa", obj.nombre));
                    }

                    obj.fecha_creacion = DateTime.Now.ToUniversalTime();
                    Db.Empresa.Add(obj);
                    await Db.SaveChangesAsync();

                    r.respuesta = Db.Empresa
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
            app.MapPut("/empresa", (TEmpresa obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    int contar = (from e in Db.Empresa
                                  where e.nombre == obj.nombre && e.empresa_id != obj.empresa_id
                                  select e).Count();

                    if (contar > 0)
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "Empresa", obj.nombre));
                    }

                    var nObj = (from e in Db.Empresa
                                where e.consorcio_id == obj.consorcio_id
                                select e).FirstOrDefault();

                    if (nObj == null)
                    {
                        throw new Exception("No se encuentra el dato consultado en la base datos");
                    }

                    nObj.estado = obj.estado;
                    nObj.nombre = obj.nombre;
                    Db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/empresa", (int id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in Db.Empresa
                                  where e.empresa_id == id
                                  select e;

                    Db.Empresa.RemoveRange(cSelect);
                    Db.SaveChangesAsync();
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
        private static void ActiveRouteUNegocio(RouteGroupBuilder app)
        {
            app.MapGet("/unegocio", (VentasDB Db) =>
            {
                return Db.UNegocio.ToList()
                is List<TUNegocio> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/unegocio/{id:long}", (long? id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    if (id == null || Db.UNegocio == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = Db.Empresa.FirstOrDefault(e => e.empresa_id == id);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/unegocio", async (TUNegocio obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {


                    if (Unegocio.existUnegocioXNombre(obj.nombre, obj.empresa_id, Db))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "empresa", obj.nombre));
                    }

                    obj.fecha_creacion = DateTime.Now;
                    Db.UNegocio.Add(obj);
                    await Db.SaveChangesAsync();

                    r.respuesta = Db.UNegocio
                        .OrderByDescending(e => e.unegocio_id)
                        .First();

                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPut("/unegocio", (TUNegocio obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    int contar = (from e in Db.UNegocio
                                  where e.nombre == obj.nombre && e.unegocio_id != obj.unegocio_id
                                  select e).Count();

                    if (contar > 0)
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "unegocio", obj.nombre));
                    }

                    var nObj = (from e in Db.UNegocio
                                where e.unegocio_id == obj.unegocio_id
                                select e).FirstOrDefault();

                    if (nObj == null)
                    {
                        throw new Exception("No se encuentra el dato consultado en la base datos");
                    }

                    nObj.estado = obj.estado;
                    nObj.nombre = obj.nombre;
                    Db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/unegocio", (int id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in Db.UNegocio
                                  where e.unegocio_id == id
                                  select e;

                    Db.UNegocio.RemoveRange(cSelect);
                    Db.SaveChangesAsync();
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
        private static void ActivarRoutesPerfil(RouteGroupBuilder app)
        {
            app.MapGet("/perfil", (VentasDB Db) =>
            {
                return Db.Perfil.ToList()
                is List<TPerfil> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/perfil/{id:long}", (long? id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    if (id == null || Db.Perfil == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = Db.Perfil.FirstOrDefault(e => e.perfil_id == id);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/perfil", async (TPerfil obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    Perfil c = new();

                    if (c.existPerfilXNombre(obj.nombre, Db))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "perfil", obj.nombre));
                    }

                    Db.Perfil.Add(obj);
                    await Db.SaveChangesAsync();

                    r.respuesta = Db.Perfil
                        .OrderByDescending(e => e.perfil_id)
                        .First();

                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPut("/perfil", (TPerfil obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    int contar = (from e in Db.Perfil
                                  where e.nombre == obj.nombre && e.empresa_id != obj.empresa_id
                                  select e).Count();

                    if (contar > 0)
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "perfl", obj.nombre));
                    }

                    var nObj = (from e in Db.Perfil
                                where e.perfil_id == obj.perfil_id
                                select e).FirstOrDefault();

                    nObj.estado = obj.estado;
                    nObj.nombre = obj.nombre;
                    Db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/perfil", (int id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in Db.Perfil
                                  where e.perfil_id == id
                                  select e;

                    Db.Perfil.RemoveRange(cSelect);
                    Db.SaveChangesAsync();
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

    }
}
