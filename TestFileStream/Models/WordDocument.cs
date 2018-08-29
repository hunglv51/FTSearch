using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFileStream.Models
{
    public partial class WordDocument
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        
        public byte[] Content { get; set; }
        public string FileType { get; set; }
    }
}
