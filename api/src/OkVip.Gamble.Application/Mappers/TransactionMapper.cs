using AutoMapper;
using OkVip.Gamble.Transactions;

namespace OkVip.Gamble.Mappers
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {
            CreateMap<Transaction, TransactionOutputDto>();
        }
    }
}
