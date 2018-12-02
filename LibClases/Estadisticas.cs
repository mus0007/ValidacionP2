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

        public DataTable rankingEncuestasPorRespuestas()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Numero", typeof(int));

            foreach (Encuesta e in encuestas)
            {
                dataTable.Rows.Add(e.Nombre, e.getOpiniones().Count);
            }

            DataView dv = dataTable.DefaultView;
            dv.Sort = "Numero desc";
            return dv.ToTable();
        }

        public DataTable rankingEncuestasPorValoracion()
        {
            DataTable dataTable = new DataTable();
            int nota_max = 1;
            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Nota", typeof(int));

            foreach (Encuesta e in encuestas)
            {
                nota_max = 1;
                foreach (Valoracion v in e.getOpiniones())
                {
                    if(nota_max < v.Valor)
                    {
                        nota_max = v.Valor;
                    }
                }
                dataTable.Rows.Add(e.Nombre, nota_max);
            }

            DataView dv = dataTable.DefaultView;
            dv.Sort = "Nota desc";
            return dv.ToTable();
        }

        public double media()
        {
            double media = 0;

            foreach(Valoracion v in valoraciones)
            {
                media += v.Valor;
            }

            return (double)media / (double)valoraciones.Count;
        }

        public double mediana()
        {
            List<double> aux = new List<double>();

            foreach(Valoracion v in valoraciones)
            {
                aux.Add(v.Valor);
            }
            aux.Sort();
            return aux[aux.Count / 2];
        }

        public double desvest()
        {
            double datos = 0;
            foreach(Valoracion v in valoraciones)
            {
                datos += Math.Pow((double)v.Valor-(double)media(), 2);
            }
            return (double)Math.Sqrt(datos / (valoraciones.Count - 1));
        }

        public DataTable numRespRangosPorEncuesta(string nombre)
        {
            DataTable dataTable = new DataTable();
            int minimo = 0;
            int maximo = 0;

            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Minimo", typeof(int));
            dataTable.Columns.Add("Maximo", typeof(int));

            foreach(Encuesta e in encuestas)
            {
                if(e.Nombre == nombre && e.getOpiniones().Count != 0)
                {
                    minimo = 5;
                    maximo = 0;
                    foreach(Valoracion v in e.getOpiniones())
                    {
                        if(maximo < v.Valor)
                        {
                            maximo = v.Valor;
                        }
                        if(minimo > v.Valor)
                        {
                            minimo = v.Valor;
                        }
                    }
                    dataTable.Rows.Add(e.Nombre, minimo, maximo);
                    break;
                }
                if(e.Nombre == nombre && e.getOpiniones().Count == 0)
                {
                    dataTable.Rows.Add(e.Nombre, 0, 0);
                    break;
                }
            }
            return dataTable;
        }

        public DataTable numRespRangos()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Minimo", typeof(int));
            dataTable.Columns.Add("Maximo", typeof(int));

            foreach (Encuesta e in encuestas)
            {
                DataTable aux = numRespRangosPorEncuesta(e.Nombre);
                dataTable.Rows.Add(aux.Rows[0][0], aux.Rows[0][1], aux.Rows[0][2]);
            }

            return dataTable;
        }
    }
}