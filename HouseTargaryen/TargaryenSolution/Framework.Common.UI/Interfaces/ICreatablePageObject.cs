namespace Framework.Common.UI.Interfaces
{
    /// <summary>
    /// This interface is used for restrictions in the DriverExtensions.CreatePageInstance method.
    /// DriverExtensions.CreatePageInstance creates a page object instances for the following PO types: 
    /// Pages, Dialogs, TabComponents and AlertsComponents.
    /// Using this interface as a restriction for DE.CreatePageInstance garanties
    /// that we can not create a non page object instances 
    /// </summary>
    public interface ICreatablePageObject
    {
    }
}