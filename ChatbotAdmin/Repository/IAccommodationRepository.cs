using ChatbotAdmin.Models;
using ChatbotAdmin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Repository
{
    public interface IAccommodationRepository
    {
        Accommodation GetAccommodations(long id);
        List<Accommodation> GetAccommodations();

        Accommodation SaveAccommodation(Accommodation accommodation);
        bool UpdateAccommodation(Accommodation accommodation);



    }
}
