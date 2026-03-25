namespace Framework.Common.UI.Products.WestLawNext.Models
{
    using System;

    /// <summary>
    /// Class contains properties for fields that should save its values from session to session 
    /// </summary>
    public class SavedOrderDocumentFormModel
    {
        /// <summary>
        /// Unique private readonly field to use in <see cref="GetHashCode"/> method
        /// </summary>
        private readonly string uniqueValue;

        /// <summary>
        /// The billing address checkbox.
        /// </summary>
        private bool billingAddressRadio;

        /// <summary>
        /// The different address checkbox.
        /// </summary>
        private bool differentAddressRadio;

        /// <summary>
        /// <see cref="SavedOrderDocumentFormModel"/> default constructor
        /// </summary>
        public SavedOrderDocumentFormModel()
        {
            this.DeliveryEmailCheckbox = true;
            this.BillingAddressRadio = true;
            this.uniqueValue = DateTime.Now.ToString();
        }

        /// <summary>
        /// The _address line 1.
        /// </summary>
        public string AddressLine1 { get; set; } = string.Empty;

        /// <summary>
        /// The _address line 12.
        /// </summary>
        public string AddressLine12 { get; set; } = string.Empty;

        /// <summary>
        /// The _address line 2.
        /// </summary>
        public string AddressLine2 { get; set; } = string.Empty;

        /// <summary>
        /// The _address line 22.
        /// </summary>
        public string AddressLine22 { get; set; } = string.Empty;

        /// <summary>
        /// The billing address checkbox.
        /// </summary>
        public bool BillingAddressRadio
        {
            get
            {
                return this.billingAddressRadio;
            }

            set
            {
                if (this.DifferentAddressRadio && value)
                {
                    this.differentAddressRadio = false;
                }
                else if (!this.DifferentAddressRadio && !value)
                {
                    this.differentAddressRadio = true;
                }

                this.billingAddressRadio = value;
            }
        }

        /// <summary>
        /// The _city.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// The _city 2.
        /// </summary>
        public string City2 { get; set; } = string.Empty;

        /// <summary>
        /// The Client ID textbox.
        /// </summary>
        public string ClientID { get; set; } = string.Empty;

        /// <summary>
        /// The _company firm.
        /// </summary>
        public string CompanyFirm { get; set; } = string.Empty;

        /// <summary>
        /// The _company firm 2.
        /// </summary>
        public string CompanyFirm2 { get; set; } = string.Empty;

        /// <summary>
        /// The Contact me checkbox.
        /// </summary>
        public bool ContactMe { get; set; }

        /// <summary>
        /// The _country.
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// The _country 2.
        /// </summary>
        public string Country2 { get; set; } = string.Empty;

        /// <summary>
        /// The delivery email checkbox.
        /// </summary>
        public bool DeliveryEmailCheckbox { get; set; }

        /// <summary>
        /// The delivery fax dropdown checkbox.
        /// </summary>
        public bool DeliveryFaxCheckbox { get; set; }

        /// <summary>
        /// The delivery first class mail checkbox.
        /// </summary>
        public bool DeliveryFirstClassMailCheckbox { get; set; }

        /// <summary>
        /// The delivery overnight carrier checkbox.
        /// </summary>
        public bool DeliveryOvernightCarrierCheckbox { get; set; }

        /// <summary>
        /// The different address checkbox.
        /// </summary>
        public bool DifferentAddressRadio
        {
            get
            {
                return this.differentAddressRadio;
            }

            set
            {
                if (this.BillingAddressRadio && value)
                {
                    this.billingAddressRadio = false;
                }
                else if (!this.BillingAddressRadio && !value)
                {
                    this.billingAddressRadio = true;
                }

                this.differentAddressRadio = value;
            }
        }

        /// <summary>
        /// The _email address.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// The _email address 2.
        /// </summary>
        public string EmailAddress2 { get; set; } = string.Empty;

        /// <summary>
        /// The _fax number.
        /// </summary>
        public string FaxNumber { get; set; } = string.Empty;

        /// <summary>
        /// The _fax number 2.
        /// </summary>
        public string FaxNumber2 { get; set; } = string.Empty;

        /// <summary>
        /// The _full name.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The _full name 2.
        /// </summary>
        public string FullName2 { get; set; } = string.Empty;

        /// <summary>
        /// The _phone number.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The _phone number 2.
        /// </summary>
        public string PhoneNumber2 { get; set; } = string.Empty;

        /// <summary>
        /// The postal code.
        /// </summary>
        public string Postalcode { get; set; } = string.Empty;

        /// <summary>
        /// The postal code 2.
        /// </summary>
        public string Postalcode2 { get; set; } = string.Empty;

        /// <summary>
        /// The _state province.
        /// </summary>
        public string StateProvince { get; set; } = string.Empty;

        /// <summary>
        /// The _state province 2.
        /// </summary>
        public string StateProvince2 { get; set; } = string.Empty;

        /// <summary>
        /// Overriden '==' operator
        /// </summary>
        /// <param name="lhsModel">left hand side model</param>
        /// <param name="rhsModel">right hand side model</param>
        /// <returns></returns>
        public static bool operator ==(SavedOrderDocumentFormModel lhsModel, SavedOrderDocumentFormModel rhsModel)
        {
            return lhsModel.Equals(rhsModel);
        }

        /// <summary>
        /// Overriden '!=' operator
        /// </summary>
        /// <param name="lhsModel"></param>
        /// <param name="rhsModel"></param>
        /// <returns></returns>
        public static bool operator !=(SavedOrderDocumentFormModel lhsModel, SavedOrderDocumentFormModel rhsModel)
        {
            return !(lhsModel == rhsModel);
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

            var item = obj as SavedOrderDocumentFormModel;

            if (object.ReferenceEquals(item, null))
            {
                return false;
            }

            return this.Equals(item);
        }

        /// <summary>
        /// Overriden 'Equals' method
        /// </summary>
        /// <param name="savedModel">object</param>
        /// <returns></returns>
        public bool Equals(SavedOrderDocumentFormModel savedModel)
        {
            if (object.ReferenceEquals(this, savedModel))
            {
                return true;
            }

            if (object.ReferenceEquals(savedModel, null))
            {
                return false;
            }

            bool result = string.Equals(this.ClientID, savedModel.ClientID);
            result &= string.Equals(this.FullName, savedModel.FullName);
            result &= string.Equals(this.EmailAddress, savedModel.EmailAddress);
            result &= string.Equals(this.PhoneNumber, savedModel.PhoneNumber);
            result &= string.Equals(this.FaxNumber, savedModel.FaxNumber);
            result &= string.Equals(this.CompanyFirm, savedModel.CompanyFirm);
            result &= string.Equals(this.AddressLine1, savedModel.AddressLine1);
            result &= string.Equals(this.AddressLine2, savedModel.AddressLine2);
            result &= string.Equals(this.City, savedModel.City);
            result &= string.Equals(this.StateProvince, savedModel.StateProvince);
            result &= string.Equals(this.Postalcode, savedModel.Postalcode);
            result &= string.Equals(this.Country, savedModel.Country);
            result &= string.Equals(this.FullName2, savedModel.FullName2);
            result &= string.Equals(this.EmailAddress2, savedModel.EmailAddress2);
            result &= string.Equals(this.PhoneNumber2, savedModel.PhoneNumber2);
            result &= string.Equals(this.FaxNumber2, savedModel.FaxNumber2);
            result &= string.Equals(this.CompanyFirm2, savedModel.CompanyFirm2);
            result &= string.Equals(this.AddressLine12, savedModel.AddressLine12);
            result &= string.Equals(this.AddressLine22, savedModel.AddressLine22);
            result &= string.Equals(this.City2, savedModel.City2);
            result &= string.Equals(this.StateProvince2, savedModel.StateProvince2);
            result &= string.Equals(this.Postalcode2, savedModel.Postalcode2);
            result &= string.Equals(this.Country2, savedModel.Country2);

            result &= this.ContactMe == savedModel.ContactMe;
            result &= this.DeliveryEmailCheckbox == savedModel.DeliveryEmailCheckbox;
            result &= this.DeliveryOvernightCarrierCheckbox == savedModel.DeliveryOvernightCarrierCheckbox;
            result &= this.DeliveryFaxCheckbox == savedModel.DeliveryFaxCheckbox;
            result &= this.DeliveryFirstClassMailCheckbox == savedModel.DeliveryFirstClassMailCheckbox;
            result &= this.BillingAddressRadio == savedModel.BillingAddressRadio;
            result &= this.DifferentAddressRadio == savedModel.DifferentAddressRadio;

            return result;
        }

        /// <summary>
        /// Overriden 'GetHashCode' method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.uniqueValue.GetHashCode();
        }

        // This section is mandatory one, is always availableon the form

        // This section is optional one, is available only if 'Different Address' radio button is selected
    }
}