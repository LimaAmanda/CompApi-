using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompApi.Models;
using CompAPI.DAL;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

 namespace CompApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TiposCompromissoController : Controller
    {
        private readonly IConfiguration _config;
        public TiposCompromissoController(IConfiguration config)
        {
            _config = config;
        }

        //Método de Banco usando Dapper abaixo:

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {            
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();   

                sql.Append("SELECT ID as Id, TX_DESCRICAO as DescricaoCompromisso ");       
                         
                sql.Append("FROM TB_TIPO_COMPROMISSO ");
                
                List<TipoCompromisso> lista = (await conexao.QueryAsync<TipoCompromisso>(sql.ToString())).ToList();
                return Ok(lista);                    
            }
        }   
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            TipoCompromisso p = null;
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();
                StringBuilder sql = new StringBuilder();    
                sql.Append("SELECT ID as Id, TX_DESCRICAO as DescricaoCompromisso ");
                sql.Append("FROM TB_TIPO_COMPROMISSO WHERE ID = @Id "); 
                p = await conexao.QueryFirstOrDefaultAsync<TipoCompromisso>(sql.ToString(), new {Id = id});
                if (p != null)
                    return Ok(p);
                else
                    return NotFound("Tipo de Compromisso não encontrado.");        
            }            
        }

    }
}