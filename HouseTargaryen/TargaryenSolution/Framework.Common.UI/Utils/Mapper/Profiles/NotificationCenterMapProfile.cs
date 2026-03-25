namespace Framework.Common.UI.Utils.Mapper.Profiles
{
    using AutoMapper;

    using Framework.Common.UI.Products.WestlawEdge.Items.NotificationsCenter;
    using Framework.Common.UI.Products.WestlawEdge.Models.NotificationsCenter;

    /// <summary>
    /// The notification center map profile
    /// </summary>
    public class NotificationCenterMapProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationCenterMapProfile"/> class. 
        /// </summary>
        public NotificationCenterMapProfile()
        {
            // NotificationCenterPageItem mapping configuration to NotificationCenterPageItemModel
            this.CreateMap<NotificationCenterPageItem, NotificationCenterPageModel>();
        }
    }
}