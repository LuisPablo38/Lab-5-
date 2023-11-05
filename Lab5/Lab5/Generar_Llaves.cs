using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab5
{
    class Generar_Llaves
    {
        static Random random = new Random(); 

        static bool EsPrimo(BigInteger primo)
        {
            if (primo <= 1)
            {
                return false;
            }
            if (primo <= 3)
            {
                return true; 
            }
            if (primo % 2 == 0 || primo % 3 == 0)
            {
                return false;
            }
            for (BigInteger i = 5; i*i <= primo; i+=6)
            {
                if (primo % i == 0 || primo % (i + 2) == 0)
                    return false;
            }
            return true; 
        }

        public static BigInteger GenerarNumero(BigInteger maximo)
        {
            BigInteger numeroAleatorio;
            do
            {
                byte[] randomByte = new byte[maximo.ToByteArray().Length];
                random.NextBytes(randomByte);
                numeroAleatorio = new BigInteger(randomByte) % maximo;

            } while (!EsPrimo(numeroAleatorio));
            return numeroAleatorio; 
        }

        public static BigInteger MultiNumeros(BigInteger numero1, BigInteger numero2)
        {
            BigInteger n = numero1 * numero2; 
            return n; 
        }

        public static BigInteger GenerarZ(BigInteger numero1, BigInteger numero2)
        {
            BigInteger z = (numero1 - 1) * (numero2 - 1);
            return z; 
        }

        public static BigInteger EncontrarCoprimo(BigInteger numero)
        {
            BigInteger coprimo;

            do
            {
                coprimo = new BigInteger(random.Next(2, int.MaxValue)); 

            } while (MCD(numero,coprimo) !=1);


            return coprimo; 
        }

        public static BigInteger MCD(BigInteger a, BigInteger b)
        {
            while (b!= 0)
            {
                BigInteger temp = b;
                b = a % b;
                a = temp; 
            }
            return a; 
        }

        public static BigInteger ClavePrivada (BigInteger k, BigInteger z)
        {
            for (BigInteger l = 1; l<z; l++)
            {
                if((k*l)%z == 1)
                {
                    return l;
                }
            }

            throw new Exception("No se encontro una clave valida");
        }

        //TXT a bigInteger 
        public static BigInteger TxtAInteger (string texto)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            return new BigInteger(bytes);
        }

        //Integer a txt para comprobar si es lo mismo 
        public static string BigIntegerToText(BigInteger number)
        {
            byte[] bytes = number.ToByteArray();
            return Encoding.UTF8.GetString(bytes);
        }

        public static BigInteger Cifrado(BigInteger M, BigInteger K, BigInteger N)
        {
            BigInteger C;
            C = BigInteger.ModPow(M, K, N);
            return C; 
        }

        public static BigInteger DESCifrado(BigInteger M, BigInteger J, BigInteger N)
        {
            BigInteger DC;
            DC = BigInteger.ModPow(M, J, N);
            return DC;
        }
    }
}
