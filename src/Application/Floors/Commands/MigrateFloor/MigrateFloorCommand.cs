using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Application.Floors.Commands.CreateFloor;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Floors.Commands.MigrateFloor
{

    public class MigrateFloorCommand : IRequest<MigrationResultModel>
    {
        public List<MigrateFloorDTO> Data { get; set; }

        public class Handler : IRequestHandler<MigrateFloorCommand, MigrationResultModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<MigrationResultModel> Handle(MigrateFloorCommand request, CancellationToken cancellationToken)
            {
                List<string> errors = new List<string>();

                var totalCount = request.Data.Count();
                var totalUploaded = 0;

                foreach (var data in request.Data)
                {
                    var refJSONString = JsonConvert.SerializeObject(data);

                    var csvEntity = JsonConvert.DeserializeObject<MigrateFloorDTO>(refJSONString);

                    if (string.IsNullOrEmpty(csvEntity.Name))
                    {
                        errors.Add($"Missing property name at row {request.Data.IndexOf(data) + 1}");
                    }

                    if (errors.Count() == 0)
                    {
                        if (!string.IsNullOrEmpty(csvEntity.Name))
                        {
                            try
                            {
                                var createCommand = new SaveFloorRequest
                                {
                                    Name = csvEntity.Name,
                                    BuildingID = csvEntity.BuildingID,
                                    Sort = csvEntity.Sort,
                                    FloorPlateSize = csvEntity.FloorPlateSize
                                };
                                var id = await _mediator.Send(createCommand);
                                totalUploaded++;
                            }
                            catch (Exception err)
                            {
                                errors.Add(err.Message);
                            }
                        }
                    }
                }

                return new MigrationResultModel
                {
                    Succeeded = errors.Count > 0 ? false : true,
                    Uploaded = totalUploaded,
                    Total = totalCount,
                    Errors = errors
                };
            }
        }

    }
}