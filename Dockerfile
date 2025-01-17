# CONSTRUCCION POR CAPAS
# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Creamos un directorio
WORKDIR /app

# Copiar archivos locales
COPY . ./

# Compilamos
RUN dotnet publish -o out

# Imagen del runtime

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .
# Creamos un volumen
VOLUME ["/app/data"]
# Exponemos el puerto 8023
EXPOSE 8023
# Creamos la variable adecuada para la moneda
ENV LANG=es_ES.UTF-8
# Comando de ejecución
ENTRYPOINT ["dotnet", "cafeteria_cs.dll"]
