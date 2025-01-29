using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public static class Utils
    {
        // Función para redondear hacia abajo
        public static int RoundResto(int number)
        {
            return (int)Math.Floor((double)number);
        }

        // Función para redondear hacia arriba
        public static int RoundMuuney(int number)
        {
            return (int)Math.Ceiling((double)number);
        }
    }
}