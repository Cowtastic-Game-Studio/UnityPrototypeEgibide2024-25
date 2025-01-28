using System;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public static class Utils
    {
        // Función para redondear hacia abajo
        public static int RoundDown(int number)
        {
            return (int)Math.Floor((double)number);
        }

        // Función para redondear hacia arriba
        public static int RoundUp(int number)
        {
            return (int)Math.Ceiling((double)number);
        }
    }
}