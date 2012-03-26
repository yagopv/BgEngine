//==============================================================================
// This file is part of BgEngine.
//
// BgEngine is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BgEngine is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BgEngine. If not, see <http://www.gnu.org/licenses/>.
//==============================================================================
// Copyright (c) 2011 Yago Pérez Vázquez
// Version: 1.0
//==============================================================================

using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;

using Ionic.Zip;

namespace BgEngine.Web.Results
{
    public class ZipResult : ActionResult
    {
        private IEnumerable<string> _files;
        private string _fileName;

        public string FileName
        {
            get
            {
                return _fileName ?? "archivo.zip";
            }
            set { _fileName = value; }
        }

        public ZipResult(params string[] files)
        {
            this._files = files;
        }

        public ZipResult(IEnumerable<string> files)
        {
            this._files = files;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            //long length = 0;
            //foreach (string file in _files)
            //{
            //    FileInfo fi = new FileInfo(file);
            //    length = length + fi.Length;
            //}

            using (ZipFile zf = new ZipFile())
            {
                zf.CompressionMethod = CompressionMethod.None;
                zf.AddFiles(_files, false, "");
                MemoryStream stream = new MemoryStream();
                zf.Save(stream);
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.BufferOutput = false;
                context.HttpContext
                    .Response.ContentType = "application/zip";
                context.HttpContext
                    .Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                context.HttpContext
                    .Response.AppendHeader("Content-Length", stream.Length.ToString());                
                context.HttpContext.Response.BinaryWrite(stream.ToArray());
                //zf.CompressionMethod = CompressionMethod.None;
                //context.HttpContext.Response.Clear();               
                //context.HttpContext.Response.BufferOutput = false;                
                //zf.AddFiles(_files, false, "");
                //context.HttpContext
                //    .Response.ContentType = "application/zip";
                //context.HttpContext
                //    .Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                //context.HttpContext
                //    .Response.AppendHeader("Content-Length", length.ToString());                
                //zf.Save(context.HttpContext.Response.OutputStream);

            }
        }
    }
}