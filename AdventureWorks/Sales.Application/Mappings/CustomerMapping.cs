namespace Sales.Application.Mappings;

public class CustomerMapping : Profile
{
    public CustomerMapping()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(destination => destination.Id,
                source => source.MapFrom(x => x.CustomerId))

            .ForMember(destination => destination.PersonId,
                source => source.MapFrom(x => x.PersonId))

            .ForMember(destination => destination.StoreId,
                source => source.MapFrom(x => x.StoreId))

            .ForMember(destination => destination.TerritoryId,
                source => source.MapFrom(x => x.TerritoryId))

            .ForMember(destination => destination.AccountNumber,
                source => source.MapFrom(x => x.AccountNumber))

            .ForMember(destination => destination.ModifiedDate,
                source => source.MapFrom(x => x.ModifiedDate));

        CreateMap<Customer, CustomerWithLinksDto>()
            .ForMember(destination => destination.Id,
                source => source.MapFrom(x => x.CustomerId))

            .ForMember(destination => destination.PersonId,
                source => source.MapFrom(x => x.PersonId))

            .ForMember(destination => destination.StoreId,
                source => source.MapFrom(x => x.StoreId))

            .ForMember(destination => destination.TerritoryId,
                source => source.MapFrom(x => x.TerritoryId))

            .ForMember(destination => destination.AccountNumber,
                source => source.MapFrom(x => x.AccountNumber))

            .ForMember(destination => destination.ModifiedDate,
                source => source.MapFrom(x => x.ModifiedDate));
    }
}