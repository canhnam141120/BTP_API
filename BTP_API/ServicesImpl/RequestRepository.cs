namespace BTP_API.ServicesImpl
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BTPContext _context;

        public RequestRepository(BTPContext context)
        {
            _context = context;
        }
        public async Task<ApiMessage> createRequestAsync(int userId, int bookid, List<int> bookOffer)
        {
            var user = await _context.ShipInfos.SingleOrDefaultAsync(s => s.UserId == userId);
            if(user.IsUpdate == false)
            {
                return new ApiMessage
                {
                    Message = Message.SHIP_INFO_EMPTY.ToString()
                };
            }

            var userInfo = await _context.Users.SingleOrDefaultAsync(s => s.Id == userId);

            var bookCheck = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookid);
            if(bookCheck == null)
            {
                return new ApiMessage
                {
                    Message = Message.BOOK_NOT_EXIST.ToString()
                };
            }
            foreach(var item in bookOffer)
            {
                var bookOfferCheck = await _context.Books.SingleOrDefaultAsync(b => b.Id == item);
                if(bookOfferCheck == null)
                {
                    return new ApiMessage
                    {
                        Message = Message.BOOK_NOT_EXIST.ToString()
                    };
                }
            }

            foreach (int i in bookOffer)
            {
                var request = new ExchangeRequest
                {
                    BookId = bookid,
                    BookOfferId = i,
                    IsAccept = false,
                    RequestTime = DateTime.Now,
                    Status = StatusRequest.Waiting.ToString(),
                    IsNewest = true,
                    Flag = true
                };
                _context.Add(request);

                var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == i);
                book.IsTrade = true;              
            }

            var notification = new Notification
            {
                UserId = bookCheck.UserId,
                Content = userInfo.Fullname + @" đã yêu cầu đổi sách """ + bookCheck.Title + @""" của bạn - Vào danh sách yêu cầu để kiểm tra!",
                CreatedDate = DateTime.Now,
                IsRead = false,
            };
            _context.Add(notification);

            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.REQUEST_SUCCESS.ToString(),
            };
        }
        public async Task<ApiMessage> cancelRequestAsync(int userId, int requestId)
        {
            var request = await _context.ExchangeRequests.SingleOrDefaultAsync(r => r.Id == requestId && r.Status == StatusRequest.Waiting.ToString());
            if (request == null)
            {
                return new ApiMessage
                {
                    Message = Message.REQUEST_NOT_EXIST.ToString(),
                };
            }

            var bookOffer = await _context.Books.Include(b => b.User).SingleOrDefaultAsync(b => b.Id == request.BookOfferId && b.UserId == userId);
            if(bookOffer == null)
            {
                return new ApiMessage
                {
                    Message = Message.REQUEST_NOT_EXIST.ToString(),
                };
            }

            bookOffer.IsTrade = false;

            request.IsNewest = false;
            request.Status = StatusRequest.Cancel.ToString();

            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == request.BookId);
            var notification = new Notification
            {
                UserId = book.UserId,
                Content = bookOffer.User.Fullname +  @" đã hủy yêu cầu đổi sách """ + bookOffer.Title  + @""" lấy sách """ + book.Title + @""" của bạn!",
                CreatedDate = DateTime.Now,
                IsRead = false,
            };
            _context.Add(notification);

            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString(),
            };
        }
        public async Task<ApiMessage> acceptRequestAsync(int userId, int requestId)
        {
            CalculateFee calculateFee = new CalculateFee(_context);
            var request = await _context.ExchangeRequests.SingleOrDefaultAsync(r => r.Id == requestId && r.Status == StatusRequest.Waiting.ToString());
            if (request == null)
            {
                return new ApiMessage
                {
                    Message = Message.REQUEST_NOT_EXIST.ToString(),
                };
            }

            var book1 = await _context.Books.Include(b => b.User).SingleOrDefaultAsync(b => b.Id == request.BookId && b.UserId == userId);
            if (book1 == null)
            {
                return new ApiMessage
                {
                    Message = Message.REQUEST_NOT_EXIST.ToString(),
                };
            }

            var listRequect = await _context.ExchangeRequests.Where(r => r.BookId == request.BookId && r.BookOfferId != request.BookOfferId
            && r.IsNewest == true && r.Status == StatusRequest.Waiting.ToString()).ToListAsync();

            List<int> bookIds = new List<int>();
            foreach (var item in listRequect)
            {
                item.Status = StatusRequest.Denied.ToString();
                item.IsNewest = false;
                bookIds.Add(item.BookOfferId);
            }
            foreach (var item in bookIds)
            {
                var bookOffer = await _context.Books.SingleOrDefaultAsync(r => r.Id == item);
                if (bookOffer != null)
                {
                    bookOffer.IsTrade = false;
                    var notification = new Notification
                    {
                        UserId = bookOffer.UserId,
                        Content = @"Yêu cầu đổi sách """ + bookOffer.Title + @""" của bạn lấy sách """ + book1.Title +  @""" của" + book1.User.Fullname + " không được chấp nhận!",
                        CreatedDate = DateTime.Now,
                        IsRead = false,
                    };
                    _context.Add(notification);
                }
            }

            request.IsAccept = true;
            request.Status = StatusRequest.Approved.ToString();

            var book2 = await _context.Books.Include(r => r.User).SingleOrDefaultAsync(r => r.Id == request.BookOfferId);

            if (book1 == null || book2 == null)
            {
                return new ApiMessage
                {
                    Message = Message.BOOK_NOT_EXIST.ToString(),
                };
            }

            book1.IsTrade = true;
            book2.IsTrade = true;

            var newExchange = new Exchange
            {
                UserId1 = book1.UserId,
                UserId2 = book2.UserId,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Status = Status.Waiting.ToString(),
            };

            int numberOfDays = 0;
            if (book1.NumberOfDays > book2.NumberOfDays)
            {
                numberOfDays = book1.NumberOfDays;
            }
            else
            {
                numberOfDays = book2.NumberOfDays;
            }

            var check1 = await _context.Exchanges.SingleOrDefaultAsync(e => e.UserId1 == newExchange.UserId1 && e.UserId2 == newExchange.UserId2 && e.Date == newExchange.Date);

            var check2 = await _context.Exchanges.SingleOrDefaultAsync(e => e.UserId1 == newExchange.UserId2 && e.UserId2 == newExchange.UserId1 && e.Date == newExchange.Date);


            //Nếu giao dịch chưa có => tạo mới
            if (check1 == null && check2 == null)
            {
                _context.Add(newExchange);
                await _context.SaveChangesAsync();
                var exchange = await _context.Exchanges.SingleOrDefaultAsync(e => e.UserId1 == newExchange.UserId1 && e.UserId2 == newExchange.UserId2 && e.Date == newExchange.Date);
                //Tạo mới cả chi tiết giao dịch và hóa đơn cho giao dịch đó
                if (exchange != null)
                {
                    var newExchangeDetail = new ExchangeDetail
                    {
                        ExchangeId = exchange.Id,
                        Book1Id = book1.Id,
                        StorageStatusBook1 = StorageStatus.Waiting.ToString(),
                        Book2Id = book2.Id,
                        StorageStatusBook2 = StorageStatus.Waiting.ToString(),
                        RequestTime = DateTime.Now,
                        ExpiredDate = DateOnly.FromDateTime(DateTime.Now.AddDays(numberOfDays)),
                        Status = Status.Waiting.ToString(),
                        Flag = true
                    };
                    _context.Add(newExchangeDetail);

                    var newBillUser1 = new ExchangeBill
                    {
                        ExchangeId = exchange.Id,
                        UserId = book1.UserId,
                        TotalBook = 1,
                        TotalAmount = 0,
                        DepositFee = book2.DepositPrice,
                        FeeId1 = calculateFee.feeShipID(book1.Weight),
                        FeeId2 = calculateFee.feeServiceID(1),
                        IsPaid = false,
                        Flag = true
                    };

                    newBillUser1.TotalAmount = calculateFee.totalAmountExchange(newBillUser1);

                    var newBillUser2 = new ExchangeBill
                    {
                        ExchangeId = exchange.Id,
                        UserId = book2.UserId,
                        TotalBook = 1,
                        TotalAmount = 0,
                        DepositFee = book1.DepositPrice,
                        FeeId1 = calculateFee.feeShipID(book2.Weight),
                        FeeId2 = calculateFee.feeServiceID(1),
                        IsPaid = false,
                        Flag = true
                    };
                    newBillUser2.TotalAmount = calculateFee.totalAmountExchange(newBillUser2);

                    _context.Add(newBillUser1);
                    _context.Add(newBillUser2);
                    await _context.SaveChangesAsync();
                }
            }
            //Nếu đã có giao dịch => Update giao dịch
            else
            {
                if (check1 != null && check2 == null)
                {
                    var newExchangeDetail = new ExchangeDetail
                    {
                        ExchangeId = check1.Id,
                        Book1Id = book1.Id,
                        StorageStatusBook1 = StorageStatus.Waiting.ToString(),
                        Book2Id = book2.Id,
                        StorageStatusBook2 = StorageStatus.Waiting.ToString(),
                        RequestTime = DateTime.Now,
                        ExpiredDate = DateOnly.FromDateTime(DateTime.Now.AddDays(numberOfDays)),
                        Status = Status.Waiting.ToString(),
                        Flag = true
                    };

                    double totalWeightBook1 = 0;
                    double totalWeightBook2 = 0;
                    var listBook = await _context.ExchangeDetails.Include(e => e.Book1).Include(e => e.Book2).Where(e => e.ExchangeId == check1.Id).ToListAsync();
                    foreach (var item in listBook)
                    {
                        totalWeightBook1 += item.Book1.Weight;
                        totalWeightBook2 += item.Book2.Weight;
                    }

                    var billUser1 = await _context.ExchangeBills.SingleOrDefaultAsync(e => e.ExchangeId == check1.Id && e.UserId == book1.UserId);
                    if (billUser1 != null)
                    {
                        billUser1.TotalBook += 1;
                        billUser1.DepositFee += book2.DepositPrice;
                        billUser1.FeeId1 = calculateFee.feeShipID(totalWeightBook1 + book1.Weight);
                        billUser1.FeeId2 = calculateFee.feeServiceID(1);
                        billUser1.FeeId3 = calculateFee.feeServiceID(2);
                        billUser1.TotalAmount = calculateFee.totalAmountExchange(billUser1);
                    }
                    var billUser2 = await _context.ExchangeBills.SingleOrDefaultAsync(e => e.ExchangeId == check1.Id && e.UserId == book2.UserId);
                    if (billUser2 != null)
                    {
                        billUser2.TotalBook += 1;
                        billUser2.DepositFee += book1.DepositPrice;
                        billUser2.FeeId1 = calculateFee.feeShipID(totalWeightBook2 + book2.Weight);
                        billUser2.FeeId2 = calculateFee.feeServiceID(1);
                        billUser2.FeeId3 = calculateFee.feeServiceID(2);
                        billUser2.TotalAmount = calculateFee.totalAmountExchange(billUser2);
                    }
                    _context.Add(newExchangeDetail);
                    await _context.SaveChangesAsync();
                }
                if (check1 == null && check2 != null)
                {
                    var newExchangeDetail = new ExchangeDetail
                    {
                        ExchangeId = check2.Id,
                        Book1Id = book2.Id,
                        StorageStatusBook1 = StorageStatus.Waiting.ToString(),
                        Book2Id = book1.Id,
                        StorageStatusBook2 = StorageStatus.Waiting.ToString(),
                        RequestTime = DateTime.Now,
                        ExpiredDate = DateOnly.FromDateTime(DateTime.Now.AddDays(numberOfDays)),
                        Status = Status.Waiting.ToString(),
                        Flag = true
                    };

                    double totalWeightBook1 = 0;
                    double totalWeightBook2 = 0;
                    var listBook = await _context.ExchangeDetails.Include(e => e.Book1).Include(e => e.Book2).Where(e => e.ExchangeId == check2.Id).ToListAsync();
                    foreach (var item in listBook)
                    {
                        totalWeightBook1 += item.Book1.Weight;
                        totalWeightBook2 += item.Book2.Weight;
                    }

                    var billUser1 = await _context.ExchangeBills.SingleOrDefaultAsync(e => e.ExchangeId == check2.Id && e.UserId == book2.UserId);
                    if (billUser1 != null)
                    {
                        billUser1.TotalBook += 1;
                        billUser1.DepositFee += book1.DepositPrice;
                        billUser1.FeeId1 = calculateFee.feeShipID(totalWeightBook1 + book1.Weight);
                        billUser1.FeeId2 = calculateFee.feeServiceID(1);
                        billUser1.FeeId3 = calculateFee.feeServiceID(2);
                        billUser1.TotalAmount = calculateFee.totalAmountExchange(billUser1);
                    }
                    var billUser2 = await _context.ExchangeBills.SingleOrDefaultAsync(e => e.ExchangeId == check2.Id && e.UserId == book1.UserId);
                    if (billUser2 != null)
                    {
                        billUser2.TotalBook += 1;
                        billUser2.DepositFee += book2.DepositPrice;
                        billUser2.FeeId1 = calculateFee.feeShipID(totalWeightBook2 + book2.Weight);
                        billUser2.FeeId2 = calculateFee.feeServiceID(1);
                        billUser2.FeeId3 = calculateFee.feeServiceID(2);
                        billUser2.TotalAmount = calculateFee.totalAmountExchange(billUser2);
                    }
                    _context.Add(newExchangeDetail);
                    await _context.SaveChangesAsync();
                }
            }
            var notificationOk = new Notification
            {
                UserId = book2.UserId,
                Content = @"Yêu cầu đổi sách """ + book2.Title + @""" của bạn lấy sách """ + book1.Title + @""" của" + book1.User.Fullname + " được chấp nhận!",
                CreatedDate = DateTime.Now,
                IsRead = false,
            };
            _context.Add(notificationOk);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString(),
            };
        }

        public async Task<ApiMessage> deniedRequestAsync(int userId, int requestId)
        {
            var request = await _context.ExchangeRequests.SingleOrDefaultAsync(r => r.Id == requestId && r.Status == StatusRequest.Waiting.ToString());
            if (request == null)
            {
                return new ApiMessage
                {
                    Message = Message.REQUEST_NOT_EXIST.ToString(),
                };
            }

            var book = await _context.Books.Include(b => b.User).SingleOrDefaultAsync(b => b.Id == request.BookId && b.UserId == userId);
            if (book == null)
            {
                return new ApiMessage
                {
                    Message = Message.REQUEST_NOT_EXIST.ToString(),
                };
            }

            request.IsNewest = false;
            request.Status = StatusRequest.Denied.ToString();

            var bookOffer = _context.Books.Include(b => b.User).SingleOrDefault(b => b.Id == request.BookOfferId);
            if (bookOffer != null)
            {
                bookOffer.IsTrade = false;
            }
            var notification = new Notification
            {
                UserId = bookOffer.UserId,
                Content = @"Yêu cầu đổi sách """ + bookOffer.Title + @""" của bạn lấy sách """ + book.Title + @""" của" + book.User.Fullname + " không được chấp nhận!",
                CreatedDate = DateTime.Now,
                IsRead = false,
            };
            _context.Add(notification);

            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString(),
            };
        }

        public async Task<ApiMessage> rentBookAsync(int userId, int bookId)
        {
            CalculateFee calculateFee = new CalculateFee(_context);

            var user = await _context.ShipInfos.SingleOrDefaultAsync(s => s.UserId == userId);
            if (user.IsUpdate == false)
            {
                return new ApiMessage
                {
                    Message = Message.SHIP_INFO_EMPTY.ToString()
                };
            }

            var userInfo = await _context.Users.SingleOrDefaultAsync(s => s.Id == userId);

            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId && b.IsRent == true && b.IsTrade == false && b.Status == StatusRequest.Approved.ToString());
            if (book == null)
            {
                return new ApiMessage
                {
                    Message = Message.BOOK_NOT_EXIST.ToString(),
                };
            }

            book.IsTrade = true;
            var rentNew = new Rent
            {
                OwnerId = book.UserId,
                RenterId = userId,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Status = Status.Waiting.ToString(),
            };

            var check = await _context.Rents.SingleOrDefaultAsync(r => r.OwnerId == book.UserId && r.RenterId == userId && r.Date == rentNew.Date);
            //Nếu chưa có giao dịch giữa 2 người này
            if (check == null)
            {
                _context.Add(rentNew);
                await _context.SaveChangesAsync();
                var rent = await _context.Rents.SingleOrDefaultAsync(r => r.OwnerId == book.UserId && r.RenterId == userId && r.Date == rentNew.Date);
                if (rent == null)
                {
                    return new ApiMessage
                    {
                        Message = Message.RENT_NOT_EXIST.ToString(),
                    };
                }

                var rentDetail = new RentDetail
                {
                    RentId = rent.Id,
                    BookId = bookId,
                    StorageStatusBook = StorageStatus.Waiting.ToString(),
                    RequestTime = DateTime.Now,
                    ExpiredDate = DateOnly.FromDateTime(DateTime.Now.AddDays(book.NumberOfDays)),
                    Status = Status.Waiting.ToString(),
                    Flag = true
                };
                _context.Add(rentDetail);

                var newBillOwner = new RentBill
                {
                    RentId = rent.Id,
                    UserId = rent.OwnerId,
                    TotalBook = 1,
                    DepositFee = 0,
                    RentFee = 0,
                    FeeId1 = calculateFee.feeShipID(book.Weight),
                    FeeId2 = calculateFee.feeServiceID(1),
                    IsPaid = false,
                    Flag = true
                };
                newBillOwner.TotalAmount = calculateFee.totalAmountRent(newBillOwner);

                var newBillRenter = new RentBill
                {
                    RentId = rent.Id,
                    UserId = rent.RenterId,
                    TotalBook = 1,
                    DepositFee = book.DepositPrice,
                    RentFee = book.RentFee,
                    FeeId1 = calculateFee.feeShipID(book.Weight),
                    FeeId2 = calculateFee.feeServiceID(1),
                    IsPaid = false,
                    Flag = true
                };
                newBillRenter.TotalAmount = calculateFee.totalAmountRent(newBillRenter);
                _context.Add(newBillOwner);
                _context.Add(newBillRenter);
                await _context.SaveChangesAsync();
            }
            //Nếu đã có giao dịch
            else
            {
                var rentDetail = new RentDetail
                {
                    RentId = check.Id,
                    BookId = bookId,
                    StorageStatusBook = StorageStatus.Waiting.ToString(),
                    RequestTime = DateTime.Now,
                    ExpiredDate = DateOnly.FromDateTime(DateTime.Now.AddDays(book.NumberOfDays)),
                    Status = Status.Waiting.ToString(),
                    Flag = true
                };

                double totalWeightBook = 0;

                var listBook = _context.RentDetails.Include(e => e.Book).Where(e => e.RentId == check.Id).ToList();
                foreach (var item in listBook)
                {
                    totalWeightBook += item.Book.Weight;
                }

                var billOnwer = await _context.RentBills.SingleOrDefaultAsync(b => b.RentId == check.Id && b.UserId == check.OwnerId);
                if (billOnwer != null)
                {
                    billOnwer.TotalBook += 1;
                    billOnwer.FeeId1 = calculateFee.feeShipID(totalWeightBook + book.Weight);
                    billOnwer.FeeId2 = calculateFee.feeServiceID(1);
                    billOnwer.FeeId3 = calculateFee.feeServiceID(2);
                    billOnwer.TotalAmount = calculateFee.totalAmountRent(billOnwer);
                }

                var billRenter = await _context.RentBills.SingleOrDefaultAsync(b => b.RentId == check.Id && b.UserId == check.RenterId);
                if (billRenter != null)
                {
                    billRenter.TotalBook += 1;
                    billRenter.DepositFee += book.DepositPrice;
                    billRenter.RentFee += book.RentFee;
                    billRenter.FeeId1 = calculateFee.feeShipID(totalWeightBook + book.Weight);
                    billRenter.FeeId2 = calculateFee.feeServiceID(1);
                    billRenter.FeeId3 = calculateFee.feeServiceID(2);
                    billRenter.TotalAmount = calculateFee.totalAmountRent(billRenter);
                }

                _context.Add(rentDetail);
                await _context.SaveChangesAsync();
            }
            var notification = new Notification
            {
                UserId = book.UserId,
                Content = @"Sách """ + book.Title + @""" của bạn được " + userInfo.Fullname + " đặt thuê!",
                CreatedDate = DateTime.Now,
                IsRead = false,
            };
            _context.Add(notification);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.SUCCESS.ToString()
            };
        }
    }
}

