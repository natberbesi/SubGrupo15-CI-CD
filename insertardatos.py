import mysql.connector
 
conexion = mysql.connector.connect(
    host="localhost",       
    user="poligrangrupo15",
    password="poli@/87**",
    database="ecommerce_db"
)
 
cursor = conexion.cursor()
 
query = """
INSERT INTO TBL_RPRODUCTO (PROD_CNOMBRE, PROD_CDESCRIPCION, PROD_NPRECIO, PROD_NSTOCK, PROD_CIMAGEN_URL) VALUES
(%s, %s, %s, %s, %s)
"""
 
productos = [
    ('TV Samsung 55"', 'TV Samsung 55" de alta calidad', 2099000, 15, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/tv-2565306_1280.jpg'),
    ('Portátil Lenovo Ryzen 7', 'Portátil Lenovo Ryzen 7 de alta calidad', 3499000, 10, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/portatil-lenovo-amd-ryzen-7-7730u-ssd-1tb-ram-40gb-led-14-pulgadas-full-hd.webp'),
    ('Smartphone Android', 'Smartphone Android de alta calidad', 899000, 30, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/phone-1138909_1280.jpg'),
    ('iPhone 13', 'iPhone 13 de alta calidad', 2999000, 20, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/iphones-7479304_1280.jpg'),
    ('iPhone 14 Pro 5G', 'iPhone 14 Pro 5G de alta calidad', 4799000, 12, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/iphone-14-pro-5g-256-gb-morado-reacondicionado.webp'),
    ('Drone Profesional 4K', 'Drone Profesional 4K de alta calidad', 1399000, 8, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/drone-profesional-camara-4k-wifi-5g-brushless-gps-lh-x75-pro.webp'),
    ('Consola PS5 Slim', 'Consola PS5 Slim de alta calidad', 2499000, 10, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/consola-ps5-slim-digital-1-tb-blanca.webp'),
    ('TV Hisense 58"', 'TV Hisense 58" de alta calidad', 2399000, 5, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Televisor-HISENSE-58-Pulgadas-LED-Uhd-4K-Smart-TV-58A6N-3537913_a.webp'),
    ('Portátil Lenovo Slim 3', 'Portátil Lenovo Slim 3 de alta calidad', 2699000, 14, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Portatil-LENOVO-IdeaPad-Slim-3-Intel-Core-i5-13420H-RAM-24-GB-512-GB-SSD-SIN-REF-3669525_a.webp'),
    ('ASUS TUF Gaming A15', 'ASUS TUF Gaming A15 de alta calidad', 4299000, 6, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Portatil-Gaming-ASUS-TUF-Gaming-A15-AMD-Ryzen-7-7435HS-RAM-8-GB-512-GB-SSD-FA506NFR-HN007W-3649571_a.webp'),
    ('ASUS Vivobook 15', 'ASUS Vivobook 15 de alta calidad', 3199000, 10, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Portatil-ASUS-Vivobook-15-Intel-Core-i5-12500H-RAM-8-GB-512-GB-SSD-X1502ZA-EJ2539W-3649568_a.webp'),
    ('Nevera MABE 394L', 'Nevera MABE 394L de alta calidad', 2490000, 7, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Nevera-MABE-No-Frost-394-Litros-Netos-RMP421FGCC-RMP421FGCC-Black-Steel-3559390_d.webp'),
    ('Lavadora Samsung 22kg', 'Lavadora Samsung 22kg de alta calidad', 2690000, 6, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Lavadora-SAMSUNG-Carga-Frontal-22-kg-48lb-WF22C6400APCO-3448182_a.webp'),
    ('Lavadora LG 22kg', 'Lavadora LG 22kg de alta calidad', 2790000, 5, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Lavadora-LG-Carga-Frontal-22-kg-48lb-WM22VV2S6BRASSECOL-3187062_a.webp'),
    ('Computador HP Ultra 5', 'Computador HP Ultra 5 de alta calidad', 3299000, 11, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Computador-HP-Intel-Core-Ultra-5-Ultra-125H-RAM-8-GB-512-GB-SSD-14-ep1001la-3568710_a.webp'),
    ('ASUS TUF F15', 'ASUS TUF F15 de alta calidad', 3899000, 9, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Computador-Gaming-ASUS-TUF-F15-Intel-Core-i5-12500H-RAM-8-GB-512-GB-SSD-FX507ZC4-HN056W-3488437_a.webp'),
    ('Xiaomi Redmi A3', 'Xiaomi Redmi A3 de alta calidad', 599000, 25, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Celular-XIAOMI-Redmi-A3-128-GB-128-GB-4-GB-RAM-Negro-3586411_a.webp'),
    ('Motorola G24', 'Motorola G24 de alta calidad', 649000, 22, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Celular-MOTOROLA-G24-256-GB-4-GB-RAM-Verde-3499945_a.webp'),
    ('Motorola Edge 50', 'Motorola Edge 50 de alta calidad', 1499000, 18, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/Celular-MOTOROLA-Edge-50-Fusion-256GB-256-GB-8-GB-RAM-Verde-3557515_a.webp'),
    ('All-In-One Lenovo DT A100', 'All-In-One Lenovo DT A100 de alta calidad', 1999000, 7, 'https://staticwebprofilecscg.blob.core.windows.net/imagesamason/All-In-One-LENOVO-DT-A100-Intel-N100-N100-RAM-8-GB-512-GB-SSD-3640117_a.webp')
]
 
cursor.executemany(query, productos)
conexion.commit()
 
print("Productos insertados correctamente.")
conexion.close()