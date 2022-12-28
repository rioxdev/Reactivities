using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities;

public class Update
{
    public class Command : IRequest
    {
        public Activity Activity { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.Activity == null)
            {
                throw new ArgumentNullException(nameof(request));   
            }

            var entity = await _context.Activities.FindAsync(request.Activity.Id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            entity.Title= request.Activity.Title;
            entity.Description= request.Activity.Description;
            entity.Venue = request.Activity.Venue;
             
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
