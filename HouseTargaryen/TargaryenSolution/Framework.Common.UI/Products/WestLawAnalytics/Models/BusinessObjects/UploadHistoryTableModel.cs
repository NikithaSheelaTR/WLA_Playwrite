namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    using System;
    using Framework.Core.Utils;

    /// <summary>
    /// Upload history model
    /// </summary>
    public class UploadHistoryTableModel
    {
        /// <summary>
        /// Date Uploaded
        /// </summary>
        public DateTime DateUploaded { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Uploader
        /// </summary>
        public string Uploader { get; set; }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return
                $"FileName: {this.FileName}, Uploader: {this.Uploader}, DateUploaded: {this.DateUploaded}, Status: {this.Status}, Description: {this.Description}";
        }

        /// <summary>
        /// Override 'Equals' method
        /// 600sec passed to method IsInRange since the average time to upload files is 10 min
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True - if equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            var m = (UploadHistoryTableModel)obj;
            return this.FileName.Equals(m.FileName) && this.Uploader.Equals(m.Uploader)
                   && this.DateUploaded.IsInRange(m.DateUploaded, 600)
                   && this.Status.Equals(m.Status) && this.Description.Equals(m.Description);
        }

        /// <summary>
        /// Overriden 'GetHashCode' method
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
