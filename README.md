# BoomBang-Emulator-2022

Este proyecto forma parte de otro proyecto "BoomBang-Launcher-2022" que puedes encontrar en mi repositorio.
BoomBang Emulador emula los archivos Flash que se encuentran en proyecto BoomBang-Launcher-2022.
De esta manera puedes personalizar este juego y desarrollar nuevas funcionalidades.

BoomBang Game Launcher: https://github.com/GenRubio/boombang-2022-app

## Contenido

 - API connection
 - Web Socket connection (Handler System).
 - Flash Socket connection (Handler System).
 - Integración interna de .dll para desplegué en servidores Ubuntu.
 - Log system para visualizar las excepciones durante la ejecución.
 - Mailtrap

## Configuración
- En el archivo Config.cs cambiamos las variables de configuración


## Base de datos
- La base de datos utilizada en el proyecto es SQL
- El archivo con la última actualización está ubicado en la carpeta /database/backups

## Arquitectura
- MVC con la capa DAO

## Web Socket - C# System

Web Socket System es un sistema que he inventado para poder conectar mediante Sockets la página web con el emulador del juego y viceversa.

El sistema está formado por librería Socket.io y .NET Socket.
>
Flujo del sistema de los packetes:
>
Mensajes desde HTML al Emulador

```sh
Socket.io Client -> Node - Socket.io ->  C# - .NET Socket
```

Mensajes desde Emulador a HTML

```sh
C# - .NET Socket -> Node - .NET Socket  -> Socket.io Client
```

## Ubuntu Server
Instalamos Mono en nuestro Servidor de Ubuntu

```sh
sudo apt install mono-complete
```

Para ejecutar el emulador usaremos el siguente comando

```sh
sudo mono BoomBang.exe
```
