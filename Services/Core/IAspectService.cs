using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.AspectRequest;
using AstroBackEnd.ViewsModel;
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

        public Dictionary<string, List<HoroscopeItemView>> CalculateAspect(DateTime birthDate, DateTime compareDate);
    }
}
