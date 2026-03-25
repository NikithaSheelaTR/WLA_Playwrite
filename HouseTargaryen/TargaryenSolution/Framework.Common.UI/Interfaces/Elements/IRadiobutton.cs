namespace Framework.Common.UI.Interfaces.Elements
{
    /// <summary>
    /// The Radio button interface.
    /// </summary>
    public interface IRadiobutton : IBaseWebElement
    {
        /// <summary>
        /// Gets a value indicating whether selected.
        /// </summary>
        bool Selected { get; }

        /// <summary>
        /// The Select
        /// </summary>
        void Select();
    }
}