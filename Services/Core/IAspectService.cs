using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.AspectRequest;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IAspectService
    {
        public Aspect CreateAspect(CreateAspectRequest createAspect);
        public Aspect GetAspect(int id);
        public IEnumerable<Aspect> FindAspect(FindAspectRequest findAspect, out int total);
        public Aspect UpdateAspect(int id, UpdateAspectRequest updateAspect);
        public Aspect DeleteAspect(int id);

        public void CalculateAspect(DateTime birthDate, DateTime compareDate);
    }
}
