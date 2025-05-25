
# Proyecto Ecommerce - Configuración Base de Datos con Docker y Python

## Descripción

Este proyecto contiene la configuración de la base de datos MySQL para un ecommerce utilizando Docker y Python. Aquí se explica cómo levantar el contenedor MySQL, crear las tablas necesarias y conectar la aplicación con la base de datos.

---

## Requisitos previos

- Docker instalado y corriendo
- Python 3 instalado
- Recomendado usar entorno virtual de Python (`venv` o `virtualenv`)
- Paquete `mysql-connector-python` instalado (`pip install mysql-connector-python`)

---

## 1. Crear y levantar el contenedor MySQL

Ejecuta el siguiente comando para levantar un contenedor MySQL con las variables de entorno para usuario, contraseña y base de datos:

```bash
docker run --name mysql-ecommerce \
 -e MYSQL_ROOT_PASSWORD=poli2025grupo15 \
 -e MYSQL_DATABASE=ecommerce_db \
 -e MYSQL_USER=poligrangrupo15 \
 -e MYSQL_PASSWORD=poli@/87** \
 -p 3306:3306 \
 -d mysql:latest
````

**Importante:** Cambia las contraseñas o nombres si quieres, pero deben coincidir con la configuración en tu aplicación.

Para verificar que el contenedor esté corriendo:

```bash
docker ps
```

---

## 2. Crear las tablas en la base de datos

Una vez que el contenedor esté activo, ejecuta el script Python `crear_tablas.py` para crear las tablas necesarias:

```bash
python3 crear_tablas.py
```

Este script creará las siguientes tablas:

* `usuarios`
* `ventas`
* `inventario`
* `saldos`

---

## 3. Código de conexión (ejemplo en Python)

Usa los siguientes datos para la conexión a la base de datos en tu aplicación Python:

```python
import mysql.connector

conexion = mysql.connector.connect(
    host='localhost',
    user='poligrangrupo15',
    password='poli@/87**',
    database='ecommerce_db',
    port=3306
)
```

---

## 4. Variables de conexión recomendadas para la app

| Parámetro     | Valor         |
| ------------- | ------------- |
| Host          | localhost     |
| Usuario       | usuario       |
| Contraseña    | claveusuario  |
| Base de datos | ecommerce\_db |
| Puerto        | 3306          |

---

## 5. Comandos útiles para manejar Docker

* **Listar contenedores activos**

  ```bash
  docker ps
  ```

* **Detener el contenedor**

  ```bash
  docker stop mysql-ecommerce
  ```

* **Iniciar el contenedor**

  ```bash
  docker start mysql-ecommerce
  ```

## 6. Uso de entorno virtual para Python

Es recomendable usar un entorno virtual para evitar conflictos en las dependencias. Para crear y activar uno:

```bash
python3 -m venv venv
source venv/bin/activate     # Linux/macOS
venv\Scripts\activate.bat    # Windows
```

Luego instala las dependencias:

```bash
pip install mysql-connector-python
```

---

## 7. Archivo `.env.example`

Puedes crear un archivo `.env` para guardar tus credenciales sin subirlas al repositorio. Un ejemplo:

```env
DB_HOST=localhost
DB_USER=poligrangrupo15
DB_PASSWORD=poli@/87**
DB_NAME=ecommerce_db
DB_PORT=3306
```

---

## 8. Problemas comunes y soluciones

* **No se conecta al contenedor MySQL**

  * Asegúrate que Docker esté corriendo.
  * Verifica que el contenedor esté activo (`docker ps`).
  * Revisa los logs del contenedor:

    ```bash
    docker logs mysql-ecommerce
    ```

* **Error de conexión en Python**

  * Revisa que las credenciales coincidan con las definidas en el contenedor.
  * Confirma que el puerto 3306 esté mapeado correctamente.
  * Verifica que el contenedor haya terminado de iniciar (puede tardar unos segundos).

* **Error instalando paquetes en Mac**

  * Usa un entorno virtual para evitar problemas con permisos:

    ```bash
    python3 -m venv venv
    source venv/bin/activate
    pip install mysql-connector-python
    ```

---

## 9. Resumen de pasos para los compañeros del equipo

1. Clonar el repositorio.
2. Crear y activar entorno virtual (opcional pero recomendado).
3. Instalar las dependencias (`pip install -r requirements.txt` si existe o `pip install mysql-connector-python`).
4. Levantar el contenedor MySQL con el comando de Docker dado en la sección 1.
5. Ejecutar el script `crear_tablas.py` para crear las tablas.
6. Configurar la aplicación con los datos de conexión del contenedor.
7. Correr la aplicación normalmente.

---

Si tienes dudas o problemas, contacta a \[tu nombre o contacto].

---

**¡Listo para comenzar a desarrollar el ecommerce con la base de datos levantada!**

```

---

¿Quieres que también te prepare el script `crear_tablas.py` o el archivo `requirements.txt` para que esté listo para subir al repo?
```
