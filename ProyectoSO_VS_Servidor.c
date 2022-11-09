#include <stdio.h>
#include <mysql.h>
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
//#include <my_global.h> 


int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9060);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	MYSQL *conn;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "BaseJuego", 0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar laconexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		int terminar =0;
		// Entramos en un bucle para atender todas las peticiones de este cliente
		//hasta que se desconecte
		while (terminar ==0)
		{
			// Ahora recibimos la petici?n
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			
			printf ("Peticion: %s\n",peticion);
			
			// vamos a ver que quieren
		
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el c?digo de la petici?n
			char nombre[20];
			char contra[20];
			char jugador[20];
			int partida;
			
/*			if (codigo != 0)*/
/*			{*/
/*				p = strtok( NULL, "/");*/
/*				strcpy (nombre, p);*/

/*				p = strtok( NULL, "/");*/
/*				strcpy (contra, p);*/
/*				p = strtok( NULL, "/");*/
/*				strcpy (jugador, p);*/

/*				p = strtok( NULL, "/");*/
/*				partida = atoi (p);*/

/*				printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);*/
/*			}*/
			
			if (codigo == 0) //petici?n de desconexi?n
				terminar == 1;
			else if (codigo == 1) //mirar si el usuario y contraseña son correctos
			{	
				//intento de pueba 

				p = strtok( NULL, "/");
				strcpy (nombre, p);
				
				p = strtok( NULL, "/");
				strcpy (contra, p);
				//*
				//codigo de pruebas
				printf ("Codigo: %d, Nombre: %s\n", codigo, nombre);
				
				
				char consulta [150];
				
				strcpy (consulta,"SELECT Usuarios.Contra FROM (Usuarios) WHERE Usuarios.Nombre = '");
				strcat (consulta, nombre);
				strcat (consulta,"'");
	
				err=mysql_query (conn, consulta);

				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				resultado = mysql_store_result (conn);
					
				row = mysql_fetch_row(resultado);
					
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				else
				{
					printf ("ROW: %s\n", row[0]);
					if (strcmp(row[0],contra)==0)
						strcpy(respuesta, "Correcto");
								
					else
						strcpy(respuesta, "Incorrecto");
				}
			}
			
			else if (codigo ==2)
			{	
				p = strtok( NULL, "/");
				strcpy (jugador, p);

				p = strtok( NULL, "/");
				partida = atoi (p);
				
				char consulta [150];
				strcpy (consulta,"SELECT Usuarios.nombre FROM Usuarios,Registro_de_partidas WHERE Registro_de_partidas.idP= '");
				strcat (consulta, p);
				strcat (consulta,"' AND Registro_de_partidas.idU = Usuarios.id");
				
				printf ("Partida: %d\n", partida);
	
				err=mysql_query (conn, consulta);

				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				int result = 0;
				while (row !=NULL)
				{
					if (strcmp( row[0], jugador) == 0)
						result=1;
					row = mysql_fetch_row (resultado);
				}
				if (result == 1)
					sprintf (respuesta, "SI");
				else
					sprintf (respuesta, "NO");

			}
			else if (codigo ==3)
			{
				p = strtok( NULL, "/");
				partida = atoi (p);
				
				char consulta [150];
				strcpy (consulta,"SELECT Partidas.Duracion FROM Partidas WHERE Partidas.id= '");
				strcat (consulta, p);
				strcat (consulta,"'");
	
				err=mysql_query (conn, consulta);

				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				if (row[0]>10)
					sprintf (respuesta, "SI");
				else 
					sprintf (respuesta, "NO");
			}

			else
			{
				p = strtok( NULL, "/");
				strcpy (jugador, p);
				
				char consulta [150];
				strcpy (consulta,"SELECT Usuarios.Estado FROM Usuarios WHERE Usuarios.Nombre= '");
				strcat (consulta, jugador);
				strcat (consulta,"'");
	
				err=mysql_query (conn, consulta);

				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				if (strcmp( row[0], "Online") == 0)
					sprintf (respuesta, "SI");
				else 
					sprintf (respuesta, "NO");
			}	
			if (codigo !=0)
			{
				
				printf ("Respuesta: %s\n", respuesta);
				// Enviamos respuesta
				write (sock_conn,respuesta, strlen(respuesta));
			}
		}
		// Se acabo el servicio para este cliente
		close(sock_conn); 
	}
}
