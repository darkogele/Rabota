using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Interop.CS.Models.Exceptions;

namespace Interop.CS.Models.Helpers
{
    public static class UrlAttribute
    {
        //Опис: Методот служи за проверка на валидноста на Url
        //Влезни параметри: вредност што ја внесуваме за Url
        public static void IsValid(object value)
        {
            //may want more here for https, etc
            Regex regex = new Regex(@"(^(http|https)://)");

            if (value == null || !regex.IsMatch(value.ToString()))
            {
                //return false;
                throw new InvalidUrlException();
            }

        }
    }
}
