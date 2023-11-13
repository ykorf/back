// Usuario.cs
public class Usuario
{
    public int Id { get; set; }
    public string NomeUsuario { get; set; }
    public string Senha { get; set; }
}

// Funcionario.cs
public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CodigoContrato { get; set; }
    public decimal SalarioBase { get; set; }
    public DateTime DataAdmissao { get; set; }
    public List<Beneficio> Beneficios { get; set; }
    public List<Contrato> Contratos { get; set; }
}

// Beneficio.cs e Frequencia.cs permanecem iguais

// Contrato.cs
public class Contrato
{
    public int Id { get; set; }
    public string CodigoContrato { get; set; }
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }
    public string Cargo { get; set; }
    public decimal SalarioBase { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
}
