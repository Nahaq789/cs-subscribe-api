using System.Runtime.Serialization;
using MediatR;

namespace Subscribe.API.Application.Command;

[DataContract]
public class CreateCategoryCommand : IRequest<bool>
{
    [DataMember]
    public string ColorCode { get; set; }

    [DataMember]
    public string? IconFilePath { get; set; }

    [DataMember]
    public bool IsDefault { get; set; }

    [DataMember]
    public bool IsActive { get; set; }

    [DataMember]
    public string CategoryName { get; set; }

    [DataMember]
    public Guid UserAggregateId { get; set; }

    public CreateCategoryCommand(string colorCode, string? iconFilePath, bool isDefault, bool isActive, string categoryName, Guid userAggregateId)
    {
        ColorCode = colorCode;
        IconFilePath = iconFilePath;
        IsDefault = isDefault;
        IsActive = isActive;
        CategoryName = categoryName;
        UserAggregateId = userAggregateId;
    }
}
