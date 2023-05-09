using api_ventas.Models.Business;
using api_ventas.Models.Tables;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.KeyPerFile;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using api_ventas.Models.Objects;
using api_ventas.Models.Data;


#region " configurción inicial "
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cnnString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<VentasDB>(options => options.UseNpgsql(cnnString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

#region " consorcios "


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
        Consorcio c = new Consorcio(db);
        if (c.existCorsorcioXNombre(obj.nombre)) {
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
        Consorcio c = new Consorcio(db);
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
#endregion

#region " empresa "
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
        Consorcio c = new Consorcio(db);
        if (! c.existCorsorcioXId(obj.consorcio_id))
        {
            throw new Exception(String.Format("No existe un {0} con id '{1}' en la base de datos",
                "consorcio", obj.consorcio_id));
        }
        Empresa e = new Empresa(db);
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
#endregion

#region " unegocio "
app.MapGet("/unegocio", (VentasDB db) =>
{
    return db.UNegocio.ToList()
    is List<UNEGOCIO> l
    ? Results.Ok(l)
    : Results.NotFound();
});

app.MapGet("/unegocio/{id:long}", (long? id, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        if (id == null || db.UNegocio == null)
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
app.MapPost("/unegocio", async (UNEGOCIO obj, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        Unegocio u = new Unegocio(db);

        if (u.existUnegocioXNombre(obj.nombre,obj.empresa_id))
        {
            throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                "empresa", obj.nombre));
        }

        obj.fecha_creacion = DateTime.Now;
        db.UNegocio.Add(obj);
        await db.SaveChangesAsync();

        r.respuesta = db.UNegocio
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
app.MapPut("/unegocio", (UNEGOCIO obj, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        int contar = (from e in db.UNegocio
                      where e.nombre == obj.nombre && e.unegocio_id != obj.unegocio_id
                      select e).Count();

        if (contar > 0)
        {
            throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                "unegocio", obj.nombre));
        }

        var nObj = (from e in db.UNegocio
                    where e.unegocio_id == obj.unegocio_id
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
app.MapDelete("/unegocio", (int id, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        var cSelect = from e in db.UNegocio
                      where e.unegocio_id == id
                      select e;

        db.UNegocio.RemoveRange(cSelect);
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
#endregion

#region " perfil "
app.MapGet("/perfil", (VentasDB db) =>
{
    return db.Perfil.ToList()
    is List<PERFIL> l
    ? Results.Ok(l)
    : Results.NotFound();
});

app.MapGet("/perfil/{id:long}", (long? id, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        if (id == null || db.Perfil == null)
        {
            throw new Exception("Datos no encontrados");
        }
        r.respuesta = db.Perfil.FirstOrDefault(e => e.perfil_id == id);
    }
    catch (Exception ex)
    {
        r.resultado = false;
        r.Error = new Errores(ex.Message);
    }
    return Results.Ok(r);
});
app.MapPost("/perfil", async (PERFIL obj, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        Perfil c = new Perfil(db);

        if (c.existPerfilXNombre(obj.nombre))
        {
            throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                "perfil", obj.nombre));
        }

        db.Perfil.Add(obj);
        await db.SaveChangesAsync();

        r.respuesta = db.Perfil
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
app.MapPut("/perfil", (PERFIL obj, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        int contar = (from e in db.Perfil
                      where e.nombre == obj.nombre && e.empresa_id != obj.empresa_id
                      select e).Count();

        if (contar > 0)
        {
            throw new Exception(String.Format("Ya existe un {0} llamado '{1}' en la base de datos",
                "perfl", obj.nombre));
        }

        var nObj = (from e in db.Perfil
                    where e.perfil_id == obj.perfil_id
                    select e).FirstOrDefault();

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
app.MapDelete("/perfil", (int id, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        var cSelect = from e in db.Perfil
                      where e.perfil_id == id
                      select e;

        db.Perfil.RemoveRange(cSelect);
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
#endregion

#region " login "
app.MapGet("/login", (VentasDB db) =>
{
    return db.Login.ToList()
    is List<LOGIN> l
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
        r.respuesta = db.Login.FirstOrDefault(e => e.usuario== login);
    }
    catch (Exception ex)
    {
        r.resultado = false;
        r.Error = new Errores(ex.Message);
    }
    return Results.Ok(r);
});
app.MapPost("/login", async (LOGIN obj, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        Login c = new Login(db);

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
app.MapPut("/login", (LOGIN obj, VentasDB db) =>
{
    Respuesta r = new();
    try
    {
        Login c = new Login(db);

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
#endregion



app.Run();