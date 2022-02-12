# BoomBang-Emulator-2022

Este proyecto forma parte de otro proyecto "BoomBang-Launcher-2022" que puedes encontrar en mi repositorio.
BoomBang Emulador emula los archivos Flash que se encuentran en proyecto BoomBang-Launcher-2022.
De esta manera puedes personalizar este juego y desarrollar nuevas funcionalidades.

## Base de datos
- La base de datos utilizada en el proyecto es SQL
- El archivo con la última actualización está ubicado en la carpeta /database/backups

## Configuración
- En el archivo Config.cs cambiamos las variables de configuración

## Arquitectura
- MVC con la capa DAO

## Contenido

 - API connection
 - Web Socket connection (Handler System).
 - Flash Socket connection (Handler System).
 - Integración interna de .dll para desplegué en servidores Ubuntu.
 - Log system para visualizar las excepciones durante la ejecución.
 - Mailtrap
