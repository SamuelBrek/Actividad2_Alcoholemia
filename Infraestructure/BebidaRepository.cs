using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using AlcoholemiaApi.Domain;

namespace AlcoholemiaApi.Infraestructure
{
    public class BebidaRepository
    {
        List<Bebidas> _bebidas;
        public BebidaRepository()
        {
            var fileName = "dummy.data.json";
            if(File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                _bebidas = JsonSerializer.Deserialize<IEnumerable<Bebidas>>(json).ToList();
            }
        }

        public IEnumerable<Bebidas> GetAll()
        {
            var query = _bebidas.Select(be => be);
            return query;
        }
        public double ObtenerMililitros(string nombre, int cantidad, int peso)
        {
            var obtener = _bebidas.FirstOrDefault(be => be.Nombre == nombre.ToLower());//.Select(be => be.Mililitros);
            double Alcoholemia;

            if(obtener == null){ //Si el nombre de bebida ingresado no existe
                Alcoholemia = -100; //Valor para validar en el controlador
            }
            else{
                double TotalAlcohol = obtener.Mililitros*cantidad;
                double PorCerveza = obtener.Grado*TotalAlcohol;
                double DirectoSangre = 0.15*PorCerveza;
                double Etanol = 0.789*DirectoSangre;
                double VolumenPeso = 0.08*peso;
                Alcoholemia = Etanol/VolumenPeso;
            }
            
            //var prueba = _bebidas.Select(be => be.Mililitros).Where();
           // var obtener2 = Convert.ToInt32(obtener);
            //var Talcohol = (obtener2 * cantidad);
            return Alcoholemia;
        }
        /*public IEnumerable<Object> GetField( ){
            var query = _bebidas.Select(bebida => new {
                Mililitros = bebida.Mililitros
            });
            return query;
        }*/
        

    }
}