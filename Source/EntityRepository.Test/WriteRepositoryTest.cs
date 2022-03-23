using EntityRepository.Core;
using EntityRepository.Test.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EntityRepository.Test
{
    [Collection("Integration")]
    public class WriteRepositoryTest
    {
        [Fact(Skip = "Integration Test")]
        public async Task Insert()
        {
            var dbContext = new ColaboradorDbContext();

            var writeRepository = new WriteRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);

            var colaborador = new Colaborador
            {
                Id = Guid.NewGuid(),
                CargoId = new Guid("2344f412-8ebe-4f46-a425-0004b7e779d4"),
                Apelido = "Teste",
                Cpf = "33411895055",
                DataNascimento = new DateTime(2020, 1, 1),
                Email = "teste@localiza.com",
                EmailPessoal = "teste_pessoal@localiza.com",
                Matricula = "999999",
                Nome = "Teste Localiza"
            };

            var resultado = await writeRepository.Insert(colaborador, new CancellationToken());
            await unitOfWork.CommitAsync();

            Assert.NotNull(resultado);
        }
    }
}
