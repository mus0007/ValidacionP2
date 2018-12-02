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
        }
    }
}
