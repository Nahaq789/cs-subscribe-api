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
    public string CategoryName { get; set; }

    [DataMember]
    public Guid UserAggregateId { get; set; }

    public CreateCategoryCommand(string colorCode, string? iconFilePath, string categoryName, Guid userAggregateId)
    {
        ColorCode = colorCode;
        IconFilePath = iconFilePath;
        CategoryName = categoryName;
        UserAggregateId = userAggregateId;
    }
}
