using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public static class Utils
    {
        // Función para redondear hacia abajo
        public static int RoundResto(double number)
        {
            return (int)Math.Floor(number);
        }

        // Función para redondear hacia arriba
        public static int RoundMuuney(double number)
        {
            return (int)Math.Ceiling(number);
        }
    }
}