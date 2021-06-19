using AutoMapper;
using Contract;
using PersonDataProcessor.Model;

namespace PersonDataProcessor.Utility.Automapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonData>();
            CreateMap<PersonData, Person>();
        }
    }
}
