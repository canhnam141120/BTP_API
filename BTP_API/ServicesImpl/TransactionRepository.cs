using BTP_API.Ultils;
using Microsoft.EntityFrameworkCore;

namespace BTP_API.ServicesImpl
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BTPContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionRepository(BTPContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiMessage> cancelExchangeAsync(int exchangeId)
        {

            var exchange = await _context.Exchanges.SingleOrDefaultAsync(b => b.Id == exchangeId);
            if (exchange != null)
            {
                var detail = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId).ToListAsync();
                List<int> bookIds = new List<int>();
                foreach (var item in detail)
                {
                    item.Status = Status.Cancel.ToString();
                    bookIds.Add(item.Book1Id);
                    bookIds.Add(item.Book2Id);
                }

                foreach (var item in bookIds)
                {
                    var book = await _context.Books.SingleOrDefaultAsync(r => r.Id == item);
                    if (book != null)
                    {
                        book.IsTrade = false;
                    }
                }

                var bill = await _context.ExchangeBills.Where(b => b.ExchangeId == exchangeId).ToListAsync();
                foreach (var item in bill)
                {
                    _context.Remove(item);
                }
                exchange.Status = Status.Cancel.ToString();
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.EXCHANGE_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> cancelExchangeDetailAsync(int exchangeDetailId)
        {
            CalculateFee calculateFee = new CalculateFee(_context);
            var exchangeDetail = await _context.ExchangeDetails.SingleOrDefaultAsync(b => b.Id == exchangeDetailId);
            if (exchangeDetail == null)
            {
                return new ApiMessage
                {
                    Message = Message.EXCHANGE_DETAIL_NOT_EXIST.ToString()
                };
            }

            var book1 = await _context.Books.SingleOrDefaultAsync(b => b.Id == exchangeDetail.Book1Id);
            var book2 = await _context.Books.SingleOrDefaultAsync(b => b.Id == exchangeDetail.Book2Id);

            book1.IsTrade = false;
            book2.IsTrade = false;

            double totalWeightBook1 = 0;
            double totalWeightBook2 = 0;
            var listBook = await _context.ExchangeDetails.Include(e => e.Book1).Include(e => e.Book2).Where(e => e.ExchangeId == exchangeDetail.ExchangeId).ToListAsync();
            foreach (var item in listBook)
            {
                totalWeightBook1 += item.Book1.Weight;
                totalWeightBook2 += item.Book2.Weight;
            }

            var bill1 = await _context.ExchangeBills.SingleOrDefaultAsync(b => b.ExchangeId == exchangeDetail.ExchangeId && b.UserId == book1.UserId);
            if (bill1 != null)
            {
                if (listBook.Count == 1)
                {
                    bill1.TotalBook = 0;
                    bill1.DepositFee = 0;
                    bill1.TotalAmount = 0;
                }
                if (listBook.Count > 2)
                {
                    bill1.TotalBook -= 1;
                    bill1.DepositFee -= book2.DepositPrice;
                    bill1.FeeId1 = calculateFee.feeShipID(totalWeightBook1 - book1.Weight);
                    bill1.FeeId2 = calculateFee.feeServiceID(1);
                    bill1.FeeId3 = calculateFee.feeServiceID(listBook.Count() - 1);
                    bill1.TotalAmount = calculateFee.totalAmountExchange(bill1);
                }
                if (listBook.Count == 2)
                {
                    bill1.TotalBook -= 1;
                    bill1.DepositFee -= book2.DepositPrice;
                    bill1.FeeId1 = calculateFee.feeShipID(totalWeightBook1 - book1.Weight);
                    bill1.FeeId2 = calculateFee.feeServiceID(1);
                    bill1.FeeId3 = null;
                    bill1.TotalAmount = calculateFee.totalAmountExchange(bill1);
                }
            }

            var bill2 = await _context.ExchangeBills.SingleOrDefaultAsync(b => b.ExchangeId == exchangeDetail.ExchangeId && b.UserId == book2.UserId);
            if (bill2 != null)
            {
                if (listBook.Count == 1)
                {
                    bill2.TotalBook = 0;
                    bill2.DepositFee = 0;
                    bill2.TotalAmount = 0;
                }
                if (listBook.Count > 2)
                {
                    bill2.TotalBook -= 1;
                    bill2.DepositFee -= book1.DepositPrice;
                    bill2.FeeId1 = calculateFee.feeShipID(totalWeightBook2 - book2.Weight);
                    bill2.FeeId2 = calculateFee.feeServiceID(1);
                    bill2.FeeId3 = calculateFee.feeServiceID(listBook.Count() - 1);
                    bill2.TotalAmount = calculateFee.totalAmountExchange(bill2);
                }
                if (listBook.Count == 2)
                {
                    bill2.TotalBook -= 1;
                    bill2.DepositFee -= book1.DepositPrice;
                    bill2.FeeId1 = calculateFee.feeShipID(totalWeightBook2 - book2.Weight);
                    bill2.FeeId2 = calculateFee.feeServiceID(1);
                    bill2.FeeId3 = null;
                    bill2.TotalAmount = calculateFee.totalAmountExchange(bill2);
                }
            }

            exchangeDetail.Status = Status.Cancel.ToString();

            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString()
            };
        }

        public async Task<ApiMessage> cancelRentAsync(int rentId)
        {

            var rent = await _context.Rents.SingleOrDefaultAsync(r => r.Id == rentId);
            if (rent == null)
            {
                return new ApiMessage
                {
                    Message = Message.RENT_NOT_EXIST.ToString()
                };
            }

            var rentDetails = await _context.RentDetails.Where(r => r.RentId == rentId).ToListAsync();
            List<int> bookIds = new List<int>();
            foreach (var item in rentDetails)
            {
                item.Status = Status.Cancel.ToString();
                bookIds.Add(item.BookId);
            }

            foreach (var item in bookIds)
            {
                var book = await _context.Books.SingleOrDefaultAsync(r => r.Id == item);
                if (book != null)
                {
                    book.IsTrade = false;
                }
            }

            var bill = await _context.RentBills.Where(b => b.RentId == rentId).ToListAsync();
            foreach (var item in bill)
            {
                _context.Remove(item);
            }
            rent.Status = Status.Cancel.ToString();
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString()
            };
        }

        public async Task<ApiMessage> cancelRentDetailAsync(int rentDetailId)
        {

            CalculateFee calculateFee = new CalculateFee(_context);
            Cookie cookie = new Cookie(_httpContextAccessor);
            if (cookie.GetUserId() == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var rentDetail = await _context.RentDetails.SingleOrDefaultAsync(b => b.Id == rentDetailId);
            if (rentDetail == null)
            {
                return new ApiMessage
                {
                    Message = Message.RENT_DETAIL_NOT_EXIST.ToString()
                };
            }

            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == rentDetail.BookId);

            book.IsTrade = false;

            double totalWeightBook = 0;
            var listBook = await _context.RentDetails.Include(e => e.Book).Where(e => e.RentId == rentDetail.RentId).ToListAsync();
            foreach (var item in listBook)
            {
                totalWeightBook += item.Book.Weight;
            }

            var billOnwer = await _context.RentBills.SingleOrDefaultAsync(b => b.RentId == rentDetail.RentId && b.UserId == book.UserId);
            if (billOnwer != null)
            {
                if (listBook.Count == 1)
                {
                    billOnwer.TotalBook = 0;
                    billOnwer.DepositFee = 0;
                    billOnwer.TotalAmount = 0;
                }
                if (listBook.Count > 2)
                {
                    billOnwer.TotalBook -= 1;
                    billOnwer.FeeId1 = calculateFee.feeShipID(totalWeightBook - book.Weight);
                    billOnwer.FeeId2 = calculateFee.feeServiceID(1);
                    billOnwer.FeeId3 = calculateFee.feeServiceID(listBook.Count() - 1);
                    billOnwer.TotalAmount = calculateFee.totalAmountRent(billOnwer);
                }
                if (listBook.Count == 2)
                {
                    billOnwer.TotalBook -= 1;
                    billOnwer.FeeId1 = calculateFee.feeShipID(totalWeightBook - book.Weight);
                    billOnwer.FeeId2 = calculateFee.feeServiceID(1);
                    billOnwer.FeeId3 = null;
                    billOnwer.TotalAmount = calculateFee.totalAmountRent(billOnwer);
                }
            }

            var billRenter = await _context.RentBills.SingleOrDefaultAsync(b => b.RentId == rentDetail.RentId && b.UserId == cookie.GetUserId());
            if (billRenter != null)
            {
                if (listBook.Count == 1)
                {
                    billRenter.TotalBook = 0;
                    billRenter.DepositFee = 0;
                    billRenter.RentFee = 0;
                    billRenter.TotalAmount = 0;
                }
                if (listBook.Count > 2)
                {
                    billRenter.TotalBook -= 1;
                    billRenter.DepositFee -= book.DepositPrice;
                    billRenter.RentFee -= book.RentFee;
                    billRenter.FeeId1 = calculateFee.feeShipID(totalWeightBook - book.Weight);
                    billRenter.FeeId2 = calculateFee.feeServiceID(1);
                    billRenter.FeeId3 = calculateFee.feeServiceID(listBook.Count() - 1);
                    billRenter.TotalAmount = calculateFee.totalAmountRent(billRenter);
                }
                if (listBook.Count == 2)
                {
                    billRenter.TotalBook -= 1;
                    billRenter.DepositFee -= book.DepositPrice;
                    billRenter.RentFee -= book.RentFee;
                    billRenter.FeeId1 = calculateFee.feeShipID(totalWeightBook - book.Weight);
                    billRenter.FeeId2 = calculateFee.feeServiceID(1);
                    billRenter.FeeId3 = null;
                    billRenter.TotalAmount = calculateFee.totalAmountRent(billRenter);
                }
            }
            rentDetail.Status = Status.Cancel.ToString();
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString()
            };
        }
    }
}
