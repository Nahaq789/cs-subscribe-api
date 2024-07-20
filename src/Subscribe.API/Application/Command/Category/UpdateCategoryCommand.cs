using System.Runtime.Serialization;
using MediatR;

namespace Subscribe.API.Application.Command;

[DataContract]
public class UpdateCategoryCommand : IRequest<bool>
{
    [DataMember]
    public Guid AggregateId { get; set; }

    [DataMember]
    public string ColorCode { get; set; }

    [DataMember]
    public string IconFilePath { get; set; }

    [DataMember]
    public bool IsActive { get; set; }

    [DataMember]
    public string CategoryName { get; set; }

    [DataMember]
    public Guid UserAggregateId { get; set; }

    public UpdateCategoryCommand(Guid aggregateId, string colorCode, string iconFilePath, bool isActive, string categoryName, Guid userAggregateId)
    {
        AggregateId = aggregateId;
        ColorCode = colorCode;
        IconFilePath = iconFilePath;
        IsActive = isActive;
        CategoryName = categoryName;
        UserAggregateId = userAggregateId;
    }
}
