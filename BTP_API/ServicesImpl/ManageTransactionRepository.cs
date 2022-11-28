using BTP_API.Models;
using BTP_API.ViewModels;

namespace BTP_API.ServicesImpl
{
    public class ManageTransactionRepository : IManageTransactionRepository
    {
        private readonly BTPContext _context;

        public ManageTransactionRepository(BTPContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> getAllExchangeAsync(int page = 1)
        {
            var exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).OrderByDescending(e => e.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Exchanges.CountAsync();
            //var result = PaginatedList<Exchange>.Create(exchanges, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllExchangeWaitingAsync(int page = 1)
        {
            var exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).Where(e => e.Status == Status.Waiting.ToString()).OrderByDescending(e => e.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Exchanges.Where(e => e.Status == Status.Waiting.ToString()).CountAsync();
            //var result = PaginatedList<Exchange>.Create(exchanges, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllExchangeTradingAsync(int page = 1)
        {
            var exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).Where(e => e.Status == Status.Trading.ToString()).OrderByDescending(e => e.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Exchanges.Where(e => e.Status == Status.Trading.ToString()).CountAsync();
            //var result = PaginatedList<Exchange>.Create(exchanges, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllExchangeCompleteAsync(int page = 1)
        {
            var exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).Where(e => e.Status == Status.Complete.ToString()).OrderByDescending(e => e.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Exchanges.Where(e => e.Status == Status.Complete.ToString()).CountAsync();
            //var result = PaginatedList<Exchange>.Create(exchanges, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllExchangeCancelAsync(int page = 1)
        {
            var exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).Where(e => e.Status == Status.Cancel.ToString()).OrderByDescending(e => e.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Exchanges.Where(e => e.Status == Status.Cancel.ToString()).CountAsync();
            //var result = PaginatedList<Exchange>.Create(exchanges, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> searchExchangeAsync(int? id, int page = 1)
        {
            List<Exchange> exchanges;
            if (id != null)
            {
                exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).Where(b => b.Id == id).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges.Skip(10 * (page - 1)).Take(10),
                NumberOfRecords = exchanges.Count
            };
        }

        public async Task<ApiResponse> getAllExchangeDetailAsync(int exchangeId)
        {
            var exchangeDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeDetails,
                NumberOfRecords = exchangeDetails.Count
            };
        }
        public async Task<ApiResponse> getAllExchangeBillAsync(int exchangeId)
        {
            var exchangeBills = await _context.ExchangeBills.Include(b => b.User).Include(b => b.FeeId1Navigation).Include(b => b.FeeId2Navigation).Include(b => b.FeeId3Navigation).Where(b => b.ExchangeId == exchangeId).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeBills,
                NumberOfRecords = exchangeBills.Count
            };
        }
        public async Task<ApiMessage> updateStatusExchangeAsync(int exchangeId, ExchangeVM exchangeVM)
        {
            var exchange = await _context.Exchanges.SingleOrDefaultAsync(b => b.Id == exchangeId);
            if(exchange != null)
            {
                exchange.Status = exchangeVM.Status;
                exchange.StorageStatus1 = exchangeVM.StorageStatus1;
                exchange.StorageStatus2 = exchangeVM.StorageStatus2;
                exchange.SendDate1 = exchangeVM.SendDate1;
                exchange.ReceiveDate1 = exchangeVM.ReceiveDate1;
                exchange.RecallDate1 = exchangeVM.RecallDate1;
                exchange.RefundDate1 = exchangeVM.RefundDate1;
                exchange.SendDate2 = exchangeVM.SendDate2;
                exchange.ReceiveDate2 = exchangeVM.ReceiveDate2;
                exchange.RecallDate2 = exchangeVM.RecallDate2;
                exchange.RefundDate2 = exchangeVM.RefundDate2;
                if (exchangeVM.Status == Status.Trading.ToString())
                {
                    var exchangeDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId && b.Status == Status.Waiting.ToString()).ToListAsync();
                    foreach (var detail in exchangeDetails)
                    {
                        detail.Status = exchangeVM.Status;
                    }
                }
                if(exchangeVM.Status == Status.Complete.ToString())
                {
                    var exchangeDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId && b.Status == Status.Trading.ToString()).ToListAsync();
                    foreach (var detail in exchangeDetails)
                    {
                        detail.Status = exchangeVM.Status;
                    }
                    var user1 = await _context.Users.SingleOrDefaultAsync(u => u.Id == exchange.UserId1);
                    var user2 = await _context.Users.SingleOrDefaultAsync(u => u.Id == exchange.UserId2);
                    user1.NumberOfTransaction += exchangeDetails.Count;
                    user2.NumberOfTransaction += exchangeDetails.Count;
                }
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
                exchangeDetail.BeforeStatusBook2 = exchangeDetailVM.BeforeStatusBook2;
                exchangeDetail.AfterStatusBook2 = exchangeDetailVM.AfterStatusBook2;
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

        public async Task<ApiResponse> getAllRentAsync(int page = 1)
        {
            var rents = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).OrderByDescending(r => r.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Rents.CountAsync();
            //var result = PaginatedList<Rent>.Create(rents, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getAllRentWaitingAsync(int page = 1)
        {
            var rents = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).Where(e => e.Status == Status.Waiting.ToString()).OrderByDescending(r => r.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Rents.Where(e => e.Status == Status.Waiting.ToString()).CountAsync();
            //var result = PaginatedList<Rent>.Create(rents, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllRentTradingAsync(int page = 1)
        {
            var rents = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).Where(e => e.Status == Status.Trading.ToString()).OrderByDescending(r => r.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Rents.Where(e => e.Status == Status.Trading.ToString()).CountAsync();
            //var result = PaginatedList<Rent>.Create(rents, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllRentCompleteAsync(int page = 1)
        {
            var rents = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).Where(e => e.Status == Status.Complete.ToString()).OrderByDescending(r => r.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Rents.Where(e => e.Status == Status.Complete.ToString()).CountAsync();
            //var result = PaginatedList<Rent>.Create(rents, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllRentCancelAsync(int page = 1)
        {
            var rents = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).Where(e => e.Status == Status.Cancel.ToString()).OrderByDescending(r => r.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Rents.Where(e => e.Status == Status.Cancel.ToString()).CountAsync();
            //var result = PaginatedList<Rent>.Create(rents, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> searchRentAsync(int? id, int page = 1)
        {
            List<Rent> rents;
            if (id != null)
            {
                rents = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).Where(b => b.Id == id).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                rents = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents.Skip(10 * (page - 1)).Take(10),
                NumberOfRecords = rents.Count
            };
        }


        public async Task<ApiResponse> getAllRentDetailAsync(int rentId)
        {
            var rentDetails = await _context.RentDetails.Where(b => b.RentId == rentId).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentDetails,
                NumberOfRecords = rentDetails.Count
            };
        }

        public async Task<ApiResponse> getAllRentBillAsync(int rentId)
        {
            var rentBills = await _context.RentBills.Include(b => b.User).Include(b => b.FeeId1Navigation).Include(b => b.FeeId2Navigation).Include(b => b.FeeId3Navigation).Where(b => b.RentId == rentId).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentBills,
                NumberOfRecords = rentBills.Count
            };
        }

        public async Task<ApiMessage> updateStatusRentAsync(int rentId, RentVM rentVM)
        {
            var rent = await _context.Rents.SingleOrDefaultAsync(b => b.Id == rentId);
            if (rent != null)
            {
               
                if (rentVM.Status == Status.Trading.ToString())
                {
                    var rentDetails = await _context.RentDetails.Where(b => b.RentId == rentId && b.Status == Status.Waiting.ToString()).ToListAsync();
                    foreach (var detail in rentDetails)
                    {
                        detail.Status = rentVM.Status;
                    }
                }
                if (rentVM.Status == Status.Complete.ToString())
                {
                    var rentDetails = await _context.RentDetails.Where(b => b.RentId == rentId && b.Status == Status.Trading.ToString()).ToListAsync();
                    foreach (var detail in rentDetails)
                    {
                        detail.Status = rentVM.Status;
                    }
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == rent.OwnerId);
                    user.NumberOfTransaction += rentDetails.Count;
                }
                rent.Status = rentVM.Status;
                rent.StorageStatus = rentVM.StorageStatus;
                rent.SendDate = rentVM.SendDate;
                rent.ReceiveDate = rentVM.ReceiveDate;
                rent.RecallDate = rentVM.RecallDate;
                rent.RefundDate = rentVM.RefundDate;
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
