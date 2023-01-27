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
	int parti;
} ListaPartidas;


// Variables Globals ---------------------
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
ListaUsuarios listaU;
ListaPartidas listaP;
int i;
int sockets[100];
Partidas partidas [100];
int AccionesSeleccionadas;

// FUNCIONS ------------------------------
/*Escribir lista una función que sirve para obtener el nombre de los conectados, no tiene un parametro de seguridad en caso de que falle porque es 
muy sencilla, y si alguien pide la lista de conectados deve estar conectado a su vez, por lo que si no hay conectados el error es de esta función*/
void EscribirLista(ListaUsuarios *LU,char res[100])
{
	for (int j=0;j<LU->num;j++)
	{
		sprintf(res,"%s/%s", res,LU->usuarios[j].nombre);
	}
	/*res[strlen(res)-1]=' ';*/
	printf("Vector conectados: %s\n",res);
}
/*AddElemento añade a la lista de usuarios un nombre y su socket correspondiente no puede fallar, pero podria llegar a llenarse de usuarios, 
por ende devuelve -1 si esta lleno de usuarios */
int AddElemento(ListaUsuarios *LU, char nom[20], int socket)
{
	int result =0;
	if (LU->num<50)
	{
		Usuario u;
		strcpy(u.nombre,nom);
		u.socket=socket;
		LU->usuarios[LU->num] = u;
		LU->num =LU->num+1;
		printf("Elemento añadido: %s, num: %d,sock: %d\n", LU->usuarios[LU->num-1].nombre, LU->num, LU->usuarios[LU->num-1].socket);
	}
	else
	{
		result=-1;
	}
	return result;
}
/*AddElementoLista crea una partida y añade el socket de los participantes, a demás devuelve la id actual de la partida*/
int AddElementoLista(ListaPartidas *LP, int sockethost, int socket[100], int numerodejugadores)
{
	int partida;
	Partidas p;
	p.socket[0]=sockethost;
	p.socket[1]=socket[0];
	p.socket[2]=socket[1];
	p.socket[3]=socket[2];
	p.jugadores =numerodejugadores;
	LP->Partida[LP->num] = p;
	partida = LP->parti;
	LP->parti =LP->parti+1;
	LP->num =LP->num+1;
	return partida;
	printf("partida: %d\n", partida);
}

int EliminarElemento(ListaUsuarios *LU, char nom[20])
{
	int found = 0;
	for (int j=0; j<LU->num; j++)
	{
		if (strcmp(LU->usuarios[j].nombre,nom) == 0)
		{
			found = 1;			
		}
		if (found == 1)
		{
			LU->usuarios[j] = LU->usuarios[j+1];
		}
	}
	return found;
	//LU->num = LU->num-1;
}
/*EliminarPartida elimina la partida con el socket host recibido y resta uno al numero de partidas, podria parecer funciones precipitadas, pero esto se */
/*deve a que es una función muy controlada que solo elimina partida si ya hay una creada, a demas se usa sobretodo para dar soporte a otras funciones y*/
/*no puede fallar en esos contextos*/
void EliminarPartida(ListaPartidas *LP, int socket)
{
	int found = 0;
	for (int j=0; j<LP->num; j++)
	{
		if (LP->Partida[j].socket[0] == socket)
		{
			found = 1;			
		}
		if (found == 1)
		{
			LP->Partida[j] = LP->Partida[j+1];
		}
	}
	LP->num = LP->num-1;
}
/*EliminarJugador es una función que como su nombre indica elimina el socket recibido no puede fallar puesto que se usa en un contexto muy especifico*/
void EliminarJugador(ListaPartidas *LP, int sockethost, int socketaeliminar)
{
	int found = 0;
	int j = 0;
	while (j<LP->num)
	{
		if (LP->Partida[j].socket[0] == sockethost)
		{
			if (LP->Partida[j].socket[1] == socketaeliminar)
			{
				LP->Partida[j].socket[1] = LP->Partida[j].socket[2];
				LP->Partida[j].socket[2] = LP->Partida[j].socket[3];
				LP->Partida[j].socket[3] = 0;
				LP->Partida[j].jugadores = LP->Partida[j].jugadores +1;
				printf("a");
			}
			else if (LP->Partida[j].socket[2] == socketaeliminar)
			{
				LP->Partida[j].socket[2] = LP->Partida[j].socket[3];
				LP->Partida[j].socket[3] = 0;
				LP->Partida[j].jugadores = LP->Partida[j].jugadores +1;
			}
			else if (LP->Partida[j].socket[3] == socketaeliminar)
			{
				LP->Partida[j].socket[2] = LP->Partida[j].socket[3];
				LP->Partida[j].socket[3] = 0;
				LP->Partida[j].jugadores = LP->Partida[j].jugadores +1;
			}
		}
		j=j+1;
	}
	if (LP->Partida[j].jugadores==1)
	{
		EliminarPartida (&LP,sockethost);
	}
}
/*EliminarSocket evita que se llene el vector de sockets, no puede fallar puesto que se usa solo cuando alguien se va del servidor y para ello tiene que*/
/*tener un socket*/
void EliminarSocket(int sockets[100], int socket)
{
	int found = 0;
	for (int j=0; j<strlen(sockets); j++)
	{
		if (sockets[j]==socket)
		{
			found = 1;			
		}
		if (found == 1)
		{
			sockets[j] = sockets[j+1];
		}
	}
}
/*VerificaContra recibe nombre y contraseña y mira que coincidan, devuelve -1 si no coincide o no existe ese usuario y 0 si coincide*/
int VerificaContra(char *nombre[20],MYSQL *conn,char *contra[20])
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	
	int result;
	char consulta [150];
	
	strcpy (consulta,"SELECT Usuarios.Contra FROM (Usuarios) WHERE Usuarios.Nombre = '");
	strcat (consulta, nombre);
	strcat (consulta,"'");
	
	int err=mysql_query (conn, consulta);
	
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	
	row = mysql_fetch_row(resultado);
	
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
		result=-1;
	}
	else
	{
		if (strcmp(row[0],contra)==0)
		{
			result=0;
		}
		else
		{
			result=-1;
		}
	}
	return result;
	printf("%15s\n",row[0]);
}
/*a eliminar*/
int Jugadorpartida(char *nombre[20],MYSQL *conn,int *partida)
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	
	printf("a1");
	char consulta [150];
	sprintf(consulta,"SELECT Usuarios.nombre FROM Usuarios,Registro_de_partidas WHERE Registro_de_partidas.idP= '%d' AND Registro_de_partidas.idU = Usuarios.id",partida);
	printf ("Partida: %d\n", partida);
	
	int err=mysql_query (conn, consulta);
	
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	printf("%15s\n",row[0]);
	int result = 0;
	while (row !=NULL)
	{
		if (strcmp( row[0], nombre) == 0)
			result=1;
		printf("%15s\n",row[0]);
		row = mysql_fetch_row (resultado);
	}
	if (result == 1)
		return 0;
	else
		return -1;
}
/*a eliminar*/
int Duracionpartida(MYSQL *conn,int *partida)
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	
	printf("a1");
	char consulta [150];
	sprintf(consulta,"SELECT Partidas.Duracion FROM Partidas WHERE Partidas.id= '%d'",partida);
	
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	int duracion;
	duracion = row[0];
	if (row == NULL)
	{
		return 1;
	}
	else
	{
		if (duracion>10)
			return 0;
		else 
			return -1;
	}
	printf("%15s\n",row[0]);
	printf("%d\n",duracion);
}
/*a eliminar*/
int Buscarganador(char *Jugador[20],MYSQL *conn,int *partida)
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	
	char consulta [150];
	printf("a");
	sprintf(consulta,"SELECT Registro_de_partidas.Resultado_Final FROM Registro_de_partidas,Usuarios,Partidas WHERE Registro_de_partidas.idU=Usuarios.id AND Usuarios.nombre='%s' AND  Registro_de_partidas.idP='%d'",Jugador,partida);
	printf("b");
	int err=mysql_query (conn, consulta);

	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}

	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
	{
		return 1;
	}
	else
	{
		if (strcmp(row[0],"Ganador"))
		{
			return 0;
		}
		else
		{
			return -1;
		}
	}
	printf("%15s\n",row[0]);
}
/*BuscaNombre mira si existe ese nombre en la base de datos devuelve 1 si no existe y 0 si existe*/
int BuscaNombre(char *jugador[20],MYSQL *conn)
{
	int resultadof;
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	char consulta[150];
	sprintf(consulta,"SELECT Usuarios.id FROM Usuarios WHERE Usuarios.Nombre = '%s'",jugador);
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row(resultado);
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
		resultadof = 1;
	}
	else
	{
		resultadof = 0;
	}
	printf("%d",resultadof);
	return resultadof;
}

/*BuscaId mira si existe esa id en la base de datos devuelve 1 si no existe y 0 si existe*/
int BuscaId(int *id,MYSQL *conn)
{
	int resultadof;
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	char consulta[150];
	sprintf(consulta,"SELECT Usuarios.nombre FROM Usuarios WHERE Usuarios.id = '%d'",id);
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row(resultado);
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
		resultadof = 1;
	}
	else
		resultado = 0;
	printf("%d",resultadof);
	return resultadof;
}
/*CrearUsuario crea un usuario, devuelve 1 si no se ha podido crear el usuario porque su id coincide o su usuario ya existe y 0 si se ha podido*/
int CrearUsuario(char *jugador[20],MYSQL *conn,char *contra[20])
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	int resultadof;
	int id = rand() % 900000 + 100000;
	char consulta [150];
	int result1 = BuscaNombre(jugador,conn);
	int result2 = BuscaId(id,conn);
	if (result1==1 && result2==1)
	{	
		sprintf(consulta, "INSERT INTO Usuarios VALUES('%d', '%s','%s');", id, jugador, contra);
		int err=mysql_query (conn, consulta);	
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		resultadof=0;
	}
	else
	{
		resultadof=1;
	}
	return resultadof;
}
/*EliminarUsuario elimina el usuario, no puede fallar porque para eliminar el Usuario es necesario estar usandolo y, por ende, deve existir*/
void EliminarUsuario(char *jugador[20],MYSQL *conn)
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	
	char consulta [150];
	sprintf (consulta,"DELETE FROM Usuarios WHERE Usuarios.nombre='%s'",jugador);
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}

void DamePartidas(char *nombre[20],MYSQL *conn, char *mispartidas[200])
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	int resultadof=0;
	printf("a1");
	char mispartidasp[200];
	char consulta [150];
	sprintf(consulta,"SELECT DISTINCT Registro_de_partidas.idP FROM Usuarios,Registro_de_partidas WHERE Usuarios.nombre= '%s' AND Registro_de_partidas.idU = Usuarios.id",nombre);
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	int numerodepartidas;
	while (row!=NULL)
	{
		int Estapartida=atoi(row[0]);
		sprintf(mispartidasp,"%s%d/",mispartidasp,Estapartida);
		numerodepartidas=numerodepartidas+1;
		row = mysql_fetch_row (resultado);
	}
/*	return resultadof;*/
	printf("%s", mispartidasp);
	printf("%d", numerodepartidas);
	sprintf(mispartidas,"%d/%s",numerodepartidas,mispartidasp);
	printf("%s", mispartidas);
}
/*Dameinfodepartidas da información de todas las partidas guardadas en la base de datos, no puede fallar porque siempre van a haber partidas guardadas*/
void Dameinfodepartidas(MYSQL *conn,char *info[200])
{
	MYSQL_RES *resultado;
	MYSQL_FIELD *campo;
	MYSQL_ROW row;
	char consulta [150];
	char infop[200];
	sprintf(consulta,"SELECT * FROM Partidas");
	int err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	int Estapartida;
	int njugadores;
	int tiempo;
/*	printf("%15s\n",row[0]);*/
/*	printf("%15s\n",row[2]);*/
/*	printf("%15s\n",row[3]);*/
/*	printf("%15s\n",row[4]);*/
/*	printf("%15s\n",row[5]);*/
/*	printf("%15s\n",row[6]);*/
/*	printf("%15s\n",row[7]);*/
	while (row!=NULL)
	{
		Estapartida=atoi(row[0]);
		/*printf("%d",Estapartida);*/
		sprintf(infop,"%s/%d",infop,Estapartida);
		njugadores=atoi(row[1]);
		printf("%d",njugadores);
		sprintf(infop,"%s/%d",infop,njugadores);
		sprintf(infop,"%s/%s",infop,row[2]);
		sprintf(infop,"%s/%s",infop,row[3]);
		sprintf(infop,"%s/%s",infop,row[4]);
		sprintf(infop,"%s/%s",infop,row[5]);
		sprintf(infop,"%s/%s",infop,row[6]);
		tiempo=atoi(row[7]);
		/*printf("%d",tiempo);*/
		sprintf(infop,"%s/%d",infop,tiempo);
		/*printf("%s",infop);*/
		row = mysql_fetch_row (resultado);
	}
	sprintf(info,"%s",infop);
}

void *AtenderCliente (void *socket)
// Abre connexiÃ³n con MySQL y devuelve la Respuesta de la consulta
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	int socketdestino;
	int socketdestinos[100];
	int PartidasJ[100];
	//int socket_conn = * (int *) socket;
	
	int notificacion = 0;
	char peticion[512];
	char respuesta[512];
	char empezartablero[512];
	int ret;
	int partidaact;
	
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

			int found = EliminarElemento(&listaU,nombre);
			if (found == 1)
			{
				listaU.num = listaU.num-1;
			}
			pthread_mutex_unlock(&mutex);
			char prob[100];
			prob[0]='\0';
			EscribirLista(&listaU,prob);
			terminar = 1;
		}	
		else if (codigo == 1) //mirar si el usuario y contraseÃ±a son correctos
		{	
			//recogemos las variables 
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			p = strtok( NULL, "/");
			strcpy (contra, p);
			//Verificamos Usuario y contraseña correctos
			int result;
			result = VerificaContra(nombre,conn,contra);
			if (result==0)
			{
				strcpy(respuesta, "Correcto");
			}
			else
			{
				strcpy(respuesta, "Incorrecto");
			}
			pthread_mutex_lock(&mutex);
			//Metemos al jugador en la lista(con prioridad pues modificamos una variable del sistema)
			int maxplayers;
			maxplayers = AddElemento(&listaU,nombre,sock_conn);
			if (maxplayers ==-1)
			{
				strcpy(respuesta, "MaxP");
			}
			char frase[100];
			EscribirLista(&listaU,frase);
			pthread_mutex_unlock(&mutex);
		}
		else if (codigo ==2)
		{	
			p = strtok( NULL, "/");
			strcpy (jugador, p);
			
			p = strtok( NULL, "/");
			partida = atoi (p);
			
			int resultado;
			resultado = Jugadorpartida(jugador,conn,partida);
			if (resultado == 0)
				sprintf (respuesta, "2/SI");
			else
				sprintf (respuesta, "2/NO");
			
		}
		else if (codigo ==3)
		{
			p = strtok( NULL, "/");
			partida = atoi (p);
			
			int resultado;
			resultado = Duracionpartida(conn,partida);
			if (resultado == 1)
			{
				sprintf (respuesta, "3/NF");
			}
			else if (resultado == 0)
				sprintf (respuesta, "3/SI");
			else 
				sprintf (respuesta, "3/NO");
		}
		
		else if (codigo ==4)
		{
			p = strtok( NULL, "/");
			strcpy (jugador, p);
			
			p = strtok( NULL, "/");
			partida = atoi (p);
			
			int resultado = 0;
/*			resultado = Buscarganador(jugador,conn,partida);*/

			if (resultado == 1)
			{
				sprintf (respuesta, "4/NF");
			}
			else
			{
				if (resultado == 0)
				{
					sprintf (respuesta, "4/SI");
				}
				else
				{
					sprintf (respuesta, "4/NO");
				}
			}
		}
		else if (codigo==5)//Invitar a jugadores a la partida
		{
			//Obtenemos los datos para realizar la operación
			socketdestinos[0]=0;
			socketdestinos[1]=0;
			socketdestinos[2]=0;
			p = strtok( NULL, "/");
			int numerodejugadores;
			numerodejugadores = atoi(p);
			printf ("%d",numerodejugadores);
			if (numerodejugadores>=1)
			{
				p = strtok( NULL, "/");
				strcpy (nombredestino, p);
				for (int j=0; j<listaU.num; j++)
				{
					if (strcmp(listaU.usuarios[j].nombre,nombredestino) == 0)
					{
						socketdestinos[0]=listaU.usuarios[j].socket;
					}
				}
			}
			if (numerodejugadores>=2)
			{
				p = strtok( NULL, "/");
				strcpy (nombredestino, p);
				for (int j=0; j<listaU.num; j++)
				{
					if (strcmp(listaU.usuarios[j].nombre,nombredestino) == 0)
					{
						socketdestinos[1]=listaU.usuarios[j].socket;
					}
				}
			}
			if (numerodejugadores>=3)
			{
				p = strtok( NULL, "/");
				strcpy (nombredestino, p);
				
				for (int j=0; j<listaU.num; j++)
				{
					if (strcmp(listaU.usuarios[j].nombre,nombredestino) == 0)
					{
						socketdestinos[2]=listaU.usuarios[j].socket;
					}
				}
			}
			printf ("Sockets: %d",socketdestinos[0]);
			printf ("%d",socketdestinos[1]);
			printf ("%d\n",socketdestinos[2]);
			pthread_mutex_lock(&mutex);
			//Añadimos los jugadores a la partida, si alguno rechaza se eliminará mas adelante(con prioridad puesto que modificamos parametros del sistema)
			int numerodejugadoresfinales = numerodejugadores+1;
			partidaact=AddElementoLista(&listaP,sock_conn,socketdestinos,numerodejugadoresfinales);
			sprintf (respuesta, "6/%s/%d",nombre,partidaact);
			pthread_mutex_unlock(&mutex);
		}
		else if (codigo==6)//Recibir respuesta de la invitación de partida
		{
			//Recibimos parametros y buscamos el socket de quien recibirá la respuesta
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
			p = strtok( NULL, "/");
			partidaact =  atoi (p);			
			if (strcmp(resp,"Yes")==0)
			{
				sprintf (respuesta, "7/%s/SI/%d",nombre,partidaact);
			}
			else
			{
				sprintf (respuesta, "7/%s/NO",nombre);
				pthread_mutex_lock(&mutex);
				//Eliminamos el usuario previamente incorporado que no quiere jugar esta partida(con prioridad puesto que modificamos parametros del sistema)
				EliminarJugador(&listaP, socketdestino, sock_conn);
				pthread_mutex_unlock(&mutex);
			}
		}
		else if (codigo==7)//Chat
		{
			p = strtok( NULL, "/");
			partidaact =  atoi (p);
			for (int j=0; j<listaP.Partida[partidaact].jugadores; j++)
			{
				socketdestinos[j]=listaP.Partida[partidaact].socket[j];
				printf ("%d",socketdestinos[j]);
			}
			char resp[20];
			p = strtok( NULL, "/");
			strcpy (resp, p);
			sprintf (respuesta, "8/%s/%s",nombre,resp);
		}
		else if (codigo==8)//Eliminar Usuario
		{
			//Obtenemos datos
			p = strtok( NULL, "/");
			strcpy (jugador, p);
			pthread_mutex_lock(&mutex);
			//Eliminamos el usuario(con prioridad ya que modificamos parametros del sistema)
			EliminarUsuario(jugador,conn);
			pthread_mutex_unlock(&mutex);
			strcpy(respuesta, "9/");
		}
		else if (codigo==9)
		{
			//Obtenemos datos
			p = strtok( NULL, "/");
			strcpy (jugador, p);
			p = strtok( NULL, "/");
			strcpy (contra, p);
			int result=1;
			pthread_mutex_lock(&mutex);
			//Creamos el usuario(con prioridad ya que modificamos parametros del sistema)
			result = CrearUsuario(jugador,conn,contra);
			pthread_mutex_unlock(&mutex);
			if (result == 0)
			{
				strcpy(respuesta, "Correcto");
			}
			else
			{
				strcpy(respuesta, "Incorrecto");
			}
		}	
		else if (codigo==10) 
		{
			int funcion;
			int numJugadores;
			p = strtok( NULL, "/");
			partidaact =  atoi (p);
			for (int j=0; j<listaP.Partida[partidaact].jugadores; j++)
			{
				socketdestinos[j]=listaP.Partida[partidaact].socket[j];
				printf ("%d",socketdestinos[j]);
			}
			p = strtok( NULL, "/");
			funcion =  atoi (p);
			char resp[20];
			p = strtok( NULL, "/");
			strcpy (resp, p);
			if	(funcion==1)
			{
				sprintf (respuesta, "10/%d/%s",partidaact,resp);
			}
			else if	(funcion==2)
			{
				sprintf (respuesta, "11/%d/%s",partidaact,resp);
			}
			else if	(funcion==3)
			{
				sprintf (respuesta, "12/%d/%s",partidaact,resp);
			}
			else if (funcion == 4)
			{
				//sprintf (respuesta, "13/%d/%s",partidaact,resp);
			}
		}
		else if (codigo ==11)//Historial de partidas
		{
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			char info[200];
			Dameinfodepartidas(conn,info);
			sprintf (respuesta, "20%s",info);
		}
		
		else if (codigo==12)//Chat
		{
			p = strtok( NULL, "/");
			partidaact =  atoi (p);
			for (int j=0; j<listaP.Partida[partidaact].jugadores; j++)
			{
				socketdestinos[j]=listaP.Partida[partidaact].socket[j];
				printf ("%d",socketdestinos[j]);
			}
			char resp[20];
			sprintf (respuesta, "13/%d",partidaact);
		}
		
		if ((codigo !=0)&&(codigo != 5)&&(codigo != 6)&&(codigo != 7)&&(codigo!=10)&&(codigo!=12))//Enviamos al Usuario del socket
		{
			printf ("%s",nombre);
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (sock_conn,respuesta, strlen(respuesta));
		}
		if (codigo == 6)//Enviamos a un Usuariodestino
		{
			printf ("%s",nombre);
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos respuesta
			write (socketdestino,respuesta, strlen(respuesta));
		}
		if ((codigo ==7)||(codigo ==5)||(codigo==10)||(codigo==12))//Enviamos a varios UsuariosDestino(Generalmente en una partida)
		{
			printf ("%s",nombre);
			printf ("Respuesta: %s\n", respuesta);
			int x;
			for (x=0; x<4; x++)
			{
				if (socketdestinos[x]!=0)
				{
					write (socketdestinos[x],respuesta, strlen(respuesta));
					printf ("%d",socketdestinos[x]);
				}
			}
		}
		if ((codigo == 1)||(codigo == 0))//Enviamos una notificación a todos los usuarios conectados
		{
			pthread_mutex_lock(&mutex);
			char frase[100];
			frase[0] = '\0';
			EscribirLista(&listaU,frase);
			char notificacion[20];
			sprintf (notificacion, "5%s", frase);
			printf ("notificacion %s", notificacion);
			pthread_mutex_unlock(&mutex);
			int j;
			
			for (j=0; j<listaU.num; j++)
			{
				write (listaU.usuarios[j].socket,notificacion, strlen(notificacion));
			}
			
		}
	}
	EliminarSocket(sockets,sock_conn);
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
	AccionesSeleccionadas = 0;
	listaU.num = 0;
	listaP.num = 0;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		i=strlen(sockets);
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		sockets[i] =sock_conn;
		
		// Crear thead y decirle lo que tiene que hacer
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
	}
}

