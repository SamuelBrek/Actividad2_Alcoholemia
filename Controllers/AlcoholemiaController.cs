using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlcoholemiaApi.Infraestructure;
using AlcoholemiaApi.Domain;

/*
Nombre de la escuela: Universidad Tecnologica Metropolitana
Asignatura: Aplicaciones Web Orientadas a Servicios
Nombre del Maestro: Chuc Uc Joel Ivan
Nombre de la actividad: Actividad 3 (Alcoholemia)
Nombre del alumno: Brek Mejia Samuel Alexander
Cuatrimestre: 4
Grupo: B
Parcial: 2
*/
//Al igual que mi anterior ejercicio, dejo evidencia de la otra forma en que lo llegué a formular.

namespace AlcoholemiaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlcoholemiaController : ControllerBase
    {
        
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var repository = new BebidaRepository();
            var drinks = repository.GetAll();
            return Ok(drinks);
        }
        
        [HttpGet]

        [Route("TestAlcoholemia/{nombre}/{cantidad}/{peso}")]
        public IActionResult TestAlcoholemia(string nombre, int cantidad, int peso)
        {
            var Mensaje = "";
            var repository = new BebidaRepository();
            var validacion = repository.ObtenerMililitros(nombre, cantidad, peso);
            if (validacion == -100){
                Mensaje = ($"La bebida ingresada {nombre} es incorrecta.");
            }
            else if(validacion > 0.8){
                Mensaje = ($"Tiene una cantidad de alcohol en la sangre de {validacion.ToString("##,##0.0000")}, se debe solicitar apoyo para el conductor");
            }
            else{
                Mensaje = ($"Tenga un excelente viaje!");
            }
            return Ok(Mensaje);
        }
        /*
        [HttpGet]
        [Route("EjecutarCalculo/{nombre}/{cantidad}/{peso}")]
        public IActionResult EjecutarCalculo(string nombre, int cantidad, int peso){
            var Mensaje = "";
            Bebida bebida = new Bebida();
            bebida.Nombre = nombre.ToLower();
            switch(bebida.Nombre){
                case "cerveza":
                    bebida.Mililitros = 330;
                    bebida.Grado = 0.05;
                break;
                case "vino":
                    bebida.Mililitros = 100;
                    bebida.Grado = 0.12;
                break;
                case "vermú":
                    bebida.Mililitros = 70;
                    bebida.Grado = 0.17;
                break;
                case "licor":
                    bebida.Mililitros = 45;
                    bebida.Grado = 0.23;
                break;
                case "brandy":
                    bebida.Mililitros = 45;
                    bebida.Grado = 0.38;
                break;
                case "combinado":
                    bebida.Mililitros = 50;
                    bebida.Grado = 0.38;
                break;
                default:
                    Mensaje = ($"La bebida ingresada: {nombre} no existe, ingrese Cerveza, Vino, Vermú, Licor, Brandy o Combinado");
                    return Ok(Mensaje);
            }
            double Talcohol = (bebida.Mililitros * cantidad); //Total de alcohol consumido
            double PorCerveza = bebida.Grado*Talcohol; //Calcula el total de alcohol por cerveza consumido
            double DirectoSangre = 0.15*PorCerveza; //Calcular la cantidad de alcohol que pasa directo a la sangre
            double Etanol = 0.789*DirectoSangre; //Calcular la masa del etanol en sangre
            double VolumenPeso = 0.08*peso; //Calcular el volumen de la sangre de la persona considerando su peso
            double Alcoholemia = Etanol/VolumenPeso; //Calcular el volumen de alcohol en la sangre (Alcoholemia)
            if (Alcoholemia > 0.8){
                Mensaje = ($"Tiene una cantidad de alcohol en la sangre de {Alcoholemia.ToString("##,##0.0000")}, se debe solicitar apoyo para el conductor");
            }
            else{
                Mensaje = ($"Tenga un excelente viaje!");
            }
            return Ok(Mensaje);

        }*/
    }
}