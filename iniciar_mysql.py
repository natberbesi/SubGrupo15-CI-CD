import os
import time

#Creando la red
os.system("docker network inspect mi_red > /dev/null 2>&1 || docker network create mi_red")

#levantando el contenedor de mysql

os.system("""
docker run -d \
  --name mysql_container \
  --network mi_red \
  -e MYSQL_ROOT_PASSWORD=poli2025grupo15 \
  -e MYSQL_DATABASE=ecommerce_db \
  -e MYSQL_USER=poligrangrupo15 \
  -e MYSQL_PASSWORD=poli@/87** \
  -p 3306:3306 \
  mysql:latest
""")

print("🟢 Contenedor MySQL iniciado. Esperando a que esté listo...")
time.sleep(20)
print(" Contenedor levantado y listo para conexiones.")

