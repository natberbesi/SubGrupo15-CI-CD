import mysql.connector
from mysql.connector import Error

try:
    # Conexión a la base de datos
    conexion = mysql.connector.connect(
        host='localhost',
        user='poligrangrupo15',
        password='poli@/87**',
        database='ecommerce_db'
    )

    if conexion.is_connected():
        print("✅ Conexión exitosa a la base de datos")

        cursor = conexion.cursor()

        # Crear tabla usuarios
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS TBL_RUSUARIO (
            USU_NID INT AUTO_INCREMENT PRIMARY KEY,
            USU_CNOMBRE VARCHAR(100),
            USU_CCORREO VARCHAR(100) UNIQUE,
            USU_CCONTRASEÑA VARCHAR(255),
            creado_en DATETIME DEFAULT CURRENT_TIMESTAMP
        )
        """)

        # Crear tabla inventario
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS TBL_RINVENTARIO (
            INV_NID INT AUTO_INCREMENT PRIMARY KEY,
            INV_CNOMBRE VARCHAR(100),
            INV_DESCRIPCION TEXT,
            INV_NPRECIO DECIMAL(10, 2),
            INV_NCANTIDAD INT
        )
        """)

        # Crear tabla ventas
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS TBL_RVENTA (
            VEN_NID INT AUTO_INCREMENT PRIMARY KEY,
            USU_NID INT,
            VEN_DFECHA DATETIME DEFAULT CURRENT_TIMESTAMP,
            VEN_NTOTAL DECIMAL(10, 2),
            FOREIGN KEY (USU_NID) REFERENCES TBL_RUSUARIO(USU_NID)
        )
        """)
        
        # Crear tabla carrito
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS TBL_CARRITO (
            CARR_NID INT AUTO_INCREMENT PRIMARY KEY,
            USU_NID INT NOT NULL,
            CARR_NSALDO DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
            CARR_DACTUALIZADO DATETIME DEFAULT CURRENT_TIMESTAMP,
            FOREIGN KEY (USU_NID) REFERENCES TBL_RUSUARIO(USU_NID)
        )
        """)
        # Crear tabla producto
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS TBL_RPRODUCTO (
            PROD_NID INT AUTO_INCREMENT PRIMARY KEY,
            PROD_CNOMBRE VARCHAR(100) NOT NULL,
            PROD_CDESCRIPCION TEXT,
            PROD_NPRECIO DECIMAL(10, 2) NOT NULL,
            PROD_NSTOCK INT NOT NULL DEFAULT 0,
            PROD_CIMAGEN_URL VARCHAR(1000) NOT NULL
        )
        """)
        
        # Crear tabla detalle_venta
        cursor.execute("""
        CREATE TABLE IF NOT EXISTS TBL_RDETALLE_VENTA (
            DET_VENTA_NID INT AUTO_INCREMENT PRIMARY KEY,
            VEN_NID INT NOT NULL,
            PROD_NID INT NOT NULL,
            DET_NCANTIDAD INT NOT NULL DEFAULT 1,
            DET_NPRECIO_UNITARIO DECIMAL(10, 2) NOT NULL,
            FOREIGN KEY (VEN_NID) REFERENCES TBL_RVENTA(VEN_NID),
            FOREIGN KEY (PROD_NID) REFERENCES TBL_RPRODUCTO(PROD_NID)
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
