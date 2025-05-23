import mysql.connector

def conectar():
    try:
        conexion = mysql.connector.connect(
            host='localhost',
            user='usuario',
            password='claveusuario',
            database='ecommerce_db'
        )
        return conexion
    except mysql.connector.Error as err:
        print(f"❌ Error al conectar: {err}")
        return None

def probar():
    conexion = conectar()
    if conexion:
        cursor = conexion.cursor()
        cursor.execute("SHOW TABLES;")
        tablas = cursor.fetchall()
        print("Tablas en la base de datos:")
        for tabla in tablas:
            print(tabla[0])
        conexion.close()
    else:
        print("No se pudo conectar a la base de datos.")

if __name__ == "__main__":
    probar()
