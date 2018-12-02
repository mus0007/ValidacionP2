using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibClases;
using System.Data;

namespace LibClases
{
    public class Estadisticas
    {
        private List<Encuesta> encuestas;
        private List<Valoracion> valoraciones;

        public Estadisticas(List<Encuesta> encuestas, List<Valoracion> valoraciones)
        {
            this.encuestas = encuestas;
            this.valoraciones = valoraciones;
        }

        public DataTable estadoEncuestas()
        {
            DataTable dataTable = new DataTable();
            int activas = 0;
            int desactivas = 0;

            foreach(Encuesta e in encuestas)
            {
                if(e.Activa)
                {
                    ++activas;
                }
                else
                {
                    ++desactivas;
                }
            }
            dataTable.Columns.Add("Estado", typeof(string));
            dataTable.Columns.Add("Numero", typeof(int));
            dataTable.Rows.Add("activas", activas);
            dataTable.Rows.Add("desactivas", desactivas);

            return dataTable;
        }

        public DataTable numeroRespuestas()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Numero", typeof(int));

            foreach(Encuesta e in encuestas)
            {
                dataTable.Rows.Add(e.Nombre, e.getOpiniones().Count);
            }

            return dataTable;
        }

        public short numeroEncuestas()
        {
            short numero = 0;

            foreach(Encuesta e in encuestas)
            {
                if(e.getOpiniones().Count != 0)
                {
                    ++numero;
                }
            }
            return numero;
        }
    }
}