namespace Localiza.Core.Responses
{
    public readonly record struct ReceitaWsResponse(
        string Nome,
        string Fantasia,
        string Cnpj,
        string Situacao,
        string Abertura,
        string Tipo,
        string NaturezaJuridica,
        IEnumerable<AtividadePrincipal> Atividade_Principal,
        string Logradouro,
        string Numero,
        string Complemento,
        string Bairro,
        string Municipio,
        string Uf,
        string Cep
    );

    public struct AtividadePrincipal
    {
        public string Codigo;
        public string Texto;
    }
}
