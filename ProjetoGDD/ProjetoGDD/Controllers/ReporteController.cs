using ProjetoGDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ProjetoGDD.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ReportesDB ReportesContext = new ReportesDB();
        private readonly TimesDB TimesContext = new TimesDB();
        // GET: Formulario
        public ActionResult Index()
        {
            var reportes = ReportesContext.Reportes.FindAll().SetSortOrder(SortBy<Reporte>.Descending(r => r.Id));
            var time = TimesContext.Times.FindAll().SetSortOrder(SortBy<Time>.Descending(r => r.Id)).FirstOrDefault();
            return View(new Tuple<IEnumerable<Reporte>,Reporte,int>(reportes,new Reporte(),time.Membros.Count));
        }
        
        public ActionResult Criar(Reporte reporte)
        {
            reporte.DataCriacao = DateTime.Now.ToShortDateString();
            ReportesContext.Reportes.Insert(reporte);
            TempData["SucessoReporte"] = true;
            return RedirectToAction("Index");
        }

        [Route("{idReporte}/formulario")]
        public ActionResult FormResposta(string idReporte)
        {
            var reporte = ReportesContext.Reportes.FindOneById(new ObjectId(idReporte));

            var items = this.PreencheMembros(idReporte);

            return View(new Tuple<Resposta,Reporte,List<SelectListItem>>(new Resposta(),reporte,items));
        }

        public ActionResult Responder([Bind(Prefix = "Item1")] Resposta resposta, string idReporte)
        {
            var reporte = ReportesContext.Reportes.FindOneById(new ObjectId(idReporte));
            if (ModelState.IsValid && resposta != null)
            {
                if (reporte.Respostas == null)
                {
                    reporte.Respostas = new List<Resposta>();
                }
                reporte.Respostas.Add(resposta);
                ReportesContext.Reportes.Save(reporte);
                TempData["SucessoResposta"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                var items = this.PreencheMembros(idReporte);

                return View("FormResposta", new Tuple<Resposta, Reporte, List<SelectListItem>>(new Resposta(), reporte, items));
            }
        }

        [Route("{idReporte}/respostas")]
        public ActionResult ListaRespostas(string idReporte)
        {
            var reporte = ReportesContext.Reportes.FindOneById(new ObjectId(idReporte));

            if(reporte.Respostas == null)
            {
                reporte.Respostas = new List<Resposta>();
            }
            return View(reporte);
        }

        public ActionResult Excluir(string idReporte)
        {
            ReportesContext.Reportes.Remove(Query.EQ("_id", new ObjectId(idReporte)));
            TempData["SucessoExclusao"] = true;
            return RedirectToAction("Index");
        }

        private List<SelectListItem> PreencheMembros(string idReporte)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var reporte = ReportesContext.Reportes.FindOneById(new ObjectId(idReporte));

            foreach (var membro in reporte.MembrosRestantes.Item2)
            {
                items.Add(new SelectListItem { Text = membro, Value = membro });
            }

            return items;
        }
    }
}