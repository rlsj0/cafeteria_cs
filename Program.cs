using Models;
using Services;
using Utilities;
using Repositories;

Cafe BrasilSolo = new Cafe(1.80m, 20, true, "Brasil", "Solo");
Cafe BrasilLargo = new Cafe(2.00m, 15, true, "Brasil", "Largo");
Cafe JamaicaLargo = new Cafe(4, 2.00m, 15, 4, true, "Jamaica", "Largo");
Cafe BrasilLeche = new Cafe(2.50m, 15, true, "Brasil", "Con leche");

//BrasilSolo.MostrarInformacion();

//BrasilLargo.RestarStock(3);
//BrasilLargo.MostrarInformacion();

//JamaicaLargo.MostrarInformacion();
//BrasilLeche.MostrarInformacion();

// Chequear cada método de Cliente

Cliente cliente = new Cliente("correo", "contrasena");
Cliente cliente2 = new Cliente("nuevo", "contrasena");
List<Cliente> listaClientes = new List<Cliente>();
listaClientes.Add(cliente);
listaClientes.Add(cliente2);
// 

// cliente.VerPedidos();
Tuple<Cafe, int> tupla = new Tuple<Cafe, int>(BrasilSolo, 2);
Tuple<Cafe, int> tupla2 = new Tuple<Cafe, int>(BrasilLeche, 3);
Tuple<Cafe, int> tupla3 = new Tuple<Cafe, int>(JamaicaLargo, 4);
List<Tuple<Cafe, int>> lista = new List<Tuple<Cafe, int>>();
lista.Add(tupla);
lista.Add(tupla2);
List<Tuple<Cafe, int>> lista23 = new List<Tuple<Cafe, int>>();
lista23.Add(tupla3);
lista.Add(tupla3);
cliente.HacerPedido(1, lista, true);
cliente2.HacerPedido(2, lista23, false);

List<Pedido> listaDePedidos = new List<Pedido>();
listaDePedidos.Add(cliente.HistoricoPedidos.ElementAt(0));
listaDePedidos.Add(cliente2.HistoricoPedidos.ElementAt(0));

// cliente.VerPedidos();

// Hago una lista de productos para pasarlo al método BuscarCafe

List<Cafe> listaCafes = new List<Cafe>();
listaCafes.Add(BrasilLeche);
listaCafes.Add(BrasilSolo);
listaCafes.Add(BrasilLargo);
listaCafes.Add(JamaicaLargo);

//modificar precio y anadir stock
//
Admin admin = new Admin("correoAdmin", "password");

/*admin.VerTotalPedidos(listaDePedidos);*/

// Probar los cafes: sumar stock, restar stock, mostrar info

BrasilLeche.MostrarInformacion();
BrasilLeche.SumarStock(1);
BrasilLeche.MostrarInformacion();
BrasilLeche.RestarStock(2);
BrasilLeche.MostrarInformacion();

// PROBANDO LA SESIÓN
// 1) Creamos una lista falsa de usuarios

List<Cliente> listaDeUsuarios = new List<Cliente>();
List<Admin> listaDeAdmin = new List<Admin>();
listaDeAdmin.Add(admin);
listaDeUsuarios.Add(cliente);
listaDeUsuarios.Add(cliente2);

// 2) Creamos la sesión

/*Sesion sesion = new Sesion("correo", "contrasena", listaDeUsuarios);*/
/*Console.WriteLine(sesion.EstaActiva);*/
/*Console.WriteLine(sesion.EsAdmin);*/
/*Console.WriteLine(sesion.UsuarioActivo.Correo);*/

// Probando los métodos para serializar

// Pasarle una lista de Pedidos
// JsonUtility.GuardarJson<List<Pedido>>("file.json", listaDePedidos);
/*var listaNuevaPedidos = JsonUtility.CargarJson<List<Pedido>>("file.json");*/
/*Console.WriteLine(listaNuevaPedidos.ElementAt(0).Id);*/
// DA ERROR CON PEDIDOS. Vamos a ver qué tal con otros objetos

/*JsonUtility.GuardarJson<List<Cafe>>("fileCafe.json", listaCafes);*/

/*var listaNuevaCafes = JsonUtility.CargarJson<List<Cafe>>("fileCafe.json");*/
/*Console.WriteLine(listaNuevaCafes.ElementAt(0).Variedad);*/

// Para cafes si funciona
// Veamos para admin y para clientes

/*JsonUtility.GuardarJson<List<Cliente>>("fileUser.json", listaDeUsuarios);*/
CafeRepository.GuardarCafes(listaCafes);
var listaNuevaCafes = CafeRepository.CargarCafes();
Console.WriteLine(listaNuevaCafes.ElementAt(0).Variedad);

PedidoRepository.GuardarPedidos(listaDePedidos);
var listaNuevaDePedidos = PedidoRepository.CargarPedidos();
Console.WriteLine(listaNuevaDePedidos.ElementAt(0).ClienteSatisfecho);

// funciona con clientes


// repositorios:
//     admin funciona
//     cliente funciona
//     pedido 
//     cafe

