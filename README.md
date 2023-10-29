# Demostrativo
Proyecto demostrativo para el BE. Se plantea crear varios micro servicios que contaran con los conocimientos acumulados y seran consumidos por diversos FE.

Win: Ejecutar con Visual Studio con docker-compose

Linux: Desde terminal
cd Demostrativo
sudo docker build -t demostrativoocelotwebapi:latest --file src/Microservices/Gateway/2.WebApi/Demostrativo.Ocelot.WebApi/Dockerfile .
sudo docker build -t demostrativohvwebapi:latest --file src/Microservices/HojaDeVida/Demostrativo.HV.WebApi/Dockerfile .
sudo docker build -t demostrativojwtwebapi:latest --file src/Microservices/Jwt/2.WebApi/Demostrativo.Jwt.WebApi/Dockerfile .

Migraciones
Win:
Comando para agregar migracion desde Consola vs: add-migration "init" -context PersistenceDbContext
Linux: Ubicarse en proyecto del contexto
Comando para agregar migracion desde Consola terminal: 
dotnet ef --startup-project ../../../2.WebApi/Demostrativo.Jwt.WebApi --verbose migrations add Initial --context PersistenceDbContext

La ejecucion de migracion a la DB se encuentra en program por lo cual no se hace uso de ese comando manualmente.