using EntityRepository.Abstractions.Helpers;
using EntityRepository.Core;
using EntityRepository.Test.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EntityRepository.Test
{
    [Collection("Integration")]
    public class ReadRepositoryTest
    {
        [Fact(Skip = "Integration Test")]
        public async Task FindById()
        {
            var dbContext = new ColaboradorDbContext();

            var readRepository = new ReadRepository(dbContext);

            var resultado = await readRepository.FindById<Colaborador>(new Guid("e6709f81-a0b3-4381-92e5-00001582957b"), new CancellationToken());

            Assert.NotNull(resultado);
        }

        [Fact(Skip = "Integration Test")]
        public async Task FindByFilter()
        {
            var dbContext = new ColaboradorDbContext();

            var readRepository = new ReadRepository(dbContext);

            var resultado = await readRepository.FindByFilter<Colaborador>(x => x.Situacao == "Ferias",
                                                                           o => o.AddOrder(x => x.Nome, OrderByType.Ascending),
                                                                           j => j.Include(x => x.Cargo),
                                                                           s => new Colaborador { Nome = s.Nome, Cargo = s.Cargo },
                                                                           new CancellationToken());

            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Nome);
            Assert.NotNull(resultado.Cargo);
            Assert.Null(resultado.CargoId);
            Assert.Null(resultado.Id);
            Assert.Null(resultado.Matricula);
            Assert.Null(resultado.Situacao);
            Assert.Null(resultado.Apelido);
            Assert.Null(resultado.Email);
            Assert.Null(resultado.EmailPessoal);
            Assert.Null(resultado.Cpf);
            Assert.Null(resultado.DataNascimento);
        }
    }
}
