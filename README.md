# README

## Contenido del Archivo

- Introducción
- Requerimientos
- Configuración del proyecto

## INTRODUCCION

Este ejercicio esta diseñado para probar su capacidad para crear un API RESTful para la gestión de transferencias entre cuentas bancarias.
Se le pedirá que cree "endpoints" que permitan a los usuarios agregar depósitos, crear retiros y transferir dinero entre cuentas.
También deberá implementar un proceso para calcular el interés diario sobre los saldos de las cuentas.

## Funcionalidades

- Cree un API RESTful que permita a los usuarios:
  - Obtener informacion de la cuenta bancaria, incluido el saldo actual, la lista de transacciones y los intereses devengados.
  - Agregar depositos a una cuenta.
  - Crear retiros desde una cuenta.
  - Transferir dinero entre cuentas.

## REQUERIMIENTOS

Para iniciar la aplicacion se necesita:

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (o la base de datos que se este utilizando)

## Configuración del proyecto

### Paso 1: Clonar el repositorio

Clona este repositorio en tu m�quina local usando el siguiente comando:

```sh
git clone https://github.com/dbolanos/BankAccount.git
cd tu-repositorio
```

### Paso 2: NuGets

#### Dependencias de NuGet

Este proyecto utiliza las siguientes dependencias de NuGet. Asegúrate de que están incluidas en tu archivo `.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>disable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="AutoMapper" Version="13.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.5">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>

</Project>
```

#### Restaurar paquetes NuGet

Ejecuta los siguiente pasos en Visual Studio:

- Restaurar paquetes NuGet
- Selecciona Administrador de paquetes NuGet.
- Luego, selecciona Consola del Administrador de paquetes NuGet.
- En la consola del Administrador de paquetes NuGet, escribe el siguiente comando y presiona Enter:

```sh
Restore-Package
```

## BASE DE DATOS

Dentro de la carpeta database, hay dos archivos SQL, crea una base de datos llamada: `BankAccountsAPI` y luego ejecuta el archivo de create-database.sql para crear las tablas y luego el de create-stored-procedure.sql para crear el procedimiento almacenado:

- create-database.sql: Para crear la base de datos.

- create-stored-procedure.sql: Para crear el procedimiento almacenado.

Ejecuta estos archivos en tu servidor SQL para configurar la base de datos inicial.

De igual manera podemos restablecer la base de datos ejecutando las migraciones con el comando:

```sh
Update-Database
```

Y haciendo uso de las migraciones se restablera las tablas, pero deberemos de correr el archivo `create-stored-procedure.sql` para agregar nuestro procedimiento almacenado.

## CONFIGURAR ARCHIVOS DE CONFIGURACION

Crea los archivos appsettings.json y appsettings.Development.json en la raíz del proyecto. Este paso es importante ya que aqui agregaremos nuestra cadena de connección a la base de datos. Debemos tener en cuenta nuestras credenciales para el funcionamiento correcto.

appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

appsettings.Development.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BankAccountsAPI;User Id=sa;Password=123456;TrustServerCertificate=True;"
  }
}
```

## EJECUTAR APLICACION

Una vez que hayas configurado todo lo anterior, puedes ejecutar la aplicación desde Visual Studio 2022 presionando F5 o seleccionando Iniciar en el menú de depuración.

¡Listo! Ahora deberías tener tu aplicación .NET 8 funcionando correctamente.
