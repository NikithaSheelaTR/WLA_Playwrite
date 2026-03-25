namespace Framework.Common.UI.Utils.Enum
{
    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;

    /// <summary>
    /// enum for pointer up and pointer down
    /// </summary>
    public enum Pointer
    {
        /// <summary>
        /// pointer down.
        /// </summary>
        [MouseEvent(Event = "pointerdown")] PointerDown,

        /// <summary>
        /// pointer up
        /// </summary>
        [MouseEvent(Event = "pointerup")] PointerUp
    }
}
