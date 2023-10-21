using System;
using GestionHerramientas.Models;

namespace GestionHerramientas.Util
{
    public class StringUtils
    {
        /**
		 * Valida si el String dado es null, esta vacio o solo se compone de espacio en blanco
		 * 
		 * Retorna True si y solo si el argumento str cumple con alguno de los criterios de evaluacion
		 */
        public static bool IsEmpty(String? str)
        {
            Console.WriteLine("Str value " + str);

            return String.IsNullOrEmpty(str)
                     || String.IsNullOrWhiteSpace(str);
        }
    }
}

