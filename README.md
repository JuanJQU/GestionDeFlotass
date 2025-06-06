# 🚛 Proyecto: Gestión de Flotas de Transporte

# 📌 Propósito
Este proyecto tiene como objetivo desarrollar una aplicación de escritorio utilizando C# y Windows Forms que permita gestionar la operación de una flota de transporte, optimizando la administración de vehículos, conductores, viajes y mantenimientos. La aplicación interactúa con una base de datos MySQL para garantizar la integridad y persistencia de la información.

# 🧠 Objetivos de Aprendizaje
-Aplicar conocimientos adquiridos en el curso de Herramientas II.
-Implementar una arquitectura limpia utilizando patrones de diseño (Repository, Singleton).
-Aplicar principios SOLID para lograr un código estructurado, escalable y mantenible.
-Utilizar procedimientos almacenados para encapsular la lógica de negocio en la base de datos.
-Emplear buenas prácticas de programación y validación de datos en una aplicación real.

# 🛠️ Funcionalidades
-Autenticación de usuarios con roles diferenciados (Administrador y Conductor).
-CRUD de vehículos: creación, consulta, modificación y eliminación.
-CRUD de conductores: registro y administración de datos del personal.
-Programación de viajes, validando disponibilidad de vehículos y conductores.
-Registro y control de mantenimientos preventivos y correctivos.
-Actualización y consulta de kilometrajes recorridos.
-Generación de reportes de uso e historial de mantenimiento.
-Interfaz amigable y validaciones visuales para mejorar la experiencia del usuario.

# 🧱 Diagrama Entidad-Relación
![diagrama_entidad_relacion](https://github.com/user-attachments/assets/689cf313-d7c9-4cc9-8689-09afbf6f8b8c)

El sistema se basa en una base de datos relacional con seis tablas principales interconectadas: Vehículos, Conductores, Viajes, Mantenimientos, Kilometrajes y Usuarios. La relación entre estas tablas garantiza la integridad referencial y facilita la obtención de reportes complejos.

# 💻 Tecnologías Utilizadas
- C# con Windows Forms
- MySQL
- MySQL Connector/NET (MySql.Data)
- BCrypt.Net para cifrado de contraseñas
- Git y GitHub para control de versiones

# 🔐 Control de Acceso
-Administrador: Acceso completo a todas las funcionalidades del sistema.
-Conductor: Acceso limitado a consulta de viajes y mantenimiento relacionado.

# 🧪 Validaciones y Manejo de Errores
-Validación de datos obligatorios en formularios (matrícula, fechas, etc.)
-Verificación de disponibilidad de vehículos y conductores al asignar viajes.
-Control del estado del vehículo antes de permitir su uso.
-Try-catch en todas las operaciones críticas para evitar fallos inesperados.


# 🖼️ Capturas de Pantalla Iniciales
(Colocar imágenes en la carpeta /Docs/Capturas/ y referenciar aquí con markdown.)
-login.png: Pantalla de autenticación.
-vehiculos.png: Listado de vehículos.
-conductores.png: Registro de conductor.
-programar_viaje.png: Formulario de programación de viaje.

# 🚀 Instrucciones para Compilar y Ejecutar

# 1. Prerrequisitos
Tener instalado Visual Studio 2022+ con soporte para C# y Windows Forms.

# 1.2 
Tener MySQL Server instalado y ejecutándose.

# 2. Pasos
# 2.1 Clonar el repositorio
-bash
-Copiar
-Editar
-git clone https://github.com/tu_usuario/GestiondeFlotas.git
-cd GestiondeFlotas
-Crear la base de datos
-Ejecutar el script SQL database.sql ubicado en la carpeta Database/ desde un cliente MySQL como MySQL Workbench.

# 2.2 Abrir la solución en Visual Studio
-Ubica y abre el archivo GestionFlotas.sln
-Instalar dependencias necesarias vía NuGet
-MySql.Data
-BCrypt.Net-Next

# 2.3 Configurar la cadena de conexión
Editar el archivo App.config con los datos del servidor MySQL (host, usuario, contraseña).

# 2.4 Compilar y ejecutar el proyecto
Presionar F5 para iniciar la aplicación.

# 👥 Acerca De
# Desarrolladores: 
-Samuel Muriel Meneses
-Juan Jose Quiros Usma
-Yohan Felipe Suaza Cano
