using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace eAgenda.webapi.Filters
{
    public class ValidarViewModelActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context consegue pegar o "contexto" dentro do excopo do metodo que esta "trabalhando" podendo saber se os dados que vem da web estão invalidos, vazios etc...
           

            if (context.ModelState.IsValid == false)
            {
                //Pega todos os erros que vem dos decorators do Forms(Suas mensagens)
                var listaErros = context.ModelState.Values
                   .SelectMany(x => x.Errors)
                   .Select(x => x.ErrorMessage);
                context.Result = new BadRequestObjectResult(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });

                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

     
    }
}
