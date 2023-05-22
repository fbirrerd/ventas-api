using api_ventas.Models.Business;
using api_ventas.Models.Data;
using api_ventas.Models.Objects;
using api_ventas.Models.Tables;

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
            app.MapGet("/login", (VentasDB Db) =>
            {
                return Db.Login.ToList()
                is List<TLogin> l
                ? Results.Ok(l)
                : Results.NotFound();
            });
            app.MapGet("/login/{login}", (string login, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    if (login == null || Db.Login == null)
                    {
                        throw new Exception("Datos no encontrados");
                    }
                    r.respuesta = Db.Login.FirstOrDefault(e => e.usuario == login);
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapPost("/login", async (TLogin obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    Login c = new(Db);

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
                    Db.Login.Add(obj);
                    await Db.SaveChangesAsync();

                    r.respuesta = Db.Login
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
            app.MapPut("/login", (TLogin obj, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    Login c = new(Db);

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
                    var nObj = (from e in Db.Login
                                where e.usuario == obj.usuario
                                select e).FirstOrDefault();

                    if (nObj == null)
                    {
                        throw new Exception("No se encuentra el dato consultado en la base datos");
                    }
                    nObj.email = obj.email;
                    nObj.clave = obj.clave;
                    Db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    r.resultado = false;
                    r.Error = new Errores(ex.Message);
                }
                return Results.Ok(r);
            });
            app.MapDelete("/login", (int id, VentasDB Db) =>
            {
                Respuesta r = new();
                try
                {
                    var cSelect = from e in Db.Empresa
                                  where e.consorcio_id == id
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
    }
}
