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
                aux.Add((double)v.Valor);
            }
            aux.Sort();
            int indice = (int)Math.Round((double)aux.Count / (double)2) - 1;
            return aux[indice];
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

        public DataTable respuestasPorAnios()
        {
            DataTable dataTable = new DataTable();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

            dataTable.Columns.Add("Anio", typeof(string));
            dataTable.Columns.Add("Numero", typeof(int));

            foreach(Valoracion v in valoraciones)
            {
                string clave = v.Fecha.Year.ToString();
                if (diccionario.ContainsKey(clave))
                {
                    int valor = diccionario[clave];
                    diccionario[clave] = ++valor;
                }
                else
                {
                    diccionario.Add(clave, 1);
                }
            }
            foreach(string clave in diccionario.Keys)
            {
                dataTable.Rows.Add(clave, diccionario[clave]);
            }
            DataView dv = dataTable.DefaultView;
            dv.Sort = "Numero desc";
            return dv.ToTable();
        }

        public string mes(string numero)
        {
            if (numero == "1")
            {
                return "Enero";
            }
            if (numero == "2")
            {
                return "Febrero";
            }
            if (numero == "3")
            {
                return "Marzo";
            }
            if (numero == "4")
            {
                return "Abril";
            }
            if (numero == "5")
            {
                return "Mayo";
            }
            if (numero == "6")
            {
                return "Junio";
            }
            if (numero == "7")
            {
                return "Julio";
            }
            if (numero == "8")
            {
                return "Agosto";
            }
            if (numero == "9")
            {
                return "Septiembre";
            }
            if (numero == "10")
            {
                return "Octubre";
            }
            if (numero == "11")
            {
                return "Noviembre";
            }
            return "Diciembre";
        }

        public DataTable respuestasPorMeses()
        {
            DataTable dataTable = new DataTable();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

            dataTable.Columns.Add("Mes", typeof(string));
            dataTable.Columns.Add("Numero", typeof(int));

            foreach(Valoracion v in valoraciones)
            {
                string clave = mes(v.Fecha.Month.ToString());
                if (diccionario.ContainsKey(clave))
                {
                    int valor = diccionario[clave];
                    diccionario[clave] = ++valor;
                }
                else
                {
                    diccionario.Add(clave, 1);
                }
            }
            foreach(string clave in diccionario.Keys)
            {
                dataTable.Rows.Add(clave, diccionario[clave]);
            }
            DataView dv = dataTable.DefaultView;
            dv.Sort = "Numero desc";
            return dv.ToTable();
        }

        public string dias(string dia)
        {
            if (dia == "Monday")
            {
                return "Lunes";
            }
            if (dia == "Tuesday")
            {
                return "Martes";
            }
            if (dia == "Wednesday")
            {
                return "Miercoles";
            }
            if (dia == "Thursday")
            {
                return "Jueves";
            }
            if (dia == "Friday")
            {
                return "Viernes";
            }
            if (dia == "Saturday")
            {
                return "Sabado";
            }
            return "Domingo";
        }

        public DataTable respuestasPorSemanas()
        {
            DataTable dataTable = new DataTable();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

            dataTable.Columns.Add("Semana", typeof(string));
            dataTable.Columns.Add("Numero", typeof(int));

            foreach (Valoracion v in valoraciones)
            {
                string clave = dias(v.Fecha.DayOfWeek.ToString());
                if (diccionario.ContainsKey(clave))
                {
                    int valor = diccionario[clave];
                    diccionario[clave] = ++valor;
                }
                else
                {
                    diccionario.Add(clave, 1);
                }
            }
            foreach (string clave in diccionario.Keys)
            {
                dataTable.Rows.Add(clave, diccionario[clave]);
            }
            DataView dv = dataTable.DefaultView;
            dv.Sort = "Numero desc";
            return dv.ToTable();
        }

        public DataTable respuestasPorHoras()
        {
            DataTable dataTable = new DataTable();
            Dictionary<string, int> diccionario = new Dictionary<string, int>();

            dataTable.Columns.Add("Hora", typeof(string));
            dataTable.Columns.Add("Numero", typeof(int));

            foreach (Valoracion v in valoraciones)
            {
                string clave = v.Fecha.Hour.ToString();
                if (diccionario.ContainsKey(clave))
                {
                    int valor = diccionario[clave];
                    diccionario[clave] = ++valor;
                }
                else
                {
                    diccionario.Add(clave, 1);
                }
            }
            foreach (string clave in diccionario.Keys)
            {
                dataTable.Rows.Add(clave, diccionario[clave]);
            }
            DataView dv = dataTable.DefaultView;
            dv.Sort = "Numero desc";
            return dv.ToTable();
        }

        public DataTable mediaPorEncuesta()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Media", typeof(double));

            foreach(Encuesta e in encuestas)
            {
                double media = 0;
                foreach(Valoracion v in e.getOpiniones())
                {
                    media += v.Valor;
                }
                dataTable.Rows.Add(e.Nombre, (double)media / (double)e.getOpiniones().Count);
            }
            DataView dv = dataTable.DefaultView;
            dv.Sort = "Media desc";
            return dv.ToTable();
        }

        public DataTable medianaPorEncuesta()
        {
            DataTable dataTable = new DataTable();
            
            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Mediana", typeof(int));

            foreach(Encuesta e in encuestas)
            {
                List<double> aux = new List<double>();
                foreach (Valoracion v in e.getOpiniones())
                {
                    aux.Add(v.Valor);
                }
                aux.Sort();
                int indice = (int)Math.Round((double)aux.Count / (double)2) - 1;
                dataTable.Rows.Add(e.Nombre, aux[indice]);
            }
            DataView dv = dataTable.DefaultView;
            dv.Sort = "Mediana desc";
            return dv.ToTable();
        }

        public DataTable desvEstPorEncuesta()
        {
            DataTable dataTable = new DataTable();
            List<double> medias = new List<double>();
            double media = 0;
            int indice = -1;

            dataTable.Columns.Add("Encuesta", typeof(string));
            dataTable.Columns.Add("Desviacion", typeof(int));

            foreach(Encuesta e in encuestas)
            {
                media = 0;
                foreach(Valoracion v in e.getOpiniones())
                {
                    media += v.Valor;
                }
                medias.Add((double)media / (double)e.getOpiniones().Count);
            }

            foreach (Encuesta e in encuestas)
            {
                double datos = 0;
                ++indice;
                foreach (Valoracion v in e.getOpiniones())
                {
                    datos += Math.Pow((double)v.Valor - (double)medias[indice], 2);
                }
                dataTable.Rows.Add(e.Nombre, (double)Math.Sqrt(datos / e.getOpiniones().Count));
            }
            DataView dv = dataTable.DefaultView;
            dv.Sort = "Desviacion desc";
            return dv.ToTable();
        }
    }
}