import mysql.connector
from mysql.connector import Error

try:
    # Conexión a la base de datos
    conexion = mysql.connector.connect(
        host='localhost',
        user='usuario',
        password='claveusuario',
        database='ecommerce_db'
    )

    if conexion.is_connected():
        print("✅ Conexión exitosa a la base de datos")

        cursor = conexion.cursor()

        # Crear tabla usuarios
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS usuarios (
            id INT AUTO_INCREMENT PRIMARY KEY,
            nombre VARCHAR(100),
            correo VARCHAR(100) UNIQUE,
            contraseña VARCHAR(255),
            creado_en DATETIME DEFAULT CURRENT_TIMESTAMP
        )
        """)

        # Crear tabla inventario
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS inventario (
            id INT AUTO_INCREMENT PRIMARY KEY,
            nombre VARCHAR(100),
            descripcion TEXT,
            precio DECIMAL(10, 2),
            cantidad INT
        )
        """)

        # Crear tabla ventas
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS ventas (
            id INT AUTO_INCREMENT PRIMARY KEY,
            usuario_id INT,
            fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
            total DECIMAL(10, 2),
            FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
        )
        """)

        # Crear tabla saldos
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS saldos (
            id INT AUTO_INCREMENT PRIMARY KEY,
            usuario_id INT,
            saldo DECIMAL(10, 2),
            actualizado DATETIME DEFAULT CURRENT_TIMESTAMP,
            FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
        )
        """)

        conexion.commit()
        print("🟢 Tablas creadas correctamente.")

except Error as e:
    print(f"❌ Error al conectar o crear tablas: {e}")

finally:
    if conexion.is_connected():
        cursor.close()
        conexion.close()
        print("🔒 Conexión cerrada.")