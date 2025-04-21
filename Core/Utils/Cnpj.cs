namespace Localize.Core.Utils
{
    public static class Cnpj
    {
        public static CnpjValidatorMessage Validate(string number)
        {
            if (string.IsNullOrEmpty(number))
                return new CnpjValidatorMessage(false, "CNPJ não pode ser nulo ou vazio");

            int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

            int soma;
            int resto;
            string digito;
            string tempCnpj;
            number = number.Trim();
            number = number.Replace(".", "").Replace("-", "").Replace("/", "");

            if (number.Length != 14)
                return new CnpjValidatorMessage(false, "Valor diferente de 14 digitos"); ;

            tempCnpj = number[..12];
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return new CnpjValidatorMessage(number.EndsWith(digito), "Válido");
        }
    }
    public class CnpjValidatorMessage
    {
        public CnpjValidatorMessage(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
