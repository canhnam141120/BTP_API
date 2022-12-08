using BTP_API.Models;
using BTP_API.ViewModels;
using System;
using System.Net.WebSockets;

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

        public async Task<ApiResponse> getExchangeByIDAsync(int exchangeId)
        {
            var exchanges = await _context.Exchanges.Include(e => e.UserId1Navigation).Include(e => e.UserId2Navigation).SingleOrDefaultAsync(e => e.Id == exchangeId);
            
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
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
            var exchangeDetails = await _context.ExchangeDetails.Include(b => b.Book1).Include(b => b.Book2).Where(b => b.ExchangeId == exchangeId).OrderByDescending(b => b.Id).ToListAsync();
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
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            var exchange = await _context.Exchanges.SingleOrDefaultAsync(b => b.Id == exchangeId);
            var bill1 = await _context.ExchangeBills.SingleOrDefaultAsync(x => x.ExchangeId == exchangeId && x.UserId == exchange.UserId1);
            var bill2 = await _context.ExchangeBills.SingleOrDefaultAsync(x => x.ExchangeId == exchangeId && x.UserId == exchange.UserId2);
            if (exchange != null)
            {
                exchange.StorageStatus1 = exchangeVM.StorageStatus1;
                exchange.StorageStatus2 = exchangeVM.StorageStatus2;
                if(exchangeVM.SendDate1 != null)
                {
                    exchange.SendDate1 = DateOnly.Parse(exchangeVM.SendDate1);
                }
                if (exchangeVM.ReceiveDate1 != null)
                {
                    exchange.ReceiveDate1 = DateOnly.Parse(exchangeVM.ReceiveDate1);
                }
                if (exchangeVM.RecallDate1 != null)
                {
                    exchange.RecallDate1 = DateOnly.Parse(exchangeVM.RecallDate1);
                }
                if (exchangeVM.RefundDate1 != null)
                {
                    bill1.IsRefund = true;
                    bill1.RefundDate = DateOnly.Parse(exchangeVM.RefundDate1);
                    exchange.RefundDate1 = DateOnly.Parse(exchangeVM.RefundDate1);
                }
                if (exchangeVM.SendDate2 != null)
                {
                    exchange.SendDate2 = DateOnly.Parse(exchangeVM.SendDate2);
                }
                if (exchangeVM.ReceiveDate2 != null)
                {
                    exchange.ReceiveDate2 = DateOnly.Parse(exchangeVM.ReceiveDate2);
                }
                if (exchangeVM.RecallDate2 != null)
                {
                    exchange.RecallDate2 = DateOnly.Parse(exchangeVM.RecallDate2);
                }
                if (exchangeVM.RefundDate2 != null)
                {
                    bill2.IsRefund = true;
                    bill2.RefundDate = DateOnly.Parse(exchangeVM.RefundDate2);
                    exchange.RefundDate2 = DateOnly.Parse(exchangeVM.RefundDate2);
                }        

                _context.Update(exchange);

                var notification = new Notification
                {
                    UserId = exchange.UserId1,
                    Content = "Có cập nhật mới từ giao dịch đổi số " + exchange.Id + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification);
                var notification1 = new Notification
                {
                    UserId = exchange.UserId2,
                    Content = "Có cập nhật mới từ giao dịch đổi số " + exchange.Id + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification1);

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

        public async Task<ApiMessage> tradingExchangeAsync(int exchangeId)
        {
            var exchange = await _context.Exchanges.SingleOrDefaultAsync(b => b.Id == exchangeId);
            if (exchange != null)
            {
                exchange.Status = Status.Trading.ToString();
                    var exchangeDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId && b.Status == Status.Waiting.ToString()).ToListAsync();
                    foreach (var detail in exchangeDetails)
                    {
                        detail.Status = Status.Trading.ToString();
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

        public async Task<ApiMessage> completeExchangeAsync(int exchangeId)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var exchange = await _context.Exchanges.SingleOrDefaultAsync(b => b.Id == exchangeId);
            if (exchange != null)
            {
                exchange.Status = Status.Complete.ToString();

                    var exchangeDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId && b.Status == Status.Trading.ToString()).ToListAsync();
                    foreach (var detail in exchangeDetails)
                    {
                        detail.Status = Status.Complete.ToString();
                        var book1 = await _context.Books.SingleOrDefaultAsync(b => b.Id == detail.Book1Id);
                        book1.IsTrade = false;
                        var book2 = await _context.Books.SingleOrDefaultAsync(b => b.Id == detail.Book2Id);
                        book2.IsTrade = false;
                    }
                    var user1 = await _context.Users.SingleOrDefaultAsync(u => u.Id == exchange.UserId1);
                    var user2 = await _context.Users.SingleOrDefaultAsync(u => u.Id == exchange.UserId2);
                    user1.NumberOfTransaction += exchangeDetails.Count;
                    user2.NumberOfTransaction += exchangeDetails.Count;

                var notification = new Notification
                {
                    UserId = exchange.UserId1,
                    Content = "Giao dịch đổi số " + exchange.Id + " của bạn đã kết thúc! Bạn có thể vào chi tiết giao dịch để đánh giá sách!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification);
                var notification1 = new Notification
                {
                    UserId = exchange.UserId2,
                    Content = "Giao dịch đổi số " + exchange.Id + " của bạn đã kết thúc! Bạn có thể vào chi tiết giao dịch để đánh giá sách!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification1);


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
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var exchangeDetail = await _context.ExchangeDetails.SingleOrDefaultAsync(b => b.Id == exchangeDetailId);
            if (exchangeDetail != null)
            {
                var exchange = await _context.Exchanges.SingleOrDefaultAsync(b => b.Id == exchangeDetail.ExchangeId);

                exchangeDetail.BeforeStatusBook1 = exchangeDetailVM.BeforeStatusBook1;
                exchangeDetail.AfterStatusBook1 = exchangeDetailVM.AfterStatusBook1;
                exchangeDetail.BeforeStatusBook2 = exchangeDetailVM.BeforeStatusBook2;
                exchangeDetail.AfterStatusBook2 = exchangeDetailVM.AfterStatusBook2;
                
                _context.Update(exchangeDetail);

                var notification = new Notification
                {
                    UserId = exchange.UserId1,
                    Content = "Có cập nhật mới từ chi tiết giao dịch đổi số " + exchangeDetail.Id + " thuộc giao dịch đổi số " + exchangeDetail.ExchangeId + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification);
                var notification1 = new Notification
                {
                    UserId = exchange.UserId2,
                    Content = "Có cập nhật mới từ chi tiết giao dịch đổi số " + exchangeDetail.Id + " thuộc giao dịch đổi số " + exchangeDetail.ExchangeId + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification1);

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

        public async Task<ApiResponse> getRentByIDAsync(int rentId)
        {
            var rent = await _context.Rents.Include(e => e.Owner).Include(e => e.Renter).SingleOrDefaultAsync(e => e.Id == rentId);

            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rent,
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
            var rentDetails = await _context.RentDetails.Include(b => b.Book).Where(b => b.RentId == rentId).OrderByDescending(b => b.Id).ToListAsync();
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
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var rent = await _context.Rents.SingleOrDefaultAsync(b => b.Id == rentId);
            var billOwner = await _context.RentBills.SingleOrDefaultAsync(x => x.RentId == rentId && x.UserId == rent.OwnerId);
            var billRenter = await _context.RentBills.SingleOrDefaultAsync(x => x.RentId == rentId && x.UserId == rent.RenterId);
            if (rent != null)
            {
                rent.StorageStatus = rentVM.StorageStatus;
                if(rentVM.SendDate != null)
                {
                    rent.SendDate = DateOnly.Parse(rentVM.SendDate);
                }
                if (rentVM.ReceiveDate != null)
                {
                    rent.ReceiveDate = DateOnly.Parse(rentVM.ReceiveDate);
                }
                if (rentVM.RecallDate != null)
                {
                    billRenter.IsRefund = true;
                    billRenter.RefundDate = DateOnly.Parse(rentVM.RecallDate);
                    rent.RecallDate = DateOnly.Parse(rentVM.RecallDate);
                }
                if (rentVM.RefundDate != null)
                {
                    billOwner.IsRefund = true;
                    billOwner.RefundDate = DateOnly.Parse(rentVM.RefundDate);
                    rent.RefundDate = DateOnly.Parse(rentVM.RefundDate);
                }
                _context.Update(rent);

                var notification = new Notification
                {
                    UserId = rent.OwnerId,
                    Content = "Có cập nhật mới từ giao dịch thuê số " + rent.Id + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification);
                var notification1 = new Notification
                {
                    UserId = rent.RenterId,
                    Content = "Có cập nhật mới từ giao dịch thuê số " + rent.Id + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification1);

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

        public async Task<ApiMessage> tradingRentAsync(int rentId)
        {
            var rent = await _context.Rents.SingleOrDefaultAsync(b => b.Id == rentId);
            if (rent != null)
            {
                rent.Status = Status.Trading.ToString();
                var rentDetails = await _context.RentDetails.Where(b => b.RentId == rentId && b.Status == Status.Waiting.ToString()).ToListAsync();
                    foreach (var detail in rentDetails)
                    {
                        detail.Status = Status.Trading.ToString();
                }
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

        public async Task<ApiMessage> completeRentAsync(int rentId)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var rent = await _context.Rents.SingleOrDefaultAsync(b => b.Id == rentId);
            if (rent != null)
            {
                rent.Status = Status.Complete.ToString();

                    var rentDetails = await _context.RentDetails.Where(b => b.RentId == rentId && b.Status == Status.Trading.ToString()).ToListAsync();
                    foreach (var detail in rentDetails)
                    {
                        detail.Status = Status.Complete.ToString();
                    var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == detail.BookId);
                    book.IsTrade = false;
                    }
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == rent.OwnerId);
                    user.NumberOfTransaction += rentDetails.Count;

                var notification = new Notification
                {
                    UserId = rent.RenterId,
                    Content = "Giao dịch thuê số " + rent.Id + " của bạn đã kết thúc! Bạn có thể vào chi tiết giao dịch để đánh giá sách!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification);
                var notification1 = new Notification
                {
                    UserId = rent.OwnerId,
                    Content = "Giao dịch đổi số " + rent.Id + " của bạn đã kết thúc!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };

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
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var rentDetail = await _context.RentDetails.SingleOrDefaultAsync(b => b.Id == rentDetailId);
            if (rentDetail != null)
            {
                var rent = await _context.Rents.SingleOrDefaultAsync(b => b.Id == rentDetail.RentId);

                rentDetail.BeforeStatusBook = rentDetailVM.BeforeStatusBook;
                rentDetail.AfterStatusBook = rentDetailVM.AfterStatusBook;
                _context.Update(rentDetail);

                var notification = new Notification
                {
                    UserId = rent.OwnerId,
                    Content = "Có cập nhật mới từ chi tiết giao dịch thuê số " + rentDetail.Id + " thuộc giao dịch thuê số " + rentDetail.RentId + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification);
                var notification1 = new Notification
                {
                    UserId = rent.RenterId,
                    Content = "Có cập nhật mới từ chi tiết giao dịch thuê số " + rentDetail.Id + " thuộc giao dịch thuê số " + rentDetail.RentId + "!",
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                    IsRead = false,
                };
                _context.Add(notification1);

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

        public async Task<ApiResponse> dashboardAsync(int quarter)
        {
            int count = 0;
            float total = 0;
            float exchange = 0;
            float rent = 0;
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var year = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo).Year;
            switch (quarter)
            {
                case 1:
                    var dateStart1 = new DateOnly(year, 1, 1);
                    var dateEnd1 = new DateOnly(year, 3, 31);
                    var listExchange1 = await _context.Exchanges.Where(b => b.Date >= dateStart1 && b.Date <= dateEnd1 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    var listRent1 = await _context.Rents.Where(b => b.Date >= dateStart1 && b.Date <= dateEnd1 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    foreach(var i in listExchange1)
                    {
                        var listBills = await _context.ExchangeBills.Where(b => b.ExchangeId == i).ToListAsync();
                        foreach(var y in listBills)
                        {
                            exchange += (y.TotalAmount - y.DepositFee);
                        }
                    }

                    foreach (var i in listRent1)
                    {
                        var listBills = await _context.RentBills.Where(b => b.RentId == i).ToListAsync();
                        foreach (var y in listBills)
                        {
                            rent += (y.TotalAmount - y.DepositFee - y.RentFee);
                        }
                    }
                    total = exchange + rent;
                    count = listExchange1.Count + listRent1.Count;
                    break;
                case 2:
                    var dateStart2 = new DateOnly(year, 4, 1);
                    var dateEnd2 = new DateOnly(year, 6, 30);
                    var listExchange2 = await _context.Exchanges.Where(b => b.Date >= dateStart2 && b.Date <= dateEnd2 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    var listRent2 = await _context.Rents.Where(b => b.Date >= dateStart2 && b.Date <= dateEnd2 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    foreach (var i in listExchange2)
                    {
                        var listBills = await _context.ExchangeBills.Where(b => b.ExchangeId == i).ToListAsync();
                        foreach (var y in listBills)
                        {
                            exchange += (y.TotalAmount - y.DepositFee);
                        }
                    }

                    foreach (var i in listRent2)
                    {
                        var listBills = await _context.RentBills.Where(b => b.RentId == i).ToListAsync();
                        foreach (var y in listBills)
                        {
                            rent += (y.TotalAmount - y.DepositFee - y.RentFee);
                        }
                    }
                    total = exchange + rent;
                    count = listExchange2.Count + listRent2.Count;
                    break;
                case 3:
                    var dateStart3 = new DateOnly(year, 7, 1);
                    var dateEnd3 = new DateOnly(year, 9, 31);
                    var listExchange3 = await _context.Exchanges.Where(b => b.Date >= dateStart3 && b.Date <= dateEnd3 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    var listRent3 = await _context.Rents.Where(b => b.Date >= dateStart3 && b.Date <= dateEnd3 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    foreach (var i in listExchange3)
                    {
                        var listBills = await _context.ExchangeBills.Where(b => b.ExchangeId == i).ToListAsync();
                        foreach (var y in listBills)
                        {
                            exchange += (y.TotalAmount - y.DepositFee);
                        }
                    }

                    foreach (var i in listRent3)
                    {
                        var listBills = await _context.RentBills.Where(b => b.RentId == i).ToListAsync();
                        foreach (var y in listBills)
                        {
                            rent += (y.TotalAmount - y.DepositFee - y.RentFee);
                        }
                    }
                    total = exchange + rent;
                    count = listExchange3.Count + listRent3.Count;
                    break;
                case 4:
                    var dateStart4 = new DateOnly(year, 10, 1);
                    var dateEnd4 = new DateOnly(year, 12, 30);
                    var listExchange4 = await _context.Exchanges.Where(b => b.Date >= dateStart4 && b.Date <= dateEnd4 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    var listRent4 = await _context.Rents.Where(b => b.Date >= dateStart4 && b.Date <= dateEnd4 && b.Status == Status.Complete.ToString()).Select(b => b.Id).ToListAsync();
                    foreach (var i in listExchange4)
                    {
                        var listBills = await _context.ExchangeBills.Where(b => b.ExchangeId == i).ToListAsync();
                        foreach (var y in listBills)
                        {
                            exchange += (y.TotalAmount - y.DepositFee);
                        }
                    }

                    foreach (var i in listRent4)
                    {
                        var listBills = await _context.RentBills.Where(b => b.RentId == i).ToListAsync();
                        foreach (var y in listBills)
                        {
                            rent += (y.TotalAmount - y.DepositFee - y.RentFee);
                        }
                    }
                    total = exchange + rent;
                    count = listExchange4.Count + listRent4.Count;
                     break;
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = total,
                NumberOfRecords = count
            };
        }

        public async Task<ApiMessage> autoTradingExchangeAsync()
        {
            var listExchange = await _context.Exchanges.Where(e => e.Status == Status.Waiting.ToString()).ToListAsync();
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            if (listExchange == null)
			{
				return new ApiMessage
				{
					Message = Message.EXCHANGE_NOT_EXIST.ToString()
				};
			}				
            foreach(var ex in listExchange)
            {
                var listBill = await _context.ExchangeBills.Where(e => e.ExchangeId == ex.Id).ToListAsync();
                if (listBill[0].IsPaid && listBill[1].IsPaid)
                {
                    ex.Status = Status.Trading.ToString();
                    var exDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == ex.Id && b.Status == Status.Waiting.ToString()).ToListAsync();
                    foreach (var detail in exDetails)
                    {
                        detail.Status = Status.Trading.ToString();

                        var notification = new Notification
                        {
                            UserId = ex.UserId1,
                            Content = "Giao dịch đổi của bạn đã được duyệt với mã giao dịch là: " + ex.Id + "!",
                            CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                            IsRead = false,
                        };
                        _context.Add(notification);
                        var notification1 = new Notification
                        {
                            UserId = ex.UserId2,
                            Content = "Giao dịch đổi của bạn đã được duyệt với mã giao dịch là: " + ex.Id + "!",
                            CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                            IsRead = false,
                        };
                        _context.Add(notification1);
                    }
                }
                if (!listBill[0].IsPaid)
                {
                    var notification = new Notification
                    {
                        UserId = listBill[0].UserId,
                        Content = "Nhắc nhở: Bạn chưa thanh toán giao dịch số " + ex.Id,
                        CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                        IsRead = false,
                    };
                    _context.Add(notification);
                }
                if (!listBill[1].IsPaid)
                {
                    var notification = new Notification
                    {
                        UserId = listBill[1].UserId,
                        Content = "Nhắc nhở: Vui lòng thanh toán giao dịch số " + ex.Id,
                        CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                        IsRead = false,
                    };
                    _context.Add(notification);
                }
            }

           
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString()
            };
        }

        public async Task<ApiMessage> autoTradingRentAsync()
        {
            var listRent = await _context.Rents.Where(e => e.Status == Status.Waiting.ToString()).ToListAsync();
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            if (listRent == null)
			{
				return new ApiMessage
				{
					Message = Message.RENT_NOT_EXIST.ToString()
				};
			}

            foreach (var ex in listRent)
            {
                var listBill = await _context.RentBills.Where(e => e.RentId == ex.Id).ToListAsync();
                if (listBill[0].IsPaid && listBill[1].IsPaid)
                {
                    ex.Status = Status.Trading.ToString();
                    var rentDetails = await _context.RentDetails.Where(b => b.RentId == ex.Id && b.Status == Status.Waiting.ToString()).ToListAsync();
                    foreach (var detail in rentDetails)
                    {
                        detail.Status = Status.Trading.ToString();
                        var notification = new Notification
                        {
                            UserId = ex.RenterId,
                            Content = "Giao dịch thuê của bạn đã được duyệt với mã giao dịch là: " + ex.Id + "!",
                            CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                            IsRead = false,
                        };
                        _context.Add(notification);
                        var notification1 = new Notification
                        {
                            UserId = ex.OwnerId,
                            Content = "Giao dịch thuê của bạn đã được duyệt với mã giao dịch là: " + ex.Id + "!",
                            CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                            IsRead = false,
                        };
                        _context.Add(notification1);
                    }
                }
                if (!listBill[0].IsPaid)
                {
                    var notification = new Notification
                    {
                        UserId = listBill[0].UserId,
                        Content = "Nhắc nhở: Vui lòng thanh toán giao dịch số " + ex.Id,
                        CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                        IsRead = false,
                    };
                    _context.Add(notification);
                }
                if (!listBill[1].IsPaid)
                {
                    var notification = new Notification
                    {
                        UserId = listBill[1].UserId,
                        Content = "Nhắc nhở: Vui lòng thanh toán giao dịch số " + ex.Id,
                        CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                        IsRead = false,
                    };
                    _context.Add(notification);
                }
            }
           
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString()
            };
        }

    }
}
