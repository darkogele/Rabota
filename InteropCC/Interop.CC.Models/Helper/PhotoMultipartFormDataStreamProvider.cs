using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interop.CC.Models.Helper
{
    public class PhotoMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {

        public PhotoMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        // Опис: Метод за екстракција и вчитување на име од локален фајл
        // Влезни параметри: HttpContentHeaders headers
        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Trim(new char[] { '"' })
                        .Replace("&", "and");
        }
    }
}
