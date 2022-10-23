using BTP_API.Models;
using BTP_API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BTP_API.ServicesImpl
{
    public class ManageTransactionRepository : IManageTransactionRepository
    {
        private readonly BTPContext _context;

        public ManageTransactionRepository(BTPContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> getAllExchangeAsync()
        {
            var exchanges = await _context.Exchanges.ToListAsync();
            if (exchanges.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = exchanges.Count
            };
        }
        public async Task<ApiResponse> getAllExchangeDetailAsync(int exchangeId)
        {
            var check = await _context.Exchanges.AnyAsync(b => b.Id == exchangeId);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.EXCHANGE_NOT_EXIST.ToString()
                };
            }
            var exchangeDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId).ToListAsync();
            if (exchangeDetails.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeDetails,
                NumberOfRecords = exchangeDetails.Count
            };
        }
        public async Task<ApiResponse> getAllExchangeBillAsync(int exchangeId)
        {
            var check = await _context.Exchanges.AnyAsync(b => b.Id == exchangeId);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.EXCHANGE_NOT_EXIST.ToString()
                };
            }
            var exchangeBills = await _context.ExchangeBills.Where(b => b.ExchangeId == exchangeId).ToListAsync();
            if (exchangeBills.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeBills,
                NumberOfRecords = exchangeBills.Count
            };
        }
        public async Task<ApiMessage> updateStatusExchangeAsync(int exchangeId, string status)
        {
            var exchange = await _context.Exchanges.SingleOrDefaultAsync(b => b.Id == exchangeId);
            if(exchange != null)
            {
                exchange.Status = status.ToString();
                _context.Update(exchange);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.UPDATE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.EXCHANGE_NOT_EXIST.ToString()
            };
        }
        public async Task<ApiMessage> updateExchangeDetailAsync(int exchangeDetailId, ExchangeDetailVM exchangeDetailVM)
        {
            var exchangeDetail = await _context.ExchangeDetails.SingleOrDefaultAsync(b => b.Id == exchangeDetailId);
            if (exchangeDetail != null)
            {
                exchangeDetail.BeforeStatusBook1 = exchangeDetailVM.BeforeStatusBook1;
                exchangeDetail.AfterStatusBook1 = exchangeDetailVM.AfterStatusBook1;
                exchangeDetail.StorageStatusBook1 = exchangeDetailVM.StorageStatusBook1;
                exchangeDetail.BeforeStatusBook2 = exchangeDetailVM.BeforeStatusBook2;
                exchangeDetail.AfterStatusBook2 = exchangeDetailVM.AfterStatusBook2;
                exchangeDetail.StorageStatusBook2 = exchangeDetailVM.StorageStatusBook2;
                exchangeDetail.Status = exchangeDetailVM.Status;
                _context.Update(exchangeDetail);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.UPDATE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.EXCHANGE_DETAIL_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiResponse> getAllRentAsync()
        {
            var rents = await _context.Rents.ToListAsync();
            if (rents.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = rents.Count
            };
        }
        public async Task<ApiResponse> getAllRentDetailAsync(int rentId)
        {
            var check = await _context.Rents.AnyAsync(b => b.Id == rentId);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.RENT_NOT_EXIST.ToString()
                };
            }
            var rentDetails = await _context.RentDetails.Where(b => b.RentId == rentId).ToListAsync();
            if (rentDetails.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentDetails,
                NumberOfRecords = rentDetails.Count
            };
        }

        public async Task<ApiResponse> getAllRentBillAsync(int rentId)
        {
            var check = await _context.Rents.AnyAsync(b => b.Id == rentId);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.RENT_NOT_EXIST.ToString()
                };
            }
            var rentBills = await _context.RentBills.Where(b => b.RentId == rentId).ToListAsync();
            if (rentBills.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentBills,
                NumberOfRecords = rentBills.Count
            };
        }

        public async Task<ApiMessage> updateStatusRentAsync(int rentId, string status)
        {
            var rent = await _context.Rents.SingleOrDefaultAsync(b => b.Id == rentId);
            if (rent != null)
            {
                rent.Status = status.ToString();
                _context.Update(rent);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.UPDATE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.RENT_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> updateRentDetailAsync(int rentDetailId, RentDetailVM rentDetailVM)
        {
            var rentDetail = await _context.RentDetails.SingleOrDefaultAsync(b => b.Id == rentDetailId);
            if (rentDetail != null)
            {
                rentDetail.BeforeStatusBook = rentDetailVM.BeforeStatusBook;
                rentDetail.AfterStatusBook = rentDetailVM.AfterStatusBook;
                rentDetail.StorageStatusBook = rentDetailVM.StorageStatusBook;
                rentDetail.Status = rentDetailVM.Status;
                _context.Update(rentDetail);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.UPDATE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.RENT_DETAIL_NOT_EXIST.ToString()
            };
        }
    }
}
