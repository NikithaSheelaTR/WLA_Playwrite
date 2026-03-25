namespace Framework.Common.UI.Products.WestLawNext.Models
{
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class contains all the 'Order Document Form' fields as auto-properties 
    /// </summary>
    public class FullOrderDocumentFormModel : SavedOrderDocumentFormModel
    {
        /// <summary>
        /// <see cref="FullOrderDocumentFormModel"/> default constructor
        /// </summary>
        public FullOrderDocumentFormModel()
        {
        }

        /// <summary>
        /// <see cref="FullOrderDocumentFormModel"/> constructor that initialize object from base object
        /// </summary>
        public FullOrderDocumentFormModel(SavedOrderDocumentFormModel baseModel)
            
        {
            this.AddressLine1 = baseModel.AddressLine1;
            this.AddressLine12 = baseModel.AddressLine12;
            this.AddressLine2 = baseModel.AddressLine2;
            this.AddressLine22 = baseModel.AddressLine22;
            this.City = baseModel.City;
            this.City2 = baseModel.City2;
            this.ClientID = baseModel.ClientID;
            this.CompanyFirm = baseModel.CompanyFirm;
            this.CompanyFirm2 = baseModel.CompanyFirm2;
            this.Country = baseModel.Country;
            this.Country2 = baseModel.Country2;
            this.EmailAddress = baseModel.EmailAddress;
            this.EmailAddress2 = baseModel.EmailAddress2;
            this.FaxNumber = baseModel.FaxNumber;
            this.FaxNumber2 = baseModel.FaxNumber2;
            this.FullName = baseModel.FullName;
            this.FullName2 = baseModel.FullName2;
            this.PhoneNumber = baseModel.PhoneNumber;
            this.PhoneNumber2 = baseModel.PhoneNumber2;
            this.Postalcode = baseModel.Postalcode;
            this.Postalcode2 = baseModel.Postalcode2;
            this.StateProvince = baseModel.StateProvince;
            this.StateProvince2 = baseModel.StateProvince2;

            this.BillingAddressRadio = baseModel.BillingAddressRadio;
            this.DeliveryEmailCheckbox = baseModel.DeliveryEmailCheckbox;
            this.DeliveryFaxCheckbox = baseModel.DeliveryFaxCheckbox;
            this.DeliveryFirstClassMailCheckbox = baseModel.DeliveryFirstClassMailCheckbox;
            this.DeliveryOvernightCarrierCheckbox = baseModel.DeliveryOvernightCarrierCheckbox;
            this.DifferentAddressRadio = baseModel.DifferentAddressRadio;
            this.ContactMe = baseModel.ContactMe;
        }

        /// <summary>
        /// The Case Caption textbox.
        /// </summary>
        public string CaseCaption { get; set; } = string.Empty;

        /// <summary>
        /// The Case Number textbox.
        /// </summary>
        public string CaseNumber { get; set; } = string.Empty;

        /// <summary>
        /// The Comments textbox.
        /// </summary>
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// The Court type dropdown.
        /// </summary>
        public string Court { get; set; } = "Select"; // the default value for select element

        /// <summary>
        /// The Include all exhibits checkbox.
        /// </summary>
        public bool IncludeAllExhibits { get; set; }

        /// <summary>
        /// The Participant Information textbox.
        /// </summary>
        public string ParticipantInformation { get; set; } = string.Empty;

        /// <summary>
        /// The Requested Documents textbox.
        /// </summary>
        public string RequestedDocuments { get; set; } = string.Empty;

        /// <summary>
        /// Overriden '==' operator
        /// </summary>
        /// <param name="lhsModel">left hand side model</param>
        /// <param name="rhsModel">right hand side model</param>
        /// <returns></returns>
        public static bool operator ==(FullOrderDocumentFormModel lhsModel, FullOrderDocumentFormModel rhsModel)
        {
            return lhsModel.Equals(rhsModel);
        }

        /// <summary>
        /// Overriden '!=' operator
        /// </summary>
        /// <param name="lhsModel"></param>
        /// <param name="rhsModel"></param>
        /// <returns></returns>
        public static bool operator !=(FullOrderDocumentFormModel lhsModel, FullOrderDocumentFormModel rhsModel)
        {
            return !(lhsModel == rhsModel);
        }

        /// <summary>
        /// This method returns the <see cref="FullOrderDocumentFormModel"/> object to the same state order document form after clicking 'Clear All' link
        /// </summary>
        public FullOrderDocumentFormModel ClearAll()
        {
            var defaultFormModel = new FullOrderDocumentFormModel
                                       {
                                           DeliveryEmailCheckbox = false,
                                           BillingAddressRadio = this.BillingAddressRadio
                                       };

            foreach (PropertyInfo property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                property.SetValue(
                    this,
                    defaultFormModel.GetType()
                                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .FirstOrDefault(prop => prop.Name == property.Name)?.GetValue(defaultFormModel));
            }

            return this;
        }

        /// <summary>
        /// Overriden 'Equals' method
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            var fullModel = obj as FullOrderDocumentFormModel;

            if (object.ReferenceEquals(fullModel, null))
            {
                return false;
            }

            return this.Equals(fullModel);
        }

        /// <summary>
        /// Strongly typed variant of 'Equals' method
        /// </summary>
        /// <param name="fullModel">object</param>
        /// <returns></returns>
        public bool Equals(FullOrderDocumentFormModel fullModel)
        {
            if (object.ReferenceEquals(this, fullModel))
                return true;

            if (object.ReferenceEquals(fullModel, null))
                return false;

            bool result = base.Equals(fullModel);

            result &= string.Equals(this.Court, fullModel.Court);
            result &= string.Equals(this.ParticipantInformation, fullModel.ParticipantInformation);
            result &= string.Equals(this.RequestedDocuments, fullModel.RequestedDocuments);
            result &= this.IncludeAllExhibits == fullModel.IncludeAllExhibits;
            result &= string.Equals(this.CaseCaption, fullModel.CaseCaption);
            result &= string.Equals(this.CaseNumber, fullModel.CaseNumber);
            result &= string.Equals(this.Comments, fullModel.Comments);
            return result;
        }

        /// <summary>
        /// Overriden 'GetHashCode' method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// The function to get instance of <see cref="FullOrderDocumentFormModel"/> that contains only the properties that should be saved from session to session
        /// </summary>
        /// <returns>The new instance of <see cref="FullOrderDocumentFormModel"/></returns>
        public FullOrderDocumentFormModel GetModelWithSavedFields()
        {
            return new FullOrderDocumentFormModel(this);
        }
    }
}