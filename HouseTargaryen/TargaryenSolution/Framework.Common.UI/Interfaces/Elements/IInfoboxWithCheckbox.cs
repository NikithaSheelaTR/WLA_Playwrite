namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// IInfoboxWithCheckbox interface
    /// </summary>
    public interface IInfoboxWithCheckbox: IInfoBox
    {
        /// <summary>
        /// Get the title
        /// </summary>
        ILabel Title { get; }

        /// <summary>
        /// Get the checkbox
        /// </summary>
        ICheckBox Checkbox { get; }
    }
}
