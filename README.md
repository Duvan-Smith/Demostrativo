# Demostrativo

Este repositorio contiene un proyecto demostrativo para el backend (BE). El objetivo es crear varios microservicios que aprovechen los conocimientos acumulados y sean consumidos por diversos frontends (FE).

## Ejecución

### Windows

- Ejecutar con Visual Studio con docker-compose o seleccionar la ejecución de múltiples proyectos. Para este último caso, asegúrate de contar con PostgreSQL y de que los proyectos se ejecuten en HTTPS.

### Linux

Desde la terminal:

```bash
cd Demostrativo
sudo docker build -t demostrativoocelotwebapi:latest --file src/Microservices/Gateway/2.WebApi/Demostrativo.Ocelot.WebApi/Dockerfile .
sudo docker build -t demostrativohvwebapi:latest --file src/Microservices/HojaDeVida/Demostrativo.HV.WebApi/Dockerfile .
sudo docker build -t demostrativojwtwebapi:latest --file src/Microservices/Jwt/2.WebApi/Demostrativo.Jwt.WebApi/Dockerfile .

cd src/DockerCompose
sudo docker compose up
```

En caso de que fallen las imágenes de pgAdmin o PostgreSQL por fallas en los volúmenes, otorga permisos a estos:

```bash
sudo chmod 777 -R ./postgres-volumen
sudo chmod 777 -R ./pgadmin-volumen
```

## Migraciones

### Windows

Comando para agregar migración desde Consola de Visual Studio:

```bash
add-migration "init" -context PersistenceDbContext
```

## Linux

Ubícate en el proyecto del contexto y ejecuta el siguiente comando en la terminal:

```bash
dotnet ef --startup-project ../../../2.WebApi/Demostrativo.Jwt.WebApi --verbose migrations add Initial --context PersistenceDbContext
```
Es importante tener en cuenta que la ejecución de migraciones a la base de datos se realiza en el programa, por lo tanto, no se hace uso de ese comando manualmente.
