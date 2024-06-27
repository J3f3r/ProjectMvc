using Microsoft.AspNetCore.Mvc;
using ProjectMvc.Context;
using ProjectMvc.Models;

namespace ProjectMvc.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AgendaContext _context;
        // vamos carregar as informações do banco de dados do contato na Index por meio do MVC
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var contatos = _context.Contatos.ToList();// transforma seus contatos existentes em uma lista
            return View(contatos);// exibe essa lista
        }

        public IActionResult Criar()// método get, quando entro na página
        {
            return View();
        }

        [HttpPost]//Post quando estou na página, envio as informações
        public IActionResult Criar(Contato contato)
        {
            if(ModelState.IsValid)
            {
                _context.Contatos.Add(contato);// adiciona contato
                _context.SaveChanges();// salva as mudanças no banco
                return RedirectToAction(nameof(Index));// redireciona para Index após criar novo contato
            }

            return View(contato);
        }

        public IActionResult Editar(int id)//métod Get, quando acessar a página
        {
            var contato = _context.Contatos.Find(id);// busca o contato no banco com base no id para ser editado, preenchendo os campos

            if(contato == null)
                return RedirectToAction(nameof(Index));
            
            return View(contato);
        }

        [HttpPost]// pega as aterações feitas e envia para o back end
        public IActionResult Editar(Contato contato)//contato que estou recebendo no campo sendo editado
        {
            var contaBanco = _context.Contatos.Find(contato.Id);

            contaBanco.Nome = contato.Nome;
            contaBanco.Telefone = contato.Telefone;
            contaBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contaBanco);// atualiza a variável
            _context.SaveChanges();//salva no banco de dados

            return RedirectToAction(nameof(Index));// após atualização redireciona para página inicial
        }

        public IActionResult Detalhes(int id)// somente de leitura, não fará nenhuma ação
        {
            var contato = _context.Contatos.Find(id);

            if(contato == null)
                return RedirectToAction(nameof(Index));
            
            return View(contato);
        }

        public IActionResult Deletar(int id)// Get
        {
            var contato = _context.Contatos.Find(id);

            if(contato == null)
                return RedirectToAction(nameof(Index));
            
            return View(contato);
        }

        [HttpPost]
        public IActionResult Deletar(Contato contato)
        {
            var contaBanco = _context.Contatos.Find(contato.Id);

            _context.Contatos.Remove(contaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}