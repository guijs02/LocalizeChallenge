namespace Localiza.Core.Responses
{
    public record ReceitaWsResponse(
        string Nome,
        string Fantasia,
        string Cnpj,
        string Situacao,
        string Abertura,
        string Tipo,
        string Natureza_Juridica,
        List<AtividadePrincipal> Atividade_Principal,
        string Logradouro,
        string Numero,
        string Complemento,
        string Bairro,
        string Municipio,
        string Uf,
        string Cep
    );

    public class AtividadePrincipal
    {
        public string Code { get; set; } = string.Empty; 
        public string Text { get; set; } = string.Empty;
    }
}
