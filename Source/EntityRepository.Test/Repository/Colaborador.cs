using System;

namespace EntityRepository.Test.Repository
{
    public class Colaborador
    {
        public Guid? Id { get; set; }
        public Guid? CargoId { get; set; }
        public string? Matricula { get; set; }
        public string? Cpf { get; set; }
        public string? Nome { get; set; }
        public string? Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Email { get; set; }
        public string? EmailPessoal { get; set; }
        public string? Situacao { get; set; }

        public Cargo Cargo { get; set; }
    }
}
