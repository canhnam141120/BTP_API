using BTP_API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BTP_API.Ultils
{
    public class CalculateFee
    {
        private readonly BTPContext _context;
        public CalculateFee(BTPContext context)
        {
            _context = context;
        }
        public int feeShipID(double bookWeight)
        {
            int feeShipID = 0;
            if (bookWeight <= 1000)
            {
                var fee = _context.Fees.SingleOrDefault(f => f.Code == Fees.S1.ToString() && f.IsActive == true);
                if (fee != null)
                {
                    feeShipID = fee.Id;
                }
            }
            if (bookWeight > 1000 && bookWeight <= 3000)
            {
                var fee = _context.Fees.SingleOrDefault(f => f.Code == Fees.S13.ToString() && f.IsActive == true);
                if (fee != null)
                {
                    feeShipID = fee.Id;
                }
            }
            if (bookWeight > 3000 && bookWeight <= 5000)
            {
                var fee = _context.Fees.SingleOrDefault(f => f.Code == Fees.S35.ToString() && f.IsActive == true);
                if (fee != null)
                {
                    feeShipID = fee.Id;
                }
            }
            if (bookWeight > 5000)
            {
                var fee = _context.Fees.SingleOrDefault(f => f.Code == Fees.S5.ToString() && f.IsActive == true);
                if (fee != null)
                {
                    feeShipID = fee.Id;
                }
            }
            return feeShipID;
        }

        public int feeServiceID(int i)
        {
            if (i == 1)
            {
                var fee = _context.Fees.SingleOrDefault(f => f.Code == Fees.B1.ToString() && f.IsActive == true);
                if (fee != null)
                {
                    return fee.Id;
                }
            }
            if (i > 1)
            {
                var fee = _context.Fees.SingleOrDefault(f => f.Code == Fees.BM.ToString() && f.IsActive == true);
                if (fee != null)
                {
                    return fee.Id;
                }
            }
            return 0;
        }

        public float totalAmountRent(RentBill bill)
        {
            var fee1 = _context.Fees.SingleOrDefault(f => f.Id == bill.FeeId1);
            var fee2 = _context.Fees.SingleOrDefault(f => f.Id == bill.FeeId2);
            var fee3 = _context.Fees.SingleOrDefault(f => f.Id == bill.FeeId3);
            if (fee1 != null && fee2 != null && fee3 == null)
            {
                return bill.DepositFee + bill.RentFee + fee1.Price + fee2.Price;
            }
            if (fee1 != null && fee2 != null && fee3 != null)
            {
                return bill.DepositFee + bill.RentFee + fee1.Price + fee2.Price + fee3.Price * (bill.TotalBook - 1);
            }
            return 0;
        }

        public float totalAmountExchange(ExchangeBill bill)
        {
            var fee1 = _context.Fees.SingleOrDefault(f => f.Id == bill.FeeId1);
            var fee2 = _context.Fees.SingleOrDefault(f => f.Id == bill.FeeId2);
            var fee3 = _context.Fees.SingleOrDefault(f => f.Id == bill.FeeId3);
            if (fee1 != null && fee2 != null && fee3 == null)
            {
                return bill.DepositFee + fee1.Price + fee2.Price;
            }
            if (fee1 != null && fee2 != null && fee3 != null)
            {
                return bill.DepositFee + fee1.Price + fee2.Price + fee3.Price * (bill.TotalBook - 1);
            }
            return 0;
        }
    }
}
