# cafeteria_cs

Proyecto para Desarrollo Web en Entorno Servidor.

El proyecto consiste en lo siguiente: se trata de un aplicativo para
una cafetería virtual. Los clientes pueden comprar varios tipos de
café a través de una aplicación de terminal. Un cliente puede ver los
productos ofrecidos y su precio, y si se registra puede hacer pedidos
y ver su historial de pedidos. A la aplicación también puede registrarse 
una figura de administrador, que puede ver todos los pedidos que se han
hecho, así como los clientes registrados. También puede añadir y/o
modificar los productos y sus precios.

## Requisitos obligatorios (5/5)

- [X] La aplicación debe componerse de:
    - [X] Menú principal y secundarios: **Menú principal, menú de
    cliente y menú de admin.**
    - [X] Gestión de alta y selección: **Registro de clientes, función
      de hacer pedidos**
    - [X] Zona privada de información: **Pedidos del cliente**
    - [X] Zona pública de información: **Productos en venta**
- [X] Modelo de datos:
    - [X] Al menos, 3 clases: **Las clases principales son: Usuario,
    Admin, Cliente, Producto, Cafe, Pedido**
    - [X] Con relaciones entre ellas: **Hay herencia entre Usuario y
    Admin/Cliente, así como entre Producto y Café. Desde los métodos
    de cliente pueden realizarse nuevos pedidos**
    - [X] Al menos 6 atributos/clase: **Tienen 6 atributos las clases
    Cliente (id, correo, hashContrasena, saltContrasena,
    fechaCreacion, historicoPedidos), Cafe (id, precio, cantidadStock,
    numeroCompras, esComercioJusto, variedad, tipo), Pedido (id,
    nextId, idCliente, productos, precioTotal, fecha,
    clienteSatisfecho).**
- [X] Funcionalidad de búsqueda: **Búsqueda de cafés por variedad y tipo**
- [X] Contenerizar la aplicación 
    - [X] Usando el puerto del contenedor 8023: **Véase el Dockerfile
      (aunque la aplicación _no necesita_ el puerto)**
    - [X] Al menos un volumen para acceder a los datos: **Dentro del
    contenedor, en `/app/data`.**
- [X] Subir el contenedor a un registro de contenedores y usar la
  aplicación contenida: **Véase `rlsanjose/cafeteria_cs` en Docker
  Hub**

## Requisitos opcionales (3/5)

- [X] Utilizar Git y Gitflow
- [X] Almacenar datos en `.json`
- [ ] Hacer un log con mensajes de error. Añadir un volumen que lo
  guarde.
- [X] Añadir al menos una variable de entorno que emplee la
  aplicación: **Configurada la variable `LANG` como `es_ES.UTF-8` para
  que muestre la moneda como euros**
- [ ] Usar librerías como "Terminal.GUI" o "Spectre.Console" para
  hacer más visual la aplicación
