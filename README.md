# Net-Salary-Test

Partiremos de que tenemos todo instalado y configurado para correr/ejecutar un proyecto con .Net Core 6 Entity Framework, además de crear o hacer la recuperación de la DB en SQL Server desde un .bak

1. Instalación de la Base de Datos usando el archivo .bak
	Desde SQL Server Management Studio (o desde el gestor de DB 
	de su preferencia) hacer la recuperación en la opción de 
	Restore Database y seleccionar el archivo salary_db.bak
  que se encuentra dentro del directorio DB
	
3. Abrir la solución del proyecto desde Visual Studio 
	
4. Cambiar las credenciales de conexión a la BD desde el código del proyecto
	Desde appsettings.json abrir el de la configuración de Desarrollo
	y buscar la cadena de conexión llamada AppDBSalary y colocar las credenciales 
	necesarias, siempre y cuando se respete el nombre de la DB
	
5. Ejecutar la solución para levantar el servidor (virtual local)
	Desde el menú de opciones del Visual Studio, seleccionar la opción
	de Depurar y seleccionar la opción de Iniciar sin depurar o presionar la siguiente
	combinación de teclas Ctrl + F5
	
6. Una vez ejecutado el proyecto, se abrirá el Sistema Web desde el
	navegador (revisar el puerto donde se esta ejecutando)
	Para poder entrar a una de las opciónes del menu superior 
	Departamentos, Asociados y Aumento Salarial deberas iniciar sesión
	colocando cualquier dato ya que solo se esta simulando una session 
	(ver Metodo Login en HomeController y ver PrivateBaseController 
	que es heredado en todas las opciones del menu ya comentadas)
	
