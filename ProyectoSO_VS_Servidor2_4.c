#include <stdio.h>
#include <mysql.h>
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <my_global.h> 

// Clases --------------------------------
typedef struct {
	char nombre[20];
	int socket;
} Usuario;

typedef struct {
	Usuario usuarios[50];
	int num;
} ListaUsuarios;

typedef struct {
	int socket[4];
	int jugadores;
} Partidas;

typedef struct {
	Partidas Partida[50];
	int num;
} ListaPartidas;


// Variables Globals ---------------------
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
ListaUsuarios listaU;
ListaPartidas listaP;
int i;
int sockets[100];
Partidas partidas [100];

// FUNCIONS ------------------------------
void EscribirLista(ListaUsuarios *LU,char res[100])
{
	for (int j=0;j<LU->num;j++)
	{
		sprintf(res,"%s%s/", res,LU->usuarios[j].nombre);
	}
	res[strlen(res)-1]=' ';
	printf("Vector conectados: %s\n",res);
}

void AddElemento(ListaUsuarios *LU, char nom[20], int socket)
{
	Usuario u;
	strcpy(u.nombre,nom);
	u.socket=socket;
	LU->usuarios[LU->num] = u;
	LU->num =LU->num+1;
	printf("Elemento a�adido: %s, num: %d,sock: %d\n", LU->usuarios[LU->num-1].nombre, LU->num, LU->usuarios[LU->num-1].socket);
}

int AddElementoLista(ListaPartidas *LP, int sockethost, int socket)
{
	int partida;
	for (int j=0;j<LP->num;j++)
	{
		if (LP->Partida[j].socket[0]=sockethost)
		{
			LP->Partida[j].socket[LP->Partida[j].jugadores]=socket;
			LP->Partida[j].jugadores=LP->Partida[j].jugadores+1;
			partida=j;
		}
	}
	return partida;
	printf("partida: %d\n", partida);
}

void EliminarElemento(ListaUsuarios *LU, char nom[20])
{
	int found = 0;
	for (int j=0; j<LU->num; j++)
	{
		if (strcmp(LU->usuarios[j].nombre,nom) == 0)
		{
			found = 1;			
		}
		if (found = 1)
		{
			LU->usuarios[j] = LU->usuarios[j+1];
		}
	}
	LU->num = LU->num-1;
}

void *AtenderCliente (void *socket)
// Abre connexión con MySQL y devuelve la Respuesta de la consulta
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	int socketdestino;
	int PartidasJ[100];
	//int socket_conn = * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
	int ret;
	
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
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T3_BaseJuego", 0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar laconexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	
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
		char nombredestino[20];
		char contra[20];
		char jugador[20];
		int partida;
		
		if (codigo == 0) //petici?n de desconexi?n			
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			pthread_mutex_lock(&mutex);

			EliminarElemento(&listaU,nombre);
			
			pthread_mutex_unlock(&mutex);
			char prob[100];
			prob[0]='\0';
			EscribirLista(&listaU,prob);
			terminar = 1;
		}	
		else if (codigo == 1) //mirar si el usuario y contraseña son correctos
		{	
			//intento de pueba 
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			
			p = strtok( NULL, "/");
			strcpy (contra, p);
			
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
				if (strcmp(row[0],contra)==0)
				{
					strcpy(respuesta, "Correcto");
					pthread_mutex_lock(&mutex);
					AddElemento(&listaU,nombre,sock_conn);
					char frase[100];
					EscribirLista(&listaU,frase);
					pthread_mutex_unlock(&mutex);
				}
				else
				{
					strcpy(respuesta, "Incorrecto");
				}			
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
				sprintf (respuesta, "2/SI");
			else
				sprintf (respuesta, "2/NO");
			
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
			if (row == NULL)
			{
				sprintf (respuesta, "3/NF");
			}
			else
			{
				if (row[0]>10)
					sprintf (respuesta, "3/SI");
				else 
					sprintf (respuesta, "3/NO");
			}
		}
		
		else if (codigo ==4)
		{
			p = strtok( NULL, "/");
			strcpy (jugador, p);
			
			p = strtok( NULL, "/");
			partida = atoi (p);
			
			char consulta [150];
			strcpy (consulta,"SELECT Registro_de_partidas.idP,Registro_de_partidas.Resultado_Final FROM Registro_de_partidas,Usuarios WHERE Registro_de_partidas.idU=Usuarios.id AND Usuarios.nombre='");
			strcat (consulta, jugador);
			strcat (consulta,"'");
			
			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			int fnd = 0;
			int winner =0;
			while (row !=NULL)
			{
				int pa = atoi(row[0]);
				if (pa == partida)
				{
					if (strcmp(row[1],"Ganador") == 0)
					{
						winner = 1;
					}
					fnd = 1;
				}
				row = mysql_fetch_row (resultado);
			}
			if (fnd == 0)
			{
				sprintf (respuesta, "4/NF");
			}
			else
			{
				if (winner == 1)
				{
					sprintf (respuesta, "4/SI");
				}
				else
				{
					sprintf (respuesta, "4/NO");
				}
			}
		}
		else if (codigo==5)
		{
			p = strtok( NULL, "/");
			strcpy (nombredestino, p);
			
			listaP.Partida[listaP.num].socket[0]=sock_conn;
			listaP.Partida[listaP.num].jugadores=1;
			PartidasJ[sizeof(PartidasJ)]=listaP.num;
			listaP.num=listaP.num+1;
			
			sprintf (respuesta, "6/%s",nombre);
			
			for (int j=0; j<listaU.num; j++)
			{
				if (strcmp(listaU.usuarios[j].nombre,nombredestino) == 0)
				{
					socketdestino=listaU.usuarios[j].socket;
				}
			}
		}
		else if (codigo==6)
		{
			p = strtok( NULL, "/");
			strcpy (nombredestino, p);
			
			for (int j=0; j<listaU.num; j++)
			{
				if (strcmp(listaU.usuarios[j].nombre,nombredestino) == 0)
				{
					socketdestino=listaU.usuarios[j].socket;
				}
			}
			char resp[20];
			p = strtok( NULL, "/");
			strcpy (resp, p);
			if (strcmp(resp,"Yes")==0)
			{
				PartidasJ[sizeof(PartidasJ)]=AddElementoLista(&listaP,socketdestino,sock_conn);
				sprintf (respuesta, "7/%s/SI",nombre);
			}
			else
			{
				sprintf (respuesta, "7/%s/NO",nombre);
			}
		}
		if ((codigo !=0)&&(codigo != 5)&&(codigo != 6))
		{
			printf ("%s",nombre);
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
		if ((codigo ==5)||(codigo == 6))
		{
			printf ("%s",nombre);
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (socketdestino,respuesta, strlen(respuesta));
		}
		if ((codigo == 1)||(codigo == 0))
		{
			pthread_mutex_lock(&mutex);
			char frase[100];
			frase[0] = '\0';
			EscribirLista(&listaU,frase);
			char notificacion[20];
			sprintf (notificacion, "5/%s", frase);
			pthread_mutex_unlock(&mutex);
			int j;
			for (j=0; j<i; j++)
			{
				write (sockets[j],notificacion, strlen(notificacion));
			}
		}
	}
	// Se acabo el servicio para este cliente
	close(sock_conn);
}

// MAIN ----------------------------------
int main(int argc, char *argv[])
{
	int sock_conn, sock_listen;
	int puerto = 50056;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket\n");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(puerto);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind\n");
	
	if (listen(sock_listen, 4) < 0)
		printf("Error en el Listen\n");
	
	pthread_t thread;
	i=0;
	listaU.num = 0;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		sockets[i] =sock_conn;
		
		// Crear thead y decirle lo que tiene que hacer
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
	}
}


