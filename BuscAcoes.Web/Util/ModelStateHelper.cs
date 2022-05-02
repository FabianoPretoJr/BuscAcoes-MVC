using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Web.Util
{
    public static class ModelStateHelper
    {
        public static Dictionary<string,string> GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            return modelState.Where(x => x.Value.Errors.Any())
               .ToDictionary(m => m.Key, m => m.Value.Errors
               .Select(s => s.ErrorMessage)
               .FirstOrDefault(s => s != null));

        }
    }
}
