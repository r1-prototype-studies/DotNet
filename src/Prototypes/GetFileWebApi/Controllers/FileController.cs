using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace GetFileWebApi.Controllers
{
    public class FileController : ApiController
    {
        private string _sampleDoc = @"..\..\..\data\sampleDoc.doc";
        private string _sampleExcel = @"..\..\..\data\sampleDoc.doc";
        private string _samplePdf = @"..\..\..\data\sampleDoc.doc";
        private string _sampleZip = @"..\..\..\data\sampleDoc.doc";

        [HttpGet]
        [Route("Ebook/{format}")]
        public IHttpActionResult GetBookFor(string format)
        {
            string fileName = GetFileName(format);

            var filebytes = File.ReadAllBytes(fileName);
            var data = new MemoryStream(filebytes);
            return new EbookResult("testing", data, Request);
        }

        /// <summary>
        /// Get the sample file name based on the format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private string GetFileName(string format)
        {
            // dll running path
            var dllPath = System.Web.HttpRuntime.AppDomainAppPath;
            var textFile = string.Empty;
            switch (format.ToUpper())
            {
                case "DOC":
                    textFile = _sampleDoc;
                    break;
                case "EXCEL":
                    textFile = _sampleExcel;
                    break;
                case "PDF":
                    textFile = _samplePdf;
                    break;
                case "ZIP":
                    textFile = _sampleZip;
                    break;
                default:
                    textFile = _sampleDoc;
                    break;
            }

            var fileName = Path.GetFullPath(Path.Combine(dllPath, _sampleDoc));
            return fileName;
        }

        public class EbookResult : IHttpActionResult
        {
            string _fileName;
            MemoryStream _bookData;
            HttpRequestMessage _request;
            public EbookResult(string fileName, MemoryStream data, HttpRequestMessage request)
            {
                _fileName = fileName;
                _bookData = data;
                _request = request;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = _request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StreamContent(_bookData);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = _fileName.Split('\\').Last();
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                return System.Threading.Tasks.Task.FromResult(response);
            }
        }
    }
}
