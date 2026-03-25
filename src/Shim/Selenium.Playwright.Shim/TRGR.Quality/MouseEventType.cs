using System;

namespace TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver
{
    /// <summary>
    /// Mouse event types for triggering mouse events via JavaScript.
    /// </summary>
    public enum MouseEventType
    {
        [MouseEvent("click")]
        Click,

        [MouseEvent("dblclick")]
        DblClick,

        [MouseEvent("mousedown")]
        MouseDown,

        [MouseEvent("mouseup")]
        MouseUp,

        [MouseEvent("mouseover")]
        MouseOver,

        [MouseEvent("mouseout")]
        MouseOut,

        [MouseEvent("mousemove")]
        MouseMove,

        [MouseEvent("contextmenu")]
        ContextMenu
    }

    /// <summary>
    /// Attribute to map MouseEventType enum values to JavaScript event names.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MouseEventAttribute : Attribute
    {
        public string Event { get; set; }

        public MouseEventAttribute()
        {
        }

        public MouseEventAttribute(string eventName)
        {
            Event = eventName;
        }
    }

    /// <summary>
    /// Extension method to get MouseEventAttribute from enum values.
    /// </summary>
    public static class MouseEventEnumExtensions
    {
        public static MouseEventAttribute GetAttribute<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(T), false);
                if (attrs.Length > 0)
                    return (MouseEventAttribute)(Attribute)attrs[0];
            }
            return new MouseEventAttribute(enumValue.ToString().ToLower());
        }
    }
}
