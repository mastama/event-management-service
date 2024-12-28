namespace com.mastama.Utils;

public class Constans
{
    public static class ResponseCodes
    {
        public static readonly ResponseCode APPROVED = new("00", "Approved");
        public static readonly ResponseCode HTTP_NOT_FOUND = new("X4", "There is No Resource Path");
        public static readonly ResponseCode HTTP_INTERNAL_ERROR = new("X5", "Service Internal Error");
        public static readonly ResponseCode INVALID_TRANSACTION = new("12", "Transaksi tidak sesuai");
        public static readonly ResponseCode INVALID_AMOUNT = new("13", "Jumlah Transaksi tidak sesuai");
        public static readonly ResponseCode ACCOUNT_NOT_FOUND = new("14", "Data tidak ditemukan");
        public static readonly ResponseCode DATA_EXISTS = new("15", "Data sudah ada");
        public static readonly ResponseCode TRANSACTION_ALREADY_POSTED = new("29", "Transaction Already Posted");
        public static readonly ResponseCode WRONG_FORMAT_DATA = new("30", "Format Data Salah");
        public static readonly ResponseCode CA_NOT_REGISTERED_OR_INVALID_CODE = new("31", "Client tidak terdaftar/salah password");
        public static readonly ResponseCode DATA_BLACKLIST = new("40", "Data Blacklist");
        public static readonly ResponseCode TRANSACTION_TIMEOUT = new("68", "Transaction Timeout");
        public static readonly ResponseCode TAGIHAN_SUDAH_TERBAYAR = new("88", "Tagihan Sudah Terbayar");
        public static readonly ResponseCode CUT_OFF_TIME = new("90", "Cut Off Time");
        public static readonly ResponseCode SYSTEM_MAINTENANCE = new("96", "System Maintenance");
        public static readonly ResponseCode GENERAL_ERROR = new("98", "General Error");

        // Nested class for the response code structure
        public class ResponseCode
        {
            public string Code { get; }
            public string Description { get; }

            public ResponseCode(string code, string description)
            {
                Code = code;
                Description = description;
            }
        }
    }
}
