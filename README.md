# cafeteria_cs

Proyecto para Desarrollo Web en Entorno Servidor.

El proyecto consiste en lo siguiente: se trata de un aplicativo para
una cafetería virtual. Los clientes pueden comprar varios tipos de
café a través de una aplicación de terminal. Un cliente puede ver los
productos ofrecidos y su precio, y si se registra puede hacer pedidos
y ver su historial de pedidos. También puede tener una _wishlist_ de
productos. A la aplicación también puede registrarse una figura de
administrador, que puede ver todos los pedidos que se han hecho, así
como los clientes y adminstradores registrados. También puede añadir
y/o modificar los productos y sus precios. La aplicación también
permite modificar el estado de un producto, que varía entre "Nuevo",
"En proceso" y "Finalizado".

## Requisitos obligatorios (0/5)

- [ ] La aplicación debe componerse de:
    - [ ] Menú principal y secundarios
    - [ ] Gestión de alta y selección
    - [ ] Zona privada de información
    - [ ] Zona pública de información
- [ ] Modelo de datos:
    - [ ] Al menos, 3 clases
    - [ ] Con relaciones entre ellas
    - [ ] Al menos 6 atributos/clase
- [ ] Funcionalidad de búsqueda (al menos por un campo)
- [ ] Contenerizar la aplicación 
    - [ ] Usando el puerto del contenedor 8023.
    - [ ] Al menos un volumen para acceder a los datos.
- [ ] Subir el contenedor a un registro de contenedores y usar la
  aplicación contenida.

## Requisitos opcionales (1/5)

- [X] Utilizar Git y Gitflow
- [ ] Almacenar datos en `.json`
- [ ] Hacer un log con mensajes de error. Añadir un volumen que lo
  guarde.
- [ ] Añadir al menos una variable de entorno que emplee la
  aplicación.
- [ ] Usar librerías como "Terminal.GUI" o "Spectre.Console" para
  hacer más visual la aplicación
