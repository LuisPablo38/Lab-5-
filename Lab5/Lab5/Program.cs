using System;
using System.Numerics;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;

namespace Lab5
{
    class Program
    {
        private static string Contraseña;
        private static string ContraSegura; 
        static void Main(string[] args)
        {
            var records = new List<Estructura>();
            using (var reader = new StreamReader(@"D:\Users\luisp\Downloads\InLab5\input.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 2 && parts[0] == "INSERT")
                    {
                        var jsonData = parts[1];
                        var registro = JsonConvert.DeserializeObject<Estructura>(jsonData);
                        records.Add(registro);
                    }
                    else if (parts.Length == 2 && parts[0] == "PATCH")
                    {
                        var jsonData = parts[1];
                        var registro = JsonConvert.DeserializeObject<Estructura>(jsonData);

                        List<Estructura> updatedRecords = new List<Estructura>();

                        foreach (var item in records)
                        {
                            if (item.dpi == registro.dpi)
                            {
                                updatedRecords.Add(registro);
                            }
                            else
                            {
                                updatedRecords.Add(item);
                            }
                        }
                        // Asigna la lista actualizada a `records`
                        records = updatedRecords;
                    }
                    else if (parts.Length == 2 && parts[0] == "DELETE"){

                        var jsonData = parts[1];
                        var registro = JsonConvert.DeserializeObject<Estructura>(jsonData);
                        foreach (var item in records)
                        {
                            if (item.dpi == registro.dpi)
                            {
                                records.Remove(item);

                                break;
                            }
                        }

                    }
                }
            }

            Console.WriteLine("Finalizar carga ");
            Console.WriteLine("     Autenticacion   ");
            Console.WriteLine(" Ingrese su nombre: ");
            string NomBuscar = Console.ReadLine();
            string nomArchCDesi = "Descifrado contraseña " + NomBuscar;
            string rutaArchivoDesi = Path.Combine(@"C:\Users\luisp\Desktop\Landivar\Cuarto año\Octavo Ciclo\Estructuras 2\Lab\Lab5\", nomArchCDesi);
            foreach (var registro in records)
            {
                if (NomBuscar == registro.name )
                {
                    Console.Clear(); 
                    Console.WriteLine("Usuario encontrado de forma correcta");
                    Console.WriteLine(" ");
                    
                }
            }
            Console.WriteLine("La contraseña del usuario " + NomBuscar + " es: ");
            Contraseña = NomBuscar;


            BigInteger maximo = BigInteger.Pow(9, 4); // Valor máximo del rango ( ,num de ceros )
            BigInteger numeroPrimoAleatorio1 = Generar_Llaves.GenerarNumero(maximo); //P
            BigInteger numeroPrimoAleatorio2 = Generar_Llaves.GenerarNumero(maximo); //Q
            BigInteger ResMulti = Generar_Llaves.MultiNumeros(numeroPrimoAleatorio1, numeroPrimoAleatorio2); // Esta es mi variable N
            BigInteger ResMulti2Z = Generar_Llaves.GenerarZ(numeroPrimoAleatorio1, numeroPrimoAleatorio2);
            BigInteger Coprimo = Generar_Llaves.EncontrarCoprimo(ResMulti2Z); //K
            BigInteger Key_Privada = Generar_Llaves.ClavePrivada(Coprimo, ResMulti2Z); //J
            BigInteger M = Generar_Llaves.TxtAInteger(Contraseña);
            BigInteger C = Generar_Llaves.Cifrado(M, Coprimo, ResMulti); //Metodo para aplicar RSA
            /*Console.WriteLine("Número primo aleatorio menor a  " + maximo + " : " + numeroPrimoAleatorio1);
            Console.WriteLine("Número primo 2 aleatorio menor a  " + maximo + " : " + numeroPrimoAleatorio2);
            Console.WriteLine("Resultado multiplicacion N : " + ResMulti);
            Console.WriteLine("Resultado Z : " + ResMulti2Z);
            Console.WriteLine("Coprimo K : " + Coprimo);
            Console.WriteLine("Llave privada : " + Key_Privada);
            Console.WriteLine(" " );*/
            //Console.WriteLine("valor de M" + M); 
            Console.WriteLine("Contraseña cifrada: " + C);
            Console.WriteLine(" ");
            Console.WriteLine(" Por favor ingrese la contraseña para validar su identidad ");
            string ContraIngresada = Console.ReadLine();
            ContraSegura = Contraseña + 2023; 
            if(ContraIngresada == ContraSegura)
            {
                Console.Clear();
                Console.WriteLine("Idetidad confirmada Bienvenido a Talent Hub");
                EscribiRArchi(rutaArchivoDesi ); 
            }
            else
            {
                Console.WriteLine("Contraseña no coincide, adios");
            }
        }
        static void EscribiRArchi( string rutaDestino)
        {
            try
            {
 
                File.WriteAllText(rutaDestino,ContraSegura);
            }
            catch (Exception)
            {
                Console.WriteLine("Error al escribir en el archivo");
            }
        }
    }
}
