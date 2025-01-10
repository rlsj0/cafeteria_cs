using Models;

Cafe BrasilSolo = new Cafe(1.80m, 20, true, "Brasil", "Solo");
Cafe BrasilLargo = new Cafe(2.00m, 15, true, "Brasil", "Largo");
Cafe JamaicaLargo = new Cafe(4, 2.00m, 15, 4, true, "Brasil", "Largo");
Cafe BrasilLeche = new Cafe(2.50m, 15, true, "Brasil", "Con leche");

BrasilSolo.MostrarInformacion();

BrasilLargo.RestarStock(3);
BrasilLargo.MostrarInformacion();

JamaicaLargo.MostrarInformacion();
BrasilLeche.MostrarInformacion();
