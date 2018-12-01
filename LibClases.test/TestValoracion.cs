using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibClases;

namespace LibClases.test
{
    [TestClass]
    public class TestValoracion
    {
        [TestMethod]
        public void TestConstructorSetyGet()
        {
            // Prueba Valor correcto
            Valoracion valoracion = new Valoracion(4, "muy buena", new DateTime(2015,5,16,14,30,15));
            Assert.AreEqual(valoracion.Valor, 4);
            Assert.AreEqual(valoracion.Opinion, "muy buena");

            // Prueba Valor mayor que 4
            Valoracion valoracion1 = new Valoracion(10, "la mejor", new DateTime(2016, 5, 16, 14, 30, 15));
            Assert.AreEqual(valoracion1.Valor, 0);
            Assert.AreEqual(valoracion1.Opinion, "la mejor");

            // Prueba Valor menor que 1
            Valoracion valoracion2 = new Valoracion(-1, "la peor", new DateTime(2017, 5, 16, 14, 30, 15));
            Assert.AreEqual(valoracion2.Valor, 0);
            Assert.AreEqual(valoracion2.Opinion, "la peor");

            // Prueba metodo set valor
            valoracion2.Valor = 4;
            Assert.AreEqual(valoracion2.Valor, 4);

            // Prueba metodo set opinion
            valoracion2.Opinion = "la mejor";
            Assert.AreEqual(valoracion2.Opinion, "la mejor");

            //Prueba metodo get fecha
            Assert.AreEqual(valoracion.Fecha.ToString(), "16/05/2015 14:30:15");
            Assert.AreEqual(valoracion1.Fecha.ToString(), "16/05/2016 14:30:15");
            Assert.AreEqual(valoracion2.Fecha.ToString(), "16/05/2017 14:30:15");

            //Prueba metodo set fecha
            valoracion.Fecha = new DateTime(2018, 12, 1, 19, 10, 15);
            valoracion1.Fecha = new DateTime(2018, 12, 1, 19, 11, 15);
            valoracion2.Fecha = new DateTime(2018, 12, 1, 19, 12, 15);

            Assert.AreEqual(valoracion.Fecha , new DateTime(2018, 12, 1, 19, 10, 15));
            Assert.AreEqual(valoracion1.Fecha, new DateTime(2018, 12, 1, 19, 11, 15));
            Assert.AreEqual(valoracion2.Fecha, new DateTime(2018, 12, 1, 19, 12, 15));

        }

    }
}

