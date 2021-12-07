using System;
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
    public class CompromissosController : ControllerBase
    {
        private readonly IConfiguration _config;
        public CompromissosController(IConfiguration config)
        {
            _config = config;
        }
    

    [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ID as Id, ID_TIPO_COMPROMISSO as TipoCompromissoId, TX_DESCRICAO as Descricao, TX_LOCALIZACAO as Localizacao, ");
                sql.Append("DT_INICIO as DataInicio, DT_TERMINO as DataTermino, FL_VISIVEL as Visivel ");
                sql.Append("FROM TB_COMPROMISSO ");

                List<Compromisso> lista = (await conexao.QueryAsync<Compromisso>(sql.ToString())).ToList();
               
                return Ok(lista);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            Compromisso c = null;

            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ID as Id, ID_TIPO_COMPROMISSO as TipoCompromissoId, TX_DESCRICAO as Descricao, TX_LOCALIZACAO as Localizacao, ");
                sql.Append("DT_INICIO as DataInicio, DT_TERMINO as DataTermino, FL_VISIVEL as Visivel ");
                sql.Append("FROM TB_COMPROMISSO WHERE ID = @Id ");

                c = await conexao.QueryFirstOrDefaultAsync<Compromisso>(sql.ToString(), new { Id = id });
               
                if (c != null)
                    return Ok(c);
                else
                    return Ok ("Tipo de Compromisso não encontrado.");
            }
        }
           [HttpPost]
        public async Task<ActionResult> InsertAsync(Compromisso c)
        {
           if(ModelState.IsValid)
            {
            
                using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
                {
                    conexao.Open();

                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO TB_COMPROMISSO (ID_TIPO_COMPROMISSO, TX_DESCRICAO, TX_LOCALIZACAO, DT_INICIO, DT_TERMINO, FL_VISIVEL) ");
                    sql.Append("VALUES (@TipoCompromissoId, @Descricao, @Localizacao, @DataInicio, @DataTermino, @Visivel) ");
                    sql.Append("SELECT CAST(SCOPE_IDENTITY() AS INT) ");

                    object o = await conexao.ExecuteScalarAsync(sql.ToString(), c);

                    if (o != null)
                        c.Id = Convert.ToInt32(o);
                }
                return Ok(c);
            
            } 
            else
             return BadRequest(ModelState);
        }
            
          [HttpPut]
           public async Task<ActionResult> UpdateAsync(Compromisso c)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE TB_COMPROMISSO SET ");
                sql.Append("ID_TIPO_COMPROMISSO = @TipoCompromissoId, ");
                sql.Append("TX_DESCRICAO = @Descricao, ");
                sql.Append("TX_LOCALIZACAO = @Localizacao, ");
                sql.Append("DT_INICIO = @DataInicio, ");
                sql.Append("DT_TERMINO = @DataTermino, ");
                sql.Append("FL_VISIVEL = @Visivel ");
                sql.Append("WHERE ID = @Id ");

                int linhasAfetadas = await conexao.ExecuteAsync(sql.ToString(), c);
                return Ok(c);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append("DELETE FROM TB_COMPROMISSO ");
                sql.Append("WHERE ID = @Id ");

                int linhasAfetadas = await conexao.ExecuteAsync(sql.ToString(), new { Id = id });
                return Ok(linhasAfetadas);
            }
        }
    }
    }


