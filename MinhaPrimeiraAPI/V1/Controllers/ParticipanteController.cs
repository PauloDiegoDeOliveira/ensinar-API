using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraAPI.Models;
using MinhaPrimeiraAPI.Repositories.Interfaces;

namespace MinhaPrimeiraAPI.V1.Controllers
{
    // O que é CRUD são as quatro operações básicas utilizadas em bases de dados relacionais.
    // Create(criar), Read(ler), Update(atualizar) e Delete(apagar)

    // O que é "API" interface de programação de aplicações, conjunto de rotinas, protocolos e ferramentas para construir aplicações.
    // Processa as solicitações do usuário.

    // "Controllers" Responsável por controllar a maneira como um usuário interage.
    // "Controller" entre colchetes, significa que tem que informar o nome do controlador.

    // [ApiVersion("1.0", Deprecated = true)] // Exemplo de obsoleta que existe uma nova versão para trabalhar
    [ApiVersion("1.0")] // Informa a versão da API
    [Route("/v{version:apiVersion}/participantes")] // Atributo "Route" define uma rota padrão para poder acessar essa API via url.
    [ApiController] // "ApiController" decora o controlador, ele define recursos e comportamentos com o objetivo de melhorar a experiência do desenvolvedor para criar as API's.
    public class ParticipanteController : ControllerBase // "ControllerBase ou Controller" fornece muitas propriedades e métodos úteis para lidar com requisições HTTP.
    {
        // Injeção de dependêcia - injeta o repositório na controller
        private readonly IParticipanteRepository participanteRepository;

        // Construtor
        public ParticipanteController(IParticipanteRepository participanteRepository)
        {
            this.participanteRepository = participanteRepository;
        }

        // Métodos usados para processar as requisições recebidas via http.

        // "ActionResult" concreto, um tipo específico de retorno.
        // "IActionResult" abstração, vários tipos de retorno.
        // "FromBody" atributo para vincular dados a partir do Body do request.
        // "Endpoint" endereço onde será acessado.
        // "HttpGet" Verbo.

        [HttpGet] // Listar tudo
        public IActionResult GetAllAsync()
        {
            IEnumerable<Participante> participantes = participanteRepository.GetAllAsync();
            return Ok(participantes);
        }

        [HttpPost] // Inserir
        public async Task<IActionResult> PostAsync([FromBody] Participante participante)
        {
            await participanteRepository.PostAsync(participante);

            return Ok(new
            {
                mensagem = "Participante criado com sucesso!",
                sucesso = true,
            });
        }

        [HttpPut] // Atualizar
        public async Task<IActionResult> PutAsync([FromBody] Participante participante)
        {
            Participante atualizado = await participanteRepository.PutAsync(participante);

            if (atualizado == null)
                return BadRequest(new { mensagem = "Nenhum participante foi encontrado com o id informado." });

            return Ok(new
            {
                mensagem = "Participante atualizado com sucesso!",
                sucesso = true,
            });
        }

        [HttpDelete("{id}")] // Remover
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            Participante removido = await participanteRepository.DeleteAsync(id);

            if (removido == null)
                return BadRequest(new { mensagem = "Nenhum participante foi encontrado com o id informado." });

            return Ok(new
            {
                mensagem = "Participante removido com sucesso.",
                sucesso = true,
            });
        }

        [HttpGet("{id:guid}")] // Obter por id
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Participante consultaParticipante = await participanteRepository.GetByIdAsync(id);

            if (consultaParticipante == null)
                return BadRequest(new { mensagem = "Nenhum participante foi encontrado com o id informado." });

            return Ok(consultaParticipante);
        }

        [HttpGet("nome")] // Obter nome
        public async Task<IActionResult> GetByNomeAsync(string nome)
        {
            IList<Participante> consultaNomeParticipante = await participanteRepository.GetByNomeAsync(nome);

            if (consultaNomeParticipante.Count == 0)
                return BadRequest(new { mensagem = "Nenhum participante foi encontrado com o nome informado." });

            return Ok(consultaNomeParticipante);
        }
    }
}