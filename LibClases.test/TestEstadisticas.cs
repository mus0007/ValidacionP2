using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibClases;
using System.Data;

namespace LibClases.test
{
    /// <summary>
    /// Descripción resumida de TestEstadisticas
    /// </summary>
    [TestClass]
    public class TestEstadisticas
    {
        //Sprint1 - estadoEncuestas()
        [TestMethod]
        public void estadoEncuestas()
        {
            DataBase db = new DataBase();
            
            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.estadoEncuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "activas");
            Assert.AreEqual(dataTable.Rows[0][1], 5);
            Assert.AreEqual(dataTable.Rows[1][0], "desactivas");
            Assert.AreEqual(dataTable.Rows[1][1], 5);
            //Comprobamos que las filas están en orden
            Assert.AreNotEqual(dataTable.Rows[0][0], "desactivas");
            Assert.AreNotEqual(dataTable.Rows[1][0], "activas");
            //Comprobamos que si añadimos una nueva encuesta, tenemos una nueva encuesta desactivada
            db.addEncuesta("Encuesta11", "Encuesta11descripción");
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.estadoEncuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "activas");
            Assert.AreEqual(dataTable.Rows[0][1], 5);
            Assert.AreEqual(dataTable.Rows[1][0], "desactivas");
            Assert.AreEqual(dataTable.Rows[1][1], 6);
            //Comprobamos que si borramos una encuesta activa, tenemos una encuesta activa menos
            db.borrarEncuesta("Encuesta1");
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.estadoEncuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "activas");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[1][0], "desactivas");
            Assert.AreEqual(dataTable.Rows[1][1], 6);
            //Comprobarmos que si modificar una encuesta de desactiva a activa, tenemos 5 activas y 5 desactivas
            db.getEncuesta("Encuesta2").Activa = true;
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.estadoEncuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "activas");
            Assert.AreEqual(dataTable.Rows[0][1], 5);
            Assert.AreEqual(dataTable.Rows[1][0], "desactivas");
            Assert.AreEqual(dataTable.Rows[1][1], 5);
        }

        //Sprint1 - numeroRespuestas()
        [TestMethod]
        public void numeroRespuestas()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.numeroRespuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[9][0], "Encuesta10");
            Assert.AreEqual(dataTable.Rows[9][1], 4);
            //Comprobamos que al añadir una nueva opinión se cambia
            db.getEncuesta("Encuesta1").setOpinion(3);
            estadisticas = db.getEstadisticas(); ;
            dataTable = estadisticas.numeroRespuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 5);
            Assert.AreEqual(dataTable.Rows[9][0], "Encuesta10");
            Assert.AreEqual(dataTable.Rows[9][1], 4);
            //Comprobamos que al borrar una encuesta ya no existe ni ella ni sus opniones
            db.borrarEncuesta("Encuesta1");
            estadisticas = db.getEstadisticas(); ;
            dataTable = estadisticas.numeroRespuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta2");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[8][0], "Encuesta10");
            Assert.AreEqual(dataTable.Rows[8][1], 4);
            //Comprobamos que al añadir una encuesta sus opniones son 0
            db.addEncuesta("Encuesta1", "Encuesta1descripción");
            estadisticas = db.getEstadisticas(); ;
            dataTable = estadisticas.numeroRespuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta2");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[9][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[9][1], 0);
        }

        //Sprint1 - numeroEncuestas()
        [TestMethod]
        public void numeroEncuestas()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            Int16 numero = estadisticas.numeroEncuestas();
            Assert.AreEqual(numero, 10);
            //Comprobamos que si borro una encuesta el número se reduce
            db.borrarEncuesta("Encuesta1");
            estadisticas = db.getEstadisticas();
            numero = estadisticas.numeroEncuestas();
            Assert.AreEqual(numero, 9);
            //Comprobamos que al añadir una encuesta sin opniones el número sigue siendo 9
            db.addEncuesta("Encuesta1","Encuesta1descripción");
            estadisticas = db.getEstadisticas();
            numero = estadisticas.numeroEncuestas();
            Assert.AreEqual(numero, 9);

        }

        //Sprint1 - rankingEncuestasPorRespuestas()
        [TestMethod]
        public void rankingEncuestasPorRespuestas()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.rankingEncuestasPorRespuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[9][0], "Encuesta10");
            Assert.AreEqual(dataTable.Rows[9][1], 4);
            //Comprobamos que al añadir una encuesta sin opniones está en la última posición
            db.addEncuesta("Encuesta11", "Encuesta11descripción");
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.rankingEncuestasPorRespuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[10][0], "Encuesta11");
            Assert.AreEqual(dataTable.Rows[10][1], 0);
            //Comprobamos que al añadir 5 opniones pasa a la primera posición
            db.getEncuesta("Encuesta11").setOpinion(3);
            db.getEncuesta("Encuesta11").setOpinion(2);
            db.getEncuesta("Encuesta11").setOpinion(1);
            db.getEncuesta("Encuesta11").setOpinion(4);
            db.getEncuesta("Encuesta11").setOpinion(3);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.rankingEncuestasPorRespuestas();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta11");
            Assert.AreEqual(dataTable.Rows[0][1], 5);
        }

        //Sprint1 - rankingEncuestasPorValoración()
        [TestMethod]
        public void rankingEncuestasPorValoracion()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.rankingEncuestasPorValoracion();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[9][0], "Encuesta2");
            Assert.AreEqual(dataTable.Rows[9][1], 2);
            //Comprobamos que añadiendo una nueva encuesta con valor 1, está la última
            db.addEncuesta("Encuesta11", "Encuesta11descripción");
            db.getEncuesta("Encuesta11").setOpinion(1);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.rankingEncuestasPorValoracion();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            Assert.AreEqual(dataTable.Rows[10][0], "Encuesta11");
            Assert.AreEqual(dataTable.Rows[10][1], 1);
            //Comprobamos que añadiendo la encuesta anterior con valor 4
            db.getEncuesta("Encuesta11").setOpinion(4);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.rankingEncuestasPorValoracion();
            Assert.AreEqual(dataTable.Rows[4][0], "Encuesta11");
            Assert.AreEqual(dataTable.Rows[4][1], 4);
            Assert.AreEqual(dataTable.Rows[10][0], "Encuesta2");
            Assert.AreEqual(dataTable.Rows[10][1], 2);
        }

        //Sprint1 - media()
        [TestMethod]
        public void media()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            double media = estadisticas.media();
            Assert.AreEqual(media,2.325);
            //Comprobamos que si añadimos una nueva respuesta con un valor de 4 aumenta la media
            db.getEncuesta("Encuesta1").setOpinion(4);
            estadisticas = db.getEstadisticas();
            media = estadisticas.media();
            Assert.AreEqual(media, (double)97/(double)41);
            //Comprobamos que al borrar una encuesta con respuestas se reduce la media
            db.borrarEncuesta("Encuesta1");
            estadisticas = db.getEstadisticas();
            media = estadisticas.media();
            Assert.AreEqual(media, (double)82/(double)36);
        }

        //Sprint1 - mediana()
        [TestMethod]
        public void mediana()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            double mediana = estadisticas.mediana();
            Assert.AreEqual(mediana, 2);
            //Comprobamos al añadir una nueva respuesta con un valor de 4
            db.getEncuesta("Encuesta1").setOpinion(4);
            estadisticas = db.getEstadisticas();
            mediana = estadisticas.mediana();
            Assert.AreEqual(mediana, 2);
            //Comprobamos al borrar una encuesta con respuestas
            db.borrarEncuesta("Encuesta1");
            estadisticas = db.getEstadisticas();
            mediana = estadisticas.mediana();
            Assert.AreEqual(mediana, 2);
        }

        //Sprint1 - desviaciones()
        [TestMethod]
        public void desvest()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            double desviacion = estadisticas.desvest();
            Assert.AreEqual(Math.Round(desviacion), Math.Round(1.02250321295966));
            //Comprobamos al añadir una nueva respuesta con un valor de 4
            db.getEncuesta("Encuesta1").setOpinion(4);
            estadisticas = db.getEstadisticas();
            desviacion = estadisticas.desvest();
            Assert.AreEqual(Math.Round(desviacion), Math.Round(1.04297884832281));
            //Comprobamos al borrar una encuesta con respuestas
            db.borrarEncuesta("Encuesta1");
            estadisticas = db.getEstadisticas();
            desviacion = estadisticas.desvest();
            Assert.AreEqual(Math.Round(desviacion), Math.Round(1.00316958005574));
        }

        //Sprint1 - numRespRangosPorEncuesta()
        [TestMethod]
        public void numRespRangosPorEncuesta()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.numRespRangosPorEncuesta("Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 1);
            Assert.AreEqual(dataTable.Rows[0][2], 4);
            //Comprobamos que si añadimos una encuesta con dos opniones, son correctos
            db.addEncuesta("Encuesta11", "Encuesta11descripción");
            db.getEncuesta("Encuesta11").setOpinion(2);
            db.getEncuesta("Encuesta11").setOpinion(3);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.numRespRangosPorEncuesta("Encuesta11");
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta11");
            Assert.AreEqual(dataTable.Rows[0][1], 2);
            Assert.AreEqual(dataTable.Rows[0][2], 3);
            //Comprobamos que si una encuesta no tiene opniones tiene valores 0
            db.addEncuesta("Encuesta12", "Encuesta12descripción");
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.numRespRangosPorEncuesta("Encuesta12");
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta12");
            Assert.AreEqual(dataTable.Rows[0][1], 0);
            Assert.AreEqual(dataTable.Rows[0][2], 0);
        }

        //Sprint1 - numRespRangos()
        [TestMethod]
        public void numRespRangos()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.numRespRangos();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 1);
            Assert.AreEqual(dataTable.Rows[0][2], 4);
            Assert.AreEqual(dataTable.Rows[9][0], "Encuesta10");
            Assert.AreEqual(dataTable.Rows[9][1], 1);
            Assert.AreEqual(dataTable.Rows[9][2], 3);
            //Comprobamos que si añadimos una encuesta con dos opiniones, son correctos
            db.addEncuesta("Encuesta11", "Encuesta11descripción");
            db.getEncuesta("Encuesta11").setOpinion(2);
            db.getEncuesta("Encuesta11").setOpinion(3);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.numRespRangos();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 1);
            Assert.AreEqual(dataTable.Rows[0][2], 4);
            Assert.AreEqual(dataTable.Rows[10][0], "Encuesta11");
            Assert.AreEqual(dataTable.Rows[10][1], 2);
            Assert.AreEqual(dataTable.Rows[10][2], 3);

            //Comprobamos que si una encuesta no tiene opniones tiene valores 0
            db.addEncuesta("Encuesta12", "Encuesta12descripción");
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.numRespRangos();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 1);
            Assert.AreEqual(dataTable.Rows[0][2], 4);
            Assert.AreEqual(dataTable.Rows[11][0], "Encuesta12");
            Assert.AreEqual(dataTable.Rows[11][1], 0);
            Assert.AreEqual(dataTable.Rows[11][2], 0);
        }

        //Sprint2 - respuestasPorAnios()
        [TestMethod]
        public void respuestasPorAnios()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.respuestasPorAnios();
            Assert.AreEqual(dataTable.Rows[0][0], "2018");
            Assert.AreEqual(dataTable.Rows[0][1], 30);
            Assert.AreEqual(dataTable.Rows[1][0], "2017");
            Assert.AreEqual(dataTable.Rows[1][1], 10);
            //Comprobamos que si añadimos una resuesta del 2016 nos lo contabiliza
            Valoracion valoracion = new Valoracion(4, "muy buena", new DateTime(2016, 12, 12, 14, 30, 15));
            db.getEncuesta("Encuesta1").setOpinionCSV(valoracion);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.respuestasPorAnios();
            Assert.AreEqual(dataTable.Rows[0][0], "2018");
            Assert.AreEqual(dataTable.Rows[0][1], 30);
            Assert.AreEqual(dataTable.Rows[1][0], "2017");
            Assert.AreEqual(dataTable.Rows[1][1], 10);
            Assert.AreEqual(dataTable.Rows[2][0], "2016");
            Assert.AreEqual(dataTable.Rows[2][1], 1);

        }

        //Sprint2 - respuestasPorMeses()
        [TestMethod]
        public void respuestasPorMeses()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.respuestasPorMeses();
            Assert.AreEqual(dataTable.Rows[0][0], "Febrero");
            Assert.AreEqual(dataTable.Rows[0][1], 7);
            Assert.AreEqual(dataTable.Rows[8][0],"Diciembre");
            Assert.AreEqual(dataTable.Rows[8][1], 1);
            //Comprobamos que si añadimos una resuesta de diciembre nos los contabiliza
            Valoracion valoracion = new Valoracion(4, "muy buena", new DateTime(2016, 12, 12, 14, 30, 15));
            db.getEncuesta("Encuesta1").setOpinionCSV(valoracion);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.respuestasPorMeses();
            Assert.AreEqual(dataTable.Rows[0][0], "Febrero");
            Assert.AreEqual(dataTable.Rows[0][1], 7);
            Assert.AreEqual(dataTable.Rows[8][0], "Diciembre");
            Assert.AreEqual(dataTable.Rows[8][1], 2);

        }

        //Sprint2 - respuestasPorSemanas()
        [TestMethod]
        public void respuestasPorSemanas()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.respuestasPorSemanas();
            Assert.AreEqual(dataTable.Rows[0][0], "Domingo");
            Assert.AreEqual(dataTable.Rows[0][1], 10);
            //Comprobamos que si añadimos una resuesta de lunes nos los contabiliza
            Valoracion valoracion = new Valoracion(4, "muy buena", new DateTime(2018, 12, 2, 14, 30, 15));
            db.getEncuesta("Encuesta1").setOpinionCSV(valoracion);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.respuestasPorSemanas();
            Assert.AreEqual(dataTable.Rows[0][0], "Domingo");
            Assert.AreEqual(dataTable.Rows[0][1], 11);
        }

        //Sprint2 - respuestasPorHoras()
        [TestMethod]
        public void respuestasPorHoras()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.respuestasPorHoras();
            Assert.AreEqual(dataTable.Rows[0][0], "8");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            //Comprobamos que si añadimos una resuesta de lunes nos los contabiliza
            Valoracion valoracion = new Valoracion(4, "muy buena", new DateTime(2018, 12, 2, 8, 30, 15));
            db.getEncuesta("Encuesta1").setOpinionCSV(valoracion);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.respuestasPorHoras();
            Assert.AreEqual(dataTable.Rows[0][0], "8");
            Assert.AreEqual(dataTable.Rows[0][1], 5);
        }

        //Sprint2 - mediaPorEncuesta()
        [TestMethod]
        public void mediaPorEncuesta()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.mediaPorEncuesta();
            Assert.AreEqual(dataTable.Rows[0][0],"Encuesta6");
            Assert.AreEqual(dataTable.Rows[0][1], (double)15/(double)4);
            //Comprobamos que si añadimos una nueva respuesta con un valor de 4 aumenta la media
            db.getEncuesta("Encuesta6").setOpinion(4);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.mediaPorEncuesta();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta6");
            Assert.AreEqual(dataTable.Rows[0][1], (double)19 / (double)5);
            //Comprobamos que al borrar una encuesta con respuestas se reduce la media
            db.borrarEncuesta("Encuesta6");
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.mediaPorEncuesta();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], (double)11 / (double)4);
        }

        //Sprint2 - medianaPorEncuesta()
        [TestMethod]
        public void medianaPorEncuesta()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.medianaPorEncuesta();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta6");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            //añadimos una nueva respuesta con un valor de 4
            db.getEncuesta("Encuesta1").setOpinion(4);
            db.getEncuesta("Encuesta1").setOpinion(4);
            db.getEncuesta("Encuesta1").setOpinion(4);
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.medianaPorEncuesta();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
            //borrar una encuesta top del ranking
            db.borrarEncuesta("Encuesta1");
            estadisticas = db.getEstadisticas();
            dataTable = estadisticas.medianaPorEncuesta();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta6");
            Assert.AreEqual(dataTable.Rows[0][1], 4);
        }

        //Sprint2 - desvEstPorEncuesta()
        [TestMethod]
        public void desvEstPorEncuesta()
        {
            DataBase db = new DataBase();

            //Comprobamos que cargo los valores por defecto
            Estadisticas estadisticas = db.getEstadisticas();
            DataTable dataTable = estadisticas.desvEstPorEncuesta();
            Assert.AreEqual(dataTable.Rows[0][0], "Encuesta1");
        }
    }
}
