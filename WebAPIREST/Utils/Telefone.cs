using System;
using System.Text.RegularExpressions;

namespace WebAPIREST.Utils
{
    public static class TelefoneUtils
    {
        public enum TipoTelefone
        {
            Celular,
            Comercial,
            Residencial
        }

        public static bool IsTipoCorreto(string tipo)
        {
            return Enum.IsDefined(typeof(TipoTelefone), tipo);
        }
    }
}
