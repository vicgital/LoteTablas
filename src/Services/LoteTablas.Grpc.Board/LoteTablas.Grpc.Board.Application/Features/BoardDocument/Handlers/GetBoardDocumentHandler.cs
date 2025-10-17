using LoteTablas.Grpc.Board.Application.Features.BoardDocument.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteTablas.Grpc.Board.Application.Features.BoardDocument.Handlers
{
    public class GetBoardDocumentHandler : IRequestHandler<GetBoardDocumentRequest, byte[]>
    {

        public Task<byte[]> Handle(GetBoardDocumentRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
