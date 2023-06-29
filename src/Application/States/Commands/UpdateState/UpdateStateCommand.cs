using MediatR;
using System.Text.Json.Serialization;

namespace apollo.Application.States.Commands.UpdateState
{
    public class UpdateStateCommand : IRequest<int>
    {
       [JsonIgnore]
        public int ID {get;set;}
        public string Name {get;set;}
        public bool IsDeleted {get;set;}
    }
}
