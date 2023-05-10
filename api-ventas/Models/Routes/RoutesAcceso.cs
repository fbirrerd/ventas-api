using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;
using NuGet.Protocol;

namespace api_ventas.Models.Routes
{
    public class RoutesAcceso
    {
        public static void ActiveRoutes(WebApplication app)
        {
            ActiveRoutesLogin(app);
        }

        public static void ActiveRoutesLogin(WebApplication app)
        {
            app.MapGet("/login", (VentasDB db) =>
            {
                return db.Login.ToList()
                is List<TLogin> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/login/{login}", (string login, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    if (login == null || db.Login == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = db.Login.FirstOrDefault(e => e.usuario == login);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/login", async (TLogin obj, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    Login c = new(db);

                    if (c.existLoginXLogin(obj.usuario))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "usuario", obj.usuario));
                    }

                    if (c.existLoginXCorreo(obj.email))
                    {
                        throw new Exception(String.Format("Ya existe un email {0} en la base de datos",
                            obj.email));
                    }


                    obj.fecha_creacion = DateTime.Now;
                    db.Login.Add(obj);
                    await db.SaveChangesAsync();

                    r.respuesta = db.Login
                        .OrderByDescending(e => e.fecha_creacion)
                        .First();

                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPut("/login", (TLogin obj, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    Login c = new(db);

                    if (c.existLoginXLogin(obj.usuario))
                    {
                        throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                            "usuario", obj.usuario));
                    }

                    if (c.existLoginXCorreo(obj.email))
                    {
                        throw new Exception(String.Format("Ya existe un email {0} en la base de datos",
                            "usuario", obj.usuario));
                    }
                    var nObj = (from e in db.Login
                                where e.usuario == obj.usuario
                                select e).FirstOrDefault();

                    if (nObj == null)
                    {
                        throw new Exception("No se encuentra el dato consultado en la base datos");
                    }
                    nObj.email = obj.email;
                    nObj.clave = obj.clave;
                    db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/login", (int id, VentasDB db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in db.Empresa
                                  where e.consorcio_id == id
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
    }
}
