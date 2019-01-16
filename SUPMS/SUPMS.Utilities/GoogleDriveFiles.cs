using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Utilities
{
    public class GoogleDriveFiles
    {
        public List<GoogleDriveFile> Items { get; set; }
    }
    public class GoogleDriveFile
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string OriginalFilename { get; set; }
        public string ThumbnailLink { get; set; }
        public string IconLink { get; set; }
        public string WebContentLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string MimeType { get; set; }
        public GoogleDriveFileLabel Labels { get; set; }
        public List<GoogleDriveParent> Parents { get; set; }
    }
    public class GoogleDriveFileLabel
    {
        public bool Starred { get; set; }
        public bool Hidden { get; set; }
        public bool Trashed { get; set; }
        public bool Restricted { get; set; }
        public bool Viewed { get; set; }
    }
    public class GoogleDriveParent
    {
        public string Kind { get; set; }
        public string Id { get; set; }
        public string ParentLink { get; set; }
        public string selfLink { get; set; }
        public string IsRoot { get; set; }
    }
}
